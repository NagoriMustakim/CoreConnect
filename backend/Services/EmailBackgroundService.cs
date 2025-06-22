
using LinkwayAPI.Constants.Account;
using LinkwayAPI.Data;
using LinkwayAPI.Enums.Role;
using LinkwayAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Net.Mail;
using System.Net;
using NuGet.Protocol.Plugins;

namespace LinkwayAPI.Services
{
    public class EmailBackgroundService : BackgroundService
    {
        private readonly IConfiguration _configuration;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public EmailBackgroundService(IServiceScopeFactory serviceScopeFactory, IConfiguration configuration)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _configuration = configuration;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {

            while (!stoppingToken.IsCancellationRequested)
            {
                var result = await ActiveInternalPrograms();

                if (result != null)
                {
                    sendMail(result);
                }

                await Task.Delay(86400);
            };

        }


        public async Task<List<(bool, string, DateTime, DateTime)>> ActiveInternalPrograms()
        {
            try
            {
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var context = scope.ServiceProvider.GetService<LinkwayDbContext>();

                    var internalPrograms = await context.MstInternalPrograms.ToListAsync();
                    int iteration = 0;
                    if (internalPrograms.Count > 0)
                    {
                        List<(bool, string, DateTime, DateTime)> activeInternalProgram = new List<(bool, string, DateTime, DateTime)>();

                        foreach (var internalProgram in internalPrograms)
                        {

                            if (internalProgram.InternalProgramReviewCycle == 0)
                            {

                            }
                            else
                            {
                                var todayDate = DateTime.Now;
                                var currentYear = DateTime.Now.Year;
                                var startDateOfYear = new DateTime(currentYear, 1, 1);
                                var dateAfterReviewCycle = startDateOfYear;

                                while (dateAfterReviewCycle.AddMonths(internalProgram.InternalProgramReviewCycle) <= todayDate)
                                {
                                    dateAfterReviewCycle = dateAfterReviewCycle.AddMonths(internalProgram.InternalProgramReviewCycle);
                                }

                                if (todayDate <= dateAfterReviewCycle.AddDays(1))
                                    activeInternalProgram.Add((true, internalProgram.InternalProgramName, dateAfterReviewCycle, dateAfterReviewCycle.AddDays(internalProgram.InternalProgramActiveDays)));
                            }
                        }

                        return activeInternalProgram;

                    }
                }

                return null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async void sendMail(List<(bool, string, DateTime, DateTime)> result)
        {
            try
            {
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var context = scope.ServiceProvider.GetService<UserManager<UsrUser>>();
                    var managers = await context.GetUsersInRoleAsync(nameof(RoleTypes.Manager));

                    foreach (var interalProgram in result)
                    {
                        foreach (var manager in managers)
                        {
                            string receiverMail = manager.Email;
                            string subject = AccountConstant.PROGRAM_ACTIVATE_EMAIL_SUBJECT;
                            string body = string.Format(AccountConstant.PROGRAM_ACTIVATE_SUCCESSFUL_EMAIL_BODY_FORMAT, interalProgram.Item2, interalProgram.Item3, interalProgram.Item4); ;
                            await sendMailAsync(receiverMail, subject, body);
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task sendMailAsync(string reciverMail, string subject, string body)
        {
            try
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
            catch (Exception)
            {
                throw;
            }
        }

    }
}
