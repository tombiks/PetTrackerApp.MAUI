using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetTrackerApp.Data.Enums
{
    public enum Importance
    {
        [Display(Name = "Not Important")]
        NotImportant = 0,  // yeni ekledik

        [Display(Name = "Low")]
        Low = 1,

        [Display(Name = "Medium")]
        Medium = 2,

        [Display(Name = "High")]
        High = 3
    }
}
