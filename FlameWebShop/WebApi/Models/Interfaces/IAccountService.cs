using Microsoft.AspNetCore.Identity;
using WebApi.Models.Dtos;
using WebApi.Models.Schemas;

namespace WebApi.Models.Interfaces
{
    public interface IAccountService
    {
        Task<bool> RegisterAsync(RegisterAccountSchema schema);
        Task<string> LogInAsync(LoginAccountSchema schema);
        Task<string> LogInExternalAsync(ExternalLoginInfo externalUser);
        Task LogOutAsync();
        Task<UserProfileDto> ReturnProfileAsync(string Id);
        Task<UserProfileDto> UpdateProfileAsync(UpdateUserSchema schema, string userName);
        Task<string> GetUserIdAsync(string userName);
        Task<bool> DeleteProfile(string userName);
        Task<bool> DeleteUser(string id);
        Task<UserProfileDto> GetProfile(string userName);
        Task<bool> ResetPassword(string email);
        Task<bool> ChangePassword(RecoverPasswordSchema schema);
        Task<bool> ChangePassword(ChangePasswordSchema schema, string email);
        
        
        
        
        
    }
}
