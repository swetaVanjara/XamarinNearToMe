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
using MvvmCross.Binding.Droid.BindingContext;
using MvvmCross.Core.ViewModels;
using MvvmCross.Droid.Support.V4;
using MvvmCross.Platform;
using NearToMe.Core.ViewModels;
using NearToMe.Droid.Views;

namespace NearToMe.Droid.Fragments
{
    public abstract class BaseFragment<TViewModel> : MvxFragment<TViewModel> where TViewModel :BaseViewModel
    {
        public new TViewModel ViewModel
        {
            get
            {
                return base.ViewModel;
            }
            set
            {
                base.ViewModel = value;
            }
        }
        public View CurrentView { get; set; }
        protected BaseHomeView CurrentActivity { get; set; }

        protected abstract int FragmentId { get; }
        protected abstract void SetUI();
        protected abstract void SetListeners();

        protected BaseFragment()
        {
            this.RetainInstance = true;
        }
        private IMvxViewModelLocatorCollection _locatorCollection;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            var vmRequest = new MvxViewModelRequest<TViewModel>(null, null);
            _locatorCollection = _locatorCollection ?? Mvx.Resolve<IMvxViewModelLocatorCollection>();
            var vm = (new MvxViewModelLoader(_locatorCollection)).LoadViewModel(vmRequest, null);
            this.ViewModel = (TViewModel)vm;
            CurrentActivity = (BaseHomeView)Activity;
            CurrentView = null;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);
            CurrentView = this.BindingInflate(FragmentId, null);
            SetUI();
            SetListeners();
            return CurrentView;
        }
    }
}