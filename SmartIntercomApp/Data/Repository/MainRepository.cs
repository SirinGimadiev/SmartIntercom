using System.Collections.Generic;
using System.Linq;
using Refit;
using Ru.Tattelecom.SmartIntercom.Data.Cache;
using Ru.Tattelecom.SmartIntercom.Data.Model;
using Ru.Tattelecom.SmartIntercom.Utilits;

namespace Ru.Tattelecom.SmartIntercom.Data.Repository
{
    public class MainRepository : IUserRepository, IIntercomReposistory
    {
        private readonly ICache _cache;

        public MainRepository(ICache cache)
        {
            _cache = cache;
        }

        public IEnumerable<Intercom> GetIntercoms()
        {
            var currentUser = _cache.GetCurrentUser();
            //TEST
            return currentUser.Id switch
            {
                1 => new[]
                {
                    new Intercom
                    {
                        VideoStream = "rtsp://192.168.1.10/live/ch00_0",
                        BaseUri = "",
                        OpenDoorAction = "",
                        Name = "Nateks Домофон Test",
                        Address = "test"
                    }
                },
                2 => new []
                {
                    new Intercom
                    {
                        VideoStream = "rtsp://192.168.1.4/live/ch00_0",
                        BaseUri = "http://192.168.1.4/fcgi/do?",
                        OpenDoorAction = "action=OpenDoor&UserName=admin&Password=admin&DoorNum=1",
                        Name = "Qtech Домофон Test",
                        Address = "test"
                    }
                },
                _ => Enumerable.Empty<Intercom>()
            };
        }

        public void OpenDoor(Intercom intercom)
        {
            var httpClient = RestService.For<IHttpClient>(intercom.BaseUri);
            httpClient.ExecuteAction(intercom.OpenDoorAction);
        }

        public User Login(string login, string password)
        {
            //TEST
            if (!password.Equals("123456"))
                return null;
            var user = login switch
            {
                "nateks@test.ru" => new User { Id = 1 },
                "qtech@test.ru" => new User { Id = 2 },
                _ => null
            };
            _cache.SetCurrentUser(user);
            return user;
        }

        public void Logout()
        {
            _cache.Clear();
        }
    }
}