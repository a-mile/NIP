using AutoMapper;
using NIP.Models;

namespace NIP
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Business, BusinessViewModel>();
        }
    }
}