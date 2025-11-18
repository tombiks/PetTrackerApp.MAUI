using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace PetTrackerApp.Data.Models
{
    public class ReminderCategory : INotifyPropertyChanging, INotifyPropertyChanged
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; } = null!;
        public virtual int PetId { get; set; }
        public virtual Pet Pet { get; set; } = null!;
        public virtual DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        // Category içindeki reminderler
        public virtual ICollection<Reminder> Reminders { get; set; } = new List<Reminder>();

        public virtual ICollection<CompletedReminder> CompletedReminders { get; set; } = new List<CompletedReminder>();

        public event PropertyChangingEventHandler? PropertyChanging;
        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
