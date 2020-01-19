using Ru.Tattelecom.SmartIntercom.Data.Model;

namespace Ru.Tattelecom.SmartIntercom.Data.Repository
{
    public interface IUserRepository
    {
        User Login(string login, string password);

        void Logout();
    }
}