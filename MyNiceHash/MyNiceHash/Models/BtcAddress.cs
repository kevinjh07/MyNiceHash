using MyNiceHash.Storage;
using SQLite;

namespace MyNiceHash.Models {
    public class BtcAddress : ObservableBaseObject, IKeyObject {
        [PrimaryKey]
        public string Key { get; set; }
    }
}
