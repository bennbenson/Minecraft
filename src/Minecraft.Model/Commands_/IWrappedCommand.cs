namespace Minecraft.Model
{
	public interface IWrappedCommand
	{
		Command InnerCommand { get; }
	}
}
