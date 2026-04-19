using System.ComponentModel.DataAnnotations;

namespace FluentEmailSample.API.Models
{
    public class EmailDTO
    {
        public class CreateEmailRequest
        {
            [Required]
            [EmailAddress]
            public string To { get; set; } = string.Empty;

            [Required]
            public string Subject { get; set; } = string.Empty;

            [Required]
            public string Body { get; set; } = string.Empty;
        }
    }
}
