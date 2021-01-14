using System.Collections.Generic;
using System.Linq;

namespace Minecraft.Model
{
	public sealed class FilteredMaskMode : MaskMode
	{
		private readonly Dictionary<string, string> _conditions;


		internal FilteredMaskMode()
		{
			_conditions = new();
		}

		private FilteredMaskMode(Dictionary<string, string> conditions)
		{
			_conditions = conditions;
		}

		protected override string GetArgumentTextImpl(Edition edition) => $"filtered [{string.Join(",", _conditions.Select((k, v) => $"{k}={v}"))}]";

		public FilteredMaskMode By(string name, string value)
		{

			return new FilteredMaskMode();
		}

		public FilteredMaskMode By(Dictionary<string, string> conditions)
		{
			Dictionary<string, string> newConditions = new();

			foreach (string key in _conditions.Keys)
				newConditions[key] = _conditions[key];

			return new FilteredMaskMode(newConditions);
		}
	}
}
