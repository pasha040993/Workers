using AutoMapper;
using WorkersServer.Models.POCOs;

namespace WorkersServer.Models.MappingProfiles
{
    public class WorkerProfile : Profile
    {
        public WorkerProfile()
        {
            CreateMap<Worker, Worker>();
            CreateMap<WorkerMessage, Worker>()
                .ForMember(desc => desc.Id, opt => opt.MapFrom(src => !string.IsNullOrEmpty(src.Id) ? new Guid(src.Id) : Guid.Empty))
                .ForMember(desc => desc.Birthday, opt => opt.MapFrom(src => new DateTime(src.Birthday)))
                .ForMember(desc => desc.Sex, opt => opt.MapFrom(src => src.Sex == Sex.Male));


            CreateMap<Worker, WorkerMessage > ()
                .ForMember(desc => desc.Id, opt => opt.MapFrom(src => src.Id.ToString()))
                .ForMember(desc => desc.Birthday, opt => opt.MapFrom(src => src.Birthday.Ticks))
                .ForMember(desc => desc.Sex, opt => opt.MapFrom(src => src.Sex ? Sex.Male : Sex.Female));
        }
    }
}
