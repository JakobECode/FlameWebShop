namespace WebApi.Models.Entities
{
    public class OrderItemEntity
    {
        public int Id { get; set; } 
        public int ProductId { get; set; }
        public int OrderId { get; set; }
      
    }
}
