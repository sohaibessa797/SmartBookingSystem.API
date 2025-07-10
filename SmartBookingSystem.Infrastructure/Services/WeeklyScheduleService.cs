using AutoMapper;
using SmartBookingSystem.Application.DTOs.WeeklySchedule;
using SmartBookingSystem.Application.Interfaces;
using SmartBookingSystem.Domain.Entities;
using SmartBookingSystem.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartBookingSystem.Infrastructure.Services
{
    public class WeeklyScheduleService : IWeeklyScheduleService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public WeeklyScheduleService(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<WeeklyScheduleResponse> GetByIdAsync(Guid scheduleId)
        {
            var schedule = await _unitOfWork.WeeklySchedules.GetByIdAsync(s => s.Id == scheduleId, x => x.Provider);
            if (schedule == null)
                throw new KeyNotFoundException("Schedule not found.");
            var scheduleResponse = _mapper.Map<WeeklyScheduleResponse>(schedule);
            return scheduleResponse;
        }

        public async Task<List<WeeklyScheduleResponse>> GetCurrentProviderScheduleAsync(Guid userId)
        {
            var provider = await _unitOfWork.Providers.GetByIdAsync(p => p.ApplicationUserId == userId);
            if (provider == null)
                throw new KeyNotFoundException("Provider not found for the current user.");
            var schedules = await _unitOfWork.WeeklySchedules.GetAllAsync(s => s.ProviderId == provider.Id, x => x.Provider);
            if (!schedules.Any())
                throw new KeyNotFoundException("No schedules found for this provider.");
            var scheduleResponses = _mapper.Map<List<WeeklyScheduleResponse>>(schedules);
            return scheduleResponses;
        }

        public async Task<List<WeeklyScheduleResponse>> GetProviderScheduleAsync(Guid providerId)
        {
            var schedules = await _unitOfWork.WeeklySchedules.GetAllAsync(s => s.ProviderId == providerId, x => x.Provider);
            if (!schedules.Any())
                throw new KeyNotFoundException("No schedules found for this provider.");
            var scheduleResponses = _mapper.Map<List<WeeklyScheduleResponse>>(schedules);
            return scheduleResponses;
        }

        public async Task<List<WeeklyScheduleResponse>> CreateWeeklyScheduleAsync(Guid userId, List<WeeklyScheduleRequest> requests)
        {
            if (requests == null || !requests.Any())
                throw new ArgumentException("Request list cannot be null or empty.", nameof(requests));

            // Prevent duplicate days in the same request
            var duplicateDays = requests.GroupBy(r => r.DayOfWeek)
                                        .Where(g => g.Count() > 1)
                                        .Select(g => g.Key)
                                        .ToList();

            if (duplicateDays.Any())
                throw new ArgumentException($"Duplicate days are not allowed: {string.Join(", ", duplicateDays)}");

            // Check that the provider exists
            var provider = await _unitOfWork.Providers.GetByIdAsync(p => p.ApplicationUserId == userId)
                          ?? throw new KeyNotFoundException("Provider not found for the current user.");

            // Get the Weekly Schedules for the provider
            var existingDays = await _unitOfWork.WeeklySchedules.GetAllAsync(s => s.ProviderId == provider.Id);

            // Check for conflicting days
            var conflictDays = existingDays.Select(e => e.DayOfWeek)
                                           .Intersect(requests.Select(s => s.DayOfWeek))
                                           .ToList();

            if (conflictDays.Any())
                throw new InvalidOperationException($"Schedules already exist for: {string.Join(", ", conflictDays)}");

            var schedules = _mapper.Map<List<WeeklySchedule>>(requests);
            schedules.ForEach(s => s.ProviderId = provider.Id);

            await _unitOfWork.WeeklySchedules.AddRangeAsync(schedules);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<List<WeeklyScheduleResponse>>(schedules);
        }


        public async Task<WeeklyScheduleResponse> UpdateAsync(Guid scheduleId, WeeklyScheduleRequest request)
        {
            var schedule = await _unitOfWork.WeeklySchedules.GetByIdAsync(s => s.Id == scheduleId);
            if (schedule == null)
                throw new KeyNotFoundException("Schedule not found.");
            var exists = await _unitOfWork.WeeklySchedules
                .AnyAsync(s => s.ProviderId == schedule.ProviderId &&
                               s.DayOfWeek == request.DayOfWeek &&
                               s.Id != scheduleId);
            if (exists)
                throw new InvalidOperationException("A schedule for this day already exists.");
            _mapper.Map(request, schedule);
            await _unitOfWork.WeeklySchedules.UpdateAsync(schedule);
            await _unitOfWork.SaveChangesAsync();
            var updatedSchedule = _mapper.Map<WeeklyScheduleResponse>(schedule);
            return updatedSchedule;
        }

        public async Task<string> DeleteAsync(Guid scheduleId)
        {
            var schedule = await _unitOfWork.WeeklySchedules.GetByIdAsync(s => s.Id == scheduleId);
            if (schedule == null)
                throw new KeyNotFoundException("Schedule not found.");
            await _unitOfWork.WeeklySchedules.DeleteAsync(schedule);
            await _unitOfWork.SaveChangesAsync();
            return "Schedule deleted successfully.";
        }
    }
}
