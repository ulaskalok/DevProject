using AutoMapper;
using Dev.Domain.Models;
using Dev.Domain;


namespace Dev.Application.Mappers
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap<Page, PageView>();
            CreateMap<Account, AccountView>();
        }
    }
}
