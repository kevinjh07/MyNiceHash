using MyNiceHash.Models;
using MyNiceHash.Storage;
using System.Collections.ObjectModel;
using MyNiceHash.Exceptions;

namespace MyNiceHash.Services {
    public class BtcAddressService : IBtcAddressService {
        public void Save(BtcAddress btcAddress) {
            var dbManager = new DatabaseManager();
            if (string.IsNullOrEmpty(btcAddress.Key) || string.IsNullOrWhiteSpace(btcAddress.Key)) {
                throw new BtcAddressNullOrEmptyException("Please enter an BTC address!");
            }
            if (dbManager.GetItem<BtcAddress>(btcAddress.Key) != null) {
                throw new BtcAddressAlreadyRegisteredException("The informed BTC address is already registered!");
            }
            dbManager.SaveValue(btcAddress);
        }

        public ObservableCollection<BtcAddress> GetBtcAddresses() {
            var dbManager = new DatabaseManager();
            return new ObservableCollection<BtcAddress>(dbManager.GetAllItems<BtcAddress>());
        }
    }
}
