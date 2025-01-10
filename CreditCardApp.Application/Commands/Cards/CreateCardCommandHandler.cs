using CreditCardApp.Application.Interfaces;
using CreditCardApp.Domain.Entities;
using MediatR;

namespace CreditCardApp.Application.Commands.Cards
{
    public class CreateCardCommandHandler : IRequestHandler<CreateCardCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateCardCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Handle(CreateCardCommand request, CancellationToken cancellationToken)
        {
            var card = new Card
            {
                CardHolderName = request.CardHolderName,
                CardNumber = request.CardNumber,
                CreditLimit = request.CreditLimit,
                CurrentBalance = request.CurrentBalance
            };

            _unitOfWork.Repository<Card>().Add(card);
            await _unitOfWork.CompleteAsync();

            return card.Id; // Retorna el ID de la tarjeta creada
        }
    }
}
