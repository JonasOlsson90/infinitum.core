using System.Text;

namespace infinitum.core.Utils;

public static class ParsingTools
{
    public static byte[] ConvertHexStringToByteArray(string hexString)
    {
        return Enumerable.Range(0, hexString.Length)
            .Where(x => x % 2 == 0)
            .Select(x => Convert.ToByte(hexString.Substring(x, 2), 16))
            .ToArray();
    }

    public static string ConvertByteArrayToHexString(byte[] bytes)
    {
        var sb = new StringBuilder();

        foreach (var b in bytes)
            sb.Append(b.ToString("x2"));

        return sb.ToString();
    }
}