using Service.Sender.Dictionary;
using Service.Sender.Service.IService;
using Service.Sender.Settings.Interfaces;
using Shared.Dictionary;
using Shared.Error;
using Shared.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Service.Sender.Service
{
    public class EmailService : IEmailService
    {
        private readonly IKeySettings _keySettings;
        private readonly IUrlLocalSettings _urlLocalSettings;
        private readonly IEmailTemplateSettings _emailTemplateSettings;
        private readonly IEmailConfigurationService _configurationService;

        public EmailService(
            IKeySettings keySettings,
            IUrlLocalSettings urlLocalSettings,
            IEmailTemplateSettings emailTemplateSettings,
            IEmailConfigurationService configurationService)
        {
            _keySettings = keySettings;
            _urlLocalSettings = urlLocalSettings;
            _emailTemplateSettings = emailTemplateSettings;
            _configurationService = configurationService;
        }

        public async Task Send(string to, string subject, string title, string content)
        {
            var conf = await _configurationService.GetEmail();

            if (conf == null || string.IsNullOrWhiteSpace(conf.Smtp) || string.IsNullOrWhiteSpace(conf.Correo) || (conf.RequiereContraseña && string.IsNullOrWhiteSpace(conf.Contraseña)))
                throw new Exception(Responses.EmailConfigurationIncomplete);

            if (string.IsNullOrWhiteSpace(to) || !IsValidEmail(to))
                throw new Exception(Responses.EmailFordwardError);

            var path = Path.Combine(_urlLocalSettings.Layout, Assets.HtmlGeneral);
            var imgArr = File.ReadAllBytes(Path.Combine(_urlLocalSettings.Images, Assets.Logo));
            var img64 = Convert.ToBase64String(imgArr);

            var html = new StringBuilder();
            using (var sr = File.OpenText(path))
            {
                html.Append(sr.ReadToEnd());
                html.Replace("{{PrimaryColor}}", _emailTemplateSettings.PrimaryColor);
                html.Replace("{{BackgroundColor}}", _emailTemplateSettings.BackgroundColor);
                html.Replace("{{Logo}}", $"data:image/png;base64, {img64}");
                html.Replace("{{Titulo}}", title);
                html.Replace("{{Mensaje}}", content.Replace("\n", "<br />"));
            }

            using MailMessage emailMessage = new();
            emailMessage.From = new MailAddress(conf.Correo, conf.Remitente);
            emailMessage.To.Add(new MailAddress(to, to));
            emailMessage.Subject = subject;
            emailMessage.Body = html.ToString();
            emailMessage.IsBodyHtml = true;
            emailMessage.Priority = MailPriority.Normal;
            //Attachment = new Attachment(,);
            using SmtpClient MailClient = new(conf.Smtp, 587);
            MailClient.EnableSsl = true;
            if (conf.RequiereContraseña)
            {
                MailClient.Credentials = new NetworkCredential(conf.Correo, Crypto.DecryptString(conf.Contraseña, _keySettings.MailKey));
            }
            MailClient.Send(emailMessage);
        }

        public async Task Send(IEnumerable<string> to, string subject, string title, string content)
        {
            var conf = await _configurationService.GetEmail();

            if (conf == null || string.IsNullOrWhiteSpace(conf.Smtp) || string.IsNullOrWhiteSpace(conf.Correo) || (conf.RequiereContraseña && string.IsNullOrWhiteSpace(conf.Contraseña)))
                throw new Exception(Responses.EmailConfigurationIncomplete);

            if (to == null || !to.Any() || to.Any(x => !IsValidEmail(x)))
                throw new Exception(Responses.EmailFordwardError);

            var path = Path.Combine(_urlLocalSettings.Layout, Assets.HtmlGeneral);
            var html = new StringBuilder();
            using (var sr = File.OpenText(path))
            {
                html.Append(sr.ReadToEnd());
                html.Replace("{{PrimaryColor}}", _emailTemplateSettings.PrimaryColor);
                html.Replace("{{BackgroundColor}}", _emailTemplateSettings.BackgroundColor);
                html.Replace("{{Logo}}", Path.Combine(_urlLocalSettings.Images, Assets.Logo));
                html.Replace("{{Titulo}}", title);
                html.Replace("{{Mensaje}}", content.Replace("\n", "<br />"));
            }

            using MailMessage emailMessage = new();
            emailMessage.From = new MailAddress(conf.Correo, conf.Remitente);

            foreach (var user in to.Where(x => !string.IsNullOrWhiteSpace(x)))
            {
                emailMessage.To.Add(new MailAddress(user, user));
            }

            emailMessage.Subject = subject;
            emailMessage.Body = html.ToString();
            emailMessage.IsBodyHtml = true;
            emailMessage.Priority = MailPriority.Normal;
            using SmtpClient MailClient = new(conf.Smtp, 587);
            MailClient.EnableSsl = true;
            if (conf.RequiereContraseña)
            {
                MailClient.Credentials = new NetworkCredential(conf.Correo, Crypto.DecryptString(conf.Contraseña, _keySettings.MailKey));
            }
            MailClient.Send(emailMessage);
        }

        private static bool IsValidEmail(string emailaddress)
        {
            try
            {
                MailAddress m = new(emailaddress);

                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }
    }
}
