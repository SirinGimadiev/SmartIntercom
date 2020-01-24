using System;
using System.Reactive.Subjects;
using System.Text;

namespace Ru.Tattelecom.SmartIntercom.Ui.Pin
{
    public class PinViewModel
    {
        private readonly StringBuilder _pin;
        private readonly ReplaySubject<string> _pinSubject;

        public PinViewModel()
        {
            _pin = new StringBuilder();
            _pinSubject = new ReplaySubject<string>();
        }

        public IObservable<string> Pin => _pinSubject;

        public void AddPin(int number)
        {
            if (_pin.Length > 3)
                return;
            _pin.Append(number);
            _pinSubject.OnNext(_pin.ToString());
        }

        public void ClearPin()
        {
            if (_pin.Length < 1)
                return;
            _pin.Remove(_pin.Length - 1, 1);
            _pinSubject.OnNext(_pin.ToString());
        }

        public void SignIn()
        {
            
        }

        public void SignInByFingerprint()
        {
        }
    }
}