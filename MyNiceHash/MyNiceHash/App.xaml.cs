using DryIoc;
using Prism.DryIoc;
using MyNiceHash.Views;
using Xamarin.Forms;
using MyNiceHash.Rest;
using MyNiceHash.Services;

namespace MyNiceHash {
    public partial class App : PrismApplication {
        public App(IPlatformInitializer initializer = null) : base(initializer) { }

        protected override void OnInitialized() {
            InitializeComponent();

            NavigationService.NavigateAsync("NavigationPage/MainPage");
        }

        protected override void RegisterTypes() {
            Container.Register<IRestRepository, RestRepository>(Reuse.Singleton);
            Container.Register<IBtcAddressService, BtcAddressService>(Reuse.Singleton);
            Container.Register<IProviderStatsService, ProviderStatsService>(Reuse.Singleton);
            Container.Register<IProviderWorkerService, ProviderWorkerService>(Reuse.Singleton);
            Container.RegisterTypeForNavigation<NavigationPage>();
            Container.RegisterTypeForNavigation<MainPage>();
            Container.RegisterTypeForNavigation<AddBtcAddressPage>();
            Container.RegisterTypeForNavigation<BtcAddressMainPage>();
            Container.RegisterTypeForNavigation<ProviderStatsPage>();
            Container.RegisterTypeForNavigation<ProviderWorkersPage>();
            Container.RegisterTypeForNavigation<WorkerDetailsPage>();
            Container.RegisterTypeForNavigation<AboutPage>();
        }
    }
}
