using System;

namespace MyNiceHash.Exceptions {
    public class BtcAddressNullOrEmptyException : Exception {
        public BtcAddressNullOrEmptyException(string message) 
            : base(message) {

        }
    }
}
