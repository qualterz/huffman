using System.CommandLine;
using System.Text;
using Huffman;
using Huffman.Extensions;

static void WriteBuffmanCodes(TextWriter writer, Node rootNode)
{
    var leafNodes = rootNode.Traverse()
                            .Where(x => x.Value != null)
                            .OrderByDescending(x => x.Frequency);

    writer.WriteLine("ASCII\tFrequency\tCode");

    foreach (var node in leafNodes)
    {
        writer.Write(Encoding.ASCII.GetString([node.Value!.Value]));
        writer.Write('\t');
        writer.Write(node.Frequency);
        writer.Write('\t');
        writer.WriteLine(node.ComputeCode().ToBitString());
    }
}

static Node ComputeTreeFromStandardInput()
{
    var stream = Console.OpenStandardInput();

    var frequencyList = stream.ReadBytesWithFrequency()
                              .Where(x => x.Key != Convert.FromHexString("0A").First())
                              .OrderBy(x => x.Value);

    var nodes = new List<Node>(frequencyList.Select(x => new Node
    {
        Value = x.Key,
        Frequency = x.Value
    }));

    return Node.ComputeTree(nodes);
}

var tree = new Lazy<Node>(ComputeTreeFromStandardInput);

var rootCommand = new RootCommand();
var treeNodesCommand = new Command("nodes");
var treeLeavesCommand = new Command("leaves");
var treeDepthCommand = new Command("depth");

rootCommand.SetHandler(() =>
    WriteBuffmanCodes(Console.Out, tree.Value));

treeNodesCommand.SetHandler(() =>
    Console.WriteLine(tree.Value.Traverse().Count()));

treeLeavesCommand.SetHandler(() =>
    Console.WriteLine(tree.Value.Traverse().Count(x => x.Value != null)));

treeDepthCommand.SetHandler(() =>
    Console.WriteLine(tree.Value.Traverse()
        .Where(x => x.Value != null)
        .Max(x => x.TraverseParent().Count())));

rootCommand.AddCommand(treeNodesCommand);
rootCommand.AddCommand(treeLeavesCommand);
rootCommand.AddCommand(treeDepthCommand);

await rootCommand.InvokeAsync(args);