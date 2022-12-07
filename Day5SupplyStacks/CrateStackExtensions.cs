namespace Day5SupplyStacks;

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

   public static IList<CrateStack> Rearrange9001(this IList<CrateStack> stacks, IEnumerable<Instruction> instructions)
   {
      instructions.ToList().ForEach(instruction => stacks.Rearrange9001(instruction));
      return stacks;
   }

   public static IList<CrateStack> Rearrange9001(this IList<CrateStack> stacks, Instruction instruction)
   {
      var tmp = new CrateStack();
      for (var i = 0; i < instruction.Count; i++) tmp.Push(stacks[instruction.From - 1].Pop());
      for (var i = 0; i < instruction.Count; i++) stacks[instruction.To - 1].Push(tmp.Pop());
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