namespace Day2RockPaperScissors;

public enum Outcome
{
   Lose,
   Draw,
   Win
}

public static class OutcomeExtensions
{
   public static int ToScore(this Outcome outcome)
   {
      return outcome switch
      {
         Outcome.Lose => 0,
         Outcome.Draw => 3,
         Outcome.Win => 6,
         _ => throw new ArgumentException("Unrecognised outcome" + outcome)
      };
   }
}