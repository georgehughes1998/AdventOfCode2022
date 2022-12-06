using System.Text.RegularExpressions;

namespace Day6TuningTrouble;

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