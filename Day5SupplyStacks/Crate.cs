namespace Day5SupplyStacks;

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