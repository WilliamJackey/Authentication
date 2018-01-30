using AutoMapper;
using AcmeWidget.Models.Entities;
using AcmeWidget.ViewModels;

namespace AcmeWidget.Helpers.Mappings
{
    public class AuthenticationUserProfile : Profile
    {
      public AuthenticationUserProfile()
      {
        CreateMap<RegistrationView, AuthenticationUser>().ForMember(au => au.UserName, map => map.MapFrom(vm => vm.Email));
      }
    }
}
