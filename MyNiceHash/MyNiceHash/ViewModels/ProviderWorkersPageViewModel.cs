using MyNiceHash.Models;
using Prism.Commands;
using Prism.Events;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System;
using Prism.Services;
using Prism.Navigation;
using MyNiceHash.Services;

namespace MyNiceHash.ViewModels {
    public class ProviderWorkersPageViewModel : ChildViewModelBase {    
        private readonly IPageDialogService _pageDialogService;

        private readonly INavigationService _navigationService;

        private readonly IProviderWorkerService _providerWorkerService;

        private int selectedAlgorithmIndex;
        public int SelectedAlgorithmIndex {
            get { return selectedAlgorithmIndex; }
            set { SetProperty(ref selectedAlgorithmIndex, value); }
        }

        private ObservableCollection<Worker> workers;
        public ObservableCollection<Worker> Workers {
            get { return workers; }
            set { SetProperty(ref workers, value); }
        }

        private Worker selectedWorker;

        public Worker SelectedWorker {
            get { return selectedWorker; }
            set { SetProperty(ref selectedWorker, value); }
        }

        private bool isBusy;
        public bool IsBusy {
            get { return isBusy; }
            set { SetProperty(ref isBusy, value); }
        }

        public bool NoWorkers {
            get {
                return !IsBusy && ((Workers == null) || (Workers.Count == 0));
            }
        }

        public DelegateCommand AlgorithmChangedCommand { get; private set; }

        public DelegateCommand RefreshCommand { get; private set; }

        public DelegateCommand ItemTappedCommand { get; private set; }

        public ProviderWorkersPageViewModel(IEventAggregator ea, IPageDialogService pageDialogService, INavigationService navigationService, 
            IProviderWorkerService providerWorkerService) : base(ea) {
            _pageDialogService = pageDialogService;
            _navigationService = navigationService;
            _providerWorkerService = providerWorkerService;
            SelectedAlgorithmIndex = -1;
            Workers = new ObservableCollection<Worker>();
            RefreshCommand = new DelegateCommand(async () => await GetWorkers());
            AlgorithmChangedCommand = new DelegateCommand(RefreshCommand.Execute);
            ItemTappedCommand = new DelegateCommand(ShowWorkerDetailsPage);
        }

        private async Task GetWorkers() {
            try {
                if (SelectedAlgorithmIndex > -1) {
                    IsBusy = true;
                    RaisePropertyChanged("NoWorkers");
                    Workers.Clear();
                    var items = await _providerWorkerService.GetProviderWorkers((AlgorithmType)SelectedAlgorithmIndex);
                    if (items != null) {
                        foreach (var worker in items) {
                            Workers.Add(worker);
                        }
                    }
                }
            } catch (Exception e) {
                await _pageDialogService.DisplayAlertAsync("Error", e.Message, "OK");
            } finally {
                IsBusy = false;
                RaisePropertyChanged("NoWorkers");
            }
        }

        private async void ShowWorkerDetailsPage() {
            var parameters = new NavigationParameters();
            parameters.Add("Worker", SelectedWorker);
            SelectedWorker = null;
            await _navigationService.NavigateAsync("NavigationPage/WorkerDetailsPage", parameters);
        }
    }
}
