using System;
using Android.App;
using Android.Runtime;
using Ninject;
using Ru.Tattelecom.SmartIntercom.Di;

namespace Ru.Tattelecom.SmartIntercom
{
    [Application]
    public class App : Application
    {
        protected App(IntPtr javaReference, JniHandleOwnership transfer)
            : base(javaReference, transfer)
        {
        }

        public static IKernel AppContainer { get; set; }

        public override void OnCreate()
        {
            AppContainer = new StandardKernel(
                new NinjectSettings {LoadExtensions = false},
                new RepositoryModule(),
                new ViewModelsModul());
            base.OnCreate();
        }
    }
}