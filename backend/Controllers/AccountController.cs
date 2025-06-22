using LinkwayAPI.Constants.Account;
using LinkwayAPI.Constants.API;
using LinkwayAPI.DTOs.Account;
using LinkwayAPI.Enums.Role;
using LinkwayAPI.Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Net;

namespace LinkwayAPI.Controllers
{
    [Route(AccountConstant.API_ACCOUNTS)]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepository _repositoryAccount;

        public AccountController(IAccountRepository repositoryAccount)
        {
            _repositoryAccount = repositoryAccount;
        }

        [Authorize(Roles = nameof(RoleTypes.HR) + AdminConstant.COMMA + nameof(RoleTypes.Manager) + AdminConstant.COMMA + nameof(RoleTypes.RMG) + AdminConstant.COMMA + nameof(RoleTypes.Admin))]
        [HttpPost(AccountConstant.SIGNUP)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> SignUp([FromBody] RegisterDTO viewModelSignUp)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _repositoryAccount.SignUpAsync(viewModelSignUp);

                    if (result.Succeeded)
                        return Ok(result);
                    return BadRequest(result.Errors);
                }

                return StatusCode(500);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost(AccountConstant.LOGIN)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Login([FromBody] LoginDTO viewModelLogin)
        {
            try
            {
                var result = await _repositoryAccount.LoginAsync(viewModelLogin);

                if (string.IsNullOrEmpty(result))
                    return StatusCode(500);

                return Ok(new { result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost(AccountConstant.CHANGE_PASSWORD)]
        public async Task<IActionResult> ChangePassword(string userId, [FromBody] ChangePasswordDTO dtoReset)
        {
            var result = await _repositoryAccount.ChnagePasswordAsync(userId, dtoReset.NewPassword);
            if (!result) return BadRequest();
            return Ok();
        }

        [HttpPost(AccountConstant.FORGOT_PASSWORD)]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDTO dtoForgotPassword)
        {
            var (token, user) = await _repositoryAccount.GenerateTokenAsync(dtoForgotPassword.Email);
            if (!token.IsNullOrEmpty())
            {
                string encodedToken = WebUtility.UrlEncode(token);
                var url = string.Format(AccountConstant.RESET_PASSWORD_URL_FORMAT, encodedToken, user.Email);
                //var url = $"http://localhost:4200/auth/reset-password?token={token}&email={user.Email}";
                await _repositoryAccount.SendPasswordResetEmail(user.Email, url);

                return Ok();
            }
            return BadRequest();
        }
        [HttpGet(AccountConstant.RESET_PASSWORD)]
        public async Task<IActionResult> ResetPassword([FromQuery] string token, [FromQuery] string email)
        {
            return Ok(new { token, email });
        }
        [HttpPost(AccountConstant.RESET_PASSWORD)]
        public async Task<IActionResult> ResetPassword(ResetPasswordDTO dtoResetPassword)
        {
            var result = await _repositoryAccount.ResetPasswordAsync(dtoResetPassword);
            if (!result.Succeeded) return BadRequest(result.Errors);
            return Ok();

        }


        [HttpPost(AccountConstant.LOGOUT)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Logout()
        {
            try
            {
                var result = await _repositoryAccount.LogoutAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

    }
}
