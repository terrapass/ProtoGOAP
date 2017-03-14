using System;
using System.Linq;
using System.Collections.Generic;

using Terrapass.Debug;

using ProtoGOAP.Graphs;

namespace ProtoGOAP.Planning
{
	public class ForwardPlanner : IPlanner
	{
		public const int DEFAULT_MAX_PLAN_LENGTH = 20;

		private readonly int maxPlanLength;
		private readonly IPathfinder<ForwardNode> pathfinder;

		public ForwardPlanner(int maxPlanLength = DEFAULT_MAX_PLAN_LENGTH)
		{
			if(maxPlanLength <= 0)
			{
				throw new ArgumentException("maxPlanLength must be positive", "maxPlanLength");
			}
			this.maxPlanLength = maxPlanLength;
			this.pathfinder = new AstarPathfinder<ForwardNode>(PathfindingHeuristic, maxPlanLength);
		}

		#region IPlanner implementation

		public Plan FormulatePlan(
			IKnowledgeProvider knowledgeProvider, 
			IEnumerable<PlanningAction> availableActions, 
			Goal goal
		)
		{
			// TODO: Ideally, world state should be populated lazily, not in advance, like here.
			// Some simple may not be needed at all to build a plan!
			// (also see comments on IPrecondition and IEffect RelevantActions property)
			WorldState initialWorldState = new WorldState();

			availableActions.Aggregate(new List<SymbolId>(), (soFar, action) => {soFar.AddRange(action.GetRelevantSymbols()); return soFar;})
				.ForEach((symbolId) => {
					if(!initialWorldState.Contains(symbolId))
					{
						initialWorldState = initialWorldState.BuildUpon()
							.SetSymbol(symbolId, knowledgeProvider.GetSymbolValue(symbolId)).Build();
					}
				});

			foreach(SymbolId symbolId in goal.PreconditionSymbols)
			{
				if(!initialWorldState.Contains(symbolId))
				{
					initialWorldState = initialWorldState.BuildUpon()
						.SetSymbol(symbolId, knowledgeProvider.GetSymbolValue(symbolId)).Build();
				}
			}

			// TODO: After lazy population is implemented, the following part of the method is the only thing
			// that must remain.
			try
			{
				var path = this.pathfinder.FindPath(
					new RegularForwardNode(initialWorldState, new ForwardNodeExpander(availableActions)),
					new GoalForwardNode(goal),
					new GoalNodeComparer()
	           	);

				return new Plan(from edge in path.Edges select ((ForwardEdge)edge).Action.Name);
			}
			catch(PathNotFoundException e)
			{
				throw new PlanNotFoundException(this, maxPlanLength, goal, e);
			}
		}

		#endregion

		private static double PathfindingHeuristic(ForwardNode sourceNode, ForwardNode targetNode)
		{
			// TODO: Ugh... Replace this blasphemous downcasting with an ortodox Visitor pattern.
			DebugUtils.Assert(sourceNode is RegularForwardNode, "sourceNode must be an instance of {1}", typeof(RegularForwardNode));
			DebugUtils.Assert(targetNode is GoalForwardNode, "targetNode must be an instance of {0}", typeof(GoalForwardNode));

			var currentNode = (RegularForwardNode)sourceNode;
			var goalNode = (GoalForwardNode)targetNode;

			// Can this really be used as a heuristic for forward planning????
			return goalNode.Goal.GetDistanceFrom(currentNode.WorldState);
		}

		private class GoalNodeComparer : IEqualityComparer<ForwardNode>
		{
			#region IEqualityComparer implementation

			public bool Equals(ForwardNode x, ForwardNode y)
			{
				// FIXME: Downcasting!
				DebugUtils.Assert(x is RegularForwardNode, "{0}.Equals() must be invoked with a regular node as the 1st argument", this.GetType());
				DebugUtils.Assert(y is GoalForwardNode, "{0}.Equals() must be invoked with the goal node as the 2nd argument", this.GetType());

				return ((GoalForwardNode)y).Goal.IsReachedIn(((RegularForwardNode)x).WorldState);
			}

			public int GetHashCode(ForwardNode obj)
			{
				return (obj is GoalForwardNode)
					? -1
					: ((RegularForwardNode)obj).WorldState.GetHashCode();
			}

			#endregion


		}
	}
}

