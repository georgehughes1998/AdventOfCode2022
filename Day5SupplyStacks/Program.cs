using System.Text.RegularExpressions;
using NUnit.Framework;

namespace Day5SupplyStacks;

public class Program
{
   public static void Main(string[] args)
   {
      Console.WriteLine("Hello World.");
   }
}

public static class SupplyStacksTests
{
   private const string TestDataFilePath = "..\\..\\..\\TestData.txt";

   [Test]
   public static void ReadInstructions_WithTestData_ReturnsExpectedValues()
   {
      IEnumerable<Instruction> expectedInstructions = new List<Instruction>
      {
         new(1, 2, 1),
         new(3, 1, 3),
         new(2, 2, 1),
         new(1, 1, 2)
      };

      Assert.AreEqual(expectedInstructions, Reader.ReadInstructions(TestDataFilePath));
   }

   [Test]
   public static void ReadCrateStacks_WithTestData_ReturnsExpectedValues()
   {
      IEnumerable<CrateStack?> expectedCrateStacks = new List<CrateStack?>
      {
         new CrateStack()
            .Push(new Crate('Z'))
            .Push(new Crate('N')),
         new CrateStack()
            .Push(new Crate('M'))
            .Push(new Crate('C'))
            .Push(new Crate('D')),
         new CrateStack()
            .Push(new Crate('P'))
      };

      Assert.AreEqual(expectedCrateStacks, Reader.ReadCrateStacks(TestDataFilePath));
   }
}

public static class Reader
{
   private static readonly Regex CrateRegex = new(@"(?<!\S) (?<char> ) (?!\S)|\[(?<char>\w)\]");
   private static readonly Regex InstructionRegex = new(@"^move (?<move>\d+) from (?<from>\d+) to (?<to>\d+)$");

   public static CrateStack Push(this CrateStack self, CrateStack stack)
   {
      return stack.Count > 0 ? self.Push(stack.Pop()) : self;
   }

   public static IEnumerable<CrateStack> ReadCrateStacks(string filename)
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
                  .Select(tuple => tuple.First.Push(tuple.Second)));
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

public class CrateStack : Stack<Crate>
{
   public override string ToString()
   {
      return Peek().ToString();
   }

   public new CrateStack Push(Crate crate)
   {
      base.Push(crate);
      return this;
   }
}

public class Crate
{
   private readonly char _identifier;

   public Crate(char identifier)
   {
      _identifier = identifier;
   }

   public override string ToString()
   {
      return _identifier.ToString();
   }

   public override bool Equals(object? obj)
   {
      return Equals(obj as Crate);
   }

   public bool Equals(Crate? other)
   {
      return other != null &&
             _identifier == other._identifier;
   }

   public override int GetHashCode()
   {
      return _identifier.GetHashCode();
   }
}

public record Instruction(int Count, int From, int To);