namespace MockBanks
{
    internal static class MockHelper
    {
        internal static BankValidationResult ValidateCard(this string creditCardNumber, string cvv,
                                                          int validToDay, int validToMonth, int validToYear, decimal paymentAmount)
        {
            var values = MockData.GetCardValues(creditCardNumber);

            if (values == null)
                return new BankValidationResult
                {
                    IsValid = false,
                    PaymentProcessKey = Guid.Empty,
                    ValidationMessage = "The credit card which you want to pay is not valid",
                    validationPart = ValidationPart.Card
                };


            if (values.Status == CardStatus.Kisitli)
                return new BankValidationResult
                {
                    IsValid = false,
                    PaymentProcessKey = Guid.Empty,
                    ValidationMessage = "The credit card which you want to pay is restricted",
                    validationPart = ValidationPart.Card
                };

            if (values.Status == CardStatus.Yasakli)
                return new BankValidationResult
                {
                    IsValid = false,
                    PaymentProcessKey = Guid.Empty,
                    ValidationMessage = "The credit card which you want to pay is forbidden",
                    validationPart = ValidationPart.Card
                };

            if (values.Cvv != cvv)
                return new BankValidationResult
                {
                    IsValid = false,
                    PaymentProcessKey = Guid.Empty,
                    ValidationMessage = "The cvv you entered is not valid",
                    validationPart = ValidationPart.Card
                };

            if (values.ValidDate != validToDay || values.ValidMonth != validToMonth || values.ValidYear != validToYear)
                return new BankValidationResult
                {
                    IsValid = false,
                    PaymentProcessKey = Guid.Empty,
                    ValidationMessage = "The dates you entered is not valid",
                    validationPart = ValidationPart.Card
                };

            var paymentKey = Guid.NewGuid();

            values.CacheValidation(paymentKey, paymentAmount);

            return new BankValidationResult
            {
                IsValid = true,
                PaymentProcessKey = Guid.Empty,
                ValidationMessage = "",
                validationPart = ValidationPart.Card
            };
        }

        internal static BankValidationResult ValidateInstitution(this IMockBank bank, string terminalNumber, string customerName,
                                                                 string customerPassword)
        {
            if (bank.TerminalNumber != terminalNumber)
                return new BankValidationResult
                {
                    IsValid = false,
                    PaymentProcessKey = Guid.Empty,
                    ValidationMessage = "Terminal Number is not valid",
                    validationPart = ValidationPart.Institution
                };

            if (customerName != bank.CustomerName || customerPassword != bank.CustomerPassword)
                return new BankValidationResult
                {
                    IsValid = false,
                    PaymentProcessKey = Guid.Empty,
                    ValidationMessage = "Your institution user not registered or password incorrect",
                    validationPart = ValidationPart.Institution
                };

            return new BankValidationResult() { IsValid = true };
        }

        internal static BankPaymentResult ToBankPaymentResult(this BankValidationResult validationResult)
        {
            return new BankPaymentResult
            {
                IsValid = validationResult.IsValid,
                ValidationMessage = validationResult.ValidationMessage,
                validationPart = validationResult.validationPart
            };
        }

        internal static void CacheValidation(this CreditCardValue values, Guid bankValipaymentKey, decimal paymentAmount)
        {

        }
    }
}
