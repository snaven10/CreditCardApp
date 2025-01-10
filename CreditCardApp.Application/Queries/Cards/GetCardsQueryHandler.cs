using CreditCardApp.Application.Interfaces;
using CreditCardApp.Application.DTOs;
using CreditCardApp.Domain.Entities;
using AutoMapper;
using MediatR;

public class GetCardsQueryHandler : IRequestHandler<GetCardsQuery, List<CardDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetCardsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<List<CardDto>> Handle(GetCardsQuery request, CancellationToken cancellationToken)
    {
        var cards = await _unitOfWork.Repository<Card>().GetAllAsync();
        return _mapper.Map<List<CardDto>>(cards);
    }
}
