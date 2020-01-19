using System;
using System.Reactive.Subjects;
using Android.Util;
using Ru.Tattelecom.SmartIntercom.Data.Repository;

namespace Ru.Tattelecom.SmartIntercom.Ui.Login
{
    public class LoginViewModel
    {
        private readonly IUserRepository _repository;
        private readonly ReplaySubject<LoginFormState> _loginFormState;
        private readonly ReplaySubject<LoginResult> _loginResult;

        public LoginViewModel(IUserRepository repository)
        {
            _repository = repository;
            _loginFormState = new ReplaySubject<LoginFormState>();
            _loginResult = new ReplaySubject<LoginResult>();
        }

        public IObservable<LoginFormState> LoginFormState => _loginFormState;

        public IObservable<LoginResult> LoginResult => _loginResult;

        public void LoginDataChanged(string login, string password)
        {
            if (!_loginFormState.HasObservers)
                return;
            if (!IsLoginValid(login))
                _loginFormState.OnNext(new LoginFormState
                {
                    LoginError = "Неверный email"
                });
            else if (!IsPasswordValid(password))
                _loginFormState.OnNext(new LoginFormState
                {
                    PasswordError = "Длина пароля должна быть не менее 6 символов"
                });
            else
                _loginFormState.OnNext(new LoginFormState
                {
                    IsDataValid = true
                });
        }

        public void Login(string login, string password)
        {
           var user = _repository.Login(login, password);
           if (user == null)
           {
               _loginResult.OnNext(new LoginResult
               {
                   Error = "Неверный email или пароль"
               });
               return;
           }
           _loginResult.OnNext(new LoginResult
           {
               Success = true
           });
            _loginFormState.OnCompleted();
            _loginResult.OnCompleted();
        }

        private static bool IsLoginValid(string login)
        {
            if (login == null)
                return false;
            return login.Contains("@")
                ? Patterns.EmailAddress.Matcher(login).Matches()
                : string.IsNullOrWhiteSpace(login);
        }

        private static bool IsPasswordValid(string password)
        {
            return !string.IsNullOrEmpty(password) && password.Trim().Length > 5;
        }
    }
}