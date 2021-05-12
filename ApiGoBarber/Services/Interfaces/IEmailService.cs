using System.Threading.Tasks;

namespace ApiGoBarber.Services.Interfaces
{
    public interface IEmailService
    {
        void SendMail(string email, string subject, string body);
    }
}
