using MyNiceHash.Models;
using Prism.Commands;
using Prism.Mvvm;
using MyNiceHash.Services;
using Prism.Navigation;
using Prism.Services;
using MyNiceHash.Exceptions;

namespace MyNiceHash.ViewModels {
    public class AddBtcAddressPageViewModel : BindableBase {
        private readonly INavigationService _navigationService;

        private readonly IPageDialogService _pageDialogService;

        private readonly IBtcAddressService _btcAddressService;

        public DelegateCommand SaveAddressCommand { get; private set; }

        public DelegateCommand NavigateBackCommand { get; private set; }

        public DelegateCommand ReadQrCodeCommand { get; private set; }

        private BtcAddress btcAddress;

        public BtcAddress BtcAddress {
            get { return btcAddress; }
            set { SetProperty(ref btcAddress, value); }
        }

        public AddBtcAddressPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService, 
            IBtcAddressService btcAddressService) {
            _navigationService = navigationService;
            _pageDialogService = pageDialogService;
            _btcAddressService = btcAddressService;
            SaveAddressCommand = new DelegateCommand(SaveAddressExecute);
            ReadQrCodeCommand = new DelegateCommand(ReadQrCodeExecute);
            BtcAddress = new BtcAddress();
        }

        private void SaveAddressExecute() {
            try {
                _btcAddressService.Save(BtcAddress);
                var parameters = new NavigationParameters();
                parameters.Add("updateList", true);
                _navigationService.GoBackAsync(parameters);
            } catch (BtcAddressNullOrEmptyException e) {
                _pageDialogService.DisplayAlertAsync("", e.Message, "OK");
            } catch (BtcAddressAlreadyRegisteredException e) {
                _pageDialogService.DisplayAlertAsync("", e.Message, "OK");
            }
        }

        private async void ReadQrCodeExecute() {
            var scanner = new ZXing.Mobile.MobileBarcodeScanner();
            var result = await scanner.Scan();
            if (result != null) {
                BtcAddress.Key = result.Text;
                RaisePropertyChanged("BtcAddress");
            }
        }
    }
}
