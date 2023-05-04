namespace infinitum.core.Utils;
using System.Security.Cryptography;
using System.Transactions;


public static class Validator
{

    // impossible to validate transactions because locally stored blockchains make it impossible to verify that the sender actually has the amount they send.
    // this is an inherent flaw in Infinitum but that is OK because only cool people will use Infinitum so that will never happen.

    private static bool ValidateHash(Block block)
    {
        var sha = SHA256.Create();
        byte[] timeStamp = BitConverter.GetBytes(block.TimeStamp);

        var transactionHash = Block.SerializeObjects(block.Transactions);

        byte[] headerBytes = new byte[timeStamp.Length + block.PreviousHash.Length + transactionHash.Length];

        Buffer.BlockCopy(timeStamp, 0, headerBytes, 0, timeStamp.Length);
        Buffer.BlockCopy(block.PreviousHash, 0, headerBytes, timeStamp.Length, block.PreviousHash.Length);
        Buffer.BlockCopy(transactionHash, 0, headerBytes, timeStamp.Length + block.PreviousHash.Length, transactionHash.Length);

        byte[] hash = sha.ComputeHash(headerBytes);

        return hash.SequenceEqual(block.Hash);
    }

    private static bool ValidateBlock(Block b, Block? prevB)
    {
        if (prevB == null)
        {
            if (b.Height != 0 || b.Transactions[0].Amount != 1000 || b.Transactions[0].Sender != null)
            {
                return false;
            }
        }
        else
        {
            if (b.Height != prevB.Height + 1 || !b.PreviousHash.SequenceEqual(prevB.Hash))
            {
                return false;
            }
        }

        return ValidateHash(b);
    }

    public static bool ValidateBlockchain(List<Block> c)
    {
        if (!ValidateBlock(c[0], null))
            return false;

        for (int i = 1; i < c.Count; i++)
        {
            if (!ValidateBlock(c[i], c[i - 1]))
                return false;
        }

        return true;
    }

    public static bool ValidatePublicKey(string privateKey, string publicKey)
    {
        var sha = SHA256.Create();
        byte[] hash = sha.ComputeHash(System.Text.Encoding.ASCII.GetBytes(privateKey));
        string str = System.Text.Encoding.UTF8.GetString(hash, 0, hash.Length);
        return String.Equals(publicKey, str);
    }

}
