using NUnit.Framework;

namespace Day6TuningTrouble;

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