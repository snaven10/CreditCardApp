using CreditCardApp.Domain.Entities;
using MediatR;

namespace CreditCardApp.Application.Queries.Cards
{
    public class GetCardByIdQuery : IRequest<Card>
    {
        public int Id { get; set; }

        public GetCardByIdQuery(int id)
        {
            Id = id;
        }
    }
}
