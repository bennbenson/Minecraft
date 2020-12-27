namespace Minecraft.Model
{
	public interface ICommandFormattable
	{
		string ToCommandString(ICommandFormatter formatter);
	}

	public interface ICommandFormatter
	{
	}
}
