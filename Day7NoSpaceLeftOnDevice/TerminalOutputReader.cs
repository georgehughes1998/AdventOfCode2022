using System.Text.RegularExpressions;

namespace Day7NoSpaceLeftOnDevice;

public static class TerminalOutputReader
{
   private static readonly Regex CommandRegex =
      new(
         @"\$ cd (?<cd>.+)|\$ (?<ls>ls)|dir (?<dir>.+)|(?<size>\d+) (?<filename>.+)");

   public static IEnumerable<TerminalItem> Read(string filename)
   {
      return CommandRegex.Matches(File.ReadAllText(filename))
         .Select(m => m.Groups)
         .Select<GroupCollection, TerminalItem>(g =>
         {
            if (g["cd"].Success)
               return new CdTerminalItem(g["cd"].Value.TrimEnd('\n', '\r'));
            if (g["ls"].Success)
               return new LsTerminalItem();
            if (g["dir"].Success)
               return new DirTerminalItem(g["dir"].Value.TrimEnd('\n', '\r'));
            if (g["size"].Success && g["filename"].Success)
               return new FileTerminalItem(g["filename"].Value.TrimEnd('\n', '\r'), int.Parse(g["size"].Value));
            throw new ArgumentException("Unrecognised terminal item");
         });
   }

   public static DirectoryNode Parse(IEnumerable<TerminalItem> commands)
   {
      var rootNode = new DirectoryNode("/");
      var targetNode = rootNode;

      foreach (var command in commands)
         switch (command)
         {
            case CdTerminalItem cdTerminalCommand:
               targetNode = cdTerminalCommand.Directory switch
               {
                  "/" => rootNode,
                  ".." => targetNode.Parent as DirectoryNode ??
                          throw new ArgumentException($"No directories found with name {cdTerminalCommand.Directory}"),
                  _ => targetNode.Find(cdTerminalCommand.Directory) as DirectoryNode ??
                       throw new ArgumentException($"No directories found with name {cdTerminalCommand.Directory}")
               };
               break;
            case LsTerminalItem:
               break;

            case DirTerminalItem dirTerminalItem:
               targetNode.AddChildNode(
                  new DirectoryNode(dirTerminalItem.Directory));
               break;
            case FileTerminalItem fileTerminalItem:
               targetNode.AddChildNode(
                  new FileNode(fileTerminalItem.FileName, fileTerminalItem.Size));
               break;
            default:
               throw new ArgumentException("Unrecognised command");
         }

      return rootNode;
   }
}