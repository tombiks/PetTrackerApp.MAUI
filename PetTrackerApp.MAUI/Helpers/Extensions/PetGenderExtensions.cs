using PetTrackerApp.Data.Enums;
using PetTrackerApp.MAUI.Resources.Languages;

namespace PetTrackerApp.MAUI.Helper.Extensions
{    public static class PetGenderExtensions
    {
        //bu method suna yarar. PetGender enum değerlerini lokalize edilmiş stringlere dönüştürür.
        //yani dili ingilizce olursa ingilizce görmeye, türkçe olursa türkçe görmeye yarar.
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
