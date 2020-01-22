using System;
using System.Collections.Generic;
using Android.Content;
using Android.Support.Design.Widget;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using LibVLCSharp.Shared;
using Ru.Tattelecom.SmartIntercom.Data.Model;
using VideoView = LibVLCSharp.Platforms.Android.VideoView;

namespace Ru.Tattelecom.SmartIntercom.Ui.IntercomList
{
    public class IntercomAdapter : RecyclerView.Adapter
    {
        private List<Intercom> _intercoms = new List<Intercom>();
        private readonly Context _context;

        public IntercomAdapter(Context context)
        {
            _context = context;
        }
        
        public override int ItemCount => _intercoms.Count;

        public EventHandler<Intercom> OpenDoorClick { get; set; }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            if (!(holder is IntercomViewHolder viewHolder))
                return;
            var currentIntercom = _intercoms[position];
            viewHolder.NameTextView.Text = currentIntercom.Name;
            viewHolder.AddressTextView.Text = currentIntercom.Address;
            viewHolder.OpenDoorFab.Click += (sender, args) => { OpenDoorClick?.Invoke(sender, currentIntercom); };
            var libVlc = new LibVLC();
            var mediaPlayer = new MediaPlayer(libVlc) { EnableHardwareDecoding = true };
            var videoView = new VideoView(_context) { MediaPlayer = mediaPlayer };
            viewHolder.VideoLayout.AddView(videoView);
            var media = new Media(libVlc, currentIntercom.VideoStream, FromType.FromLocation);
            videoView.MediaPlayer.Play(media);
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            var view = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.view_intercom_item, parent, false);
            return new IntercomViewHolder(view);
        }

        public void SetIntercoms(List<Intercom> intercoms)
        {
            _intercoms = intercoms;
            NotifyDataSetChanged();
        }

        private class IntercomViewHolder : RecyclerView.ViewHolder
        {
            public IntercomViewHolder(View itemView)
                : base(itemView)
            {
                VideoLayout = itemView.FindViewById<FrameLayout>(Resource.Id.videoLayout);
                NameTextView = itemView.FindViewById<TextView>(Resource.Id.nameTextView);
                AddressTextView = itemView.FindViewById<TextView>(Resource.Id.addressTextView);
                OpenDoorFab = itemView.FindViewById<FloatingActionButton>(Resource.Id.openDoorFab);
            }

            public FrameLayout VideoLayout { get; }

            public TextView NameTextView { get; }

            public TextView AddressTextView { get; }

            public FloatingActionButton OpenDoorFab { get; }
        }
    }
}