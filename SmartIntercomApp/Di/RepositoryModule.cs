using Ninject.Modules;
using Ru.Tattelecom.SmartIntercom.Data.Cache;
using Ru.Tattelecom.SmartIntercom.Data.Repository;

namespace Ru.Tattelecom.SmartIntercom.Di
{
    public class RepositoryModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ICache>().To<LocalCache>().InSingletonScope();
            Bind<IUserRepository>().To<MainRepository>().InSingletonScope();
            Bind<IIntercomReposistory>().To<MainRepository>().InSingletonScope();
        }
    }
}