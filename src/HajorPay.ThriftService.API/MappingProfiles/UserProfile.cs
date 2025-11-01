using AutoMapper;
using HajorPay.ThriftService.Application.DTOs.User;
using HajorPay.ThriftService.Infrastructure.Identity;

namespace HajorPay.ThriftService.API.MappingProfiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserRegistrationDto, ApplicationUser>()
                .ForMember(x => x.UserName, opt => opt.MapFrom(x => x.EmailAddress));
        }
    }
}
