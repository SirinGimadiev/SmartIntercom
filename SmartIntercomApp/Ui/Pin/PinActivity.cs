using System;
using Android.App;
using Android.OS;
using Android.Support.V4.Content;
using Android.Support.V4.Graphics.Drawable;
using Android.Support.V7.App;
using Android.Widget;
using Ru.Tattelecom.SmartIntercom.Utilits;

namespace Ru.Tattelecom.SmartIntercom.Ui.Pin
{
    [Activity(MainLauncher = true)]
    public class PinActivity : AppCompatActivity
    {
        private PinViewModel _pinViewModel;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_pin);
            _pinViewModel = ViewModelFactory.GetViewModel<PinViewModel>();

            var pinEditText1 = FindViewById<EditText>(Resource.Id.pinText1);
            var pinEditText2 = FindViewById<EditText>(Resource.Id.pinText2);
            var pinEditText3 = FindViewById<EditText>(Resource.Id.pinText3);
            var pinEditText4 = FindViewById<EditText>(Resource.Id.pinText4);
            InitPinViews(pinEditText1, pinEditText2, pinEditText3, pinEditText4);

            var oneButton = FindViewById<Button>(Resource.Id.oneButton);
            var twoButton = FindViewById<Button>(Resource.Id.twoButton);
            var threeButton = FindViewById<Button>(Resource.Id.threeButton);
            var fourButton = FindViewById<Button>(Resource.Id.fourButton);
            var fiveButton = FindViewById<Button>(Resource.Id.fiveButton);
            var sixButton = FindViewById<Button>(Resource.Id.sixButton);
            var sevenButton = FindViewById<Button>(Resource.Id.sevenButton);
            var eightButton = FindViewById<Button>(Resource.Id.eightButton);
            var nineButton = FindViewById<Button>(Resource.Id.nineButton);
            var zeroButton = FindViewById<Button>(Resource.Id.zeroButton);
            InitNumericButtons(zeroButton, oneButton, twoButton, threeButton, fourButton, fiveButton, sixButton,
                sevenButton, eightButton, nineButton);

            var clearButton = FindViewById<Button>(Resource.Id.clearButton);
            clearButton.Click += (sender, args) => { _pinViewModel.ClearPin(); };

            var signInButton = FindViewById<Button>(Resource.Id.signInButton);
            signInButton.Click += (sender, args) => { _pinViewModel.SignIn(); };

            var fingerprintView = FindViewById<ImageView>(Resource.Id.fingerprintView);
            fingerprintView.Click += (sender, args) => { _pinViewModel.SignInByFingerprint(); };
        }

        private void InitPinViews(params EditText[] pinEditTexts)
        {
            for (var i = 0; i < pinEditTexts.Length; i++)
            {
                var local = i;
                pinEditTexts[local].TransformationMethod = new AsteriskPasswordTransformationMethod();
                _pinViewModel.Pin.Subscribe(pin =>
                {
                    var pinEditText = pinEditTexts[local];
                    if (pin.Length > local)
                    {
                        pinEditText.Text = pin[local].ToString();
                        DrawableCompat.SetTint(pinEditText.Background,
                            ContextCompat.GetColor(this, Resource.Color.colorPrimary));
                    }
                    else
                    {
                        pinEditText.Text = string.Empty;
                        DrawableCompat.SetTint(pinEditText.Background,
                            ContextCompat.GetColor(this, Resource.Color.colorGray));
                    }
                });
            }
        }

        private void InitNumericButtons(params Button[] numericButtons)
        {
            for (var i = 0; i < numericButtons.Length; i++)
            {
                var local = i;
                numericButtons[local].Click += (sender, args) => { _pinViewModel.AddPin(local); };
            }
        }
    }
}