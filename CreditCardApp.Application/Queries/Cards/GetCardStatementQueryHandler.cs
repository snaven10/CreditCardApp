using CreditCardApp.Application.Interfaces;
using CreditCardApp.Application.DTOs;
using CreditCardApp.Domain.Entities;
using MediatR;

namespace CreditCardApp.Application.Queries.Cards
{
    public class GetCardStatementQueryHandler : IRequestHandler<GetCardStatementQuery, CardStatementDto>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetCardStatementQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<CardStatementDto> Handle(GetCardStatementQuery request, CancellationToken cancellationToken)
        {
            var card = await _unitOfWork.Repository<Card>()
                .GetByIdAsync(request.CardId);

            if (card == null) return null;

            var purchasesThisMonth = card.Purchases
                .Where(p => p.PurchaseDate.Month == DateTime.Now.Month).ToList();

            var totalPurchases = purchasesThisMonth.Sum(p => p.Amount);
            var totalPayments = card.Payments.Sum(p => p.Amount);

            return new CardStatementDto
            {
                CardHolderName = card.CardHolderName,
                CardNumber = card.CardNumber,
                CurrentBalance = card.CurrentBalance,
                CreditLimit = card.CreditLimit,
                AvailableCredit = card.CreditLimit - card.CurrentBalance,
                TotalPurchasesThisMonth = totalPurchases,
                TotalPayments = totalPayments,
                MinimumPaymentDue = card.CurrentBalance * 0.05m,
                FullPaymentWithInterest = card.CurrentBalance * 1.25m
            };
        }
    }
}
