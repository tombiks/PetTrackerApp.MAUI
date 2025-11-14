using PetTrackerApp.MAUI.Enums;
using PetTrackerApp.MAUI.Resources.Languages;

namespace PetTrackerApp.MAUI.Helper.Extensions
{    public static class PetGenderExtensions
    {
        public static string ToLocalizedString(this PetGender petGender)
        {
            return petGender switch
            {
                PetGender.Male => AppResources.PetGender_Male,
                PetGender.Female => AppResources.PetGender_Female,
                _ => petGender.ToString()
            };
        }
    }
}
