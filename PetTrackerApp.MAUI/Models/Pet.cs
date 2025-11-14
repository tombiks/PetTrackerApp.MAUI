using PetTrackerApp.MAUI.Enums;

namespace PetTrackerApp.MAUI.Models
{
    internal class Pet
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public PetType PetType { get; set; } = PetType.Others;
        public PetGender PetGender { get; set; } = PetGender.Male;
        public DateTime DateOfBirth { get; set; }
        public string PicturePath { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }
}
