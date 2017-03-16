using System;

namespace ProtoGOAP.Planning.Preconditions
{
	public sealed class IsNotGreater : SingleSymbolPrecondition
	{
		public IsNotGreater(SymbolId symbolId, int targetValue)
			: base(symbolId, (value) => value <= targetValue, (value) => Math.Max(0, value - targetValue))
		{
			
		}
	}
}

