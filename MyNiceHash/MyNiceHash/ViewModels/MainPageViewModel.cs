using MyNiceHash.Models;
using MyNiceHash.Rest;
using MyNiceHash.Services;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System.Collections.ObjectModel;
using MyNiceHash.Helpers;

namespace MyNiceHash.ViewModels {
    public class MainPageViewModel : BindableBase, INavigationAware {
        private readonly IRestRepository _restRepository;

        private readonly INavigationService _navigationService;

        private readonly IBtcAddressService _btcAddressService;

        public DelegateCommand ShowAddAddressPageCommand { get; private set; }

        public DelegateCommand ShowAboutPageCommand { get; private set; }

        public DelegateCommand ItemTappedCommand { get; private set; }

        public ObservableCollection<BtcAddress> btcAddressess;

        public ObservableCollection<BtcAddress> BtcAddressess {
            get { return btcAddressess; }
            set { SetProperty(ref btcAddressess, value); }
        }

        private BtcAddress selectedBtcAddress;
        public BtcAddress SelectedBtcAddress {
            get { return selectedBtcAddress; }
            set { SetProperty(ref selectedBtcAddress, value); }
        }

        public MainPageViewModel(IRestRepository restRepository, INavigationService navigationService, IBtcAddressService btcAddressService) {
            _restRepository = restRepository;
            _navigationService = navigationService;
            _btcAddressService = btcAddressService;
            ShowAddAddressPageCommand = new DelegateCommand(async () => await _navigationService.NavigateAsync("AddBtcAddressPage"));
            ShowAboutPageCommand = new DelegateCommand(async () => await _navigationService.NavigateAsync("AboutPage"));
            ItemTappedCommand = new DelegateCommand(ShowBtcAddressmainPage);
            BtcAddressess = _btcAddressService.GetBtcAddresses();
        }

        public void OnNavigatedFrom(NavigationParameters parameters) {

        }

        public void OnNavigatingTo(NavigationParameters parameters) {

        }

        public void OnNavigatedTo(NavigationParameters parameters) {
            if (parameters.ContainsKey("updateList")) {
                BtcAddressess = _btcAddressService.GetBtcAddresses();
            }
        }

        private async void ShowBtcAddressmainPage() {
            Settings.BtcAddress = SelectedBtcAddress.Key;
            SelectedBtcAddress = null;
            await _navigationService.NavigateAsync("BtcAddressMainPage");
        }
    }
}
