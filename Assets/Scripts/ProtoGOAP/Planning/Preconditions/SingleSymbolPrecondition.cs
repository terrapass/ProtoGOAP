using System;

using Terrapass.Debug;

using ProtoGOAP.Planning;
using System.Collections.Generic;

namespace ProtoGOAP.Planning.Preconditions
{
	public abstract class SingleSymbolPrecondition : IPrecondition
	{
		private readonly SymbolId symbolId;
		private readonly Predicate<int> satisfactionCondition;
		private readonly Func<int, double> distanceMetric;

		// TODO: Introduce precondition weighing either via an additional constructor param,
		// or via a separate proxy Weigh(weight, precondition) IPrecondition implementer.
		public SingleSymbolPrecondition(SymbolId symbolId, Predicate<int> satisfactionCondition, Func<int, double> distanceMetric)
		{
			this.symbolId = symbolId;
			this.satisfactionCondition = PreconditionUtils.EnsureNotNull(satisfactionCondition, "satisfactionCondition");
			this.distanceMetric = PreconditionUtils.EnsureNotNull(distanceMetric, "distanceMetric");
		}

		public bool IsSatisfiedBy(WorldState worldState)
		{
			return this.satisfactionCondition(worldState[this.symbolId]);
		}

		public IEnumerable<SymbolId> RelevantSymbols
		{
			get {
				return new List<SymbolId>() { symbolId };
			}
		}

		public double GetDistanceFrom(WorldState worldState)
		{
			return distanceMetric(worldState[this.symbolId]);
		}
	}
}

