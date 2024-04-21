using Huffman.Extensions;

namespace Huffman.Tests;

public class NodeTests
{
    [Fact]
    public void ComputeTree_ComputesCorrectly()
    {
        var stream = new MemoryStream("A_DEAD_DAD_CEDED_A_BAD_BABE_A_BEADED_ABACA_BED"u8.ToArray());

        var bytesFrequency = stream.ReadBytesWithFrequency();

        var nodes = new List<Node>(bytesFrequency.Select(x => new Node
        {
            Value = x.Key,
            Frequency = x.Value
        }));

        var root = Node.ComputeTree(nodes);

        var cByte = root.Traverse().First(x => x.Value == 'C');
        var dByte = root.Traverse().First(x => x.Value == 'D');

        Assert.Equal([true, true, true, false], cByte.ComputeCode());
        Assert.Equal([false, true], dByte.ComputeCode());
    }
}