using AutoMapper;
using SmartBookingSystem.Application.DTOs.ProviderPost;
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
    public class ProviderPostService : IProviderPostService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ProviderPostService(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<ProviderPostResponse>> GetAllByProviderAsync(Guid providerId)
        {
            var posts = await _unitOfWork.ProviderPosts.GetAllAsync(p=>p.ProviderId == providerId,p=>p.Images,p=>p.Reactions);
            if (posts == null)
                throw new Exception("Post not found");
            var orderedPosts = posts.OrderByDescending(p => p.CreatedAt).ToList();
            var response = _mapper.Map<List<ProviderPostResponse>>(orderedPosts);
            return response;
        }

        public async Task<ProviderPostResponse> GetByIdAsync(Guid postId)
        {
            var post = await _unitOfWork.ProviderPosts.GetByIdAsync(p => p.Id == postId, p => p.Images, p => p.Reactions,p=>p.Provider);
            if (post == null)
                throw new KeyNotFoundException("Post not found.");
            return _mapper.Map<ProviderPostResponse>(post);
        }

        public async Task<List<ProviderPostResponse>> GetCurrentProviderPostAsync(Guid userId)
        {
            var provider = await _unitOfWork.Providers.GetByIdAsync(p => p.ApplicationUserId == userId);
            if (provider == null)
                throw new KeyNotFoundException("Provider not found.");

            return await GetAllByProviderAsync(provider.Id);

        }

        public async Task<ProviderPostResponse> CreateAsync(Guid userId, ProviderPostRequest request)
        {
            var provider = await _unitOfWork.Providers.GetByIdAsync(p => p.ApplicationUserId == userId);
            var post = new ProviderPost
            {
                Id = Guid.NewGuid(),
                ProviderId = provider.Id,
                Title = request.Title,
                Content = request.Content,
                Images = new List<ProviderPostImage>()
            };

            string rootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
            string uploadPath = Path.Combine(rootPath, "Images", "ProviderPosts");

            if (!Directory.Exists(uploadPath))
                Directory.CreateDirectory(uploadPath);

            if (request.Images != null && request.Images.Any())
            {
                foreach (var file in request.Images)
                {
                    string fileName = Guid.NewGuid().ToString();
                    string ext = Path.GetExtension(file.FileName);
                    string filePath = Path.Combine(uploadPath, fileName + ext);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    post.Images.Add(new ProviderPostImage
                    {
                        Id = Guid.NewGuid(),
                        ImageUrl = $"/Images/ProviderPosts/{fileName}{ext}"
                    });
                }
            }

            await _unitOfWork.ProviderPosts.AddAsync(post);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<ProviderPostResponse>(post);
        }



        public async Task<string> UpdateAsync(Guid postId, ProviderPostRequest request)
        {
            // 1. Get existing images related to the post from the database
            var existingImages = await _unitOfWork.ProviderPostImages.GetAllAsync(img => img.ProviderPostId == postId);

            // 2. Define the root path where images are stored on the server
            string rootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
            if (request.Images != null && request.Images.Any())
            {
                // 3. Delete existing images from the server
                foreach (var image in existingImages)
                {
                    var fullImagePath = Path.Combine(rootPath, image.ImageUrl.TrimStart('/').Replace("/", Path.DirectorySeparatorChar.ToString()));
                    if (File.Exists(fullImagePath))
                        File.Delete(fullImagePath);
                }

                // 4. Delete existing image records from the database
                await _unitOfWork.ProviderPostImages.DeleteRangeAsync(existingImages.ToList());
            }
        

            // 5. Get the post entity to be updated
            var post = await _unitOfWork.ProviderPosts.GetByIdAsync(
                p => p.Id == postId,
                predicate => predicate.Reactions // Include Reactions (if needed for tracking or serialization)
            );

            if (post == null)
                throw new KeyNotFoundException("Post not found");

            // 6. Update post title and content
            post.Title = request.Title;
            post.Content = request.Content;

            // 7. Prepare the upload directory for new images
            string uploadPath = Path.Combine(rootPath, "Images", "ProviderPosts");
            if (!Directory.Exists(uploadPath))
                Directory.CreateDirectory(uploadPath);

            // 8. Process and save new images
            if (request.Images != null && request.Images.Any())
            {
                var newImages = new List<ProviderPostImage>();

                foreach (var file in request.Images)
                {
                    if (file.Length > 0)
                    {
                        // 🔸 Generate a unique file name
                        string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                        string filePath = Path.Combine(uploadPath, fileName);

                        // 🔸 Save the image to the server
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await file.CopyToAsync(stream);
                        }

                        // 🔸 Create a new image entity
                        newImages.Add(new ProviderPostImage
                        {
                            Id = Guid.NewGuid(),
                            ProviderPostId = post.Id,
                            ImageUrl = $"/Images/ProviderPosts/{fileName}"
                        });
                    }
                }

                // 9. Save new image records to the database
                await _unitOfWork.ProviderPostImages.AddRangeAsync(newImages);
            }

            // 10. Mark the post entity as modified (optional if tracked)
            await _unitOfWork.ProviderPosts.UpdateAsync(post);

            // 11. Save all changes to the database
            await _unitOfWork.SaveChangesAsync();

            // 12. Return success message
            return $"Post titled \"{post.Title}\" was updated successfully.";
        }




        public async Task<string> DeleteAsync(Guid postId)
        {
            var post = await _unitOfWork.ProviderPosts.GetByIdAsync(
                p => p.Id == postId,
                p => p.Images
            );

            if (post == null)
                return "Post not found";

            string rootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");

            foreach (var image in post.Images)
            {
                var fullImagePath = Path.Combine(rootPath, image.ImageUrl.TrimStart('/').Replace("/", Path.DirectorySeparatorChar.ToString()));
                if (File.Exists(fullImagePath))
                    File.Delete(fullImagePath);
            }

            await _unitOfWork.ProviderPosts.DeleteAsync(post);
            await _unitOfWork.SaveChangesAsync();

            return $"Post titled \"{post.Title}\" was deleted successfully.";
        }



        public Task<string> ReactAsync(Guid userId, ProviderPostReactionRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<string> RemoveReactionAsync(Guid postId, Guid userId)
        {
            throw new NotImplementedException();
        }

        
    }
}
