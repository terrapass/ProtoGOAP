using System;

using ProtoGOAP.Planning;

namespace ProtoGOAP.Planning.Preconditions
{
	public sealed class IsNotSmaller : SingleSymbolPrecondition
	{
		public IsNotSmaller(SymbolId symbolId, int targetValue)
			: base(symbolId, (value) => value >= targetValue, (value) => Math.Max(0, targetValue - value))
		{
		}
	}
}

