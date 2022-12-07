using System.Text.RegularExpressions;
using NUnit.Framework;

namespace Day5SupplyStacks;

public class Program
{
   public static void Main(string[] args)
   {
      Console.WriteLine(Reader
         .ReadCrateStacks(args[0])
         .Rearrange(Reader.ReadInstructions(args[0]))
         .GetTops());
   }
}

public static class SupplyStacksTests
{
   private const string TestDataFilePath = "..\\..\\..\\TestData.txt";

   private static readonly IEnumerable<Instruction> TestDataInstructions = new List<Instruction>
   {
      new(1, 2, 1),
      new(3, 1, 3),
      new(2, 2, 1),
      new(1, 1, 2)
   };

   private static readonly Func<IList<CrateStack>> TestDataCrateStacksCreator = () => new List<CrateStack>
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

   [Test]
   public static void ReadInstructions_WithTestData_ReturnsExpectedValues()
   {
      Assert.AreEqual(TestDataInstructions, Reader.ReadInstructions(TestDataFilePath));
   }

   [Test]
   public static void ReadCrateStacks_WithTestData_ReturnsExpectedValues()
   {
      Assert.AreEqual(TestDataCrateStacksCreator(), Reader.ReadCrateStacks(TestDataFilePath));
   }

   [Test]
   public static void Rearrange_WithTestData_ReturnsExpectedValues()
   {
      IList<CrateStack> expectedResultStack = new List<CrateStack>
      {
         new CrateStack()
            .Push(new Crate('C')),
         new CrateStack()
            .Push(new Crate('M')),
         new CrateStack()
            .Push(new Crate('P'))
            .Push(new Crate('D'))
            .Push(new Crate('N'))
            .Push(new Crate('Z'))
      };

      var testDataCrateStacks = TestDataCrateStacksCreator();

      testDataCrateStacks.Rearrange(TestDataInstructions);
      Assert.AreEqual(expectedResultStack, testDataCrateStacks);
   }

   [Test]
   public static void ToString_WithTestCrateStacks_ReturnsExpectedValues()
   {
      Assert.AreEqual("NDP", TestDataCrateStacksCreator().GetTops());
   }
}

public static class CrateStackExtensions
{
   public static IList<CrateStack> Rearrange(this IList<CrateStack> stacks, Instruction instruction)
   {
      for (var i = 0; i < instruction.Count; i++) stacks[instruction.To - 1].Push(stacks[instruction.From - 1].Pop());
      return stacks;
   }

   public static IList<CrateStack> Rearrange(this IList<CrateStack> stacks, IEnumerable<Instruction> instructions)
   {
      instructions.ToList().ForEach(instruction => stacks.Rearrange(instruction));
      return stacks;
   }

   public static CrateStack Push(this CrateStack self, CrateStack stack)
   {
      return stack.Count > 0 ? self.Push(stack.Pop()) : self;
   }

   public static string GetTops(this IList<CrateStack> stacks)
   {
      return stacks.Select(s => s.Peek().Identifier).Aggregate(string.Empty, (s1, s2) => s1 + s2);
   }
}

public static class Reader
{
   private static readonly Regex CrateRegex = new(@" (?<char> ) (?: |$)|\[(?<char>\w)\](?: |$)");
   private static readonly Regex InstructionRegex = new(@"^move (?<move>\d+) from (?<from>\d+) to (?<to>\d+)$");

   public static IList<CrateStack> ReadCrateStacks(string filename)
   {
      var a = File.ReadLines(filename)
         .Where(line => CrateRegex.IsMatch(line))
         .Select(line =>
            CrateRegex.Matches(line));

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

public class CrateStack : Stack<Crate>
{
   public new CrateStack Push(Crate crate)
   {
      base.Push(crate);
      return this;
   }
}

public class Crate
{
   public Crate(char identifier)
   {
      Identifier = identifier;
   }

   public char Identifier { get; }

   public override bool Equals(object? obj)
   {
      return Equals(obj as Crate);
   }

   public bool Equals(Crate? other)
   {
      return other != null &&
             Identifier == other.Identifier;
   }

   public override int GetHashCode()
   {
      return Identifier.GetHashCode();
   }
}

public record Instruction(int Count, int From, int To);