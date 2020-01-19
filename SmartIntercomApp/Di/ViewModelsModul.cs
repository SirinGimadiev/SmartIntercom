using Ninject.Modules;
using Ru.Tattelecom.SmartIntercom.Ui.Login;
using Ru.Tattelecom.SmartIntercom.Ui.Main;

namespace Ru.Tattelecom.SmartIntercom.Di
{
    public class ViewModelsModul : NinjectModule
    {
        public override void Load()
        {
            Bind<LoginViewModel>().To<LoginViewModel>();
        }
    }
}