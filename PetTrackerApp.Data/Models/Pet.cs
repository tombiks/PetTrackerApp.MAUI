using PetTrackerApp.Data.Enums;
using System.ComponentModel;

namespace PetTrackerApp.Data.Models
{
    //changetrackingproxies özelligini kullanabilmek icin INotifyPropertyChanging ve INotifyPropertyChanged interface'lerini implement ediyoruz
    public class Pet : INotifyPropertyChanging, INotifyPropertyChanged
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

        // Navigation properties
        public virtual ICollection<ReminderCategory> ReminderCategories { get; set; } = new List<ReminderCategory>();
        public virtual ICollection<NoteCategory> NoteCategories { get; set; } = new List<NoteCategory>();


        //implement ettigimiz interface'lerin event'lerini tanimliyoruz
        //bunları tanımlamazsak hata alırız - bunu ef core changetrackingproxies için kullanacak ama istersek bizde degisiklikleri dinlemek icin kullanabiliriz
        public event PropertyChangingEventHandler? PropertyChanging;

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
