namespace WebApp.Models.Dtos
{
    public class Comment
    {
        public int Id { get; set; } 

        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string CommentsText { get; set; } = null!;
    }
}
