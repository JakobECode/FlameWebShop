using WebApp.Models.Dtos;

namespace WebApp.ViewModels
{
    public class CollectionViewModel
    {
        public IEnumerable<Product> Products { get; set; } = new List<Product>();
    }
}
