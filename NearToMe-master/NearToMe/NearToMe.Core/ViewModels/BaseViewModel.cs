
using MvvmCross.Core.ViewModels;
using System.Threading.Tasks;

namespace NearToMe.Core.ViewModels
{
    public class BaseViewModel :MvxViewModel
    {
        public BaseViewModel()
        {
        }
        public override Task Initialize()
        {
            //TODO: Add starting logic here

            return base.Initialize();
        }
    }
}
