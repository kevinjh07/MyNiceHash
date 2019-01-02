using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MyNiceHash.Models {
    public class ObservableBaseObject : INotifyPropertyChanged {
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        public void RaisePropertyChanged([CallerMemberName]string propertyName = null) {
            if (!string.IsNullOrWhiteSpace(propertyName))
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
