using MyNiceHash.Models;
using System.Collections.ObjectModel;

namespace MyNiceHash.Services {
    public interface IBtcAddressService {
        void Save(BtcAddress btcAddress);
        ObservableCollection<BtcAddress> GetBtcAddresses();
    }
}
