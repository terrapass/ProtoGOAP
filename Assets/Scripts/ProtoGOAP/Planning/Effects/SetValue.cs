using System;

using ProtoGOAP.Planning;

namespace ProtoGOAP.Planning.Effects
{
	public sealed class SetValue : SingleSymbolEffect
	{
		public SetValue(SymbolId symbolId, int newValue)
			: base(symbolId, (initialValue) => newValue)
		{
			
		}
	}
}

