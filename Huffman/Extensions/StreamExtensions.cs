namespace Huffman.Extensions;

public static class StreamExtensions
{
    public static IReadOnlyDictionary<byte, ulong> ReadBytesWithFrequency(this Stream stream)
    {
        var reader = new BinaryReader(stream);
        var bytesWithFrequency = new Dictionary<byte, ulong>();

        while (true)
        {
            byte @byte;

            try
            {
                @byte = reader.ReadByte();
            }
            catch (EndOfStreamException)
            {
                break;
            }

            if (bytesWithFrequency.TryGetValue(@byte, out ulong frequency))
            {
                bytesWithFrequency[@byte] = frequency + 1;
            }
            else
            {
                bytesWithFrequency.Add(@byte, 1);
            }
        }

        return bytesWithFrequency;
    }
}