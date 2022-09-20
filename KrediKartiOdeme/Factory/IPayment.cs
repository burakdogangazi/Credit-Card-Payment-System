namespace KrediKartiOdeme.Factory
{
    public interface IPayment
    {
        PaymentResult Pay(Guid paymentProcessKey, string cardNumber, string cvv, int validToDay, int validToMonth, int validToYear, decimal amount);
    }
}
