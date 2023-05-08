using System.Text.Json;
using System.Text.Json.Serialization;

namespace infinitum.core.Utils;

public class ByteArrayJsonConverter : JsonConverter<byte[]>
{
    public override byte[]? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) =>
        ParsingTools.ConvertHexStringToByteArray(reader.GetString());

    public override void Write(Utf8JsonWriter writer, byte[] value, JsonSerializerOptions options) =>
        writer.WriteStringValue(ParsingTools.ConvertByteArrayToHexString(value));
}