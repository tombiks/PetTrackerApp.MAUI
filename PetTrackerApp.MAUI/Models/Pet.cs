using PetTrackerApp.MAUI.Enums;

namespace PetTrackerApp.MAUI.Models
{
    internal class Pet
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public PetType PetType { get; set; }
        public PetGender PetGender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PicturePath { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
