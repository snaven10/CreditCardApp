using MediatR;

namespace CreditCardApp.Application.Commands.Cards
{
    public class UpdateCardCommand : IRequest<bool> // Retorna true si la operación fue exitosa
    {
        public int Id { get; set; } // ID de la tarjeta a actualizar
        public string CardHolderName { get; set; } = string.Empty;
        public string CardNumber { get; set; } = string.Empty;
        public decimal CreditLimit { get; set; }
        public decimal CurrentBalance { get; set; }
    }
}
