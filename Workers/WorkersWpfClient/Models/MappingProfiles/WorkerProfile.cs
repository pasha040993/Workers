using System;
using AutoMapper;
using WorkersWpfClient.ViewModels;

namespace WorkersWpfClient.Models.MappingProfiles
{
    public class WorkerProfile : Profile
    {
        public WorkerProfile()
        {
            CreateMap<WorkerMessage, WorkerViewModel>()
                .ForMember(desc => desc.Id, opt => opt.MapFrom(src => !string.IsNullOrEmpty(src.Id) ? new Guid(src.Id) : Guid.Empty))
                .ForMember(desc => desc.Birthday, opt => opt.MapFrom(src => new DateTime(src.Birthday)))
                .ForMember(desc => desc.Sex, opt => opt.MapFrom(src => src.Sex == Sex.Male));


            CreateMap<WorkerViewModel, WorkerMessage > ()
                .ForMember(desc => desc.Id, opt => opt.MapFrom(src => src.Id.ToString()))
                .ForMember(desc => desc.Birthday, opt => opt.MapFrom(src => src.Birthday.Ticks))
                .ForMember(desc => desc.Sex, opt => opt.MapFrom(src => src.Sex ? Sex.Male : Sex.Female));
        }
    }
}
