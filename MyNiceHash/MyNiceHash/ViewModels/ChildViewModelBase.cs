using Prism;
using Prism.Events;
using Prism.Navigation;
using MyNiceHash.Events;
using System;


namespace MyNiceHash.ViewModels {
    public class ChildViewModelBase : BaseViewModel, IActiveAware, INavigatingAware, IDestructible {
        private IEventAggregator _ea { get; }

        public event EventHandler IsActiveChanged;

        private bool _isActive;
        public bool IsActive {
            get { return _isActive; }
            set { SetProperty(ref _isActive, value); }
        }

        public ChildViewModelBase(IEventAggregator eventAggregator) {
            _ea = eventAggregator;
            _ea.GetEvent<InitializeTabbedChildrenEvent>().Subscribe(OnInitializationEventFired);
            IsActiveChanged += (sender, e) => { };
        }

        void OnInitializationEventFired(NavigationParameters parameters) {
            
        }

        public override void OnNavigatingTo(NavigationParameters parameters) {

        }

        public override void Destroy() {
            _ea.GetEvent<InitializeTabbedChildrenEvent>().Unsubscribe(OnInitializationEventFired);
        }
    }
}
