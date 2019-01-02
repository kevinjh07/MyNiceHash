using MyNiceHash.Events;
using Prism.Events;
using Prism.Navigation;
using MyNiceHash.Helpers;

namespace MyNiceHash.ViewModels {
    public class BtcAddressMainPageViewModel : BaseViewModel, INavigationAware {
        private IEventAggregator _ea { get; }

        public BtcAddressMainPageViewModel(IEventAggregator eventAggregator) {
            _ea = eventAggregator;
            Title = Settings.BtcAddress;
        }

        public void OnNavigatedFrom(NavigationParameters parameters) {
            
        }

        public void OnNavigatedTo(NavigationParameters parameters) {
            
        }

        public override void OnNavigatingTo(NavigationParameters parameters) {
            _ea.GetEvent<InitializeTabbedChildrenEvent>().Publish(parameters);
        }
    }
}
