namespace Minecraft.Model
{
	public class SetBlockCommand : Command
	{
		public SetBlockCommand(Coord3 point, Block block)
			: base("setblock")
		{
			Point = point;
			Block = block;
		}


		public Coord3 Point { get; }

		public Block Block { get; }

		public override string CommandText
		{
			get
			{
				string result = $"/setblock {Point.ArgumentText} {Block.ID}";
				if (Block.Data > 0)
					result += $" {Block.Data}";
				return result;
			}
		}
	}
}
