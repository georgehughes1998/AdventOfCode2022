using System.Text.RegularExpressions;
using NUnit.Framework;

namespace Day6TuningTrouble;

public class Program
{
   public static void Main(string[] args)
   {
      Console.WriteLine(Reader.Read(args[0]).MarkerAfterStartMessageMarkerIndex());
   }
}

public static class Reader
{
   public static DataStream Read(string filePath)
   {
      return new DataStream(File.ReadAllText(filePath).Trim());
   }
}

public record DataStream(string Buffer);

public static class DataStreamExtensions
{
   private static readonly Regex StartPacketRegex =
      new(@"(\w)(?!\1)(\w)(?!(?:\1|\2))(\w)(?!(?:\1|\2|\3))(\w)(?!(?:\1|\2|\3|\4))");

   private static readonly Regex StartMessageRegex = new(
      @"(\w)(?!\1)(\w)(?!(?:\1|\2))(\w)(?!(?:\1|\2|\3))(\w)(?!(?:\1|\2|\3|\4))(\w)(?!(?:\1|\2|\3|\4|\5))(\w)(?!(?:\1|\2|\3|\4|\5|\6))(\w)(?!(?:\1|\2|\3|\4|\5|\6|\7))(\w)(?!(?:\1|\2|\3|\4|\5|\6|\7|\8))(\w)(?!(?:\1|\2|\3|\4|\5|\6|\7|\8|\9))(\w)(?!(?:\1|\2|\3|\4|\5|\6|\7|\8|\9|\10))(\w)(?!(?:\1|\2|\3|\4|\5|\6|\7|\8|\9|\10|\11))(\w)(?!(?:\1|\2|\3|\4|\5|\6|\7|\8|\9|\10|\11|\12))(\w)(?!(?:\1|\2|\3|\4|\5|\6|\7|\8|\9|\10|\11|\12|\13))");

   public static int MarkerAfterStartPacketMarkerIndex(this DataStream dataStream)
   {
      return dataStream.StartPacketMarkerIndex() + 4;
   }

   public static int MarkerAfterStartMessageMarkerIndex(this DataStream dataStream)
   {
      return dataStream.StartMessageMarkerIndex() + 14;
   }

   private static int StartMessageMarkerIndex(this DataStream dataStream)
   {
      return StartMessageRegex.Match(dataStream.Buffer).Groups[0].Index;
   }

   private static int StartPacketMarkerIndex(this DataStream dataStream)
   {
      return StartPacketRegex.Match(dataStream.Buffer).Groups[0].Index;
   }
}

public static class TuningTroubleTests
{
   [TestCase("mjqjpqmgbljsphdztnvjfqwrcgsmlb", 7)]
   [TestCase("bvwbjplbgvbhsrlpgdmjqwftvncz", 5)]
   [TestCase("nppdvjthqldpwncqszvftbrmjlhg", 6)]
   [TestCase("nznrnfrfntjfmvfwmzdfjlvtqnbhcprsg", 10)]
   [TestCase("zcfzfwzzqfrljwzlrfnpqdbhtmscgvjw", 11)]
   public static void MarkerAfterStartPacketMarkerIndex_WithExamples_ReturnsExpectedIndex(string buffer,
      int expectedIndex)
   {
      Assert.AreEqual(expectedIndex,
         new DataStream(buffer).MarkerAfterStartPacketMarkerIndex());
   }

   [TestCase("mjqjpqmgbljsphdztnvjfqwrcgsmlb", 19)]
   [TestCase("bvwbjplbgvbhsrlpgdmjqwftvncz", 23)]
   [TestCase("nppdvjthqldpwncqszvftbrmjlhg", 23)]
   [TestCase("nznrnfrfntjfmvfwmzdfjlvtqnbhcprsg", 29)]
   [TestCase("zcfzfwzzqfrljwzlrfnpqdbhtmscgvjw", 26)]
   public static void MarkerAfterStartMessageMarkerIndex_WithExamples_ReturnsExpectedIndex(string buffer,
      int expectedIndex)
   {
      Assert.AreEqual(expectedIndex,
         new DataStream(buffer).MarkerAfterStartMessageMarkerIndex());
   }

   [Test]
   public static void Read_WithTestData_ReturnsExpectedData()
   {
      var expectedDataStream = new DataStream("mjqjpqmgbljsphdztnvjfqwrcgsmlb");

      Assert.AreEqual(expectedDataStream,
         Reader.Read(Path.GetDirectoryName(
                        Path.GetDirectoryName(
                           Path.GetDirectoryName(
                              TestContext.CurrentContext.TestDirectory))) +
                     "\\TestData.txt"));
   }
}