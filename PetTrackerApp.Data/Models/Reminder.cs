using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PetTrackerApp.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetTrackerApp.Data.Models
{
    public class Reminder : INotifyPropertyChanging, INotifyPropertyChanged
    {
        public virtual int Id { get; set; }

        // Artık pet yerine kategoriye bağlı
        public virtual int ReminderCategoryId { get; set; }
        public virtual ReminderCategory ReminderCategory { get; set; } = null!;

        public virtual string Title { get; set; } = null!;
        public virtual string Description { get; set; } = null!;
        public virtual DateTime CreatedDate { get; set; } = DateTime.UtcNow; // Kullanıcı override edebilir
        public virtual DateTime ReminderDate { get; set; }
        public virtual bool IsCompleted { get; set; } = false;
        public int RepeatIntervalDays { get; set; } = 0; // default 0 = tekrar etmiyor

        // Yeni property: Önem seviyesi
        public virtual Importance Importance { get; set; } = Importance.NotImportant;  //Varsayılan olarak NotImportant ayarlanmış.

        public event PropertyChangingEventHandler? PropertyChanging;
        public event PropertyChangedEventHandler? PropertyChanged;

        
    }
}
