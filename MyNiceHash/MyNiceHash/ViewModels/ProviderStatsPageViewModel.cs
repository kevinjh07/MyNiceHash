using Java.Lang;
using MyNiceHash.Models;
using MyNiceHash.Services;
using Prism.Commands;
using Prism.Events;
using Prism.Services;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace MyNiceHash.ViewModels {
    public class ProviderStatsPageViewModel : ChildViewModelBase {
        private readonly IProviderStatsService _providerStatsService;

        private readonly IPageDialogService _pageDialogService;

        private ObservableCollection<ProviderStats> stats;
        public ObservableCollection<ProviderStats> Stats {
            get { return stats; }
            set { SetProperty(ref stats, value); }
        }

        private bool isBusy;
        public bool IsBusy {
            get { return isBusy; }
            set { SetProperty(ref isBusy, value); }
        }

        private string totalBalance;
        public string TotalBalance {
            get { return totalBalance; }
            set { SetProperty(ref totalBalance, value); }
        }

        public DelegateCommand RefreshCommand { get; private set; }

        public ProviderStatsPageViewModel(IEventAggregator ea, IProviderStatsService providerStatsService, IPageDialogService pageDialogService) : base(ea) {
            _providerStatsService = providerStatsService;
            _pageDialogService = pageDialogService;
            Stats = new ObservableCollection<ProviderStats>();
            RefreshCommand = new DelegateCommand(async () => await GetProviderStats());
            RefreshCommand.Execute();
        }

        private async Task GetProviderStats() {
            IsBusy = true;
            try {
                var items = await _providerStatsService.GetProviderStats();
                Stats.Clear();
                var total = (double)0;
                if (items != null) {
                    foreach (var item in items) {
                        Stats.Add(item);
                        total += double.Parse(item.Balance);
                    }
                }
                TotalBalance = total.ToString();
            } catch (Exception e) {
                await _pageDialogService.DisplayAlertAsync("Error", e.Message, "OK");
            } finally {
                IsBusy = false;
            }
        }
    }
}
