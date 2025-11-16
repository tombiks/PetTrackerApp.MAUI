using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PetTrackerApp.Data.Enums;

namespace PetTrackerApp.Data.Dtos
{
    public class PetDto
    {
        public string name { get; set; } = string.Empty;
        public PetType petType { get; set; }
        public PetGender petGender { get; set; }
        public DateTime dateOfBirth { get; set; }
        public string picturePath { get; set; } = string.Empty;

    }
}
