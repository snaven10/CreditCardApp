namespace CreditCardApp.Api.DTOs
{
    public class CardDto
    {
        public int Id { get; set; }

        // Nombre del titular de la tarjeta
        public string CardHolderName { get; set; } = string.Empty;

        // Número de tarjeta de crédito
        public string CardNumber { get; set; } = string.Empty;

        // Límite de crédito asignado
        public decimal CreditLimit { get; set; }

        // Saldo actual utilizado
        public decimal CurrentBalance { get; set; }
    }
}
