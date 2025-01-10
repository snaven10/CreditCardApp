namespace CreditCardApp.Api.Entities
{
    public class Card
    {
        public int Id { get; set; }
        public string CardHolderName { get; set; } = string.Empty;
        public string CardNumber { get; set; } = string.Empty;
        public decimal CreditLimit { get; set; }
        public decimal CurrentBalance { get; set; }

        public ICollection<Purchase> Purchases { get; set; } = new List<Purchase>();
        public ICollection<Payment> Payments { get; set; } = new List<Payment>();
        public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
    }
}
