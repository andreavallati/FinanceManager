using AutoMapper;
using FinanceManager.API.Domain.Entities;
using FinanceManager.Shared.Application.Dtos;

namespace FinanceManager.API.Application.Mapping
{
    public class FinanceManagerProfile : Profile
    {
        public FinanceManagerProfile()
        {
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<Transaction, TransactionDto>().ReverseMap();
        }
    }
}