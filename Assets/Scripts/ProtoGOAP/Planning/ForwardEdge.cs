using System;
using System.Collections.Generic;

using Terrapass.Debug;

using ProtoGOAP.Graphs;

namespace ProtoGOAP.Planning
{
	public class ForwardEdge : IGraphEdge<ForwardNode>
	{
		public PlanningAction Action { get; }

		public ForwardEdge(PlanningAction action, ForwardNode sourceNode, ForwardNode targetNode)
		{
			this.Action = PreconditionUtils.EnsureNotNull(action, "action");
			this.SourceNode = sourceNode;
			this.TargetNode = targetNode;
		}

		#region IGraphEdge implementation

		public double Cost
		{
			get {
				return this.Action.Cost;
			}
		}

		public ForwardNode SourceNode { get; }

		public ForwardNode TargetNode { get; }

		#endregion
	}
}

