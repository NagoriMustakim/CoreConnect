using LinkwayAPI.Constants.Account;
using LinkwayAPI.Data;
using LinkwayAPI.DTOs.Account;
using LinkwayAPI.Enums.Role;
using LinkwayAPI.Models;
using LinkwayAPI.Repository.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using System.Text;

namespace LinkwayAPI.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly UserManager<UsrUser> _managerUser;
        private readonly SignInManager<UsrUser> _managerSignIn;
        private readonly RoleManager<IdentityRole> _managerRole;
        private readonly IConfiguration _configuration;
        private readonly LinkwayDbContext _dbContextLinkway;
        public AccountRepository(UserManager<UsrUser> managerUser, SignInManager<UsrUser> managerSignIn, RoleManager<IdentityRole> managerRole, IConfiguration configuration, LinkwayDbContext dbContextLinkway)
        {
            _managerUser = managerUser;
            _managerSignIn = managerSignIn;
            _managerRole = managerRole;
            _configuration = configuration;
            _dbContextLinkway = dbContextLinkway;
        }

        public async Task<IdentityResult> SignUpAsync(RegisterDTO dtoRegister)
        {
            var designation = await _dbContextLinkway.MstDesignations.SingleOrDefaultAsync(d => d.DesignationGuid == dtoRegister.DesignationGuid);
            if (designation == null) return null;

            var user = new UsrUser()
            {
                Email = dtoRegister.Email,
                UserName = dtoRegister.Email,
                FirstName = dtoRegister.FirstName,
                LastName = dtoRegister.LastName,
                CreationDate = dtoRegister.CreationDate,
                ModificationDate = DateTime.UtcNow,
                DesignationId = designation.DesignationId
            };

            string role = ((RoleTypes)dtoRegister.Role).ToString();

            var result = await _managerUser.CreateAsync(user, dtoRegister.Password);

            if (result.Succeeded)
            {
                if (!await _managerRole.RoleExistsAsync(RoleTypes.Admin.ToString()))
                    result = await _managerRole.CreateAsync(new IdentityRole(RoleTypes.Admin.ToString()));

                if (!await _managerRole.RoleExistsAsync(role))
                    result = await _managerRole.CreateAsync(new IdentityRole(role));

                if (await _managerRole.RoleExistsAsync(role))
                    result = await _managerUser.AddToRoleAsync(user, role);

                if (role.Equals(RoleTypes.Admin.ToString()))
                    await _managerUser.AddClaimsAsync(user, ClaimsStore.AdminClaims);

                if (role.Equals(RoleTypes.Manager.ToString()))
                    await _managerUser.AddClaimsAsync(user, ClaimsStore.ManagerClaims);

                if (role.Equals(RoleTypes.RMG.ToString()))
                    await _managerUser.AddClaimsAsync(user, ClaimsStore.RMGClaims);

                if (role.Equals(RoleTypes.HR.ToString()))
                    await _managerUser.AddClaimsAsync(user, ClaimsStore.HRClaims);

                if (role.Equals(RoleTypes.Candidate.ToString()))
                    await _managerUser.AddClaimsAsync(user, ClaimsStore.CandidateClaims);

                SendAccountCreationEmail(dtoRegister.Email, dtoRegister.Password);
            }

            return result;
        }

        public async Task<string> LoginAsync(LoginDTO viewModelLogin)
        {
            var user = await _managerUser.FindByEmailAsync(viewModelLogin.Email);

            if (user != null && await _managerUser.CheckPasswordAsync(user, viewModelLogin.Password))
            {
                var userRoles = await _managerUser.GetRolesAsync(user);

                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Email,viewModelLogin.Email),
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                    new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
                };

                var userClaims = await _managerUser.GetClaimsAsync(user);
                authClaims.AddRange(userClaims);

                foreach (var userRole in userRoles)
                {
                    var role = await _managerRole.FindByNameAsync(userRole);
                    if (role != null)
                    {
                        authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                        var roleClaims = await _managerRole.GetClaimsAsync(role);
                        foreach (var roleClaim in roleClaims)
                        {
                            authClaims.Add(roleClaim);
                        }
                    }
                }

                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration[AccountConstant.JWT_KEY]));

                var securityToken = new JwtSecurityToken(
                    issuer: _configuration[AccountConstant.JWT_VALIDISSUER],
                    audience: _configuration[AccountConstant.JWT_VALIDAUDIENCE],
                    claims: authClaims,
                    expires: DateTime.Now.AddHours(12),
                    signingCredentials: new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256));

                return new JwtSecurityTokenHandler().WriteToken(securityToken);
            }

            return null;
        }

        public async Task<bool> ChnagePasswordAsync(string userId, string newPassword)
        {
            if (userId != null && newPassword != null)
            {
                var user = await _managerUser.FindByIdAsync(userId);
                if (user != null)
                {
                    var token = await _managerUser.GeneratePasswordResetTokenAsync(user);
                    var resetResult = await _managerUser.ResetPasswordAsync(user, token, newPassword);
                    if (resetResult.Succeeded)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public async Task<(string, UsrUser)> GenerateTokenAsync(string email)
        {
            try
            {
                var user = await _managerUser.FindByEmailAsync(email);
                if (user == null)
                {
                    return (string.Empty, null);
                }
                var token = await _managerUser.GeneratePasswordResetTokenAsync(user);
                return (token, user);
            }
            catch
            {
                return (string.Empty, null);
            }
        }
        public async Task<IdentityResult> ResetPasswordAsync(ResetPasswordDTO dtoResetPassword)
        {
            try
            {
                var user = await _managerUser.FindByEmailAsync(dtoResetPassword.Email);
                if (user == null)
                {
                    return IdentityResult.Failed(new IdentityError { Description = AccountConstant.ERROR_USER_NOT_FOUND });
                }
                var checkPasswordResult = await _managerUser.CheckPasswordAsync(user, dtoResetPassword.Password);
                if (checkPasswordResult)
                {
                    return IdentityResult.Failed(new IdentityError { Description = AccountConstant.ERROR_OLD_NEW_PASSWORD_SAME });
                }
                IdentityResult resetResult = await _managerUser.ResetPasswordAsync(user, dtoResetPassword.Token, dtoResetPassword.Password);
                if (resetResult.Succeeded)
                {
                    await _managerSignIn.SignOutAsync();
                }

                return resetResult;
            }
            catch (Exception ex)
            {
                return IdentityResult.Failed(new IdentityError { Description = ex.Message });
            }
        }
        public async Task<bool> LogoutAsync()
        {
            try
            {
                await _managerSignIn.SignOutAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        private async Task SendAccountCreationEmail(string receiver, string password)
        {
            string receiverMail = receiver;
            string subject = AccountConstant.ACCOUNT_CREATION_EMAIL_SUBJECT;
            string body = string.Format(AccountConstant.ACCOUNT_CREATION_SUCCESSFUL_EMAIL_BODY_FORMAT, receiver, password); ;

            await SendMail(receiverMail, subject, body);
        }

        public async Task SendPasswordResetEmail(string receiver, string link)
        {
            string receiverMail = receiver;
            string subject = AccountConstant.RESET_PASSWORD_EMAIL_SUBJECT;
            string body = string.Format(AccountConstant.EMAIL_BODY_FORMAT, link);

            await SendMail(receiverMail, subject, body);
        }


        public async Task SendMail(string reciverMail, string subject, string body)
        {
            string fromMail = _configuration[AccountConstant.EMAILCREDENTIALS_MAILID];
            string fromPassword = _configuration[AccountConstant.EMAILCREDENTIALS_PASSWORD];

            MailMessage message = new MailMessage();
            message.From = new MailAddress(fromMail);
            message.Subject = subject;
            message.To.Add(new MailAddress(reciverMail));
            message.Body = body;
            message.IsBodyHtml = true;

            var smtpClient = new SmtpClient(AccountConstant.SMTP_GMAIL)
            {
                Port = 587,
                Credentials = new NetworkCredential(fromMail, fromPassword),
                EnableSsl = true
            };
            smtpClient.Send(message);

        }
    }
}
