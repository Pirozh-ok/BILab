﻿using BILab.Domain.Contracts.Services;
using BILab.Domain.Settings;
using Microsoft.Extensions.Options;
using MimeKit;
using MailKit.Net.Smtp;

namespace BILab.BusinessLogic.Services {
    public class EmailService : IEmailService {
        private readonly IOptions<SmtpSettings> _smtpSettings;

        public EmailService(
            IOptions<SmtpSettings> smtpSettings) {
            _smtpSettings = smtpSettings;
        }

        public async Task SendEmailAsync(string email, string subject, string message) {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress(_smtpSettings.Value.FromName, _smtpSettings.Value.AdminEmail));
            emailMessage.To.Add(new MailboxAddress("", email));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html) {
                Text = message
            };

            using (var client = new SmtpClient()) {
                await client.ConnectAsync(_smtpSettings.Value.SmtpHost, _smtpSettings.Value.SmtpPort, false);
                await client.AuthenticateAsync(_smtpSettings.Value.AdminEmail, _smtpSettings.Value.AdminPassword);
                await client.SendAsync(emailMessage);
                await client.DisconnectAsync(true);
            }
        }

        public async Task SendEmailResultAuthentificationWithGoogle(string email) {
            //await SendEmailAsync(email, UserResource.SubjectAuthWithGoogle, UserResource.MessageAuthWithGoogle);
        }
    }
}
