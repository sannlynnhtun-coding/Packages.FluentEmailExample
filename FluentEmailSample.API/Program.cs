using FluentEmailSample.API.Services;
using Scalar.AspNetCore;
using System.Net;
using System.Net.Mail;
using Serilog;

try
{
    var builder = WebApplication.CreateBuilder(args);

    Log.Logger = new LoggerConfiguration()
       .MinimumLevel.Debug()
       .WriteTo.Console()
       .WriteTo.File("logs/mini_pos_log.txt", rollingInterval: RollingInterval.Hour)
       .CreateLogger();

    //Add Serilog
    builder.Services.AddSerilog();

    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    // FluentEmail Configuration
    var smtpSettings = builder.Configuration.GetSection("SmtpSettings");
    string fromEmail = smtpSettings["SenderEmail"] ?? "no-reply@test.com";
    string fromName = smtpSettings["SenderName"] ?? "Test Email";
    string smtpServer = smtpSettings["Server"] ?? "localhost";
    int smtpPort = int.Parse(smtpSettings["Port"] ?? "587");
    string smtpUser = smtpSettings["Username"] ?? "";
    string smtpPass = smtpSettings["Password"] ?? "";

    builder.Services
        .AddFluentEmail(fromEmail, fromName)
        .AddSmtpSender(new SmtpClient(smtpServer)
        {
            Port = smtpPort,
            Credentials = new NetworkCredential(smtpUser, smtpPass),
            EnableSsl = true
        });

    builder.Services.AddScoped<IEmailService, EmailService>();

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.MapSwagger("/openapi/{documentName}.json");
        app.MapScalarApiReference();
    }

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application start-up failed");
}
finally
{
    Log.CloseAndFlush();
}

