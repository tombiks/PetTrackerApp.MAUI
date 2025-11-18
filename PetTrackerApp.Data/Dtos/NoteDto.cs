using PetTrackerApp.Data.Enums;
using PetTrackerApp.Data.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetTrackerApp.Data.Dtos
{
    public class NoteDto
    {
        public int Id { get; set; }
        public int PetId { get; set; }
        public int NoteCategoryId { get; set; }
        public string Content { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        
        public Importance Importance { get; set; } = Importance.NotImportant;


        // UI için opsiyonel//DisplayDate gibi property’ler sadece UI için, veritabanında yok.
        public string DisplayDate => CreatedDate.ToString("dd.MM.yyyy HH:mm");

        //UI’da enum display name gösterilecekse gerekli olur
        public string ImportantDisplay => Importance.GetDisplayName();
    }
}
