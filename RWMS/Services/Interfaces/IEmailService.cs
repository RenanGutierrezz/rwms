namespace RWMS.Services.Interfaces;

public interface IEmailService
{
    Task SendEmailAsync(string toEmail, string toName, string subject, string htmlBody);
    Task SendOrderConfirmationAsync(string toEmail, string toName, int orderId);
    Task SendOrderStatusUpdateAsync(string toEmail, string toName, int orderId, string newStatus);
}
