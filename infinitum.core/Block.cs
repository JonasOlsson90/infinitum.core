using System.Security.Cryptography;
using System;
using System.Collections.Generic;
using System.Text.Json;

namespace infinitum.core;

public class Block
{
    public int Height { get; set; }
    public long TimeStamp { get; set; }
    public byte[] PreviousHash { get; set; }
    public byte[] Hash { get; set; }
    public List<Transaction> Transactions { get; set; }

    public Block(int height, byte[] previousHash, List<Transaction> transactions)
    {
        Height = height;
        TimeStamp = DateTime.Now.Ticks;
        PreviousHash = previousHash;
        Hash = GenerateHash();
        Transactions = transactions;
    }

    private byte[] GenerateHash()
    {
        var sha = SHA256.Create();
        byte[] timeStamp = BitConverter.GetBytes(TimeStamp);

        var transactionHash = SerializeObjects(Transactions);

        byte[] headerBytes = new byte[timeStamp.Length + PreviousHash.Length + transactionHash.Length];

        Buffer.BlockCopy(timeStamp, 0, headerBytes, 0, timeStamp.Length);
        Buffer.BlockCopy(PreviousHash, 0, headerBytes, timeStamp.Length, PreviousHash.Length);
        Buffer.BlockCopy(transactionHash, 0, headerBytes, timeStamp.Length + PreviousHash.Length, transactionHash.Length);

        byte[] hash = sha.ComputeHash(headerBytes);

        return hash;
    }

    public static byte[] SerializeObjects(List<Transaction> t)
    {
        string json = JsonSerializer.Serialize(t);
        return System.Text.Encoding.UTF8.GetBytes(json);
    }
}
