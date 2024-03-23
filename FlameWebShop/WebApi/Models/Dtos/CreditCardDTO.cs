namespace WebApi.Models.Dtos
{
    public class CreditCardDto
    {
        public int Id { get; set; }
        public string NameOnCard { get; set; } = null!;
        public string CardNo { get; set; } = null!;
        public int ExpireMonth { get; set; }
        public int ExpireYear { get; set; }
        public string CVV { get; set; } = null!;
    }
}
