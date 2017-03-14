using System;

using ProtoGOAP.Planning;

namespace ProtoGOAP.Planning.Effects
{
	public sealed class Subtract : SingleSymbolEffect
	{
		public Subtract(SymbolId symbolId, int difference)
			: base(symbolId, (initialValue) => initialValue - difference)
		{
			
		}
	}
}

