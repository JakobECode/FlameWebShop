using WebApi.Models.Entities;

namespace WebApi.Models.Dtos
{
    public class CategoryDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;

        public static implicit operator CategoryDTO(CategoryEntity entity)
        {
            return new CategoryDTO
            {
                Id = entity.Id,
                Name = entity.Name
            };
        }
    }
}



