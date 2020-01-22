namespace Ru.Tattelecom.SmartIntercom.Data.Model
{
    public class User
    {
        public int Id { get; set; }

        public SipUser Sip { get; set; }

        public User()
        {
            Sip = new SipUser
            {
                Login = "8432228021",
                Password = "v8AZd4z5",
                Domain = "Sip.tattelecom.ru"
            };
        }
    }
}