using System.Text.RegularExpressions;

namespace Day5SupplyStacks;

public static class Reader
{
   private static readonly Regex CrateRegex = new(@" (?<char> ) (?: |$)|\[(?<char>\w)\](?: |$)");
   private static readonly Regex InstructionRegex = new(@"^move (?<move>\d+) from (?<from>\d+) to (?<to>\d+)$");

   public static IList<CrateStack> ReadCrateStacks(string filename)
   {
      return File.ReadLines(filename)
         .Where(line => CrateRegex.IsMatch(line))
         .Select(line =>
            CrateRegex.Matches(line)
               .Select(match => char.Parse(match.Groups["char"].Value))
               .Select(@char => @char.Equals(' ') ? new CrateStack() : new CrateStack().Push(new Crate(@char))))
         .Reverse()
         .Aggregate(
            (firstRow, secondRow) =>
               firstRow.Zip(secondRow)
                  .Select(tuple => tuple.First.Push(tuple.Second)))
         .ToList();
   }

   public static IEnumerable<Instruction> ReadInstructions(string filename)
   {
      return File.ReadLines(filename)
         .Where(line => InstructionRegex.IsMatch(line))
         .Select(line => InstructionRegex.Match(line).Groups)
         .Select(g => new Instruction(
            int.Parse(g["move"].Value),
            int.Parse(g["from"].Value),
            int.Parse(g["to"].Value)));
   }
}