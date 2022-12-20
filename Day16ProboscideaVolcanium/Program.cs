using NUnit.Framework;

namespace Day16ProboscideaVolcanium;

public class Program
{
   public static void Main(string[] args)
   {
      Console.WriteLine("Hello World");
   }
}

public record Node(string Name, int Value);

public record Edge(Node A, Node B);

public record Graph(Node Start, IEnumerable<Edge> Edges);

public record Adjacency(Node A, Node B, int Score);

public static class GraphExtensions
{
   public static IEnumerable<Adjacency> DepthFirstSearch(this Graph graph)
   {
      Stack<Node> nodeStack = new();
      List<Node> discovered = new();
      nodeStack.Push(graph.Start);

      while (nodeStack.Count > 0)
      {
         var currentNode = nodeStack.Pop();
         if (discovered.Contains(currentNode))
            continue;
         discovered.Add(currentNode);
         foreach (var (_, bNode) in graph.Edges.Where(edge => edge.A == currentNode)) nodeStack.Push(bNode);
      }
   }
}

public class NoSpaceLeftOnDeviceTests
{
   private static readonly string TestDataFilePath = "..\\..\\..\\TestData.txt";

   [Test]
   public static void FindMaxPressure_WithTestData_IsExpectedValue()
   {
      Assert.AreEqual(1651, null);
   }
}