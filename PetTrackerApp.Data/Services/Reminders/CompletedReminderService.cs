using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PetTrackerApp.Data.Dtos;
using PetTrackerApp.Data.Models;

namespace PetTrackerApp.Data.Services.Reminders   
{                                       
    public class CompletedReminderService
    {
        private readonly IMapper _mapper;
        private readonly PetTrackerAppDbContext _dbContext;

        public CompletedReminderService(IMapper mapper, PetTrackerAppDbContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }

        // UI için GET işlemleri
        //  ID ile getir
        public async Task<CompletedReminderDto?> GetAsync(int id)
        {
            var item = await _dbContext.CompletedReminders
                .FirstOrDefaultAsync(x => x.Id == id);

            return item == null ? null : _mapper.Map<CompletedReminderDto>(item);
        }

        //  Tüm Completed Reminders
        public async Task<List<CompletedReminderDto>> GetAllAsync()
        {
            var items = await _dbContext.CompletedReminders
                .OrderByDescending(x => x.CompletedDate)
                .ToListAsync();

            return _mapper.Map<List<CompletedReminderDto>>(items);
        }

        public async Task<List<CompletedReminderDto>> GetByCategoryAsync(int categoryId)
        {
            var items = await _dbContext.CompletedReminders
                .Where(x => x.ReminderCategoryId == categoryId)
                .OrderByDescending(x => x.CompletedDate)
                .ToListAsync();

            return _mapper.Map<List<CompletedReminderDto>>(items);
        }


        // Yeni Completed Reminder ekle (UI için)
        public async Task<CompletedReminderDto> AddAsync(CompletedReminderDto dto)
        {
            var entity = _mapper.Map<CompletedReminder>(dto);
            _dbContext.CompletedReminders.Add(entity);
            await _dbContext.SaveChangesAsync();
            return _mapper.Map<CompletedReminderDto>(entity);
        }

        //Yeni metot: Reminder üzerinden CompletedReminder oluşturma
        //Amaç: Reminder üzerinden CompletedReminder entity’si oluşturmayı ve bunu service içinde yönetmeyi sağlamak.
        public async Task<CompletedReminder> CreateFromReminder(Reminder reminder)
        {
            var entity = new CompletedReminder
            {
                OriginalReminderId = reminder.Id,
                ReminderCategoryId = reminder.ReminderCategoryId,
                Title = reminder.Title,
                Description = reminder.Description,
                Importance = reminder.Importance,
                CompletedDate = DateTime.UtcNow
            };

            _dbContext.CompletedReminders.Add(entity);
            await _dbContext.SaveChangesAsync();

            return entity;
        }

        //  Sil (opsiyonel ama genelde lazım)
        public async Task DeleteAsync(int id)
        {
            var entity = await _dbContext.CompletedReminders.FindAsync(id);
            if (entity != null)
            {
                _dbContext.CompletedReminders.Remove(entity);
                await _dbContext.SaveChangesAsync();
            }
        }


    }
}