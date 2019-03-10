using AutoMapper;
using Dev.Domain.Models;
using Dev.Domain;


namespace Dev.Application.Mappers
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<PageView, Page>();
            CreateMap<AccountView, Account>();
        }
    }
}
