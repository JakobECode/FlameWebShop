using WebApp.Helpers;
using WebApp.Models.Dtos;

namespace WebApp.Services
{
    public class CategoryService
    {
        private readonly ApiHelper _api;

        public CategoryService(ApiHelper api)
        {
            _api = api;
        }

        public async Task<List<Category>> GetAll()
        {
            using var http = new HttpClient();
            var product = await http.GetFromJsonAsync<List<Category>>(_api.ApiAddressRoot($"/categories/all?x-api-key={_api.ApiKey()}"));

            return product!;
        }
    }
}
