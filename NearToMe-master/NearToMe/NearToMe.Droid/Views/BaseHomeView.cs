
using System;
using Android.App;
using Android.OS;
using Android.Support.V4.Widget;
using Android.Views;
using MvvmCross.Droid.Support.V7.AppCompat;
using NearToMe.Core.ViewModels;
using NearToMe.Droid.Fragments;

namespace NearToMe.Droid.Views
{
    [Activity(Label = "BaseHomeView",Theme = "@style/NTMTheme",AlwaysRetainTaskState =true)]
    public class BaseHomeView : MvxAppCompatActivity<BaseViewModel>
    {
        protected BaseViewModel _thisViewModel { get; set; }
       // public DrawerLayout drawerLayout;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            RequestWindowFeature(WindowFeatures.NoTitle);
            base.OnCreate(savedInstanceState);
            //SetContentView(Resource.Layout.HomeView);
            _thisViewModel = ViewModel;
            this.Window.SetSoftInputMode(SoftInput.AdjustPan);
            SetUI();
            if (savedInstanceState == null)
            {
              //  NavigateToFragment<HomeFragment>("HomeFragment", true);
            }
        }
        private void SetUI()
        {

        }
        /// <summary>
        /// Use to navigation between fragments.
        /// </summary>
        /// <param name="fragmentId">fragment name for identifing in back stack.</param>
        /// <param name="isaddToBackStack">If set to <c>true</c> is previous fragment to back stack.</param>
        /// <typeparam name="TFragment">The fragment type parameter.</typeparam>
        //private void NavigateToFragment<TFragment>(string fragmentId, bool isaddToBackStack, bool isPopBackIfExist = true) where TFragment : Android.Support.V4.App.Fragment
        //{
        //    if (isPopBackIfExist && IsFragmentInBackStack(fragmentId))
        //    {
        //        this.SupportFragmentManager.PopBackStackImmediate(fragmentId, (int)PopBackStackFlags.None);
        //    }
        //    else
        //    {
        //        var fragment = Activator.CreateInstance<TFragment>();
        //        var fragmentInstanance = fragment;
        //        var fragmenttransaction = this.SupportFragmentManager.BeginTransaction();
        //        //fragmenttransaction.Replace(Resource.Id.content_frame, fragmentInstanance, fragmentId);
        //        if (isaddToBackStack)
        //        {
        //            fragmenttransaction.AddToBackStack(fragmentId);
        //        }
        //        fragmenttransaction.Commit();
        //    }
        //}
        private bool IsFragmentInBackStack(string fragmentId)
        {
            return SupportFragmentManager.FindFragmentByTag(fragmentId) != null;
        }


    }
}