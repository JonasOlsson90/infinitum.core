namespace infinitum.core;

public class Block
{
    public int Height { get; set; }
    public long TimeStamp { get; set; }
    public byte[] PreviousHash { get; set; }
    public byte[] Hash { get; set; }
    public Transaction[] Transactions { get; set; }
}