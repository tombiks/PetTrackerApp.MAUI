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
        public string Name { get; set; } = string.Empty;
        public PetType PetType { get; set; }
        public PetGender PetGender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PicturePath { get; set; } = string.Empty;


        //UI icin alttaki property'leri ekliyoruz, bu propertylerin veritabaninda karsiligi yok - sadece UI'de kullanilacak
        //Sadece get olarak tanımlı olduklari icin mapping islemlerinde sorun yasamayiz(Umarım)

        //erkekse typegenderbackground mavi, dişiyse pembe olsun
        public string GenderColor => PetGender == PetGender.Male ? "#99D9EA" : "#FFB6D9";

        //erkekse erkek sembolü, dişiyse dişi sembolü
        public string GenderSymbol => PetGender == PetGender.Male ? "♂" : "♀";

        //pet türüne göre ikon belirleme (pets.ttf fontunda tanımlı ikonlar)
        public string PetTypeIcon => PetType switch
        {
            PetType.Bird => "\ue900",
            PetType.Dog => "\ue901",
            PetType.ExoticPet => "\ue902",
            PetType.Fish => "\ue903",
            PetType.Others => "\ue904",
            PetType.Rabbit => "\ue905",
            PetType.Reptile => "\ue906",
            PetType.Cat => "\ue907",
            PetType.SmallMammal => "\ue908",
            _ => "\ue904"
        };

        //eğer picturePath boşsa default avatar göster, doluysa picturePath'i göster
        public string DisplayPicture => string.IsNullOrEmpty(PicturePath) ? "default_avatar.png" : PicturePath;

        //yas hesaplama (yil.ay olarak)
        public double Age
        {
            get
            {
                var today = DateTime.Today;
                var years = today.Year - DateOfBirth.Year;

                if (DateOfBirth.Date > today.AddYears(-years))
                    years--;

                var months = today.Month - DateOfBirth.Month;
                if (today.Day < DateOfBirth.Day)
                    months--;

                if (months < 0)
                    months += 12;

                return Math.Round(years + (months / 12.0), 1);
            }
        }

    }
}
