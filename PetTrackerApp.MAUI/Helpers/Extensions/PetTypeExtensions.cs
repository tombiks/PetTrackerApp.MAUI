using PetTrackerApp.Data.Enums;
using PetTrackerApp.MAUI.Resources.Languages;

namespace PetTrackerApp.MAUI.Helper.Extensions
{    
    public static class PetTypeExtensions
    {        
        public static string ToLocalizedString(this PetType petType)
        {
            return petType switch
            {
                PetType.Cat => AppResources.PetType_Cat,
                PetType.Dog => AppResources.PetType_Dog,
                PetType.Fish => AppResources.PetType_Fish,
                PetType.Bird => AppResources.PetType_Bird,
                PetType.SmallMammal => AppResources.PetType_SmallMammal,
                PetType.Rabbit => AppResources.PetType_Rabbit,
                PetType.Reptile => AppResources.PetType_Reptile,
                PetType.ExoticPet => AppResources.PetType_ExoticPet,
                PetType.Others => AppResources.PetType_Others,
                _ => petType.ToString()
            };
        }
    } 
}