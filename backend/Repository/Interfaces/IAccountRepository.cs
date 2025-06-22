using LinkwayAPI.Data;
using LinkwayAPI.DTOs.Account;
using Microsoft.AspNetCore.Identity;

namespace LinkwayAPI.Repository.Interfaces
{
    public interface IAccountRepository
    {
        Task<IdentityResult> SignUpAsync(RegisterDTO viewModelSignUp);
        Task<string> LoginAsync(LoginDTO viewModelLogin);
        Task<bool> LogoutAsync();
        Task<bool> ChnagePasswordAsync(string userId, string newPassword);
        Task<(string,UsrUser)> GenerateTokenAsync(string email);
        Task SendMail(string reciverMail, string subject, string body);
        Task<IdentityResult> ResetPasswordAsync(ResetPasswordDTO dtoReset);
        Task SendPasswordResetEmail(string receiver, string link);
    }
}
