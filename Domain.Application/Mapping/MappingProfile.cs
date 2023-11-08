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
                .ForMember(dt => dt.Errors, opt => opt.MapFrom(src => src.ErrorMsg.Count))
                .ForMember(dt => dt.ErrorMsg, opt => opt.MapFrom(src => src.ErrorMsg))
                .ForMember(dt => dt.Obs, opt => opt.MapFrom(src => src.ObsMsg.Count))
                .ForMember(dt => dt.ObsMsg, opt => opt.MapFrom(src => src.ObsMsg));
        }
    }
}
