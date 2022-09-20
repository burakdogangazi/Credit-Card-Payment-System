namespace MockBanks
{
    public interface IMockBank
    {
        string TerminalNumber { get; }
        string CustomerName { get; }
        string CustomerPassword { get; }
        string BankName { get; }

        BankValidationResult ValidateCard(string terminalNumber, string customerName,
                                   string customerPassword, 
                                   string cvv, string creditCardNumber, int validToDay,
                                   int validToMonth, int validToYear, decimal paymentAmount);

        BankPaymentResult Pay(string terminalNumber, string customerName,
                                          string customerPassword, 
                                          string cvv, string creditCardNumber, int validToDay,
                                          int validToMonth, int validToYear,
                                          decimal paymentAmount, Guid paymentProcessKey);






    }
}
