namespace Day2RockPaperScissors;

public static class StringExtensions
{
   public static Move ToMove(this string move, Player player)
   {
      return player switch
         {
            Player.Opponent => move switch
            {
               "A" => Move.Rock,
               "B" => Move.Paper,
               "C" => Move.Scissors,
               _ => throw new ArgumentException("Unrecognised opponent move " + move)
            },
            Player.Player => move switch
            {
               "X" => Move.Rock,
               "Y" => Move.Paper,
               "Z" => Move.Scissors,
               _ => throw new ArgumentException("Unrecognised player move " + move)
            },
            _ => throw new ArgumentException("Unrecognised Player " + player)
         }
         ;
   }
}