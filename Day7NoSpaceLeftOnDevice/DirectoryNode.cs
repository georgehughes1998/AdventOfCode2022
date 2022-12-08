namespace Day7NoSpaceLeftOnDevice;

public class DirectoryNode : INode
{
   private readonly IList<INode> _childNodes;

   public DirectoryNode(string name)
   {
      Name = name;
      _childNodes = new List<INode>();
      Parent = this;
   }

   public string Name { get; }

   public int Size => ChildNodes().Sum(n => n.Size);

   public INode Parent { get; set; }

   public IList<INode> ChildNodes()
   {
      return _childNodes.ToList();
   }

   public DirectoryNode AddChildNode(INode node)
   {
      _childNodes.Add(node);
      node.Parent = this;
      return this;
   }

   public override string ToString()
   {
      return $"Dir({Name}, ChildNodes({ChildNodes().Aggregate(string.Empty, (n1, n2) => $"{n1} {n2}")}))";
   }

   public override bool Equals(object? obj)
   {
      return Equals(obj as DirectoryNode);
   }

   public bool Equals(DirectoryNode? other)
   {
      return other != null &&
             Name == other.Name &&
             ChildNodes().SequenceEqual(other.ChildNodes());
   }


   public override int GetHashCode()
   {
      return HashCode.Combine(Name, ChildNodes()
         .Select(x => x.GetHashCode())
         .Aggregate(HashCode.Combine));
   }
}