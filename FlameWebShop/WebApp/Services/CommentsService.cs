using WebApp.Helpers;
using WebApp.Models.Dtos;

namespace WebApp.Services
{
    public class CommentsService
    {
        private readonly ApiHelper _api;

        public CommentsService(ApiHelper api)
        {
            _api = api;
        }

        public async Task<IEnumerable<Comment>> GetAllAsync()
        {
            using var http = new HttpClient();
            var comments = await http.GetFromJsonAsync<IEnumerable<Comment>>(_api.ApiAddressRoot($"/comments/all?x-api-key={_api.ApiKey()}"));

            return comments!;
        }

        public async Task<Comment> GetByIdAsync(int id)
        {
            using var http = new HttpClient();
            var comment = await http.GetFromJsonAsync<Comment>(_api.ApiAddressRoot($"/comments/id?id={id}&x-api-key={_api.ApiKey()}"));

            return comment!;
        }


        public async Task<HttpResponseMessage> CreateAsync(Comment comment)
        {
            using var httpClient = new HttpClient();
            return await httpClient.PostAsJsonAsync(
                _api.ApiAddressRoot($"/comments/add?x-api-key={_api.ApiKey()}"), comment);
        }
    }
}
