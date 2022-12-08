using NUnit.Framework;

namespace Day7NoSpaceLeftOnDevice;

public class NoSpaceLeftOnDeviceTests
{
   private static readonly string TestDataFilePath = "..\\..\\..\\TestData.txt";

   private static readonly DirectoryNode TestDirectoryStructure = new DirectoryNode("/")
      .AddChildNode(
         new DirectoryNode("a")
            .AddChildNode(
               new DirectoryNode("e")
                  .AddChildNode(new FileNode("i", 584)))
            .AddChildNode(new FileNode("f", 29116))
            .AddChildNode(new FileNode("g", 2557))
            .AddChildNode(new FileNode("h.lst", 62596)))
      .AddChildNode(new FileNode("b.txt", 14848514))
      .AddChildNode(new FileNode("c.dat", 8504156))
      .AddChildNode(
         new DirectoryNode("d")
            .AddChildNode(new FileNode("j", 4060174))
            .AddChildNode(new FileNode("d.log", 8033020))
            .AddChildNode(new FileNode("d.ext", 5626152))
            .AddChildNode(new FileNode("k", 7214296)));

   [Test]
   public static void Size_OfTestDirectoryStructure_ReturnsExpectedValue()
   {
      Assert.AreEqual(48381165, TestDirectoryStructure.Size);
   }

   [Test]
   public static void Read_WithTestData_ReturnsExpectedValue()
   {
      Assert.AreEqual(TestDirectoryStructure, TestDataFilePath.ReadAndParse());
   }

   [Test]
   public static void Equals_TwoFileNodesWithSameNameAndSize_AreEqual()
   {
      Assert.AreEqual(new FileNode("A", 12), new FileNode("A", 12));
   }

   private static DirectoryNode DirectoryCreator()
   {
      return new DirectoryNode("/")
         .AddChildNode(new DirectoryNode("a")
            .AddChildNode(new DirectoryNode("e")
               .AddChildNode(new FileNode("i", 584)))
            .AddChildNode(new FileNode("f", 29116)));
   }

   [Test]
   public static void Equals_TwoDirectoriesWithSameFilesAndStructure_AreEqual()
   {
      Assert.AreEqual(DirectoryCreator(), DirectoryCreator());
   }

   [Test]
   public static void Equals_TwoDirectoriesWithDifferentFilesAndStructure_AreNotEqual()
   {
      Assert.AreNotEqual(DirectoryCreator(), DirectoryCreator().AddChildNode(new FileNode("ne", 64)));
   }

   [Test]
   public static void SumOfDirectoriesSmallerThan100000_DirectoriesWithSizeThreshold_ReturnsExpectedValues()
   {
      Assert.AreEqual(95437,
         TestDirectoryStructure.SumOfDirectoriesSmallerThan100000());
   }

   [Test]
   public static void SizeOfSmallestDirectoryToFreeUpEnoughSpace_DirectoriesWithSizeThreshold_ReturnsExpectedValues()
   {
      Assert.AreEqual(24933642,
         TestDirectoryStructure.SizeOfSmallestDirectoryToFreeUpEnoughSpace());
   }
}