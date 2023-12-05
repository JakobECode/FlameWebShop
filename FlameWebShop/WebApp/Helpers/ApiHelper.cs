namespace WebApp.Helpers
{
    public class ApiHelper
    {
        private readonly IConfiguration _configuration;

        public ApiHelper(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string ApiAddressRoot()
        {
            return _configuration.GetValue<string>("ApiAddress")! + "/api";
        }
        public string ApiAddressRoot(string endPoint)
        {
            return _configuration.GetValue<string>("ApiAddress")! + "/api" + endPoint;
        }

        public string ApiKey()
        {
            return _configuration.GetValue<string>("ApiKey")!;
        }
    }
}
