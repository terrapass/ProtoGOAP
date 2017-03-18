using System;

using ProtoGOAP.Planning;

namespace ProtoGOAP.Planning.Effects
{
	public sealed class SetValue : SingleSymbolEffect
	{
		private readonly int newValue;

		// FIXME: Identity deapplication is likely not correct for this effect.
		public SetValue(SymbolId symbolId, int newValue)
			: base(symbolId, (initialValue) => newValue)
		{
			this.newValue = newValue;
		}

		public override int? ValueAssigned
		{
			get {
				return this.newValue;
			}
		}
	}
}

