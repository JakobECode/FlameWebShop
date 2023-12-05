using WebApi.Models.Entities;

namespace WebApi.Models.Dtos
{
    public class Comment
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string CommentsText { get; set; } = null!;

        // Definierar en implicit konvertering från CommentEntity till Comment.
        // Detta gör det möjligt att automatiskt konvertera en CommentEntity-instans till en Comment-instans.

        public static implicit operator Comment(CommentEntity entity)
        {
            return new Comment
            {
                Id = entity.Id,
                Name = entity.Name,
                Email = entity.Email,
                CommentsText = entity.CommentsText,
            };
        }
        // Definierar en implicit konvertering från Comment till CommentEntity.
        public static implicit operator CommentEntity(Comment entity)
        {
            return new CommentEntity
            {
                Name = entity.Name,
                Email = entity.Email,
                CommentsText = entity.CommentsText,
            };
        }
    }
}
