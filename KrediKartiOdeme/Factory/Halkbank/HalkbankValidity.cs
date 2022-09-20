﻿using MockBanks;

namespace KrediKartiOdeme.Factory.Halkbank
{
    public class HalkbankValidity : IValidity
    {
        public string TerminalNumber { get; }
        public string CustomerName { get; }
        public string CustomerPassword { get; }

        public HalkbankValidity(string terminalNumber, string customerName, string customerPassword)
        {
            TerminalNumber = terminalNumber;
            CustomerPassword = customerPassword;
            CustomerName = customerName;
        }

        public ValidationResult Validate(string cardNumber, string cvv, int validToDay, int validToMonth, int validToYear, decimal amount)
        {
            var bankPaymenSystem = new MockHalkbank();
            var bankPaymentSystemResult =
                bankPaymenSystem.ValidateCard(TerminalNumber, CustomerName, CustomerPassword,
                cvv, cardNumber, validToDay, validToMonth, validToYear, amount);

            return new ValidationResult
            {
                PaymentProcessKey = bankPaymentSystemResult.IsValid ? bankPaymentSystemResult.PaymentProcessKey : Guid.Empty,
                ExceptionMessage = bankPaymentSystemResult.IsValid ? String.Empty : bankPaymentSystemResult.ValidationMessage,
                HasException = !bankPaymentSystemResult.IsValid
            };
        }
    }
}