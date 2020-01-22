using System;
using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using LibVLCSharp.Shared;
using ActionBar = Android.App.ActionBar;
using VideoView = LibVLCSharp.Platforms.Android.VideoView;

namespace Ru.Tattelecom.SmartIntercom.Ui.VideoCall
{
    [Activity]
    public class VideoCallActivity : AppCompatActivity
    {
        private VideoCallViewModel _viewModel;
        private LibVLC _libVlc;
        private MediaPlayer _mediaPlayer;
        private FrameLayout _videoLayout;
        private VideoView _videoView;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
        }

        private void CreatePlayer(string streamUrl)
        {
            
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
                _mediaPlayer = new MediaPlayer(_libVlc) { EnableHardwareDecoding = true };
                _videoView = new VideoView(this) { MediaPlayer = _mediaPlayer };
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