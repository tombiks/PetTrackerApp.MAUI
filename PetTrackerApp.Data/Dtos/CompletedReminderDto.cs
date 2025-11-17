using PetTrackerApp.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetTrackerApp.Data.Dtos
{
    public class CompletedReminderDto
    {
        public int Id { get; set; }
        public int ReminderCategoryId { get; set; }
        public int OriginalReminderId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime CompletedDate { get; set; }
        
        // Enum property adı model ile uyumlu olsun
        public Importance Importance { get; set; } = Importance.NotImportant;

        // Display adı UI için
        public string ImportantDisplay { get; set; } = string.Empty;

        //Bu şekilde artık UI tarafında CompletedReminderDto.ImportantDisplay kullanarak kullanıcı dostu enum isimlerini gösterilebilir.
    }
}
