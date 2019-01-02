using System;

namespace MyNiceHash.Exceptions {
    public class BtcAddressAlreadyRegisteredException : Exception {
        public BtcAddressAlreadyRegisteredException(string message) 
            : base(message) {

        }
    }
}
