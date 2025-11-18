using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PetTrackerApp.Data.Dtos;
using PetTrackerApp.Data.Models;

namespace PetTrackerApp.Data.Services.Reminders
{
    public class ReminderCategoryService
    {
        private readonly IMapper _mapper;
        private readonly PetTrackerAppDbContext _dbContext;

        public ReminderCategoryService(IMapper mapper, PetTrackerAppDbContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }

        // Create
        public async Task<int> CreateCategoryAsync(ReminderCategoryDto dto)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));

            // Pet var mı kontrol et
            var petExists = await _dbContext.Pets.AnyAsync(p => p.Id == dto.PetId);
            if (!petExists)
                throw new Exception("Pet not found");

            var category = _mapper.Map<ReminderCategory>(dto);
            _dbContext.ReminderCategories.Add(category);
            await _dbContext.SaveChangesAsync();

            return category.Id;
        }

        // Update
        public async Task UpdateCategoryAsync(int id, ReminderCategoryDto dto)
        {
            var category = await _dbContext.ReminderCategories.FindAsync(id);
            if (category == null)
                throw new Exception($"ReminderCategory with id {id} not found.");

            _mapper.Map(dto, category);
            await _dbContext.SaveChangesAsync();
        }

        // Delete
        public async Task DeleteCategoryAsync(int id)
        {
            var category = await _dbContext.ReminderCategories.FindAsync(id);
            if (category != null)
            {
                _dbContext.ReminderCategories.Remove(category);
                await _dbContext.SaveChangesAsync();
            }
        }

        // Get by Id
        public async Task<ReminderCategoryDto?> GetCategoryAsync(int id)
        {
            var category = await _dbContext.ReminderCategories.FindAsync(id);
            return category == null ? null : _mapper.Map<ReminderCategoryDto>(category);
        }

        // Get all categories for a Pet
        public async Task<List<ReminderCategoryDto>> GetCategoriesByPetAsync(int petId)
        {
            var categories = await _dbContext.ReminderCategories
                .Where(c => c.PetId == petId)
                .OrderBy(c => c.Name)
                .ToListAsync();

            return _mapper.Map<List<ReminderCategoryDto>>(categories);
        }

        // Get all categories
        public async Task<List<ReminderCategoryDto>> GetAllCategoriesAsync()
        {
            var categories = await _dbContext.ReminderCategories.ToListAsync();
            return _mapper.Map<List<ReminderCategoryDto>>(categories);
        }
    }
}