using System;
using System.Collections.Generic;

using ProtoGOAP.Graphs;

namespace ProtoGOAP.Planning.Internal
{
	internal interface INodeExpander<Node> where Node : IGraphNode<Node>
	{
		IEnumerable<IGraphEdge<Node>> ExpandNode(Node node);
	}
}

