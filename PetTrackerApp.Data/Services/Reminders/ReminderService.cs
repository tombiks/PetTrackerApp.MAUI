using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PetTrackerApp.Data.Dtos;
using PetTrackerApp.Data.Enums;
using PetTrackerApp.Data.Models;

namespace PetTrackerApp.Data.Services.Reminders
{
    public class ReminderService
    {
        private readonly IMapper _mapper;
        private readonly PetTrackerAppDbContext _dbContext;
        private readonly CompletedReminderService _completedReminderService;

        public ReminderService(IMapper mapper, PetTrackerAppDbContext dbContext, CompletedReminderService completedReminderService)
        {
            _mapper = mapper;
            _dbContext = dbContext;
            _completedReminderService = completedReminderService;
        }

        public async Task<int> CreateReminderAsync(ReminderDto reminderDto)
        {
            if (reminderDto == null) throw new ArgumentNullException(nameof(reminderDto));
            if (!Enum.IsDefined(typeof(Importance), reminderDto.Importance))
                throw new Exception("Invalid importance value.");

            var reminder = _mapper.Map<Reminder>(reminderDto);
            reminder.IsCompleted = false;

            _dbContext.Reminders.Add(reminder);
            await _dbContext.SaveChangesAsync();
            return reminder.Id;
        }


        public async Task UpdateReminderAsync(int id, ReminderDto reminderDto)
        {
            var existingReminder = await _dbContext.Reminders.FindAsync(id);
            if (existingReminder == null) throw new Exception($"Reminder with id {id} not found");
            if (!Enum.IsDefined(typeof(Importance), reminderDto.Importance))
                throw new Exception("Invalid importance value.");

            _mapper.Map(reminderDto, existingReminder);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteReminderAsync(int id)
        {
            var reminder = await _dbContext.Reminders.FindAsync(id);
            if (reminder != null)
            {
                _dbContext.Reminders.Remove(reminder);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<ReminderDto?> GetReminderAsync(int id)
        {
            var reminder = await _dbContext.Reminders.FindAsync(id);
            return reminder == null ? null : _mapper.Map<ReminderDto>(reminder);
        }

        public async Task<List<ReminderDto>> GetRemindersByCategoryAsync(int categoryId)
        {
            var reminders = await _dbContext.Reminders
                .Where(r => r.ReminderCategoryId == categoryId)
                .OrderBy(r => r.ReminderDate)
                .ToListAsync();

            return _mapper.Map<List<ReminderDto>>(reminders);
        }

        public async Task<List<ReminderDto>> GetAllRemindersAsync()
        {
            var reminders = await _dbContext.Reminders.ToListAsync();
            return _mapper.Map<List<ReminderDto>>(reminders);
        }

        public async Task MarkAsCompletedAsync(int id)
        {
            var reminder = await _dbContext.Reminders.FindAsync(id);
            if (reminder == null) throw new Exception("Reminder not found");

            reminder.IsCompleted = true;

            // DTO kullanma, doğrudan entity oluşturma
            await _completedReminderService.CreateFromReminder(reminder);

            // Tekrar eden reminder varsa yeni oluştur
            if (reminder.RepeatIntervalDays > 0)
            {
                var nextReminder = new Reminder
                {
                    ReminderCategoryId = reminder.ReminderCategoryId,
                    Title = reminder.Title,
                    Description = reminder.Description,
                    ReminderDate = reminder.ReminderDate.AddDays(reminder.RepeatIntervalDays),
                    Importance = reminder.Importance,
                    IsCompleted = false
                };
                _dbContext.Reminders.Add(nextReminder);
            }

            await _dbContext.SaveChangesAsync();
        }

    }
}

