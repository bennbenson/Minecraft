namespace Minecraft.Model.Bedrock
{
	public abstract class CloneMode
	{
		public static readonly CloneMode Force = new ForceCloneMode();
		public static readonly CloneMode Move = new MoveCloneMode();
		public static readonly CloneMode Normal = new NormalCloneMode();


		private protected CloneMode()
		{
		}


		public abstract string GetArgumentText();
	}
}
