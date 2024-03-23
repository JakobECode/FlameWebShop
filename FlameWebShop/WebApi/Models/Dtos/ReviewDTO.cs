using WebApi.Models.Entities;

namespace WebApi.Models.Dtos
{
    public class ReviewDto
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        public string Name { get; set; } = null!;

        public string Comment { get; set; } = null!;

        public double Rating { get; set; }
        public string? ImageUrl { get; set; }
        public DateTime CreatedDate { get; set; }

        public static implicit operator ReviewDto(ReviewEntity entity)
        {
            return new ReviewDto
            {
                Id = entity.Id,
                ProductId = entity.ProductId,
                Name = entity.Name,
                Comment = entity.Comment,
                Rating = entity.Rating,
                CreatedDate = entity.CreatedDate,
                ImageUrl = entity.ImageUrl,
            };
        }
    }
}
