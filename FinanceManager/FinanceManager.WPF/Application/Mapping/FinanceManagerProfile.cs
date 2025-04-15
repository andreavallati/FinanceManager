using AutoMapper;
using FinanceManager.Shared.Application.Dtos;
using FinanceManager.WPF.Domain.Models;

namespace FinanceManager.WPF.Application.Mapping
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
