namespace Day5SupplyStacks;

public class Program
{
   public static void Main(string[] args)
   {
      Console.WriteLine(Reader
         .ReadCrateStacks(args[0])
         .Rearrange(Reader.ReadInstructions(args[0]))
         .GetTops());

      Console.WriteLine(Reader
         .ReadCrateStacks(args[0])
         .Rearrange9001(Reader.ReadInstructions(args[0]))
         .GetTops());
   }
}