using AutoMapper;
using CreditCardApp.Api.Entities;
using CreditCardApp.Api.DTOs;

namespace CreditCardApp.Api.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Card, CardDto>().ReverseMap();
            CreateMap<Purchase, PurchaseDto>().ReverseMap();
            CreateMap<Payment, PaymentDto>().ReverseMap();
            CreateMap<Transaction, TransactionDto>().ReverseMap();
        }
    }
}
