using WebApp.Models.Dtos;

namespace WebApp.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Product> Featured { get; set; } = new List<Product>();
        public IEnumerable<Product> Popular { get; set; } = new List<Product>();
        public IEnumerable<Product> New { get; set; } = new List<Product>();
    }
}
