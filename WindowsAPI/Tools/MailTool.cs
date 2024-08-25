using System.Net.Mail;

namespace WindowsAPI.Tools
{
    public class MailTool
    {
        static string iniPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Config.ini");
        IniFiles ini = new IniFiles(iniPath);

        public MailTool()
        {
            ini.FindAndCreate(iniPath);
            GetIniFileItems();
        }

        string? SMTP_SERVER, SMTP_PORT, SMTP_USERNAME, SMTP_PASSWORD, FROM_ADDRESS, TO_ADDRESS;

        private void GetIniFileItems()
        {
            SMTP_SERVER = ini.IniReadValue("Mail", "SMTP_SERVER");
            SMTP_PORT = ini.IniReadValue("Mail", "SMTP_PORT");
            SMTP_USERNAME = ini.IniReadValue("Mail", "SMTP_USERNAME");
            SMTP_PASSWORD = ini.IniReadValue("Mail", "SMTP_PASSWORD");
            FROM_ADDRESS = ini.IniReadValue("Mail", "FROM_ADDRESS");
            TO_ADDRESS = ini.IniReadValue("Mail", "TO_ADDRESS");
        }

        /// <summary>
        /// 发送邮件通知
        /// </summary>
        /// <param name="toAddress">邮箱</param>
        /// <param name="subject">主题</param>
        /// <param name="body">内容</param>
        public void SendEmailNotification(string subject, string body)
        {
            if (!string.IsNullOrEmpty(SMTP_SERVER) && 
                !string.IsNullOrEmpty(SMTP_PORT) &&
                !string.IsNullOrEmpty(SMTP_USERNAME) &&
                !string.IsNullOrEmpty(SMTP_PASSWORD) &&
                !string.IsNullOrEmpty(FROM_ADDRESS) &&
                !string.IsNullOrEmpty(TO_ADDRESS))
            {
                try
                {
                    MailMessage mail = new MailMessage();
                    SmtpClient SmtpServer = new SmtpClient(SMTP_SERVER);

                    mail.From = new MailAddress(FROM_ADDRESS);
                    mail.To.Add(TO_ADDRESS);
                    mail.Subject = subject;
                    mail.Body = body;

                    SmtpServer.Port = int.Parse(SMTP_PORT); // 或其他端口号
                    SmtpServer.Credentials = new System.Net.NetworkCredential(SMTP_USERNAME, SMTP_PASSWORD);
                    SmtpServer.EnableSsl = true;

                    SmtpServer.Send(mail);
                } catch { }
            }
        }
    }
}
