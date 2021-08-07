using AutoMapper;

using BandAccountManager.BlazorApp.Shared.Accounts;
using BandAccountManager.Core.Accounts;

namespace BandAccountManager.BlazorApp.Server.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Account, AccountDto>().ReverseMap();
            CreateMap<Account, AccountRefDto>();
            CreateMap<NewAccountDto, Account>();
            CreateMap<Transaction, TransactionDto>().ReverseMap();
        }
    }
}
