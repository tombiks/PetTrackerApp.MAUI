using AutoMapper;
using PetTrackerApp.Data.Dtos;
using PetTrackerApp.Data.Helpers;
using PetTrackerApp.Data.Models;

namespace PetTrackerApp.Data.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            //Pet'den PetDto'ya dönüsüm (peti okuyup dto ya yazma)
            CreateMap<Pet, PetDto>();

            //PetDto'dan Pet'e dönüsüm (dto dan pet e yazma) id ve createdat bilgilerini almayacagi icin yok say.
            CreateMap<PetDto, Pet>() // DTO → Entity
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore());


            // Reminder -> ReminderDto
            CreateMap<Reminder, ReminderDto>()
                .ForMember(dest => dest.ImportantDisplay, opt => opt.MapFrom(src => src.Importance.GetDisplayName())); // Display adı için

            // ReminderDto -> Reminder
            CreateMap<ReminderDto, Reminder>()
                .ForMember(dest => dest.Importance, opt => opt.MapFrom(src => src.Importance)) // enum doğrudan eşleşiyor
                .ForMember(dest => dest.Id, opt => opt.Ignore()) // ID'yi ignore edelim, DB'de otomatik
                .ForMember(dest => dest.CreatedDate, opt => opt.Ignore()); // oluşturulurken ayarlansın


            // Note -> NoteDto
            CreateMap<Note, NoteDto>()
                .ForMember(dest => dest.Importance, opt => opt.MapFrom(src => src.Importance));

            // NoteDto -> Note
            CreateMap<NoteDto, Note>()
                .ForMember(dest => dest.Importance, opt => opt.MapFrom(src => src.Importance))
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedDate, opt => opt.Ignore());


            // REMINDER CATEGORY

            CreateMap<ReminderCategory, ReminderCategoryDto>();
            CreateMap<ReminderCategoryDto, ReminderCategory>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            
            // NOTE CATEGORY
            
            CreateMap<NoteCategory, NoteCategoryDto>();
            CreateMap<NoteCategoryDto, NoteCategory>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            //CompletedReminder
            //ReminderCategory ve Reminder navigation'larını DTO’ya map etmek istemeyiz (sonsuz döngü olur).
            CreateMap<CompletedReminder, CompletedReminderDto>()
             .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
             .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
             .ForMember(dest => dest.CompletedDate, opt => opt.MapFrom(src => src.CompletedDate))
             .ForMember(dest => dest.ImportantDisplay, opt => opt.MapFrom(src => src.Importance.GetDisplayName()))
             .ForMember(dest => dest.Importance, opt => opt.MapFrom(src => src.Importance));

            // CompletedReminderDto -> CompletedReminder
            CreateMap<CompletedReminderDto, CompletedReminder>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.ReminderCategory, opt => opt.Ignore())
                .ForMember(dest => dest.OriginalReminder, opt => opt.Ignore())
                .ForMember(dest => dest.Importance, opt => opt.MapFrom(src => src.Importance));
        }
    }
}
