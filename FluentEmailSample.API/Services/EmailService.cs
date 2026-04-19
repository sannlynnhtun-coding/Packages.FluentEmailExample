using FluentEmail.Core;
using Microsoft.AspNetCore.Http.HttpResults;

namespace FluentEmailSample.API.Services
{
    public class EmailService : IEmailService
    {
        private readonly IFluentEmail _fluentEmail;

        public EmailService(IFluentEmail fluentEmail)
        {
            _fluentEmail = fluentEmail;
        }

        public async Task<bool> SendAsync(string to, string subject, string body)
        {
            var response = await _fluentEmail
                .To(to)
                .Subject(subject)
                .Body(body)
                .SendAsync();

            return response.Successful;
        }
    }
}
