using MyNiceHash.Rest;
using Prism.Mvvm;
using Prism.Navigation;

namespace MyNiceHash.ViewModels {
    public class AboutPageViewModel : BindableBase, INavigationAware {
        private readonly IRestRepository _restRepository;

        private string apiVersion;
        public string ApiVersion {
            get { return apiVersion; }
            set { SetProperty(ref apiVersion, value); }
        }

        private bool isBusy;
        public bool IsBusy {
            get { return isBusy; }
            set { SetProperty(ref isBusy, value); }
        }

        public AboutPageViewModel(IRestRepository restRepository) {
            _restRepository = restRepository;
        }

        public void OnNavigatedFrom(NavigationParameters parameters) {
            
        }

        public async void OnNavigatedTo(NavigationParameters parameters) {
            IsBusy = true;
            ApiVersion = await _restRepository.GetAPIVersoin();
            IsBusy = false;
        }

        public void OnNavigatingTo(NavigationParameters parameters) {
            
        }
    }
}
