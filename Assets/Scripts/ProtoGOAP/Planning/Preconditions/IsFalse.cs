 using System;

using ProtoGOAP.Planning;

namespace ProtoGOAP.Planning.Preconditions
{
	public sealed class IsFalse : SingleSymbolPrecondition
	{
		public IsFalse(SymbolId symbolId)
			: base(symbolId, (value) => value == 0, (value) => (value != 0) ? 1.0 : 0.0)
		{
			
		}
	}
}

