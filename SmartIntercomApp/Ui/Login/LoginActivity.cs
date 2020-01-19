using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V7.App;
using Android.Text;
using Android.Widget;
using Ru.Tattelecom.SmartIntercom.Ui.Main;

namespace Ru.Tattelecom.SmartIntercom.Ui.Login
{
    [Activity(MainLauncher = true)]
    public class LoginActivity : AppCompatActivity
    {
        private LoginViewModel _loginViewModel;
        private EditText _loginEditText;
        private EditText _passwordEditText;
        private Button _loginButton;
        private IDisposable _loginFormStateSubscribe;
        private IDisposable _loginResultSubscribe;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_login);
            _loginViewModel = ViewModelFactory.GetViewModel<LoginViewModel>();
            _loginEditText = FindViewById<EditText>(Resource.Id.loginEditText);
            _passwordEditText = FindViewById<EditText>(Resource.Id.passwordEditText);
            _loginButton = FindViewById<Button>(Resource.Id.signInButton);
        }

        private void LoginButtonOnClick(object sender, EventArgs e)
        {
            _loginViewModel.Login(_loginEditText.Text, _passwordEditText.Text);
        }

        private void PasswordEditorAction(object sender, TextView.EditorActionEventArgs e)
        {
            _loginViewModel.Login(_loginEditText.Text, _passwordEditText.Text);
        }

        private void LoginDataChanged(object sender, AfterTextChangedEventArgs e)
        {
            _loginViewModel.LoginDataChanged(_loginEditText.Text, _passwordEditText.Text);
        }

        protected override void OnResume()
        {
            base.OnResume();
            _loginFormStateSubscribe = _loginViewModel.LoginFormState.Subscribe(state =>
            {
                if (state == null) return;
                _loginButton.Enabled = state.IsDataValid;
                if (state.LoginError != null) _loginEditText.Error = state.LoginError;
                if (state.PasswordError != null) _passwordEditText.Error = state.PasswordError;
            });
            _loginResultSubscribe = _loginViewModel.LoginResult
                .Subscribe(result =>
                {
                    if (result == null)
                        return;
                    //TODO add progressbar
                    if (!result.Success)
                    {
                        ShowLoginFailed(result.Error);
                        return;
                    }

                    UpdateUi();
                    Finish();
                });

            _loginEditText.AfterTextChanged += LoginDataChanged;
            _passwordEditText.AfterTextChanged += LoginDataChanged;
            _passwordEditText.EditorAction += PasswordEditorAction;
            _loginButton.Click += LoginButtonOnClick;
        }

        protected override void OnPause()
        {
            base.OnPause();
            _loginFormStateSubscribe.Dispose();
            _loginResultSubscribe.Dispose();
            _loginEditText.AfterTextChanged -= LoginDataChanged;
            _passwordEditText.AfterTextChanged -= LoginDataChanged;
            _passwordEditText.EditorAction -= PasswordEditorAction;
            _loginButton.Click -= LoginButtonOnClick;
        }

        private void UpdateUi()
        {
            var intent = new Intent(this, typeof(MainActivity));
            StartActivity(intent);
        }

        private void ShowLoginFailed(string error)
        {
            Toast.MakeText(this, error, ToastLength.Short).Show();
        }
    }
}