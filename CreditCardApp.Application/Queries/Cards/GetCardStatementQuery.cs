using MediatR;
using CreditCardApp.Application.DTOs;

namespace CreditCardApp.Application.Queries.Cards
{
    public class GetCardStatementQuery : IRequest<CardStatementDto>
    {
        public int CardId { get; }

        public GetCardStatementQuery(int cardId)
        {
            CardId = cardId;
        }
    }
}
