using Entities.DbEntities;
using KrediKartiOdeme.Factory;
using KrediKartiOdeme.Factory.Akbank;
using KrediKartiOdeme.Factory.AmericanExpress;
using KrediKartiOdeme.Factory.Denizbank;
using KrediKartiOdeme.Factory.Garantibank;
using KrediKartiOdeme.Factory.Ziraatbank;
using KrediKartiOdeme.Helpers;
using KrediKartiOdeme.Model;
using Microsoft.AspNetCore.Mvc;

namespace KrediKartiOdeme.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        [HttpPost]
        [Route("api/pay")]
        public Result Pay(string identityNumber, string cardNumber, DateTime validityDate, string cvv, decimal amount)
        {
            //Customer validation 
            var identityCheck = identityNumber.ValidateCustomer(new DataAccess.Repositories.EfCustomerDAL());
            if (identityCheck.HasException)
                return identityCheck;

            //prepare card configuration (validate and prepare)
            var card = cardNumber.ToCreditCardConfiguration(new DataAccess.Repositories.EfCreditCardConfigurationDAL());
            if (card.HasException)
                return card;

            //Banka verisi okunur
            var cardBank = ((CreditCardConfiguration)card.Entity).BankConfigurationId.ToBankConfiguration(new DataAccess.Repositories.EfBankConfigurationDAL());

            //Kredi kartının herhangi bir bankaya ait olmaması durumunda kendimiz bu tip kartların ödemelerini
            //İlgili bankaya yönlendirebiliriz. Default bir banka ödeme için seçim yapabiliriz.
            //Veya geliştireceğimiz bir algoritmayla sıralı, random v.b. Payment Factory elde edilebilir.
            if (cardBank == null)
                return new Result(ApplicationMessages.BANK_NOT_FOUND_TO_PAY);

            IPaymentFactory paymentFactory = EvaluatePaymentFactory(((BankConfiguration)cardBank.Entity).BankName);

            var bankValidationResult = paymentFactory.GetValidity().Validate(cardNumber, cvv, validityDate.Day, validityDate.Month, validityDate.Year, amount);

            if(bankValidationResult.HasException)
                return new Result { HasException = bankValidationResult.HasException, ExceptionMessage = bankValidationResult.ExceptionMessage};

            var bankPaymentResult = paymentFactory.GetPayment().Pay(bankValidationResult.PaymentProcessKey, cardNumber, cvv, validityDate.Day, validityDate.Month, validityDate.Year, amount);

            return new Result { HasException = bankPaymentResult.HasException, ExceptionMessage = bankPaymentResult.ExceptionMessage, PaymentMessage = bankPaymentResult.PaymentMessage };
        }

        private IPaymentFactory EvaluatePaymentFactory(string bankName)
        {
            IPaymentFactory paymentFactory;

            switch (bankName)
            {
                case "Vakifbank":
                    paymentFactory = new VakifbankPaymentFactory();
                    break;
                case "Ziraatbank":
                    paymentFactory = new ZiraatbankPaymentFactory();
                    break;
                case "Garantibank":
                    paymentFactory = new GarantibankPaymentFactory();
                    break;
                case "Akbank":
                    paymentFactory = new AkbankPaymentFactory();
                    break;
                case "Denizbank":
                    paymentFactory = new DenizbankPaymentFactory();
                    break;
                case "AmericanExpress":
                    paymentFactory = new AmericanExpressPaymentFactory();
                    break;
                default:
                    paymentFactory = null;
                    break;
            }

            return paymentFactory;
        }
    }
}
