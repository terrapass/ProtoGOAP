using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Terrapass.Time;

using ProtoGOAP.Graphs;

public class TestAstarPathfinding : MonoBehaviour
{
	private class Node : IGraphNode<Node>
	{
		private string name;
		private IList<IGraphEdge<Node>> outgoingEdges;

		public Node(string name)
		{
			this.name = name;
			this.outgoingEdges = new List<IGraphEdge<Node>>();
		}

		#region IGraphNode implementation

		public IEnumerable<IGraphEdge<Node>> OutgoingEdges
		{
			get {
				return this.outgoingEdges;
			}
		}

		#endregion

		public override string ToString()
		{
			return this.name;
		}

		public void AddOutgoingEdge(IGraphEdge<Node> outgoingEdge)
		{
			if(!outgoingEdge.SourceNode.Equals(this))
			{
				throw new ArgumentException("outgoingEdge must have this Node as its source", "outgoingEdge");
			}
			this.outgoingEdges.Add(outgoingEdge);
		}

		public override bool Equals(object obj)
		{
			Node otherNode = obj as Node;
			return otherNode != null
				? this.name.Equals(otherNode.name)
				: false;
		}

		public override int GetHashCode()
		{
			return this.name.GetHashCode();
		}

		public string Name
		{
			get {
				return this.name;
			}
		}
	}

	private class Edge : IGraphEdge<Node>
	{
		#region IGraphEdge implementation

		public double Cost {get;}

		public Node SourceNode {get;}

		public Node TargetNode {get;}

		#endregion

		public Edge(Node sourceNode, Node targetNode, double cost)
		{
			this.SourceNode = sourceNode;
			this.TargetNode = targetNode;
			this.Cost = cost;
		}

		public override string ToString()
		{
			return string.Format("[Edge: Cost={0}, SourceNode={1}, TargetNode={2}]", Cost, SourceNode, TargetNode);
		}
	}

	private static double Heuristic(Node sourceNode, Node targetNode)
	{
		if(!targetNode.Name.Equals("F"))
		{
			throw new NotImplementedException(
				string.Format(
					"Heuristic is not implemented for any target node, except for F (got {0})",
					targetNode.Name
				)
			);
		}

		switch(sourceNode.Name)
		{
		default:
			throw new ArgumentException(string.Format("Unknown source node {0}", sourceNode.Name), "sourceNode");
		case "A":
			return 4;
		case "B":
			return 3;
		case "C":
			return 3;
		case "D":
			return 2;
		case "E":
			return 2;
		case "F":
			return 0;
		}
	}

	// Use this for initialization
	void Start()
	{
		Node a = new Node("A");
		Node b = new Node("B");
		Node c = new Node("C");
		Node d = new Node("D");
		Node e = new Node("E");
		Node f = new Node("F");

		a.AddOutgoingEdge(new Edge(a, b, 4));
		a.AddOutgoingEdge(new Edge(a, c, 2));
		a.AddOutgoingEdge(new Edge(a, f, 50));
		b.AddOutgoingEdge(new Edge(b, c, 5));
		b.AddOutgoingEdge(new Edge(b, d, 10));
		c.AddOutgoingEdge(new Edge(c, e, 3));
		e.AddOutgoingEdge(new Edge(e, d, 4));
		d.AddOutgoingEdge(new Edge(d, f, 11));

		var pathfinder = new AstarPathfinder<Node>(Heuristic);
		var timer = new SystemExecutionTimer();
		var path = pathfinder.FindPath(a, f);
		print(string.Format("In {1} seconds found the following path with cost {0} from A to F:", path.Cost, timer.ElapsedSeconds));
		print(path.Edges.Aggregate("", (soFar, edge) => soFar + (soFar.Length > 0 ? " -> " : "") + edge.ToString()));
	}
	
	// Update is called once per frame
	void Update()
	{
		
	}
}
