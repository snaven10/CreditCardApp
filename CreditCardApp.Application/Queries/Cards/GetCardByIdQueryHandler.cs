using CreditCardApp.Application.Interfaces;
using CreditCardApp.Application.Queries.Cards;
using CreditCardApp.Domain.Entities;
using MediatR;

namespace CreditCardApp.Application.Handlers.Cards
{
    public class GetCardByIdQueryHandler : IRequestHandler<GetCardByIdQuery, Card>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetCardByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Card> Handle(GetCardByIdQuery request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.Repository<Card>().GetByIdAsync(request.Id);
        }
    }
}
