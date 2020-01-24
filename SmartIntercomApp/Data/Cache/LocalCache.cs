using Ru.Tattelecom.SmartIntercom.Data.Model;

namespace Ru.Tattelecom.SmartIntercom.Data.Cache
{
    public class LocalCache : ICache
    {
        private User _user; //TODO ROOM sqlite


        public void Clear()
        {
            _user = null;
        }

        public User GetCurrentUser()
        {
            return _user;
        }

        public void SetCurrentUser(User item)
        {
            _user = item;
        }
    }
}