namespace Day6TuningTrouble;

public class Program
{
   public static void Main(string[] args)
   {
      var dataStream = Reader.Read(args[0]);
      Console.WriteLine(dataStream.MarkerAfterStartPacketMarkerIndex());
      Console.WriteLine(dataStream.MarkerAfterStartMessageMarkerIndex());
   }
}