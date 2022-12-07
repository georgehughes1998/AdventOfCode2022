namespace Day5SupplyStacks;

public class CrateStack : Stack<Crate>
{
   public new CrateStack Push(Crate crate)
   {
      base.Push(crate);
      return this;
   }
}