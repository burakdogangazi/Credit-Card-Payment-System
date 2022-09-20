namespace MockBanks
{
    public class CreditCardValue
    {
        public string? BankName { get; set; }
        public string? CardNumber { get; set; }
        public int ValidDate { get; set; }
        public int ValidMonth { get; set; }
        public int ValidYear { get; set; }
        public string? Cvv { get; set; }
        public CardStatus Status { get; set; }
    }
}
