using AutoMapper;
using TimeTracker.DAL.Entities;
using TimeTracker.Services.Types;

namespace TimeTracker.Services.Configuration.Mapping.Profiles
{
    public class JobProfile : Profile
    {
        public JobProfile()
        {
            CreateMap<JobDto, Job>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.HourlyRate, opt => opt.MapFrom(src => src.HourlyRate))
                .ForMember(dest => dest.Employees, opt => opt.MapFrom(src => src.Employees))
                .ForMember(dest => dest.TrackedTimes, opt => opt.MapFrom(src => src.TrackedTimes));

            CreateMap<Job, JobDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.HourlyRate, opt => opt.MapFrom(src => src.HourlyRate))
                .ForMember(dest => dest.Employees, opt => opt.MapFrom(src => src.Employees))
                .ForMember(dest => dest.TrackedTimes, opt => opt.MapFrom(src => src.TrackedTimes));
        }
    }
}
