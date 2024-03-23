using WebApi.Models.Dtos;
using WebApi.Models.Entities;
using WebApi.Models.Interfaces;
using WebApi.Models.Schemas;
using WebApi.Helpers.Repositories;

namespace WebApi.Helpers.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly CategoryRepository _categoryRepo;

        public CategoryService(CategoryRepository categoryRepo)
        {
            _categoryRepo = categoryRepo;
        }

        public async Task<IEnumerable<CategoryDto>> GetAllAsync()
        {
            try
            {
                var categories = await _categoryRepo.GetAllAsync();
                var dtos = new List<CategoryDto>();

                foreach (var entity in categories)
                {
                    dtos.Add(entity);
                }

                return dtos;
            }
            catch { }
            return null!;
        }

        public async Task<CategoryDto> GetByIdAsync(int id)
        {
            try
            {
                var category = await _categoryRepo.GetAsync(x => x.Id == id);
                CategoryDto dto = category;

                return dto;
            }
            catch { }
            return null!;
        }

        public async Task<bool> CreateAsync(CategorySchema schema)
        {
            try
            {
                CategoryEntity entity = schema;
                await _categoryRepo.AddAsync(entity);

                return true;
            }
            catch { }
            return false;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var entity = await _categoryRepo.GetAsync(x => x.Id == id);
                await _categoryRepo.DeleteAsync(entity!);

                return true;
            }
            catch { }
            return false;
        }
        public async Task<bool> CheckOrCreateAsync(string category)
        {
            try
            {
                var result = await _categoryRepo.GetAsync(x => x.Name == category);
                if (result == null)
                {
                    var entity = new CategoryEntity()
                    {
                        Name = category,
                    };
                    await _categoryRepo.AddAsync(entity);
                }
                return true;
            }
            catch { }
            return false;
        }
    }
}

