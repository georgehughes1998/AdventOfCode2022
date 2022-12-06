namespace Day2RockPaperScissors;

public enum Move
{
   Rock,
   Paper,
   Scissors
}

public static class MoveExtensions
{
   public static int ToScore(this Move move)
   {
      return move switch
      {
         Move.Rock => 1,
         Move.Paper => 2,
         Move.Scissors => 3,
         _ => throw new ArgumentException("Unrecognised move" + move)
      };
   }

   public static int ToScore(this string move, Player player)
   {
      return move.ToMove(player).ToScore();
   }
}