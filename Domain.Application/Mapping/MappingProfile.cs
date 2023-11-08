using AutoMapper;
using Domain.Application.Models;
using Domain.Core.Models;

namespace Externos.Domain.Application.Mappings
{
    public class MappingProfileApp : Profile
    {
        public MappingProfileApp()
        {
            CreateMap<SyntaxStatus, SyntaxResponseVM>()
                .ForMember(dt => dt.IsOk, opt => opt.MapFrom(src => src.IsOk))
                .ForMember(dt => dt.Errors, opt => opt.MapFrom(src => src.Errors))
                .ForMember(dt => dt.Obs, opt => opt.MapFrom(src => src.Obs))
                .ForMember(dt => dt.Msg, opt => opt.MapFrom(src => src.Msg));
        }
    }
}
