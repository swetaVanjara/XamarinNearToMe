using System;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using MvvmCross.Droid.Views;
using NearToMe.Core.ViewModels;
using NearToMe.Droid.Views;
using Xamarin.Facebook;

namespace NearToMe.Droid
{
    [Activity(Label = "NearToMe",MainLauncher = true,Icon = "@drawable/Logo",Theme = "@style/Theme.Splash",NoHistory = true,ScreenOrientation = ScreenOrientation.Portrait)]
    public class SplashScreen : BaseView<SplashViewModel>
    {
        public SplashScreen()
        {
        }
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.SplashScreen);
            MoveToActivity();
        }

        async void MoveToActivity()
        {
            await Task.Delay(3000);
            if (AccessToken.CurrentAccessToken != null)
            {
                Intent homeViewIntent = new Intent(this, typeof(MainView));
                homeViewIntent.AddFlags(ActivityFlags.ClearTop);
                StartActivity(homeViewIntent);
                Finish();
            }
            else
            {
                Intent homeViewIntent = new Intent(this, typeof(LoginView));
                homeViewIntent.AddFlags(ActivityFlags.ClearTop);
                StartActivity(homeViewIntent);
                Finish();
            }
        }
    }
}
