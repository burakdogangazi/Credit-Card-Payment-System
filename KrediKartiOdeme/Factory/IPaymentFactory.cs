namespace KrediKartiOdeme.Factory
{
    public interface IPaymentFactory
    {
        IPayment GetPayment();
        IValidity GetValidity();
    }
}
