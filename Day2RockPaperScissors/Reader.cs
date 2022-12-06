namespace Day2RockPaperScissors;

public static class Reader
{
   public static IEnumerable<Round> ReadMoves(string filePath)
   {
      return File.ReadAllLines(filePath)
         .ToList()
         .Select(line => line.Split(" "))
         .Select(moves => new Round(moves[0].ToMove(Player.Opponent), moves[1].ToMove(Player.Player)));
   }
}