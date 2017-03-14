using System;
using System.Collections.Generic;

using Terrapass.Debug;

using ProtoGOAP.Planning;

namespace ProtoGOAP.Planning.Effects
{
	public abstract class SingleSymbolEffect : IEffect
	{
		private readonly SymbolId symbolId;
		private readonly Func<int, int> effectApplication;

		public SingleSymbolEffect(SymbolId symbolId, Func<int, int> effectApplication)
		{
			this.symbolId = symbolId;
			this.effectApplication = PreconditionUtils.EnsureNotNull(effectApplication, "effectApplication");
		}

		public WorldState ApplyTo(WorldState initialState)
		{
			return initialState.BuildUpon()
				.SetSymbol(this.symbolId, this.effectApplication(initialState[this.symbolId]))
				.Build();
		}

		public IEnumerable<SymbolId> RelevantSymbols
		{
			get {
				return new List<SymbolId>() { symbolId };
			}
		}
	}
}

