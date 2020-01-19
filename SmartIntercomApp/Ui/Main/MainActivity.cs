using System;
using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using LibVLCSharp.Shared;
using ActionBar = Android.App.ActionBar;
using VideoView = LibVLCSharp.Platforms.Android.VideoView;

namespace Ru.Tattelecom.SmartIntercom.Ui.Main
{
    [Activity]
    public class MainActivity : AppCompatActivity
    {
        private const string StreamUrl =
            "http://test_domofon:12345678@89.232.115.66:8000/live/media/AXXON-CLIENTS/DeviceIpint.319/SourceEndpoint.video:0:0?w=1920&h=0&format=mp4";

        private LibVLC _libVlc;
        private MediaPlayer _mediaPlayer;
        private FrameLayout _videoLayout;
        private VideoView _videoView;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_intercom_video);
            _videoLayout = FindViewById<FrameLayout>(Resource.Id.videoLayout);
        }

        protected override void OnResume()
        {
            base.OnResume();
            CreatePlayer(StreamUrl);
        }

        private void CreatePlayer(string streamUrl)
        {
            Core.Initialize();
            //var options = new string[] {
            //    "--file-caching=150", "--network-caching=150",
            //    "--clock-jitter=0", "--live-caching=150", "--clock-synchro=0",
            //    "-vvv", "--drop-late-frames", "--skip-frames"
            //};
            //var options = new[] {"-vvv"};
            //_libVlc = new LibVLC(options);
            try
            {
                _libVlc = new LibVLC();
                _mediaPlayer = new MediaPlayer(_libVlc) {EnableHardwareDecoding = true};
                _videoView = new VideoView(this) {MediaPlayer = _mediaPlayer};
                _videoLayout.AddView(_videoView,
                    new ActionBar.LayoutParams(ViewGroup.LayoutParams.WrapContent, ViewGroup.LayoutParams.MatchParent));
                var media = new Media(_libVlc, streamUrl, FromType.FromLocation);
                _videoView.MediaPlayer.Play(media);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }
    }
}