using FluentEmailSample.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static FluentEmailSample.API.Models.EmailDTO;

namespace FluentEmailSample.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly IEmailService _emailService;

        public EmailController(IEmailService emailService)
        {
            _emailService = emailService;
        }

        [HttpPost]
        public async Task<IActionResult> Send([FromBody] CreateEmailRequest request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await _emailService.SendAsync(request.To,
                request.Subject,
                request.Body);

            if (!result) return StatusCode(500, "Failed to send email!");

            return Ok(result);
        }

    }
}
