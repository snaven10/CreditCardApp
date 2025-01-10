using CreditCardApp.Application.Interfaces;
using CreditCardApp.Domain.Entities;
using MediatR;

public class DeleteCardCommandHandler : IRequestHandler<DeleteCardCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteCardCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(DeleteCardCommand request, CancellationToken cancellationToken)
    {
        var card = await _unitOfWork.Repository<Card>().GetByIdAsync(request.Id);
        if (card == null) return false;

        _unitOfWork.Repository<Card>().Delete(card);
        await _unitOfWork.CompleteAsync();
        return true;
    }
}
