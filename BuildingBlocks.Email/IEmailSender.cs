using System.Threading.Tasks;

namespace BuildingBlocks.Email
{
    public interface IEmailSender
    {
        void Send(EmailMessage message);
        Task SendAsync(EmailMessage message);
    }
}
