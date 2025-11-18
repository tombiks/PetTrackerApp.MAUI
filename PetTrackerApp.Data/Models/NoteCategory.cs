using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace PetTrackerApp.Data.Models
{
    public class NoteCategory : INotifyPropertyChanging, INotifyPropertyChanged
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; } = null!;
        public virtual int PetId { get; set; }
        public virtual Pet Pet { get; set; } = null!;
        public virtual DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        public virtual ICollection<Note> Notes { get; set; } = new List<Note>();

        public event PropertyChangingEventHandler? PropertyChanging;
        public event PropertyChangedEventHandler? PropertyChanged;
    }
}