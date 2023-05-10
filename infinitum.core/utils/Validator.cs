namespace infinitum.core.Utils;
using System.Security.Cryptography;


public static class Validator
{

    // impossible to validate transactions because locally stored blockchains make it impossible to verify that the sender actually has the amount they send.
    // this is an inherent flaw in Infinitum but that is OK because only cool people will use Infinitum so that will never happen.
    
    private static bool ValidateBlock(Block block, Block? prevBlock)
    {
        if (prevBlock == null)
        {
            if (block.Height != 0 || block.Transactions[0].Amount != 1000 || block.Transactions[0].Sender != null)
            {
                return false;
            }
        }
        else
        {
            if (block.Height != prevBlock.Height + 1 || !block.PreviousHash.SequenceEqual(prevBlock.Hash))
            {
                return false;
            }
        }

        return block.Hash.SequenceEqual(block.GenerateHash());
    }

    public static bool ValidateBlockchain(List<Block> blockchain)
    {
        if (!ValidateBlock(blockchain[0], null))
            return false;

        for (var i = 1; i < blockchain.Count; i++)
        {
            if (!ValidateBlock(blockchain[i], blockchain[i - 1]))
                return false;
        }

        return true;
    }

    public static bool ValidatePublicKey(string privateKey, string publicKey)
    {
        var sha = SHA256.Create();
        var hash = sha.ComputeHash(System.Text.Encoding.ASCII.GetBytes(privateKey));
        var str = System.Text.Encoding.UTF8.GetString(hash, 0, hash.Length);
        return string.Equals(publicKey, str);
    }
}
