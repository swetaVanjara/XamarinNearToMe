using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Gms.Maps;
using Android.Locations;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using NearToMe.Core.ViewModels;
using Android.Provider;
using Android.Gms.Maps.Model;
using NearToMe.Droid.Helpers;
using Xamarin.Facebook;
using Xamarin.Facebook.Login;
using Android.Gms.Common.Apis;
using Android.Gms.Auth.Api.SignIn;
using Android.Gms.Auth.Api;
using Android.Gms.Plus;

namespace NearToMe.Droid.Views
{
    [Activity(Label = "MainView", Theme = "@style/NTMTheme")]
    public class MainView : BaseView<MainViewModel>, IOnMapReadyCallback, ILocationListener
    {
        LocationManager _locationManager;
        string _locationProvider;
        Boolean enabled;
        Location _currentLocation;
        private GoogleMap gmap;
        private static  int LOCATION_INTERVAL = 10000;
        public Marker _marker;
        ImageButton btnLogout;
        private GoogleApiClient mGoogleApiClient;
        int i = 0;

        public void OnMapReady(GoogleMap googleMap)
        {
            this.gmap = googleMap;
            //https://snazzymaps.com/style/74/becomeadinosaur
           // gmap.SetMapStyle(MapStyleOptions.LoadRawResourceStyle(this, Resource.Raw.map_style));
            gmap.UiSettings.ZoomControlsEnabled = true;
            gmap.UiSettings.ScrollGesturesEnabled = true;
            gmap.UiSettings.ZoomGesturesEnabled = true;
            gmap.MyLocationEnabled = true;
        }
        protected override void OnResume()
        {
            base.OnResume();
            enabled = _locationManager.IsProviderEnabled(_locationProvider);
            _locationManager.RequestLocationUpdates(LocationManager.NetworkProvider, LOCATION_INTERVAL, 0, this);

        }
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.MainView);
            SetUI();
            InitializeLocationManager();
            EnableGPS();
            SetUpMap();
        }

        private void SetUpMap()
        {
            if (gmap == null)
            {
                FragmentManager.FindFragmentById<MapFragment>(Resource.Id.googlemap).GetMapAsync(this);
            }
        }
        public void InitializeLocationManager()
        {
            _locationManager = (LocationManager)this.GetSystemService(Service.LocationService);

            Criteria criteriaForLocationService = new Criteria
            {
                Accuracy = Accuracy.Fine
            };
            IList<string> acceptableLocationProviders = _locationManager.GetProviders(criteriaForLocationService, true);

            if (acceptableLocationProviders.Any())
            {
                _locationProvider = acceptableLocationProviders.First();
            }
            else
            {
                _locationProvider = string.Empty;
            }
        }

        public Boolean EnableGPS()
        {
            enabled = _locationManager.IsProviderEnabled(_locationProvider);

            if (!enabled)
            {
                AlertDialog.Builder alert = new AlertDialog.Builder(this);
                alert.SetTitle("Location Services Not Active");

                alert.SetMessage("Please enable Location Services and GPS");
                alert.SetPositiveButton("Ok", (c, ev) =>
                {
                    StartActivity(new Intent(Settings.ActionLocationSourceSettings));
                });
                alert.Show();
            }
            return enabled;
        }

        private void SetUI()
        {
            btnLogout = FindViewById<ImageButton>(Resource.Id.Logout);
            btnLogout.Click += BtnLogout_Click;
        }

        private void BtnLogout_Click(object sender, EventArgs e)
        {
            AlertDialog.Builder alert = new AlertDialog.Builder(this);
            alert.SetTitle("Logout");

            alert.SetMessage("Are you sure to want to Logout ??");
            alert.SetPositiveButton("Ok", (c, ev) =>
            {
#pragma warning disable CS0618 // Type or member is obsolete
                FacebookSdk.SdkInitialize(this);
#pragma warning restore CS0618 // Type or member is obsolete
                LoginManager.Instance.LogOut();
                mGoogleApiClient.Disconnect();
                Intent homeViewIntent = new Intent(this, typeof(LoginView));
                homeViewIntent.AddFlags(ActivityFlags.ClearTop);
                StartActivity(homeViewIntent);
                Finish();
            });
            alert.Show();
           
        }
        protected override void OnStart()
        {
            base.OnStart();
            GoogleSignInOptions gso = new GoogleSignInOptions.Builder(GoogleSignInOptions.DefaultSignIn)
                  .RequestEmail()
                  .Build();

            mGoogleApiClient = new GoogleApiClient.Builder(this)
                    .AddApi(Auth.GOOGLE_SIGN_IN_API, gso)
                    .Build();

            mGoogleApiClient.Connect();
        }
        public void OnLocationChanged(Location location)
        {
            _currentLocation = location;
            GlobalConst.lat = _currentLocation.Latitude;
            GlobalConst.lang = _currentLocation.Longitude;
            AddMarker();
        }

        private void AddMarker()
        {
            if (_currentLocation != null)
            {
                RunOnUiThread(() =>
                {
                    if (_marker != null)
                    {
                        _marker.Remove();
                    }
                    LatLng latlngnew = new LatLng(_currentLocation.Latitude, _currentLocation.Longitude);
                    BitmapDescriptor icon = BitmapDescriptorFactory.FromResource(Resource.Drawable.marker);

                    MarkerOptions markerOption = new MarkerOptions()
                             .SetPosition(latlngnew)
                             .SetIcon(icon);
                    _marker = gmap.AddMarker(markerOption);
                    //BitmapDescriptorFactory.DefaultMarker(BitmapDescriptorFactory.HueCyan)
                    if (i == 0)
                    {
                        gmap.AnimateCamera(CameraUpdateFactory.NewLatLngZoom(latlngnew, 19.0f));
                        i++;
                    }
                });
            }
        }

        public void OnProviderDisabled(string provider)
        {
            //throw new NotImplementedException();
        }

        public void OnProviderEnabled(string provider)
        {
            // throw new NotImplementedException();
        }

        public void OnStatusChanged(string provider, [GeneratedEnum] Availability status, Bundle extras)
        {
            // throw new NotImplementedException();
        }
    }
}