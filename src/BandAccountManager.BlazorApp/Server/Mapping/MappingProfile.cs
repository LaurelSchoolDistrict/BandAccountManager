using AutoMapper;

using BandAccountManager.BlazorApp.Shared.Accounts;
using BandAccountManager.Core.Accounts;

namespace BandAccountManager.BlazorApp.Server.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Account, AccountDto>();
            CreateMap<AccountDto, Account>();
            CreateMap<AccountUser, AccountUserDto>();
            CreateMap<AccountUserDto, AccountUser>();
            CreateMap<Transaction, TransactionDto>();
            CreateMap<TransactionDto, Transaction>();
        }
    }
}
