using MyNiceHash.Models;
using Prism.Mvvm;
using Prism.Navigation;

namespace MyNiceHash.ViewModels {
    public class WorkerDetailsPageViewModel : BindableBase, INavigationAware {
        private Worker worker;
        public Worker Worker {
            get { return worker; }
            set { SetProperty(ref worker, value); }
        }

        public WorkerDetailsPageViewModel() {

        }

        public void OnNavigatedFrom(NavigationParameters parameters) {
            
        }

        public void OnNavigatedTo(NavigationParameters parameters) {
            Worker = (Worker)parameters["Worker"];
        }

        public void OnNavigatingTo(NavigationParameters parameters) {
            
        }
    }
}
