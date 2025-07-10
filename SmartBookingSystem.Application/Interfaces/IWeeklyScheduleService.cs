using SmartBookingSystem.Application.DTOs.Service;
using SmartBookingSystem.Application.DTOs.WeeklySchedule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartBookingSystem.Application.Interfaces
{
    public interface IWeeklyScheduleService
    {
        Task<List<WeeklyScheduleResponse>> GetProviderScheduleAsync(Guid providerId);
        Task<WeeklyScheduleResponse> GetByIdAsync(Guid scheduleId);
        Task<List<WeeklyScheduleResponse>> GetCurrentProviderScheduleAsync(Guid userId);
        Task<List<WeeklyScheduleResponse>> CreateWeeklyScheduleAsync(Guid userId, List<WeeklyScheduleRequest> requests);
        Task<WeeklyScheduleResponse> UpdateAsync(Guid scheduleId, WeeklyScheduleRequest request);
        Task<string> DeleteAsync(Guid scheduleId);
    }
}
