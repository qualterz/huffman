using System.Text;

namespace Huffman.Extensions;

public static class ArrayExtensions
{
    public static string ToBitString(this bool[] array)
    {
        StringBuilder builder = new();

        foreach (var value in array)
        {
            builder.Append(value ? "1" : "0");
        }

        return builder.ToString();
    }
}