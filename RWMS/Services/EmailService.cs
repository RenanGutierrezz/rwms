using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using RWMS.Services.Interfaces;

namespace RWMS.Services;

public class EmailService : IEmailService
{
    private readonly IConfiguration _configuration;
    private readonly ILogger<EmailService> _logger;

    public EmailService(IConfiguration configuration, ILogger<EmailService> logger)
    {
        _configuration = configuration;
        _logger = logger;
    }

    public async Task SendEmailAsync(string toEmail, string toName, string subject, string htmlBody)
    {
        var smtpSettings = _configuration.GetSection("Smtp");

        var message = new MimeMessage();
        message.From.Add(new MailboxAddress(
            smtpSettings["SenderName"] ?? "RWMS",
            smtpSettings["SenderEmail"] ?? string.Empty));
        message.To.Add(new MailboxAddress(toName, toEmail));
        message.Subject = subject;

        var bodyBuilder = new BodyBuilder { HtmlBody = htmlBody };
        message.Body = bodyBuilder.ToMessageBody();

        using var client = new SmtpClient();

        await client.ConnectAsync(
            smtpSettings["Host"],
            int.Parse(smtpSettings["Port"] ?? "587"),
            SecureSocketOptions.StartTls);

        await client.AuthenticateAsync(
            smtpSettings["Username"],
            smtpSettings["Password"]);

        await client.SendAsync(message);
        await client.DisconnectAsync(true);

        _logger.LogInformation("Email sent to {Email} — Subject: {Subject}", toEmail, subject);
    }

    public async Task SendOrderConfirmationAsync(string toEmail, string toName, int orderId)
    {
        var subject = $"Order #{orderId} Received — RWMS";
        var body = $"""
            <p>Hi {toName},</p>
            <p>Your order <strong>#{orderId}</strong> has been received and is currently <strong>pending review</strong>.</p>
            <p>You will be notified once the status is updated.</p>
            <br/>
            <p>Restaurant Wholesale Management System</p>
            """;

        await SendEmailAsync(toEmail, toName, subject, body);
    }

    public async Task SendOrderStatusUpdateAsync(string toEmail, string toName, int orderId, string newStatus)
    {
        var subject = $"Order #{orderId} Status Updated — RWMS";
        var body = $"""
            <p>Hi {toName},</p>
            <p>The status of your order <strong>#{orderId}</strong> has been updated to: <strong>{newStatus}</strong>.</p>
            <p>Log in to your account to view full order details.</p>
            <br/>
            <p>Restaurant Wholesale Management System</p>
            """;

        await SendEmailAsync(toEmail, toName, subject, body);
    }
}
