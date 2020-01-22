using System;
using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Support.V7.Widget;
using Android.Views;
using Ru.Tattelecom.SmartIntercom.Data.Model;

namespace Ru.Tattelecom.SmartIntercom.Ui.IntercomList
{
    [Activity]
    public class IntercomListActivity : AppCompatActivity
    {
        private IntercomListViewModel _viewModel;
        private RecyclerView _recyclerView;
        private IntercomAdapter _adapter;
        private IDisposable _intercomsSubscribe;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_main);
            _viewModel = ViewModelFactory.GetViewModel<IntercomListViewModel>();
            var toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);
            _recyclerView = FindViewById<RecyclerView>(Resource.Id.recyclerView);
            var layoutManager = new LinearLayoutManager(this);
            _recyclerView.SetLayoutManager(layoutManager);
            _recyclerView.HasFixedSize = true;
            _adapter = new IntercomAdapter(this);
            _recyclerView.SetAdapter(_adapter);

            _intercomsSubscribe = _viewModel.Intercoms
                .Subscribe(list => { _adapter.SetIntercoms(list); });
            _adapter.OpenDoorClick += OpenDoorClick;
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.main_menu, menu);
            return true;
        }

        private void OpenDoorClick(object sender, Intercom intercom)
        {
            _viewModel.OpenDoor(intercom);
        }

        protected override void OnDestroy()
        {
            _intercomsSubscribe.Dispose();
            base.OnDestroy();
        }
    }
}