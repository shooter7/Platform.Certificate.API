using AutoMapper;
using Platform.Certificate.API.Models.Dbs;
using Platform.Certificate.API.Models.Dtos.User;
using Platform.Certificate.API.Models.Forms.User;
using Platform.Certificate.API.Common.Extensions;
using Platform.Certificate.API.Models.Dtos.Certificates;

namespace Platform.Certificate.API.MapperProfiles
{
    public class CertificateMapper : Profile
    {
        public CertificateMapper()
        {
            CreateMap<User, UserDto>();
            CreateMap<User, UserAllDto>();
            CreateMap<CreateUserForm, User>()
                .ForMember(dst => dst.Password, opt => opt.MapFrom(src => src.Password.HashPassword()));
            CreateMap<UpdateUserForm, User>()
                .ForMember(x => x.Password, act => act.Ignore())
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));


            CreateMap<Models.Dbs.Certificate, CertificateDto>();
        }
    }
}