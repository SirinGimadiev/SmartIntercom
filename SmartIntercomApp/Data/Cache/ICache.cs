using Ru.Tattelecom.SmartIntercom.Data.Model;

namespace Ru.Tattelecom.SmartIntercom.Data.Cache
{
    public interface ICache
    {
        void Clear();

        User GetCurrentUser();

        void SetCurrentUser(User item);
    }
}