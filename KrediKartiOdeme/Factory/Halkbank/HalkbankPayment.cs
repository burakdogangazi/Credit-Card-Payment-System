using MockBanks;

namespace KrediKartiOdeme.Factory.Halkbank
{
    public class HalkbankPayment : IPayment
    {
        public string TerminalNumber { get; }
        public string CustomerName { get; }
        public string CustomerPassword { get; }

        public HalkbankPayment(string terminalNumber, string customerName, string customerPassword)
        {
            TerminalNumber = terminalNumber;
            CustomerPassword = customerPassword;
            CustomerName = customerName;
        }

        public PaymentResult Pay(Guid paymentProcessKey, string cardNumber, string cvv, int validToDay, int validToMonth, int validToYear, decimal amount)
        {
            var bankPaymenSystem = new MockHalkbank();
            var bankPaymentSystemResult =
                bankPaymenSystem.Pay(TerminalNumber, CustomerName, CustomerPassword,
                cvv, cardNumber, validToDay, validToMonth, validToYear, amount, paymentProcessKey);

            return new PaymentResult
            {
                ExceptionMessage = bankPaymentSystemResult.IsValid ? String.Empty : bankPaymentSystemResult.ValidationMessage,
                HasException = !bankPaymentSystemResult.IsValid
            };
        }
    }
}
