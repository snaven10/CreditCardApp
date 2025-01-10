namespace CreditCardApp.Application.DTOs
{
    public class CardStatementDto
    {
        public string CardHolderName { get; set; }
        public string CardNumber { get; set; }
        public decimal CurrentBalance { get; set; }
        public decimal CreditLimit { get; set; }
        public decimal AvailableCredit { get; set; }
        public decimal TotalPurchasesThisMonth { get; set; }
        public decimal TotalPayments { get; set; }
        public decimal MinimumPaymentDue { get; set; }
        public decimal FullPaymentWithInterest { get; set; }
    }
}
