using System;
using System.Collections.Generic;
using Minecraft.Model;
using Minecraft.Model.Java;

namespace Minecraft.Construction.Java
{
	internal static class CommandSequenceExtensions
	{
		private static readonly int[] TranslateIdentity = new int[] { 1, 0, 0, 1 };
		private static readonly int[] TranslateRisingDiagonal = new int[] { 0, 1, 1, 0 };
		private static readonly int[] TranslateFallingDiagonal = new int[] { -1, 0, 0, -1 };
		private static readonly int[] TranslateXAxis = new int[] { -1, 0, 0, 1 };
		private static readonly int[] TranslateZAxis = new int[] { 1, 0, 0, -1 };
		private static readonly int[] TranslateRotate90 = new int[] { 0, -1, 1, 0 };
		private static readonly int[] TranslateRotate180 = new int[] { -1, 0, 0, -1 };
		private static readonly int[] TranslateRotate270 = new int[] { 0, 1, -1, 0 };


		public static IEnumerable<Command> TranslateAndMove(this IEnumerable<Command> commands, Translation translation, Coord3 offset)
		{
			if (commands is null)
				throw new ArgumentNullException(nameof(commands));

			int[] matrix = translation switch
			{
				Translation.RisingDiagonal => TranslateRisingDiagonal,
				Translation.FallingDiagonal => TranslateFallingDiagonal,
				Translation.XAxis => TranslateXAxis,
				Translation.ZAxis => TranslateZAxis,
				Translation.Rotate90 => TranslateRotate90,
				Translation.Rotate180 => TranslateRotate180,
				Translation.Rotate270 => TranslateRotate270,
				Translation.Identity or _ => TranslateIdentity
			};

			return TranslateAndMoveInternal(commands, matrix, offset);
		}

		private static IEnumerable<Command> TranslateAndMoveInternal(IEnumerable<Command> commands, int[] matrix, Coord3 offset)
		{
			foreach (Command command in commands)
				yield return TranslateAndMoveCommand(command, matrix, offset);
		}

		private static Command TranslateAndMoveCommand(Command command, int[] matrix, Coord3 offset)
		{
			return command switch
			{
				IWrappedCommand wrappedCommand => TranslateAndMoveCommand(wrappedCommand.InnerCommand, matrix, offset),
				FillCommand fillCommand => TranslateAndMoveFillCommand(fillCommand, matrix, offset),
				SetBlockCommand setBlockCommand => TranslateAndMoveSetBlockCommand(setBlockCommand, matrix, offset),
				_ => command,
			};
		}


		private static FillCommand TranslateAndMoveFillCommand(FillCommand command, int[] translationMatrix, Coord3 offset)
		{
			return new FillCommand(
				Move(TranslateXZ(command.From, translationMatrix), offset),
				Move(TranslateXZ(command.To, translationMatrix), offset),
				command.Block,
				command.FillMode
				);
		}

		private static SetBlockCommand TranslateAndMoveSetBlockCommand(SetBlockCommand command, int[] translationMatrix, Coord3 offset)
		{
			return new SetBlockCommand(
				Move(TranslateXZ(command.Position, translationMatrix), offset),
				command.Block
				);
		}










		private static Position Move(Position value, Coord3 offset)
		{
			return new Position(
				new PositionValue(value.X.Type, value.X.Value + offset.X),
				new PositionValue(value.Y.Type, value.Y.Value + offset.Y),
				new PositionValue(value.Z.Type, value.Z.Value + offset.Z)
				);
		}

		public static Position TranslateXZ(Position position, int[] matrix)
		{
			if (matrix == TranslateIdentity)  // Depends on reference equality!
				return position;

			// Matrix structure:
			//   [ matrix[0] matrix[1] ]
			//   [ matrix[2] matrix[3] ]

			return new Position(
				new PositionValue(position.X.Type, position.X.Value * matrix[0] + position.Z.Value * matrix[2]),
				position.Y,
				new PositionValue(position.Z.Type, position.X.Value * matrix[1] + position.Z.Value * matrix[3])
				);
		}

	}
}
