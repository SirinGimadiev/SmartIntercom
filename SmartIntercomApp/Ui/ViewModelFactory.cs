using System;
using Ninject;

namespace Ru.Tattelecom.SmartIntercom.Ui
{
    public static class ViewModelFactory
    {
        public static T GetViewModel<T>()
        {
            var viewModel = App.AppContainer.Get<T>();
            if (viewModel == null)
            {
                throw new NotImplementedException();
            }
            return viewModel;
        }
    }
}