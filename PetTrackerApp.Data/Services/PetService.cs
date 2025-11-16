using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PetTrackerApp.Data.Dtos;
using PetTrackerApp.Data.Models;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetTrackerApp.Data.Services
{    
    public class PetService
    {
        //readonly olarak map ve context tanimliyoruz. readonly olarak tanimlamamizin sebebi bu degerlerin sadece constructor(kurucu metot) icinde set edilebilecek olmasi
        private readonly IMapper _mapper;
        private readonly PetTrackerAppDbContext _dbContext;

        //constructor'in mapper ve context parametresiz calismayacaggini söylüyoruz.
        //gelecek olan mapper ve context degerlerini readonly olarak tanimladigimiz degiskenlere atiyoruz.
        public PetService(IMapper mapper, PetTrackerAppDbContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }

        //Pet C - Pet Create
        //parametre olarak petdto alıyoruz
        //task'in icinde int donduruyoruz, petidyi dondurmek icin, petid'yi test icin donduruyoruz. ekliyor mu?
        public async Task<int> CreatePetAsync(PetDto petDto)
        {
            //dto nesnesini entity nesnesine mapliyoruz
            var pet = _mapper.Map<Pet>(petDto);

            //olusturma tarihini dtodan almadıgimiz icin kendimiz ekliyoruz
            //id zaten primary key oldugu icin otomatik artacaktir.
            pet.CreatedAt = DateTime.UtcNow;

            //veritabanina ekleme islemi yapip degisiklikleri kaydediyoruz
            _dbContext.Pets.Add(pet);
            await _dbContext.SaveChangesAsync();

            return pet.Id;
        }

        //Pet U - Pet Update
        public async Task UptadePetAsync(int id, PetDto petDto)
        {
            var existingPet = await _dbContext.Pets.FindAsync(id);
            
            //gelen dto ile mevcut pet nesnesini guncelliyoruz
            _mapper.Map(petDto, existingPet);

            //existingPet.name = petDto.name; gibi ifadeleri bilerek kullanmıyoruz cunku AutoMapper ile otomatik olarak yapabiliyoruz

            //_context.Pets.Update(pet); bunu bilerek kullanmıyoruz cunku ChangeTracking kütüphanesi zaten mevcut nesnede degisiklik oldugunu biliyor

            await _dbContext.SaveChangesAsync();
        }

        //Pet D - Pet Delete
        public async Task DeletePetAsync(int petId)
            {
                var pet = await _dbContext.Pets.FindAsync(petId);
                if (pet != null)
                {
                    _dbContext.Pets.Remove(pet);
                    await _dbContext.SaveChangesAsync();
                }
            }

        //Pet R - Pet Read
        //parametre olarak petid aliyoruz - hangi peti gösterecegimizi belirlemek icin
        //bu metotun temel amaci veritabaninindaki peti dto formatinda dondurmek ve return etmek
        //boylelikle uide kullanacagiz
        public async Task<PetDto> GetPetAsync(int petId)
        {
            var pet = await _dbContext.Pets.FindAsync(petId);            
            var petDto = _mapper.Map<PetDto>(pet);
            return petDto;
        }

        //Pets R - Pet Read All
        //veri tabanindaki tum petleri, dto ve liste halinde donduren metot
        public async Task<List<PetDto>> GetAllPetsAsync()
        {
            //veritabanindaki tum petleri aliyoruz liste haline getirip pets degiskenine atiyoruz
            var pets = await _dbContext.Pets.ToListAsync();

            //bu listeyi petdto listesi haline getirip geri donduruyoruz
            var petDtos = _mapper.Map<List<PetDto>>(pets);
            return petDtos;
        }

        //ileride sadece petin fotografini veritabanından kaldirma gibi islemler eklenecek.


    }
}
