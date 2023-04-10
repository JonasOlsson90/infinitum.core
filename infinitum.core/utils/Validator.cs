namespace infinitum.core.Utils;
using System.Security.Cryptography;


public class Validator
{
    public bool ValidateBlock(Block b)
    {
        return true;
    }

    public bool ValidateBlockchain(List<Block> c)
    {
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