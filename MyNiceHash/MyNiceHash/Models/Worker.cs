namespace MyNiceHash.Models {
    public class Worker {
        public string Name { get; set; }
        public string AcceptedSpeed { get; set; }
        public string RejectedTargetSpeed { get; set; }
        public string RejectedStaleSpeed { get; set; }
        public string RejectedDuplicateSpeed { get; set; }
        public string RejectedOtherSpeed { get; set; }
        public string TimeConnected { get; set; }
        public string XnSub { get; set; }
        public string Difficulty { get; set; }
        public string Location { get; set; }
    }
}
