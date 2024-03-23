using System.ComponentModel.DataAnnotations;
using WebApi.Models.Entities;

namespace WebApi.Models.Schemas
{
    public class ReviewSchema
    {
        public int ProductId { get; set; }

        [Required]
        public string Comment { get; set; } = null!;
        [Required]
        public double Rating { get; set; }

        public static implicit operator ReviewEntity(ReviewSchema schema)
        {
            DateTime now = DateTime.Now;
            return new ReviewEntity
            {
                ProductId = schema.ProductId,
                Comment = schema.Comment,
                Rating = schema.Rating,
                CreatedDate = new DateTime(now.Year, now.Month, now.Day),
            };
        }
    }
}
