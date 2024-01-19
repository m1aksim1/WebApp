using System;
using System.Net;
using System.Net.Mail;

public class EmailSender
{
    private string _smtpServer;
    private int _smtpPort;
    private string _smtpUsername;
    private string _smtpPassword;

    public EmailSender(string smtpServer, int smtpPort, string smtpUsername, string smtpPassword)
    {
        _smtpServer = smtpServer;
        _smtpPort = smtpPort;
        _smtpUsername = smtpUsername;
        _smtpPassword = smtpPassword;
    }

    async public void SendEmail(string to, string subject, string body)
    {
        await Task.Run(() =>
        {
            using (MailMessage mailMessage = new MailMessage(_smtpUsername, to, subject, body))
            {
                using (SmtpClient smtpClient = new SmtpClient(_smtpServer, _smtpPort))
                {
                    smtpClient.Credentials = new NetworkCredential(_smtpUsername, _smtpPassword);
                    smtpClient.EnableSsl = true; // Установите в true, если ваш SMTP-сервер требует SSL
                    smtpClient.Send(mailMessage);
                }
            }
        });
    }
}