using System;

using ProtoGOAP.Planning;

namespace ProtoGOAP.Planning.Effects
{
	public sealed class Add : SingleSymbolEffect
	{
		public Add(SymbolId symbolId, int difference)
			: base(symbolId, (initialValue) => initialValue + difference)
		{
			
		}
	}
}

