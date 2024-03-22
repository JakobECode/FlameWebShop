using Microsoft.AspNetCore.Identity;
using WebApi.Helpers.Repositories;
using WebApi.Models.Dtos;
using WebApi.Models.Entities;
using WebApi.Models.Interfaces;
using WebApi.Models.Schemas;

namespace WebApi.Helpers.Services
{
    public class ReviewService : IReviewService
    {
        private readonly ReviewRepository _reviewRepo;
        private readonly IProductService _productService;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly UserProfileRepository _userProfileRepo;

        public ReviewService(ReviewRepository reviewRepo, IProductService productService, UserManager<IdentityUser> userManager, UserProfileRepository userProfileRepo)
        {
            _reviewRepo = reviewRepo;
            _productService = productService;
            _userManager = userManager;
            _userProfileRepo = userProfileRepo;
        }

        public async Task<IEnumerable<ReviewDTO>> GetAllAsync()
        {
            try
            {
                var products = await _reviewRepo.GetAllAsync();
                var dtos = new List<ReviewDTO>();

                foreach (var entity in products)
                {
                    var user = await _userProfileRepo.GetAsync(x => x.UserId == entity.UserId);
                    entity.ImageUrl = user.ImageUrl;
                    await _reviewRepo.UpdateAsync(entity);
                    dtos.Add(entity);
                }

                return dtos;
            }
            catch { }
            return null!;
        }

        public async Task<IEnumerable<ReviewDTO>> GetByProductId(Guid productId)
        {
            try
            {
                var reviews = await _reviewRepo.GetListAsync(p => p.ProductId == productId);
                var dtos = new List<ReviewDTO>();
                foreach (var entity in reviews)
                {
                    var user = await _userProfileRepo.GetAsync(x => x.UserId == entity.UserId);
                    entity.ImageUrl = user.ImageUrl;
                    await _reviewRepo.UpdateAsync(entity);
                    dtos.Add(entity);
                }

                return dtos;
            }
            catch { }
            return null!;
        }

        public async Task<bool> CreateAsync(ReviewSchema schema, string userName)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(userName);
                var userProfile = await _userProfileRepo.GetAsync(x => x.UserId == user!.Id);
                if (userProfile != null)
                {
                    ReviewEntity entity = schema;
                    entity.UserId = user!.Id;
                    entity.Name = $"{userProfile.FirstName} {userProfile.LastName}";
                    await _reviewRepo.AddAsync(entity);
                    await _productService.UpdateRatingAsync(entity.ProductId);

                    return true;
                }

            }
            catch { }
            return false;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            try
            {
                var entity = await _reviewRepo.GetAsync(r => r.Id == id);
                await _reviewRepo.DeleteAsync(entity!);

                return true;
            }
            catch { }
            return false;
        }
    }
}
