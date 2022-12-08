namespace Day7NoSpaceLeftOnDevice;

public class Program
{
   public static void Main(string[] args)
   {
      Console.WriteLine(args[0].ReadAndParse().SumOfDirectoriesSmallerThan100000());
      Console.WriteLine(args[0].ReadAndParse().SizeOfSmallestDirectoryToFreeUpEnoughSpace());
   }
}