using System;
using System.Linq;
using System.Collections.Generic;

using Terrapass.Debug;

using ProtoGOAP.Graphs;

namespace ProtoGOAP.Planning
{
	// TODO: Replace this hierarchy with a single ForwardNode type (this will also take care of all the downcasting).
	public abstract class ForwardNode : IGraphNode<ForwardNode>, IEquatable<ForwardNode>
	{
		#region IGraphNode implementation

		public abstract IEnumerable<IGraphEdge<ForwardNode>> OutgoingEdges {get;}

		#endregion

		#region IEquatable implementation

		public abstract bool Equals(ForwardNode other);

		#endregion
	}

	public sealed class RegularForwardNode : ForwardNode
	{
		private readonly WorldState worldState;
		private readonly INodeExpander<ForwardNode> expander;

		private IEnumerable<IGraphEdge<ForwardNode>> outgoingEdges;

		public RegularForwardNode(WorldState worldState, INodeExpander<ForwardNode> expander)
		{
			this.worldState = worldState;
			this.expander = PreconditionUtils.EnsureNotNull(expander, "expander");
			this.outgoingEdges = null;
		}

		#region IGraphNode implementation

		public override IEnumerable<IGraphEdge<ForwardNode>> OutgoingEdges
		{
			get {
				if(this.outgoingEdges == null)
				{
					this.outgoingEdges = this.expander.ExpandNode(this);
				}
				DebugUtils.Assert(
					outgoingEdges.All(edge => edge.SourceNode == this),
					"this node must be the source of every outgoing edge"
				);
//				foreach(var edge in outgoingEdges)
//				{
//					var forwardEdge = (ForwardEdge)edge;
//					UnityEngine.Debug.LogFormat(
//						"{0} --\"{1}\"--> \"{2}\"",
//						((RegularForwardNode)forwardEdge.SourceNode).WorldState,
//						forwardEdge.Action.Name,
//						((RegularForwardNode)forwardEdge.TargetNode).WorldState
//					);
//				}
				return this.outgoingEdges;
			}
		}

		#endregion

		public override bool Equals(ForwardNode other)
		{
			DebugUtils.Assert(other is RegularForwardNode, "{0}.Equals() must be called with an instance of RegularForwardNode", this.GetType());
			return this.WorldState.Equals(((RegularForwardNode)other).WorldState);
		}

		public WorldState WorldState
		{
			get {
				return this.worldState;
			}
		}
	}

	public sealed class GoalForwardNode : ForwardNode
	{
		public Goal Goal;

		public GoalForwardNode(Goal goal)
		{
			this.Goal = goal;
		}

		#region implemented abstract members of ForwardNode

		public override IEnumerable<IGraphEdge<ForwardNode>> OutgoingEdges
		{
			get {
				DebugUtils.Assert(false, "{0}.OutgoingEdges must not be queried", this.GetType());
				throw new InvalidOperationException(string.Format("{0}.OutgoingEdges must not be queried", this.GetType()));
			}
		}

		#endregion

		public override bool Equals(ForwardNode other)
		{
			DebugUtils.Assert(other is RegularForwardNode, "{0}.Equals() must never be called", this.GetType());
			throw new NotImplementedException();
		}
	}
}

