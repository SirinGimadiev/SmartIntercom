namespace Ru.Tattelecom.SmartIntercom.Data.Model
{
    public class User
    {
        public int Id { get; set; }

        public SipUser Sip { get; set; }

        public User()
        {
        }
    }
}