using MediatR;
using CreditCardApp.Application.DTOs;

public class GetCardsQuery : IRequest<List<CardDto>> { }
