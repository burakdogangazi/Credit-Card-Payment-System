using DataAccess.Repositories;
using KrediKartiOdeme.Model;

namespace KrediKartiOdeme.Helpers
{
    public static class CardHelper
    {
        /// <summary>
        /// Kart numarası doğrulama.
        /// </summary>
        /// <param name="creditCardNumber"></param>
        /// <param name="dal"></param>
        /// <returns></returns>
        public static Result ToCreditCardConfiguration(this string creditCardNumber, EfCreditCardConfigurationDAL dal)
        {
            if(creditCardNumber.Length != 16)
                return new Result(ApplicationMessages.CARD_NUMBER_IS_NOT_VALID);

            var conf = dal.Find(creditCardNumber.Substring(0, 6));

            if (conf == null)
                return new Result(ApplicationMessages.CARD_NOT_FOUND);

            return new Result(conf);
        }
    }
}
