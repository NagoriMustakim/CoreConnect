namespace LinkwayAPI.Constants.Account
{
    public class AccountConstant
    {
        public const string API_ACCOUNTS = "api/accounts";
        public const string SIGNUP = "signup";
        public const string LOGIN = "login";
        public const string JWT_KEY = "JWT:Key";
        public const string JWT_VALIDISSUER = "JWT:ValidIssuer";
        public const string JWT_VALIDAUDIENCE = "JWT:ValidAudience";
        public const string EMAILCREDENTIALS_MAILID = "EmailCredentials:MailId";
        public const string EMAILCREDENTIALS_PASSWORD = "EmailCredentials:Password";
        public const string SMTP_GMAIL = "smtp.gmail.com";
        public const string LOGOUT = "logout";
        public const string CHANGE_PASSWORD = "change-password/{userId}";
        public const string FORGOT_PASSWORD = "forgot-password";
        public const string RESET_PASSWORD = "reset-password";
        public const string RESET_PASSWORD_URL_FORMAT = "http://localhost:4200/auth/reset-password?token={0}&email={1}";
        public const string RESET_PASSWORD_EMAIL_SUBJECT = "Reset Your Password for Linkway";
        public const string EMAIL_BODY_FORMAT = @"
                            <html>
                            <head>
                                <title>Reset Your Password</title>
                            </head>
                            <body style='font-family: Arial, sans-serif; margin: 0; padding: 0; background-color: #f4f4f4;'>
                                <div style='max-width: 600px; margin: 0 auto; padding: 20px; background-color: #ffffff; border-radius: 5px;'>
        
                                    <div style='padding: 0 20px;'>
                                        <h2 style='color: #333333; margin-bottom: 20px;'>Reset Your Password</h2>
                                        <p style='color: #666666; margin-bottom: 20px;'>
                                            Hello User,
                                            <br><br>
                                            We received a request to reset your password. If you did not make this request, please ignore this email. Otherwise, you can reset your password using the link below.
                                        </p>
                                        <p style='color: #666666; margin-top: 20px;'>
                                            <a href='{0}' style='color: #007bff; text-decoration: none;'>Reset Password</a>
                                        </p>
                                        <p style='color: #666666; margin-top: 20px;'>
                                            If you have any questions or need further assistance, please don't hesitate to contact our support team.
                                        </p>
                                        <p style='color: #666666; margin-top: 20px;'>
                                            Best regards,
                                            <br>
                                            Gateway Group of Companies
                                        </p>
                                    </div>
                                </div>
                            </body>
                            </html>";
        public const string ACCOUNT_CREATION_EMAIL_SUBJECT = "Welcome to linkway!";
        public const string ACCOUNT_CREATION_SUCCESSFUL_EMAIL_BODY_FORMAT = @"
<html>
<head>
    <title>Account Creation Successful</title>
</head>
<body style='font-family: Arial, sans-serif; margin: 0; padding: 0; background-color: #f4f4f4;'>
    <div style='max-width: 600px; margin: 0 auto; padding: 20px; background-color: #ffffff; border-radius: 5px;'>
        
        <div style='padding: 0 20px;'>
            <h2 style='color: #333333; margin-bottom: 20px;'>Welcome to Linkway!</h2>
            <p style='color: #666666; margin-bottom: 20px;'>
                Hello User,
                <br><br>
                We're excited to have you on board! Your account has been successfully created.
            </p>
            <p style='color: #666666; margin-top: 20px;'>
                Your email is: <strong>{0}</strong>
                <br>
                Your password is: <strong>{1}</strong>
            </p>
            <p style='color: #666666; margin-top: 20px;'>
                Please make sure to keep your password secure and do not share it with anyone.
            </p>
            <p style='color: #666666; margin-top: 20px;'>
                If you have any questions or need further assistance, please don't hesitate to contact our support team.
            </p>
            <p style='color: #666666; margin-top: 20px;'>
                Best regards,
                <br>
                Gateway Group of Companies
            </p>
        </div>
    </div>
</body>
</html>";
        public const string ERROR_USER_NOT_FOUND = "User Not Found.";
        public const string ERROR_OLD_NEW_PASSWORD_SAME = "New password must be different from the current password.";


        public const string PROGRAM_ACTIVATE_EMAIL_SUBJECT = "Program Activated";
        public const string PROGRAM_ACTIVATE_SUCCESSFUL_EMAIL_BODY_FORMAT = @"
        <html>
        <head>
        <title>{0} Program Activated</title>
        </head>
        <body style='font-family: Arial, sans-serif; margin: 0; padding: 0; background-color: #f4f4f4;'>
        <div style='max-width: 600px; margin: 0 auto; padding: 20px; background-color: #ffffff; border-radius: 5px;'>
        
        <div style='padding: 0 20px;'>
            <h2 style='color: #333333; margin-bottom: 20px;'>Welcome to Linkway!</h2>
            <p style='color: #666666; margin-bottom: 20px;'>
                Hello Manangers,
                <br><br>
                We're excited to remind you that <strong>{0}</strong> is actived.
            </p>
            <p style='color: #666666; margin-top: 20px;'>
                Start Date is: <strong>{1}</strong>
                <br>
                End Date is: <strong>{2}</strong>
            </p>
            <p style='color: #666666; margin-top: 20px;'>
                Start Nominating candidates for <strong>{0}</strong> program.
            </p>
            <p style='color: #666666; margin-top: 20px;'>
                If you have any questions or need further assistance, please don't hesitate to contact our support team.
            </p>
            <p style='color: #666666; margin-top: 20px;'>
                Best regards,
                <br>
                Gateway Group of Companies
            </p>
        </div>
        </div>
        </body>
        </html>";
    }
}


