using Microsoft.AspNetCore.Mvc;
using WebApp.Helpers;
using WebApp.Services;
using WebApp.ViewModels;

namespace WebApp.Controllers
{
	public class AccountController : Controller
	{
		private readonly AccountService _accountService;
		private readonly ApiHelper _api;
		private readonly AuthenticationService _auth;

		public AccountController(AccountService accountService, ApiHelper api, AuthenticationService auth)
		{
			_accountService = accountService;
			_api = api;
			_auth = auth;
		}
		public IActionResult Index()
		{
			var token = HttpContext.Request.Cookies["accessToken"];
			if (token != null)
			{
				if (_auth.IsTokenValidAndContainsRole(token!))
				{
					return RedirectToAction("Index", "Admin");
				}
			}

			return View();
		}
		public IActionResult Register()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Register(RegisterViewModel model)
		{
			if (ModelState.IsValid)
			{
				var result = await _accountService.RegisterAsync(model);

				if (result.IsSuccessStatusCode)
					return RedirectToAction("Login", "Account");

				ModelState.AddModelError("", "Incorrect input");
			}

			return View(model);
		}

		public IActionResult Login()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Login(LoginViewModel model)
		{

			if (ModelState.IsValid)
			{
				using var httpClient = new HttpClient();

				var result = await httpClient.PostAsJsonAsync(
					_api.ApiAddressRoot($"/Authentication/Login?x-api-key={_api.ApiKey()}"), model);

				var token = await result.Content.ReadAsStringAsync();

				HttpContext.Response.Cookies.Append("accessToken", token, new CookieOptions
				{
					HttpOnly = true,
					Secure = true,
					Expires = DateTime.Now.AddDays(1)
				});

				if (result.IsSuccessStatusCode)
					return RedirectToAction("Index", "Home");

				ModelState.AddModelError("", "Incorrect login details");
			}
			return View();
		}

        [HttpPost]
        public IActionResult Logout()
        {
            HttpContext.Response.Cookies.Delete("accessToken");

            return RedirectToAction("Index", "Home");
        }
    }
}
