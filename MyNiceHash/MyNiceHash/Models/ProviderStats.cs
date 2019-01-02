namespace MyNiceHash.Models {
    public class ProviderStats {
        public string Balance { get; set; }
        public AlgorithmType Algorithm { get; set; }
        public string AcceptedSpeed { get; set; }
        public string RejectedSpeed { get; set; }
    }
}
