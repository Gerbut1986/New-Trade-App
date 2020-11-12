namespace New_Trade_Soft_App
{
    using System;
    using System.Net;
    using System.Net.Mail;
    using System.Threading.Tasks;

    class SMTP
    {
        public static void SendEmailAsync(string email, string username, string msg_contant = "Very slow of a Quote!")
        {
            MailAddress from = new MailAddress(email, "ActiveTrade");
            MailAddress to = new MailAddress(email);
            MailMessage m = new MailMessage(from, to);
            //m.Attachments.Add(new Attachment("D://temlog.txt"));
            m.Subject = msg_contant;
            m.Body = "SMTP Client";
            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            smtp.Credentials = new NetworkCredential("layparadize@gmail.com", "ag4313112");
            smtp.EnableSsl = true;
            smtp.SendMail(m);
        }
    }
}
