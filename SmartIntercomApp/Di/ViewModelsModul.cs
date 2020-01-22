using Ninject.Modules;
using Ru.Tattelecom.SmartIntercom.Ui.IntercomList;
using Ru.Tattelecom.SmartIntercom.Ui.Login;

namespace Ru.Tattelecom.SmartIntercom.Di
{
    public class ViewModelsModul : NinjectModule
    {
        public override void Load()
        {
            Bind<LoginViewModel>().To<LoginViewModel>();
            Bind<IntercomListViewModel>().To<IntercomListViewModel>();
        }
    }
}