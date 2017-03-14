using System;
using System.Linq;
using System.Collections.Generic;

using Terrapass.Debug;

using ProtoGOAP.Graphs;

namespace ProtoGOAP.Planning
{
	public class ForwardNodeExpander : INodeExpander<ForwardNode>
	{
		private readonly IEnumerable<PlanningAction> availableActions;

		public ForwardNodeExpander(IEnumerable<PlanningAction> availableActions)
		{
			this.availableActions = PreconditionUtils.EnsureNotNull(availableActions, "availableActions");
		}


		#region INodeExpander implementation
		public IEnumerable<IGraphEdge<ForwardNode>> ExpandNode(ForwardNode node)
		{
			DebugUtils.Assert(node is RegularForwardNode, "node must be an instance of {0}", typeof(RegularForwardNode));
			var regularNode = (RegularForwardNode)node;
			return (from action in availableActions 
					where action.IsAvailableIn(regularNode.WorldState) 
				select new ForwardEdge(
					action,
					node,
					new RegularForwardNode(
						action.Apply(regularNode.WorldState),
						this
					)
				)).Cast<IGraphEdge<ForwardNode>>();
		}
		#endregion
	}
}

