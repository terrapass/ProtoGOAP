using System;

namespace ProtoGOAP.Planning.Preconditions
{
	public sealed class IsEqual : SingleSymbolPrecondition
	{
		public IsEqual(SymbolId id, int targetValue)
			: base(id, (value) => value == targetValue, (value) => Math.Abs(targetValue - value))
		{
			
		}
	}
}

