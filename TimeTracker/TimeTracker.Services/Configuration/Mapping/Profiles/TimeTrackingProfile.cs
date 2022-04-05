using AutoMapper;
using TimeTracker.DAL.Entities;
using TimeTracker.Services.Types;

namespace TimeTracker.Services.Configuration.Mapping.Profiles
{
    public class TimeTrackingProfile : Profile
    {
        public TimeTrackingProfile()
        {
            CreateMap<TimeTrackingDto, TimeTracking>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.StartTime, opt => opt.MapFrom(src => src.StartTime))
                .ForMember(dest => dest.EndTime, opt => opt.MapFrom(src => src.EndTime))
                .ForMember(dest => dest.TotalHours, opt => opt.MapFrom(src => src.TotalHours))
                .ForMember(dest => dest.TotalCost, opt => opt.MapFrom(src => src.TotalCost))
                .ForMember(dest => dest.EmployeeId, opt => opt.MapFrom(src => src.EmployeeId))
                .ForMember(dest => dest.JobId, opt => opt.MapFrom(src => src.JobId));

            CreateMap<TimeTracking, TimeTrackingDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.StartTime, opt => opt.MapFrom(src => src.StartTime))
                .ForMember(dest => dest.EndTime, opt => opt.MapFrom(src => src.EndTime))
                .ForMember(dest => dest.TotalHours, opt => opt.MapFrom(src => src.TotalHours))
                .ForMember(dest => dest.TotalCost, opt => opt.MapFrom(src => src.TotalCost))
                .ForMember(dest => dest.EmployeeId, opt => opt.MapFrom(src => src.EmployeeId))
                .ForMember(dest => dest.JobId, opt => opt.MapFrom(src => src.JobId));
        }
    }
}
