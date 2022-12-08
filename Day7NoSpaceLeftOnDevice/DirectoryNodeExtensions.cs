namespace Day7NoSpaceLeftOnDevice;

public static class DirectoryNodeExtensions
{
   public static INode Find(this DirectoryNode directoryNode, string name)
   {
      return directoryNode.ChildNodes().Single(node => node.Name == name);
   }

   public static IEnumerable<INode> WhereRecursive(this DirectoryNode rootNode,
      Func<INode, bool> conditionFunc)
   {
      return rootNode.ChildNodes()
         .Where(conditionFunc)
         .Concat(rootNode
            .ChildNodes()
            .OfType<DirectoryNode>()
            .SelectMany(n => n.WhereRecursive(conditionFunc)));
   }

   public static int SumOfDirectoriesSmallerThan100000(this DirectoryNode rootNode)
   {
      return rootNode
         .WhereRecursive(n => n.Size < 100000)
         .OfType<DirectoryNode>()
         .Sum(n => n.Size);
   }

   public static int SizeOfSmallestDirectoryToFreeUpEnoughSpace(this DirectoryNode rootNode)
   {
      return rootNode
         .WhereRecursive(n => n.Size > 30000000 - (70000000 - rootNode.Size))
         .OfType<DirectoryNode>()
         .Min(n => n.Size);
   }
}