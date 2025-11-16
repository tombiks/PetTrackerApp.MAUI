using AutoMapper;
using PetTrackerApp.Data.Dtos;
using PetTrackerApp.Data.Models;

namespace PetTrackerApp.Data.Mappings
{
    public class PetMappingProfile : Profile
    {
        public PetMappingProfile() 
        {
            //Pet'den PetDto'ya dönüsüm (peti okuyup dto ya yazma)
            CreateMap<Pet, PetDto>();

            //PetDto'dan Pet'e dönüsüm (dto dan pet e yazma) id ve createdat bilgilerini almayacagi icin yok say.
            CreateMap<PetDto, Pet>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore());
        }
    }
}
