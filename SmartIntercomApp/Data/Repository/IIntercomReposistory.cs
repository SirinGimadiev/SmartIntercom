using System.Collections.Generic;
using Ru.Tattelecom.SmartIntercom.Data.Model;

namespace Ru.Tattelecom.SmartIntercom.Data.Repository
{
    public interface IIntercomReposistory
    {
        IEnumerable<Intercom> GetIntercoms(int userId);
    }
}