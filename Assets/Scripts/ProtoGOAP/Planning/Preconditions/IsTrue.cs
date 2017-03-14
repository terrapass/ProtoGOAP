using System;

using ProtoGOAP.Planning;

namespace ProtoGOAP.Planning.Preconditions
{
	public sealed class IsTrue : SingleSymbolPrecondition
	{
		public IsTrue(SymbolId symbolId)
			: base(symbolId, (value) => value != 0, (value) => (value != 0) ? 0.0 : 1.0)
		{
			
		}

//		public bool IsSatisfiedBy(WorldState worldState)
//		{
//			// TODO: Maybe check for UnknownSymbolException and return false if caught?
//			return (bool)worldState[this.symbolId];
//		}
	}
}

