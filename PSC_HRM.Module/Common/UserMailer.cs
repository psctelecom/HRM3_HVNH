using System;
using System.Configuration;
using System.IO;
using System.Net.Mail;
using System.Web;
using System.Net.Mime;
using System.Data;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
//
namespace PSC_HRM.Module
{
    public class UserMailer
    {
        public static bool isSSL = true;
        public static bool isAuthen = true;

        public bool SendMail(string fromMail, string displayname, string passMail, int port, string smtpServer, string toMail, string subject, string body, string mailReplyTo)
        {
            bool sended = true;
            if (toMail == null || !CheckEmail(toMail) || !CheckEmailExists(toMail))
            {
                sended = false;
            }
            else
            {
                    MailMessage mail = new MailMessage();
                    mail.From = new MailAddress(mailReplyTo, displayname, System.Text.Encoding.UTF8);
                    mail.To.Add(new MailAddress(toMail));
                    mail.Subject = subject;
                    mail.Body = body;
                    mail.ReplyTo = new MailAddress(mailReplyTo);
                    mail.IsBodyHtml = true;

                    SmtpClient client = new SmtpClient();
                    client.Credentials = new System.Net.NetworkCredential(fromMail, passMail);
                    client.EnableSsl = isSSL;
                    client.Timeout = 100000;
                    client.Port = port;
                    client.Host = smtpServer;
                    client.DeliveryMethod = SmtpDeliveryMethod.Network;
                    client.Send(mail);
            }
            return sended;
        }

        public bool CheckEmail(string email)
        {
            string strRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                  @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                  @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
            Regex re = new Regex(strRegex);
            if (re.IsMatch(email))
                return (true);
            else
                return (false);
        }

        private bool CheckEmailExists(string email)
        {
            bool ok = true;
            //
            TcpClient tClient = new TcpClient("gmail-smtp-in.l.google.com", 25);
            string CRLF = "\r\n";
            byte[] dataBuffer;
            string ResponseString;
            NetworkStream netStream = tClient.GetStream();
            StreamReader reader = new StreamReader(netStream);
            ResponseString = reader.ReadLine();
            /* Perform HELO to SMTP Server and get Response */
            dataBuffer = BytesFromString("HELO KirtanHere" + CRLF);
            netStream.Write(dataBuffer, 0, dataBuffer.Length);
            ResponseString = reader.ReadLine();
            dataBuffer = BytesFromString("MAIL FROM:<pscerp@gmail.com>" + CRLF);
            netStream.Write(dataBuffer, 0, dataBuffer.Length);
            ResponseString = reader.ReadLine();
            /* Read Response of the RCPT TO Message to know from google if it exist or not */
            dataBuffer = BytesFromString("RCPT TO:<" + email + ">" + CRLF);
            netStream.Write(dataBuffer, 0, dataBuffer.Length);
            ResponseString = reader.ReadLine();
            if (GetResponseCode(ResponseString) == 550)
            {
                ok = false;
            }
            /* QUITE CONNECTION */
            dataBuffer = BytesFromString("QUITE" + CRLF);
            netStream.Write(dataBuffer, 0, dataBuffer.Length);
            tClient.Close();

            return ok;
        }

        private byte[] BytesFromString(string str)
        {
            return Encoding.ASCII.GetBytes(str);
        }
        private int GetResponseCode(string ResponseString)
        {
            return int.Parse(ResponseString.Substring(0, 3));
        }
    }
}
