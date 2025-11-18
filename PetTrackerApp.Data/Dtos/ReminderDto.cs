using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PetTrackerApp.Data.Enums;

namespace PetTrackerApp.Data.Dtos
{
    public class ReminderDto
    {
        public int ReminderCategoryId { get; set; }  // Yeni
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime ReminderDate { get; set; }
        public bool IsCompleted { get; set; } = false;
        public Importance Importance { get; set; }  // Enum
       
        // Display adı UI için
        public string ImportantDisplay { get; set; } = string.Empty;

        // UI için opsiyonel
        public string DisplayDate => ReminderDate.ToString("dd.MM.yyyy HH:mm");
    }
}
