using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using MvvmCross.Droid.Views.Attributes;
using NearToMe.Core.ViewModels;

namespace NearToMe.Droid.Fragments
{
    [MvxFragmentPresentation(typeof(BaseViewModel), Resource.Id.content_frame)]
    [Register("neartome.droid.HomeFragment")]
    public class HomeFragment : BaseFragment<HomeViewModel>
    {
        protected override int FragmentId => Resource.Layout.LoginView;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            return base.OnCreateView(inflater, container, savedInstanceState);
        }

        protected override void SetListeners()
        {
           
        }

        protected override void SetUI()
        {
           
        }
    }
}