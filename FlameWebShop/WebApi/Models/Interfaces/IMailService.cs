using WebApi.Models.Email;

namespace WebApi.Models.Interfaces
{
    public interface IMailService
    {
        Task<bool> SendAsync(MailData mailData, CancellationToken ct = default);
    }
}
