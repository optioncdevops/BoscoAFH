using BoscoAFH.CommonService.MailService;
using System.Net;
using System.Net.Mail;

namespace BoscoAFH.CommonService
{
    public interface ISMTPMailService
    {
        Task<bool> SendMailAsync(string mailSubject, string mailContent, string toAddress, string cCAddress = "", string attachmentFile = "", string bCCAddress = "");
    }

    public class SMTPMailService: ISMTPMailService
    {
        public SMTPMailService()
        {
        }

        private static ConfSettings LoadData()
        {
            var obj = new ConfSettingsService();
            return obj.Settings;
        }

        public static string FormatMailContent(string mailContent)
        {
            var settings = LoadData();
            var applicationLogo = settings?.SMTPMailConfig?.LogoUrl;

            return $@"
            <div style=""color: #000; font-family: Verdana, Arial, Helvetica, sans-serif; font-size: 13px; text-rendering: optimizelegibility; line-height: 1.629; background: linear-gradient(90deg, rgb(180, 208, 224) 0%, rgb(221, 238, 255) 100%); box-shadow: inset 0 0 20px #82b3cf;padding: 1rem 2rem 2rem;"">
                <div style=""text-align:center""><img src=""{applicationLogo}"" alt=""Logo"" style=""max-width:200px;margin-bottom:1rem"" /></div>
                <div style=""background-color:#fff;padding: 2rem;"">{mailContent}</p>
                <p style=""margin-top:2rem;border-top:solid 1px #808080"">
                    <span style=""text-transform: uppercase; font-family: Verdana, Arial, Helvetica, sans-serif; color: black; font-size: 10pt; "">DISCLAIMER</span><br />
                    <span style=""color: black; line-height: 12.26px; font-family: Verdana, Arial, Helvetica, sans-serif; font-size: 8pt"">
                        Please do not respond directly to this email. The originating email, {settings?.SMTPMailConfig?.ContactUsMailId}, is not monitored.
                    </span>
                </p>
                </div>
            </div>";
        }

        public static string FormatMailContent(string template, Dictionary<string, string> placeholders)
        {
            if (string.IsNullOrEmpty(template))
            {
                throw new ArgumentException("Template cannot be null or empty.");
            }
            foreach (var placeholder in placeholders)
            {
                template = template.Replace($"[{placeholder.Key}]", placeholder.Value ?? string.Empty);
            }
            return template;
        }

        public async Task<bool> SendMailAsync(string mailSubject, string mailContent, string toAddress, string cCAddress = "", string attachmentFile = "", string bCCAddress = "")
        {
            var settings = LoadData();

            var retryCount = 0;
            var isSuccess = false;
            var sendMailFlag = settings.SMTPMailConfig?.SendMailFlag ?? string.Empty;

            if (sendMailFlag.Equals("1", StringComparison.Ordinal))
            {
                do
                {
                    var displayName = settings?.SMTPMailConfig?.DisplayName ?? string.Empty;
                    var username = settings?.SMTPMailConfig?.MUserName ?? string.Empty;
                    var password = settings?.SMTPMailConfig?.MPassword ?? string.Empty;
                    var smtpServer = settings?.SMTPMailConfig?.SMTPServer ?? string.Empty;
                    var smtpPort = Convert.ToInt32(settings?.SMTPMailConfig?.SMTPPort ?? "0");
                    var enableSsl = settings?.SMTPMailConfig?.IsSSLEnabled == "1";

                    try
                    {
                        using var mail = new MailMessage { From = new MailAddress(username, displayName), Subject = mailSubject, BodyEncoding = System.Text.Encoding.UTF8, IsBodyHtml = true, Body = FormatMailContent(mailContent) };
                        if (!string.IsNullOrEmpty(toAddress))
                        {
                            foreach (var address in toAddress.Split(';'))
                            {
                                mail.To.Add(address.Trim());
                            }
                        }

                        if (!string.IsNullOrEmpty(cCAddress))
                        {
                            foreach (var address in cCAddress.Split(';'))
                            {
                                mail.CC.Add(address.Trim());
                            }
                        }

                        if (!string.IsNullOrEmpty(bCCAddress))
                        {
                            foreach (var address in bCCAddress.Split(';'))
                            {
                                mail.Bcc.Add(address.Trim());
                            }
                        }

                        if (!string.IsNullOrEmpty(attachmentFile))
                        {
                            foreach (var file in attachmentFile.Split(','))
                            {
                                try
                                {
                                    mail.Attachments.Add(new Attachment(file.Trim()));
                                }
                                catch (Exception)
                                {
                                    // Log the attachment failure
                                }
                            }
                        }

                        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                        mail.BodyEncoding = System.Text.Encoding.GetEncoding("utf-8");
                        mail.IsBodyHtml = true;

                        //using var smtpClient = new SmtpClient(smtpServer, smtpPort)
                        //{
                        //    DeliveryMethod = SmtpDeliveryMethod.Network,
                        //    Credentials = new NetworkCredential(username, password),
                        //    EnableSsl = enableSsl
                        //};

                        SmtpClient client = new SmtpClient(smtpServer, smtpPort);
                        client.Credentials = new NetworkCredential(username, password);
                        client.EnableSsl = enableSsl; // This is correct; it signals STARTTLS, not implicit SSL.
                        client.DeliveryMethod = SmtpDeliveryMethod.Network;
                        client.UseDefaultCredentials = false;
                        client.TargetName = "STARTTLS/smtp.office365.com"; // Optional, but helps ensure STARTTLS is used.

                        await client.SendMailAsync(mail);
                        isSuccess = true;
                    }
                    catch (Exception)
                    {
                        retryCount++;
                        if (retryCount >= 2)
                        {
                            break;
                        }
                    }
                } while (!isSuccess && retryCount < 2);
            }

            return isSuccess;
        }
    }
}
