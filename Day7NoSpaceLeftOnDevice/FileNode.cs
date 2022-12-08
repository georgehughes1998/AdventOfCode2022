namespace Day7NoSpaceLeftOnDevice;

public class FileNode : INode
{
   public FileNode(string name, int size)
   {
      Name = name;
      Size = size;
      Parent = this;
   }

   public INode Parent { get; set; }

   public string Name { get; }

   public int Size { get; }

   public override string ToString()
   {
      return $"File ({Name}, {Size})";
   }

   public override bool Equals(object? obj)
   {
      return Equals(obj as FileNode);
   }

   public bool Equals(FileNode? other)
   {
      return other != null &&
             Name == other.Name &&
             Size == other.Size;
   }

   public override int GetHashCode()
   {
      return HashCode.Combine(Name, Size);
   }
}