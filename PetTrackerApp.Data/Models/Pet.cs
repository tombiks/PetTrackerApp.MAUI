using PetTrackerApp.Data.Enums;

namespace PetTrackerApp.Data.Models
{
    public class Pet
    {
        //burada tüm degiskenlerimiz virtual olarak tanimliyoruz
        //böylelikle ef core bu property'leri override edebilir
        //changetrackingproxies özelligini kullanabilmek icin gerekli

        public virtual int Id { get; set; }
        public virtual string Name { get; set; } = string.Empty;
        public virtual PetType PetType { get; set; }
        public virtual PetGender PetGender { get; set; }
        public virtual DateTime DateOfBirth { get; set; }
        public virtual string PicturePath { get; set; } = string.Empty;
        public virtual DateTime CreatedAt { get; set; }
    }
}
