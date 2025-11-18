using PetTrackerApp.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetTrackerApp.Data.Dtos
{
    public class ReminderCategoryDto
    {
        public int Id { get; set; }
        public int PetId { get; set; }
        public string Name { get; set; } = string.Empty;

        
    }
}
