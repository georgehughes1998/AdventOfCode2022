namespace Day7NoSpaceLeftOnDevice;

public static class TerminalOutputReaderExtensions
{
   public static DirectoryNode ReadAndParse(this string filename)
   {
      return TerminalOutputReader.Parse(TerminalOutputReader.Read(filename));
   }
}