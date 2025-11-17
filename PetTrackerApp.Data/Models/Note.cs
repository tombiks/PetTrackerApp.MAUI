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
    public class Note : INotifyPropertyChanging, INotifyPropertyChanged
    {
        public virtual int Id { get; set; }
        public virtual int PetId { get; set; } // Note modeline eklenmeli
        public virtual Pet Pet { get; set; } = null!;

        public virtual int NoteCategoryId { get; set; }
        public virtual NoteCategory NoteCategory { get; set; } = null!;

        public virtual string? Content { get; set; }
        public virtual DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        //CreatedDate otomatik olarak şimdi atanıyor.

        // Yeni property: Önem seviyesi
        public virtual Importance Importance { get; set; } = Importance.NotImportant; //Varsayılan olarak NotImportant ayarlanmış.

       
        public event PropertyChangingEventHandler? PropertyChanging;
        public event PropertyChangedEventHandler? PropertyChanged;

       
    }
}

