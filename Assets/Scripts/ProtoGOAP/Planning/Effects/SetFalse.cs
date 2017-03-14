using System;

namespace ProtoGOAP.Planning.Effects
{
	public sealed class SetFalse : SingleSymbolEffect
	{
		public SetFalse(SymbolId symbolId)
			: base(symbolId, (initialValue) => 0)
		{
			
		}
	}
}

