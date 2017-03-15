using System;
using System.Collections.Generic;
using UnityEngine;
using Terrapass.Debug;

namespace ProtoGOAP.Graphs
{
	public class AstarPathfinderConfiguration<GraphNode> where GraphNode : IGraphNode<GraphNode>
	{
		public const int UNLIMITED_SEARCH_DEPTH = -1;

		/// <summary>
		/// Heuristic, which returns 0 for any two nodes.
		/// Using this heuristic for A* makes it equivalent to Dijkstra's algorithm.
		/// </summary>
		/// <returns>Heuristic path cost - always zero.</returns>
		/// <param name="sourceNode">Source node.</param>
		/// <param name="targetNode">Target node.</param>
		public static double ZeroPathCostHeuristic(GraphNode sourceNode, GraphNode targetNode)
		{
			return 0;
		}

		public PathCostHeuristic<GraphNode> Heuristic { get; }
		public int MaxSearchDepth { get; }

		private AstarPathfinderConfiguration(
			PathCostHeuristic<GraphNode> heuristic,
			int maxSearchDepth
		)
		{
			this.Heuristic = PreconditionUtils.EnsureNotNull(heuristic, "heuristic must not be null");
			this.MaxSearchDepth = maxSearchDepth;
		}

		public class Builder
		{
			private PathCostHeuristic<GraphNode> heuristic;
			private int maxSearchDepth;

			/// <summary>
			/// Initializes a new instance of the <see cref="ProtoGOAP.Graphs.AstarPathfinderConfiguration`1+Builder"/>
			/// with default configuration (zero heuristic, no limit on search depth).
			/// </summary>
			public Builder()
			{
				this.heuristic = ZeroPathCostHeuristic;
				this.maxSearchDepth = UNLIMITED_SEARCH_DEPTH;
			}

			/// <summary>
			/// Specifies the heuristic to be used by A*.
			/// </summary>
			/// <returns>Builder instance.</returns>
			/// <param name="heuristic">Heuristic to be used.</param>
			public Builder UseHeuristic(PathCostHeuristic<GraphNode> heuristic)
			{
				this.heuristic = PreconditionUtils.EnsureNotNull(heuristic, "heuristic");
				return this;
			}

			/// <summary>
			/// Limits the search depth.
			/// </summary>
			/// <returns>Builder instance.</returns>
			/// <param name="maxSearchDepth">
			/// Maximum depth for search, i.e. maximum number of edges, allowed in a path.
			/// A negative value indicates no depth limit.
			/// </param>
			public Builder LimitSearchDepth(int maxSearchDepth)
			{
				this.maxSearchDepth = maxSearchDepth;
				return this;
			}

			public AstarPathfinderConfiguration<GraphNode> Build()
			{
				return new AstarPathfinderConfiguration<GraphNode>(
					this.heuristic,
					this.maxSearchDepth
				);
			}
		}
	}
}