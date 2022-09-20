namespace KrediKartiOdeme.Factory
{
    public interface IValidity
    {
        ValidationResult Validate(string cardNumber, string cvv, int validToDay, int validToMonth, int validToYear, decimal amount);
      
    }
}
