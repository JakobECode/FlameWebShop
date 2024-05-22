using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApi.Helpers.Filters;
using WebApi.Models.Interfaces;
using WebApi.Models.Schemas;
using Microsoft.AspNetCore.Authentication.Google;

namespace WebApi.Controllers
{
    //[UseApiKey]
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        #region Standard CRUD
        [Route("Register")]
        [HttpPost]
        public async Task<IActionResult> Register(RegisterAccountSchema schema)
        {
            if (ModelState.IsValid)
            {
                if (await _accountService.RegisterAsync(schema))
                {
                    return Ok();
                }
                else
                {
                    return BadRequest("Something went wrong, try again!");
                }
            }
            else
            {
                return BadRequest("Something went wrong, try again!");
            }
        }
        [Route("Login")]
        [HttpPost]
        public async Task<IActionResult> Login(LoginAccountSchema schema)
        {
            if (ModelState.IsValid)
            {
                var loginResponse = await _accountService.LogInAsync(schema);
                if (loginResponse != null && !string.IsNullOrEmpty(loginResponse.token))
                {
                    return Ok(new { Token = loginResponse.token, Role = loginResponse.role, loginResponse.Email });
                }
            }
            return BadRequest("Incorrect email or password");
        }
        //[Authorize]
        [Route("LogOut")]
        [HttpPost]
        public async Task<IActionResult> LogOutAsync()
        {
            await _accountService.LogOutAsync();
            return Ok();
        }
       // [Authorize]
        [Route("UpdateProfile")]
        [HttpPut]
        public async Task<IActionResult> UpdateProfileAsync(UpdateUserSchema schema)
        {

            if (ModelState.IsValid)
            {
                var userName = HttpContext.User.Identity!.Name;
                var result = await _accountService.UpdateProfileAsync(schema, userName!);
                if (result != null)
                {
                    return Ok("Update is done");
                }
                return Problem("Model valid, something else is wrong");
            }
            return BadRequest("Model not valid");
        }
       // [Authorize]
        [Route("GetProfile")]
        [HttpGet]
        public async Task<IActionResult> GetProfile()
        {

            if (ModelState.IsValid)
            {
                var userName = HttpContext.User.Identity!.Name;
                var result = await _accountService.GetProfile(userName!);
                if (result != null)
                {
                    return Ok(result);
                }
                return Problem("Model valid, something else is wrong");
            }
            return BadRequest("Model not valid");
        }
        [Route("ResetPassword")]
        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordSchema schema)
        {
            if (ModelState.IsValid)
            {
                if (await _accountService.ResetPassword(schema.Email))
                {
                    return Ok("An email has been sent");
                }
                return Problem("Something went wrong on the server");
            }
            return BadRequest("You must enter an email");
        }
        [Route("RecoverPassword")]
        [HttpPost]
        public async Task<IActionResult> RecoverPassword(RecoverPasswordSchema schema)
        {
            if (ModelState.IsValid)
            {
                var result = await _accountService.ChangePassword(schema);
                if (result)
                {
                    return Ok("Your password has been changed");
                }
            }

            return BadRequest();
        }
        [Route("ChangePassword")]
        [HttpPost]
       // [Authorize]
        public async Task<IActionResult> ChangePassword(ChangePasswordSchema schema)
        {
            if (ModelState.IsValid)
            {
                var userName = HttpContext.User.Identity!.Name;
                var result = await _accountService.ChangePassword(schema, userName!);
                if (result)
                {
                    return Ok("Your password is changed");
                }
                return Problem("Something went wrong on the server");
            }
            return BadRequest("");
        }
        [Route("DeleteById/{id}")]
        [HttpDelete]
      //  [Authorize]
        public async Task<IActionResult> DeleteAccountById(string id)
        {
            var result = await _accountService.DeleteUser(id);
            if (result)
            {
                return Ok();
            }
            return BadRequest();
        }
        [Route("RemoveAccount")]
        [HttpDelete]
       // [Authorize]
        public async Task<IActionResult> RemoveAccount()
        {
            var userName = HttpContext.User.Identity!.Name;
            var result = await _accountService.DeleteProfile(userName!);
            if (result)
            {
                return Ok();
            }
            return Problem();
        }

        #endregion

        #region External Login

        [Route("Google")]
        [HttpGet]
        public async Task Google() => await HttpContext.ChallengeAsync(
            GoogleDefaults.AuthenticationScheme,
            new AuthenticationProperties { RedirectUri = Url.Action("ExternalAuthGoogle") }
        );

        [Route("ExternalGoogle")]
        [HttpGet]
        public async Task<IActionResult> ExternalAuthGoogle()
        {
            var result = await HttpContext.AuthenticateAsync(GoogleDefaults.AuthenticationScheme);

            if (result.Succeeded)
            {
                ExternalLoginInfo externalUser = new ExternalLoginInfo
                    (
                        result.Principal,
                        result.Principal.Identity!.AuthenticationType!,
                        result.Principal.Claims.First().ToString(),
                        result.Principal.Identity.AuthenticationType!
                    );

                var token = await _accountService.LogInExternalAsync(externalUser);

                if (!string.IsNullOrEmpty(token))
                {
                    return Ok(token);
                }
            }

            return BadRequest("Failiure to authenticate.");
        }

        #endregion

    }
}
