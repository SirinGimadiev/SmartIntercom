using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Subjects;
using Ru.Tattelecom.SmartIntercom.Data.Model;
using Ru.Tattelecom.SmartIntercom.Data.Repository;

namespace Ru.Tattelecom.SmartIntercom.Ui.IntercomList
{
    public class IntercomListViewModel
    {
        private readonly IIntercomReposistory _reposistory;
        private readonly ReplaySubject<List<Intercom>> _intercoms;

        public IntercomListViewModel(IIntercomReposistory reposistory)
        {
            _reposistory = reposistory;
            _intercoms = new ReplaySubject<List<Intercom>>();
            _intercoms.OnNext(_reposistory.GetIntercoms().ToList()); 
        }

        public IObservable<List<Intercom>> Intercoms => _intercoms;

        public void OpenDoor(Intercom intercom)
        {
            _reposistory.OpenDoor(intercom);
        }
    }
}