using PetTrackerApp.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetTrackerApp.Data.Models
{
    public class CompletedReminder
    {
        public int Id { get; set; }

        // Foreign Keys
        public int ReminderCategoryId { get; set; } // Orijinal kategoriyi tutar
        public int OriginalReminderId { get; set; } // Hangi reminder’dan geldi

        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime CompletedDate { get; set; } = DateTime.UtcNow;
        public Importance Importance { get; set; } = Importance.NotImportant; //varsayılan NotImportant

        // Navigation Properties
        public ReminderCategory ReminderCategory { get; set; } = null!;
        public Reminder OriginalReminder { get; set; } = null!;
    }
}
