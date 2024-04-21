namespace Huffman;

public record Node
{
    public byte? Value { get; set; }
    public ulong Frequency { get; set; }
    public Node? Parent { get; set; }
    public IList<Node>? Childs { get; set; }

    public static Node ComputeTree(IEnumerable<Node> nodes)
    {
        var resultNodes = nodes.OrderBy(x => x.Frequency).ToList();

        while (resultNodes.Count != 1)
        {
            var left = resultNodes[0];
            var right = resultNodes[1];

            resultNodes.Remove(left);
            resultNodes.Remove(right);

            var newNode = new Node()
            {
                Childs = [left, right],
                Frequency = left.Frequency + right.Frequency,
            };

            left.Parent = newNode;
            right.Parent = newNode;

            int insertIndex = 0;

            var frequencyLargerIndex = resultNodes.FindIndex(x => x.Frequency > newNode.Frequency);

            if (frequencyLargerIndex == -1)
                insertIndex = resultNodes.Count;
            else
                insertIndex = frequencyLargerIndex;

            resultNodes.Insert(insertIndex, newNode);
        }

        return resultNodes.First();
    }

    public IEnumerable<Node> Traverse()
    {
        if (Childs != null)
        {
            foreach (var child in Childs!)
            {
                yield return child;

                foreach (var deepChild in child.Traverse())
                {
                    yield return deepChild;
                }
            }
        }
    }

    public IEnumerable<Node> TraverseParent()
    {
        if (Parent != null)
        {
            yield return Parent;

            foreach (var deepParent in Parent.TraverseParent())
            {
                yield return deepParent;
            }
        }
    }

    public bool[] ComputeCode()
    {
        var bitsResult = new List<bool>();

        Node? current = this;

        while (current.Parent != null && current.Parent.Childs != null)
        {
            var currentNodeIndex = current.Parent.Childs.IndexOf(current);

            bitsResult.Add(currentNodeIndex != 0);

            current = current.Parent;
        }

        bitsResult.Reverse();

        return [.. bitsResult];
    }
}