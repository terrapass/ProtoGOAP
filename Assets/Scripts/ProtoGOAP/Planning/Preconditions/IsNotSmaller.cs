using System;

using ProtoGOAP.Planning;

namespace ProtoGOAP.Planning.Preconditions
{
	public sealed class IsNotSmaller : IsInRange//SingleSymbolPrecondition
	{
		//private readonly int targetValue;

		public IsNotSmaller(SymbolId symbolId, int targetValue)
//			: base(symbolId, (value) => value >= targetValue, (value) => Math.Max(0, targetValue - value))
			: base(symbolId, ValueRange.GreaterThanOrEqual(targetValue))
		{
//			this.targetValue = targetValue;
		}

//		public override string ToString()
//		{
//			return string.Format("({0} >= {1})", SymbolId, targetValue);
//		}
	}
}

