namespace infinitum.core
{
    public class Transaction
    {
        public string Sender { get; set; }
        public string Recipient { get; set; }
        public decimal Amount { get; set; }
        public long TimeStamp { get; set; }
    }
}