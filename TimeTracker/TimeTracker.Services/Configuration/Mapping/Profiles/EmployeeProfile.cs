using AutoMapper;
using TimeTracker.DAL.Entities;
using TimeTracker.Services.Types;

namespace TimeTracker.Services.Configuration.Mapping.Profiles
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<EmployeeDto, Employee>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.Jobs, opt => opt.MapFrom(src => src.Jobs))
                .ForMember(dest => dest.TrackedTimes, opt => opt.MapFrom(src => src.TrackedTimes));

            CreateMap<Employee, EmployeeDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.Jobs, opt => opt.MapFrom(src => src.Jobs))
                .ForMember(dest => dest.TrackedTimes, opt => opt.MapFrom(src => src.TrackedTimes));
        }
    }
}
