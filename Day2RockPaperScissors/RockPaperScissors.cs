namespace Day2RockPaperScissors;

public class RockPaperScissors
{
   public static int Main(string[] args)
   {
      if (args.Length != 1)
      {
         Console.Error.WriteLine("Incorrect number of arguments for program");
         return 1;
      }

      var filePath = args[0];

      if (!File.Exists(filePath))
      {
         Console.Error.WriteLine("File not found: " + filePath);
         return 1;
      }

      Console.WriteLine("Total Score: " + Reader.ReadMoves(filePath).ToScore());
      return 0;
   }
}