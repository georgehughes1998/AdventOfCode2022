using NUnit.Framework;

namespace Day5SupplyStacks;

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
   public static void Rearrange9001_WithTestData_ReturnsExpectedValues()
   {
      IList<CrateStack> expectedResultStack = new List<CrateStack>
      {
         new CrateStack()
            .Push(new Crate('M')),
         new CrateStack()
            .Push(new Crate('C')),
         new CrateStack()
            .Push(new Crate('P'))
            .Push(new Crate('Z'))
            .Push(new Crate('N'))
            .Push(new Crate('D'))
      };

      var testDataCrateStacks = TestDataCrateStacksCreator();

      testDataCrateStacks.Rearrange9001(TestDataInstructions);
      Assert.AreEqual(expectedResultStack, testDataCrateStacks);
   }

   [Test]
   public static void ToString_WithTestCrateStacks_ReturnsExpectedValues()
   {
      Assert.AreEqual("NDP", TestDataCrateStacksCreator().GetTops());
   }
}