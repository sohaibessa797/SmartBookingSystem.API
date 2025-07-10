using SmartBookingSystem.Domain.Entities;
using SmartBookingSystem.Domain.Interfaces;
using SmartBookingSystem.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SmartBookingSystem.Infrastructure.Repositories
{
    public class ProviderPostRepository : GenericRepository<ProviderPost>, IProviderPostRepository
    {
        public ProviderPostRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task UpdateAsync(ProviderPost providerPost)
        {
            var existingProviderPost = await _context.ProviderPosts.FindAsync(providerPost.Id);
            if (existingProviderPost == null)
                throw new Exception("Post not found");

            existingProviderPost.Title = providerPost.Title;
            existingProviderPost.Content = providerPost.Content;
            await _context.SaveChangesAsync();

        }
    }
}
