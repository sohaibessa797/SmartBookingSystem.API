using SmartBookingSystem.Application.DTOs.ProviderPost;
using SmartBookingSystem.Application.DTOs.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartBookingSystem.Application.Interfaces
{
    public interface IProviderPostService
    {
        Task<ProviderPostResponse> CreateAsync(Guid userId, ProviderPostRequest request);
        Task<string> UpdateAsync(Guid postId, ProviderPostRequest request);
        Task<string> DeleteAsync(Guid postId);

        Task<ProviderPostResponse> GetByIdAsync(Guid postId);
        Task<List<ProviderPostResponse>> GetAllByProviderAsync(Guid providerId);
        Task<List<ProviderPostResponse>> GetCurrentProviderPostAsync(Guid userId);


        Task<string> ReactAsync(Guid userId, ProviderPostReactionRequest request);
        Task<string> RemoveReactionAsync(Guid postId, Guid userId);
    }
}
