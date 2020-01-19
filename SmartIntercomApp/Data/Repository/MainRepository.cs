using System.Collections.Generic;
using Ru.Tattelecom.SmartIntercom.Data.Cache;
using Ru.Tattelecom.SmartIntercom.Data.Model;

namespace Ru.Tattelecom.SmartIntercom.Data.Repository
{
    public class MainRepository : IUserRepository, IIntercomReposistory
    {
        private ICache _cache;

        public MainRepository(ICache cache)
        {
            _cache = cache;
        }

        public IEnumerable<Intercom> GetIntercoms(int userId)
        {
            return new[]
            {
                new Intercom
                {
                    StreamLogin = "test_domofon",
                    StreamPassword = "12345678",
                    VideoStream =
                        "89.232.115.66:8000/live/media/AXXON-CLIENTS/DeviceIpint.319/SourceEndpoint.video:0:0?w=1920&h=0&format=mp4",
                    Width = 1920,
                    Height = 0
                    //"http://test_domofon:12345678@89.232.115.66:8000/live/media/AXXON-CLIENTS/DeviceIpint.319/SourceEndpoint.video:0:0?w=1920&h=0&format=mp4"
                }
            };
        }

        public User Login(string login, string password)
        {
            if (login.Equals("test@test.ru") && password.Equals("123456"))
                return new User();
            return null;
        }

        public void Logout()
        {
            //TODO clear cache
        }
    }
}