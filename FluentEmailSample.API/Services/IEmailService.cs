namespace FluentEmailSample.API.Services
{
    public interface IEmailService
    {
        Task<bool> SendAsync(string to, string subject, string body);
        
    }
}
