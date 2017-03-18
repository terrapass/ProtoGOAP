using System;

using ProtoGOAP.Planning;

namespace ProtoGOAP.Planning.Effects
{
	public sealed class Subtract : SingleSymbolEffect
	{
		private readonly int delta;

		public Subtract(SymbolId symbolId, int delta)
			: base(symbolId, (initialValue) => initialValue - delta/*, (initialValue) => initialValue + delta*/)
		{
			this.delta = delta;
		}

		public override int? ValueDelta
		{
			get {
				return -delta;
			}
		}
	}
}

