using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PetTrackerApp.Data.Dtos;
using PetTrackerApp.Data.Enums;
using PetTrackerApp.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetTrackerApp.Data.Services.Notes
{
    public class NoteService
    {
        private readonly IMapper _mapper;
        private readonly PetTrackerAppDbContext _dbContext;

        public NoteService(IMapper mapper, PetTrackerAppDbContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }

        // Create Note
        public async Task<int> CreateNoteAsync(NoteDto noteDto)
        {
            if (noteDto == null) throw new ArgumentNullException(nameof(noteDto));

            if (!Enum.IsDefined(typeof(Importance), noteDto.Importance))
                throw new Exception("Invalid importance value.");

            //Not oluşturulmadan önce verilen PetId gerçekten var mı kontrol et.
            // Bu, geçersiz hayvanlara bağlı not oluşmasını engeller ve veritabanı tutarlılığını korur.
            var petExists = await _dbContext.Pets.AnyAsync(p => p.Id == noteDto.PetId);
            if (!petExists)
                throw new Exception("Pet not found");

            // DTO → Entity dönüşümü
            var note = _mapper.Map<Note>(noteDto);
            //  Oluşturulma tarihini manuel olarak ayarla
            note.CreatedDate = DateTime.UtcNow;

            //  Veritabanına kaydet
            _dbContext.Notes.Add(note);
            await _dbContext.SaveChangesAsync();

            return note.Id; // Yeni oluşturulan notun ID'sini geri döndür
        }

        // Update Note
        public async Task UpdateNoteAsync(int id, NoteDto noteDto)
        {
            // Güncellenmek istenen notu bul
            var existingNote = await _dbContext.Notes.FindAsync(id);
            if (existingNote == null)
                throw new Exception($"Note with id {id} not found.");


            if (!Enum.IsDefined(typeof(Importance), noteDto.Importance))
                throw new Exception("Invalid importance value.");

            // Yeni değerleri mevcut note üzerine uygula
            _mapper.Map(noteDto, existingNote);

            // EF Core ChangeTracker değişiklikleri algılar, Update demeye gerek yok.
            await _dbContext.SaveChangesAsync();
        }

        // Delete Note
        public async Task DeleteNoteAsync(int id)
        {
            // Notu bul
            var note = await _dbContext.Notes.FindAsync(id);
            if (note != null)
            {
                // Veritabanından kaldır
                _dbContext.Notes.Remove(note);
                await _dbContext.SaveChangesAsync();
            }
        }

        // Get Single Note
        public async Task<NoteDto?> GetNoteAsync(int id)
        {
            var note = await _dbContext.Notes.FindAsync(id);
            // Eğer note yoksa null döner
            return note == null ? null : _mapper.Map<NoteDto>(note);
        }

        // Notes belonging to a specific pet
        public async Task<List<NoteDto>> GetNotesByPetAsync(int petId)
        {
            // PetId'ye göre filtrele + tarihe göre ters sırala
            var notes = await _dbContext.Notes
                .Where(n => n.PetId == petId)
                .OrderByDescending(n => n.CreatedDate)
                .ToListAsync();

            return _mapper.Map<List<NoteDto>>(notes);
        }

        //  Get All Notes (opt)
        public async Task<List<NoteDto>> GetAllNotesAsync()
        {
            var notes = await _dbContext.Notes.ToListAsync();
            return _mapper.Map<List<NoteDto>>(notes);
        }
    }
}
