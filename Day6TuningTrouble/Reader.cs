namespace Day6TuningTrouble;

public static class Reader
{
   public static DataStream Read(string filePath)
   {
      return new DataStream(File.ReadAllText(filePath).Trim());
   }
}