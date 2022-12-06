namespace Day2RockPaperScissors;

public record Round(Move OpponentMove, Move PlayerMove);

public static class RoundExtensions
{
   public static Outcome ToOutcome(this Round round)
   {
      return round.PlayerMove switch
      {
         Move.Rock => round.OpponentMove switch
         {
            Move.Rock => Outcome.Draw,
            Move.Paper => Outcome.Lose,
            Move.Scissors => Outcome.Win,
            _ => throw new ArgumentException("Unrecognised opponent move " + round.OpponentMove)
         },
         Move.Paper => round.OpponentMove switch
         {
            Move.Rock => Outcome.Win,
            Move.Paper => Outcome.Draw,
            Move.Scissors => Outcome.Lose,
            _ => throw new ArgumentException("Unrecognised opponent move " + round.OpponentMove)
         },
         Move.Scissors => round.OpponentMove switch
         {
            Move.Rock => Outcome.Lose,
            Move.Paper => Outcome.Win,
            Move.Scissors => Outcome.Draw,
            _ => throw new ArgumentException("Unrecognised opponent move " + round.OpponentMove)
         },
         _ => throw new ArgumentException("Unrecognised player move " + round.PlayerMove)
      };
   }

   public static int ToScore(this Round round)
   {
      return round.PlayerMove.ToScore() + round.ToOutcome().ToScore();
   }

   public static int ToScore(this IEnumerable<Round> rounds)
   {
      return rounds.Sum(round => round.ToScore());
   }
}