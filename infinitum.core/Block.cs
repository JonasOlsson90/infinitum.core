using System.Security.Cryptography;
using System.Text.Json;
using System.Text.Json.Serialization;
using infinitum.core.Utils;

namespace infinitum.core;

public class Block
{
    public int Height { get; set; }
    public long TimeStamp { get; set; }
    [JsonConverter(typeof(ByteArrayJsonConverter))]
    public byte[] PreviousHash { get; set; }
    [JsonConverter(typeof(ByteArrayJsonConverter))]
    public byte[] Hash { get; set; }
    public List<Transaction> Transactions { get; set; }

    public Block(int height, byte[] previousHash, List<Transaction> transactions)
    {
        Height = height;
        TimeStamp = DateTime.Now.Ticks;
        PreviousHash = previousHash;
        Transactions = transactions;
        Hash = GenerateHash();
    }

    public byte[] GenerateHash()
    {
        var timeStamp = BitConverter.GetBytes(TimeStamp);

        var transactionHash = SerializeObjects(Transactions);

        var headerBytes = new byte[timeStamp.Length + PreviousHash.Length + transactionHash.Length];

        Buffer.BlockCopy(timeStamp, 0, headerBytes, 0, timeStamp.Length);
        Buffer.BlockCopy(PreviousHash, 0, headerBytes, timeStamp.Length, PreviousHash.Length);
        Buffer.BlockCopy(transactionHash, 0, headerBytes, timeStamp.Length + PreviousHash.Length, transactionHash.Length);

        var hash = SHA256.HashData(headerBytes);

        return hash;
    }

    private static byte[] SerializeObjects(List<Transaction> t)
    {
        var json = JsonSerializer.Serialize(t);
        return System.Text.Encoding.UTF8.GetBytes(json);
    }
}
