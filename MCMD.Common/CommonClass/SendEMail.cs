using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Configuration;
using System.Net.Mail;


namespace MCMD.Common.CommonClass
{
    public class SendEMail
    {


        //send mail
        public void Send_EMail(string emailid, string subject, string body)
        {

            WriteLog("Send Email Start..");
            SmtpClient client = new SmtpClient();
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.EnableSsl = false;// To encrept the connection
            client.Host = ConfigurationManager.AppSettings["SMTPHost"];
            client.Port = Convert.ToInt32(ConfigurationManager.AppSettings["SMPTPort"]);

            System.Net.NetworkCredential credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["SMPTUserName"], ConfigurationManager.AppSettings["SMPTPassword"]);
            client.UseDefaultCredentials = false;
            client.Credentials = credentials;

            MailMessage msg = new MailMessage();
            msg.From = new MailAddress(ConfigurationManager.AppSettings["SMPTEmail"]);
            msg.To.Add(new MailAddress(emailid));

            msg.Subject = subject;
            msg.IsBodyHtml = true;
            msg.Body = body;

            //Add this line to bypass the certificate validation
            System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate(object s,
                    System.Security.Cryptography.X509Certificates.X509Certificate certificate,
                    System.Security.Cryptography.X509Certificates.X509Chain chain,
                    System.Net.Security.SslPolicyErrors sslPolicyErrors)
            {
                return true;
            };


            WriteLog("Send Email detail.." + client.Host + " " + client.Port + "  Email :" + emailid);
             client.Send(msg);
            WriteLog("Send Email Success");
        }


        public void WriteLog(string log)
        {

            var fs = new FileStream(ConfigurationManager.AppSettings["LogPath"].ToString() + "\\ODSLogs_" + DateTime.Now.ToString("yyyyMMdd") + ".txt", FileMode.OpenOrCreate, FileAccess.Write);
            var mStreamWriter = new StreamWriter(fs);
            mStreamWriter.BaseStream.Seek(0, SeekOrigin.End);
            mStreamWriter.WriteLine(log + " " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString() + "\n");
            mStreamWriter.Flush();
            mStreamWriter.Close();
        }

    }
}
