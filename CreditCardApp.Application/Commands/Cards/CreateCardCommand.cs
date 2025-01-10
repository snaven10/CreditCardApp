using MediatR;

namespace CreditCardApp.Application.Commands.Cards
{
    public class CreateCardCommand : IRequest<int> // Retorna el ID de la tarjeta creada
    {
        public string CardHolderName { get; set; } = string.Empty;
        public string CardNumber { get; set; } = string.Empty;
        public decimal CreditLimit { get; set; }
        public decimal CurrentBalance { get; set; }
    }
}
