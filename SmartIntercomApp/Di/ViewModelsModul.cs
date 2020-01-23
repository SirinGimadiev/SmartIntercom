using Ninject.Modules;
using Ru.Tattelecom.SmartIntercom.Ui.IntercomList;
using Ru.Tattelecom.SmartIntercom.Ui.Login;
using Ru.Tattelecom.SmartIntercom.Ui.Pin;

namespace Ru.Tattelecom.SmartIntercom.Di
{
    public class ViewModelsModul : NinjectModule
    {
        public override void Load()
        {
            Bind<LoginViewModel>().To<LoginViewModel>();
            Bind<IntercomListViewModel>().To<IntercomListViewModel>();
            Bind<PinViewModel>().To<PinViewModel>();
        }
    }
}