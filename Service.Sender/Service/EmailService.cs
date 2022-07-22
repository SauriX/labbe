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
using System.Threading.Tasks;

namespace Service.Sender.Service
{
    public class EmailService : IEmailService
    {
        private readonly IKeySettings _keySettings;
        private readonly IUrlSettings _urlSettings;
        private readonly IUrlLocalSettings _urlLocalSettings;
        private readonly IEmailConfigurationService _configurationService;

        public EmailService(IKeySettings keySettings, IUrlSettings urlSettings, IUrlLocalSettings urlLocalSettings, IEmailConfigurationService configurationService)
        {
            _keySettings = keySettings;
            _urlSettings = urlSettings;
            _urlLocalSettings = urlLocalSettings;
            _configurationService = configurationService;
        }

        public async Task Send(string to, string subject, string title, string content)
        {
            var conf = await _configurationService.GetEmail();

            if (conf == null || string.IsNullOrWhiteSpace(conf.Smtp) || string.IsNullOrWhiteSpace(conf.Correo) || (conf.RequiereContraseña && string.IsNullOrWhiteSpace(conf.Contraseña)))
                throw new Exception(Responses.EmailConfigurationIncomplete);

            if (string.IsNullOrWhiteSpace(to) || !IsValid(to))
                throw new CustomException(HttpStatusCode.FailedDependency, Responses.EmailFordwardError);

            var path = Path.Combine(_urlLocalSettings.Layout, Assets.HtmlGeneral);
            var html = "";
            using (var ot = File.OpenText(path))
            {
                html += ot.ReadToEnd();
                html = html.Replace("[Logo]", Path.Combine(_urlSettings.Images, Assets.Logo));
                html = html.Replace("[Titulo]", title);
                html = html.Replace("[Mensaje]", content.Replace("\n", "<br />"));
            }

            using MailMessage emailMessage = new();
            emailMessage.From = new MailAddress(conf.Correo, conf.Remitente);
            emailMessage.To.Add(new MailAddress(to, to));
            emailMessage.Subject = subject;
            emailMessage.Body = html;
            emailMessage.IsBodyHtml = true;
            emailMessage.Priority = MailPriority.Normal;
            using SmtpClient MailClient = new(conf.Smtp);
            if (conf.RequiereContraseña)
            {
                MailClient.Credentials = new NetworkCredential(conf.Correo, Crypto.DecryptString(conf.Contraseña, _keySettings.MailPassword));
            }
            MailClient.Send(emailMessage);
        }

        public async Task Send(IEnumerable<string> to, string subject, string title, string content)
        {
            var conf = await _configurationService.GetEmail();

            if (conf == null || string.IsNullOrWhiteSpace(conf.Smtp) || string.IsNullOrWhiteSpace(conf.Correo) || (conf.RequiereContraseña && string.IsNullOrWhiteSpace(conf.Contraseña)))
                throw new Exception(Responses.EmailConfigurationIncomplete);

            if (to == null || !to.Any() || to.Any(x => !IsValid(x)))
                throw new CustomException(HttpStatusCode.FailedDependency, Responses.EmailFordwardError);

            var path = Path.Combine(_urlLocalSettings.Layout, Assets.HtmlGeneral);
            var html = "";
            using (var ot = File.OpenText(path))
            {
                html += ot.ReadToEnd();
                html = html.Replace("[Logo]", Path.Combine(_urlSettings.Images, Assets.Logo));
                html = html.Replace("[Titulo]", title);
                html = html.Replace("[Mensaje]", content.Replace("\n", "<br />"));
            }

            using MailMessage emailMessage = new();
            emailMessage.From = new MailAddress(conf.Correo, conf.Remitente);

            foreach (var user in to.Where(x => !string.IsNullOrWhiteSpace(x)))
            {
                emailMessage.To.Add(new MailAddress(user, user));
            }

            emailMessage.Subject = subject;
            emailMessage.Body = html;
            emailMessage.IsBodyHtml = true;
            emailMessage.Priority = MailPriority.Normal;
            using SmtpClient MailClient = new(conf.Smtp);
            if (conf.RequiereContraseña)
            {
                MailClient.Credentials = new NetworkCredential(conf.Correo, Crypto.DecryptString(conf.Contraseña, _keySettings.MailPassword));
            }
            MailClient.Send(emailMessage);
        }

        private bool IsValid(string emailaddress)
        {
            try
            {
                MailAddress m = new MailAddress(emailaddress);

                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }
    }
}
