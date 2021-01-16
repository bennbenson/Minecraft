using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Minecraft.Model
{
	[Serializable]
	public class CommandTextException : Exception
	{
		public CommandTextException()
		{
		}

		public CommandTextException(string message)
			: base(message)
		{
		}

		public CommandTextException(string message, Exception innerException)
			: base(message, innerException)
		{
		}


		#region ISerializable members
		protected CommandTextException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}


		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
		#endregion
	}

	[Serializable]
	public class CommandTextEditionException : Exception
	{
		public CommandTextEditionException()
		{
		}

		public CommandTextEditionException(string message)
			: base(message)
		{
		}

		public CommandTextEditionException(string message, Exception innerException)
			: base(message, innerException)
		{
		}


		#region ISerializable members
		protected CommandTextEditionException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}


		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
		#endregion
	}
}
