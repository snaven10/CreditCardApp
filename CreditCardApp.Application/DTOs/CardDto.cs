namespace CreditCardApp.Application.DTOs
{
    public class CardDto
    {
        public int Id { get; set; }

        // Nombre del titular de la tarjeta
        public string CardHolderName { get; set; } = string.Empty;

        // N�mero de tarjeta de cr�dito
        public string CardNumber { get; set; } = string.Empty;

        // L�mite de cr�dito asignado
        public decimal CreditLimit { get; set; }

        // Saldo actual utilizado
        public decimal CurrentBalance { get; set; }
    }
}
