namespace New_Trade_Soft_App
{
    using System.Net;
    using System.Net.Mail;

    class SMTP
    {
        public static void SendEmail(string email, string username, string msg_contant = "Very slow of a Quote!", string attach = null)
        {
            MailAddress from = new MailAddress(email, username);
            MailAddress to = new MailAddress(email);
            MailMessage msg = new MailMessage(from, to);
            if (attach != null) 
                msg.Attachments.Add(new Attachment(attach));
            msg.Subject = "Msg from MT5API Client";
            msg.Body = msg_contant;
            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            smtp.Credentials = new NetworkCredential("layparadize@gmail.com", "ag4313112");
            smtp.EnableSsl = true;
            try
            {
                smtp.Send(msg);
            }
            catch { }
        }
    }
}
