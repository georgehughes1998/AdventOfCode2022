namespace Day7NoSpaceLeftOnDevice;

public interface INode
{
   string Name { get; }

   int Size { get; }

   INode Parent { get; set; }
}