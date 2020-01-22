using System;
using Refit;

namespace Ru.Tattelecom.SmartIntercom.Utilits
{
    public interface IHttpClient
    {
        [Get("{action}")]
        string ExecuteAction(string action);
    }
}