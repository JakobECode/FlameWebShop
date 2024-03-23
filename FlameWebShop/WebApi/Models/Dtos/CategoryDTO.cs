using WebApi.Models.Entities;

namespace WebApi.Models.Dtos
{
    public class CategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public static implicit operator CategoryDto(CategoryEntity entity)
        {
            return new CategoryDto
            {
                Id = entity.Id,
                Name = entity.Name
            };
        }
    }
}



