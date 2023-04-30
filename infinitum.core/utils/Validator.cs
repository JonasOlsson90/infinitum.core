namespace infinitum.core.Utils;
using System.Security.Cryptography;


public class Validator
{

    // impossible to validate transactions because locally stored blockchains make it impossible to verify that the sender actually has the amount they send.
    // this is an inherent flaw in Infinitum but that is OK because only cool people will use Infinitum so that will never happen.

    public bool ValidateBlock(Block b, Block? prevB)
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
            if (b.Height != prevB.Height + 1 || b.PreviousHash != prevB.Hash)
            {
                return false;
            }
        }

        return true;
    }

    public bool ValidateBlockchain(List<Block> c)
    {
        for (int i = 1; i < c.Count; i++)
        {
            if (!ValidateBlock(c[i], c[i - 1]))
            {
                return false;
            }
        }

        return true;
    }

    public bool ValidatePublicKey(string privateKey, string publicKey)
    {
        var sha = SHA256.Create();
        byte[] hash = sha.ComputeHash(System.Text.Encoding.ASCII.GetBytes(privateKey));
        string str = System.Text.Encoding.UTF8.GetString(hash, 0, hash.Length);
        return String.Equals(publicKey, str);
    }

}
