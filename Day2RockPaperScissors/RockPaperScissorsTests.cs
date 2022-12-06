using NUnit.Framework;

namespace Day2RockPaperScissors;

public static class RockPaperScissorsTests
{
   private static readonly List<Round> TestMoves = new()
   {
      new Round(Move.Rock, Move.Paper),
      new Round(Move.Paper, Move.Rock),
      new Round(Move.Scissors, Move.Scissors)
   };

   [Test]
   public static void ReadMoves_WithTestFile_ReturnsCorrectValues()
   {
      Assert.AreEqual(TestMoves,
         Reader.ReadMoves(
            Path.GetDirectoryName(
               Path.GetDirectoryName(
                  Path.GetDirectoryName(
                     TestContext.CurrentContext.TestDirectory))) +
            "\\testDataset.txt"));
   }

   [Test]
   public static void GetTotalScore_WithExamples_ReturnsCorrectValue()
   {
      Assert.AreEqual(15, TestMoves.ToScore());
   }

   [TestCase(Move.Rock, Move.Rock, 4)]
   [TestCase(Move.Rock, Move.Paper, 8)]
   [TestCase(Move.Rock, Move.Scissors, 3)]
   [TestCase(Move.Paper, Move.Rock, 1)]
   [TestCase(Move.Paper, Move.Paper, 5)]
   [TestCase(Move.Paper, Move.Scissors, 9)]
   [TestCase(Move.Scissors, Move.Rock, 7)]
   [TestCase(Move.Scissors, Move.Paper, 2)]
   [TestCase(Move.Scissors, Move.Scissors, 6)]
   public static void GetRoundScore_WithExamples_ReturnsCorrectValue(Move opponentMove, Move playerMove,
      int expectedScore)
   {
      Assert.AreEqual(expectedScore, new Round(opponentMove, playerMove).ToScore());
   }

   [TestCase(Move.Rock, Move.Paper, Outcome.Win)]
   [TestCase(Move.Paper, Move.Rock, Outcome.Lose)]
   [TestCase(Move.Scissors, Move.Scissors, Outcome.Draw)]
   public static void GetRoundOutcome_WithExamples_ReturnsCorrectValue(Move opponentMove, Move playerMove,
      Outcome expectedOutcome)
   {
      Assert.AreEqual(expectedOutcome, new Round(opponentMove, playerMove).ToOutcome());
   }

   [Test]
   public static void ToScore_WithValidPlayerMoves_ReturnsCorrectValue()
   {
      Assert.AreEqual(1, "X".ToScore(Player.Player));
      Assert.AreEqual(2, "Y".ToScore(Player.Player));
      Assert.AreEqual(3, "Z".ToScore(Player.Player));
   }

   [Test]
   public static void ToScore_WithValidOpponentMoves_ReturnsCorrectValue()
   {
      Assert.AreEqual(1, "A".ToScore(Player.Opponent));
      Assert.AreEqual(2, "B".ToScore(Player.Opponent));
      Assert.AreEqual(3, "C".ToScore(Player.Opponent));
   }
}