using Microsoft.AspNetCore.Http.Extensions;
using WebApp.Helpers;
using WebApp.Models.Dtos;

namespace WebApp.Services
{
    public class ProductService
    {
        private readonly ApiHelper _api;

        public ProductService(ApiHelper api)
        {
            _api = api;
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            using var http = new HttpClient();
            var url = _api.ApiAddressRoot($"/Products/all?x-api-key={_api.ApiKey()}");

            var products = await http.GetFromJsonAsync<IEnumerable<Product>>(url);

            return products!;
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            using var http = new HttpClient();
            var product = await http.GetFromJsonAsync<Product>(_api.ApiAddressRoot($"/products/id?id={id}&x-api-key={_api.ApiKey()}"));

            return product!;
        }


        public async Task<IEnumerable<Product>> GetByTagAsync(string tag)
        {
            using var http = new HttpClient();
            var products = await http.GetFromJsonAsync<IEnumerable<Product>>(_api.ApiAddressRoot($"/products/tag?tag={tag}&x-api-key={_api.ApiKey()}"));

            return products;
        }

        public async Task<HttpResponseMessage> CreateAsync(Product product)
        {
            using var httpClient = new HttpClient();
            return await httpClient.PostAsJsonAsync(
                _api.ApiAddressRoot($"/products/add?x-api-key={_api.ApiKey()}"), product);
        }
    }
}
