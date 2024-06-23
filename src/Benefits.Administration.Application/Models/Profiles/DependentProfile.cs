using AutoMapper;
using Benefits.Administration.Application.Entities;

namespace Benefits.Administration.Application.Models.Profiles
{
  public class DependentProfile : Profile
  {
    public DependentProfile()
    {
      CreateMap<Dependent, Dependent>()
        .ForMember(dest => dest.Id, opt => opt.Ignore());
    }
  }
}
