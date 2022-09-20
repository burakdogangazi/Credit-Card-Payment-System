namespace MockBanks
{
    public class BankValidationResult
    {
        public Guid PaymentProcessKey { get; set; }
        public string ValidationMessage { get; set; }
        public ValidationPart validationPart { get; set; }
        public bool IsValid { get; set; }
    }

    public class BankPaymentResult
    {
        public string ValidationMessage { get; set; }
        public ValidationPart validationPart { get; set; }
        public bool IsValid { get; set; }
    }

    public enum ValidationPart
    {
        Card,
        Institution
    }
}
