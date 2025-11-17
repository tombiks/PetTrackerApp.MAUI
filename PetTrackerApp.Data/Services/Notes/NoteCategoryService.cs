using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PetTrackerApp.Data.Dtos;
using PetTrackerApp.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetTrackerApp.Data.Services.Notes
{
    public class NoteCategoryService
    {
        private readonly IMapper _mapper;
        private readonly PetTrackerAppDbContext _dbContext;

        public NoteCategoryService(IMapper mapper, PetTrackerAppDbContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }

        // Create NoteCategory
        public async Task<int> CreateNoteCategoryAsync(NoteCategoryDto dto)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));

            // Pet kontrolü
            var petExists = await _dbContext.Pets.AnyAsync(p => p.Id == dto.PetId);
            if (!petExists)
                throw new Exception("Pet not found");

            var entity = _mapper.Map<NoteCategory>(dto);
            entity.CreatedDate = DateTime.UtcNow;

            _dbContext.NoteCategories.Add(entity);
            await _dbContext.SaveChangesAsync();

            return entity.Id;
        }

        // Update NoteCategory
        public async Task UpdateNoteCategoryAsync(int id, NoteCategoryDto dto)
        {
            var existing = await _dbContext.NoteCategories.FindAsync(id);
            if (existing == null)
                throw new Exception($"NoteCategory with id {id} not found.");

            _mapper.Map(dto, existing);
            await _dbContext.SaveChangesAsync();
        }

        // Delete NoteCategory
        public async Task DeleteNoteCategoryAsync(int id)
        {
            var existing = await _dbContext.NoteCategories.FindAsync(id);
            if (existing != null)
            {
                _dbContext.NoteCategories.Remove(existing);
                await _dbContext.SaveChangesAsync();
            }
        }

        // Get single NoteCategory
        public async Task<NoteCategoryDto?> GetNoteCategoryAsync(int id)
        {
            var entity = await _dbContext.NoteCategories.FindAsync(id);
            return entity == null ? null : _mapper.Map<NoteCategoryDto>(entity);
        }

        // Get all categories for a specific pet
        public async Task<List<NoteCategoryDto>> GetCategoriesByPetAsync(int petId)
        {
            var categories = await _dbContext.NoteCategories
                .Where(c => c.PetId == petId)
                .OrderBy(c => c.Name)
                .ToListAsync();

            return _mapper.Map<List<NoteCategoryDto>>(categories);
        }

        // Get all categories (optional)
        public async Task<List<NoteCategoryDto>> GetAllCategoriesAsync()
        {
            var categories = await _dbContext.NoteCategories.ToListAsync();
            return _mapper.Map<List<NoteCategoryDto>>(categories);
        }
    }
}