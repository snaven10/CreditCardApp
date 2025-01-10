using CreditCardApp.Application.Interfaces;
using CreditCardApp.Domain.Entities;
using MediatR;

namespace CreditCardApp.Application.Commands.Cards
{
    public class UpdateCardCommandHandler : IRequestHandler<UpdateCardCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateCardCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(UpdateCardCommand request, CancellationToken cancellationToken)
        {
            // Obtiene la tarjeta existente
            var card = await _unitOfWork.Repository<Card>().GetByIdAsync(request.Id);

            if (card == null)
            {
                return false; // No se encontró la tarjeta
            }

            // Actualiza las propiedades
            card.CardHolderName = request.CardHolderName;
            card.CardNumber = request.CardNumber;
            card.CreditLimit = request.CreditLimit;
            card.CurrentBalance = request.CurrentBalance;

            // Actualiza la entidad en el repositorio
            _unitOfWork.Repository<Card>().Update(card);

            // Guarda los cambios
            await _unitOfWork.CompleteAsync();

            return true; // Operación exitosa
        }
    }
}
