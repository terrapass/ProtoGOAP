using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProtoGOAP.Demo
{
	public class ToolBench : MonoBehaviour
	{
		[Serializable]
		private struct StackViewPair
		{
			public ToolType toolType;
			public StackView stackView;
		}

		[SerializeField]
		private int axes;
		[SerializeField]
		private int pickaxes;
		[SerializeField]
		private int hammers;
		[SerializeField]
		private int saws;

		[SerializeField]
		private Town town;
		[SerializeField]
		private StackViewPair[] stackViewPairs;

		private IDictionary<ToolType, StackView> stackViews;

		void Start()
		{
			this.stackViews = new Dictionary<ToolType, StackView>();
			foreach(var pair in stackViewPairs)
			{
				pair.stackView.SingleViewPrefab = town.ToolPrefabs[pair.toolType];
				this.stackViews.Add(pair.toolType, pair.stackView);
			}
		}

		void Update()
		{
			SafeUpdateStack(ToolType.Axe, axes);
			SafeUpdateStack(ToolType.Pickaxe, pickaxes);
			SafeUpdateStack(ToolType.Hammer, hammers);
			SafeUpdateStack(ToolType.Saw, saws);
		}

		private void SafeUpdateStack(ToolType toolType, int count)
		{
			if(stackViews.ContainsKey(toolType))
			{
				stackViews[toolType].Count = count;
			}
		}

		public int GetToolCount(ToolType toolType)
		{
			switch(toolType)
			{
			default:
				throw new ArgumentException(string.Format("unrecognized resource type {0}", toolType), "resourceType");
			case ToolType.None:
				throw new ArgumentException(string.Format("Resource type {0} cannot be used as an argument for GetResourceCount()", toolType), "resourceType");
			case ToolType.Axe:
				return axes;
			case ToolType.Pickaxe:
				return pickaxes;
			case ToolType.Hammer:
				return hammers;
			case ToolType.Saw:
				return saws;
			}
		}

		public void SetToolCount(ToolType toolType, int count)
		{
			switch(toolType)
			{
			default:
				throw new ArgumentException(string.Format("unrecognized resource type {0}", toolType), "resourceType");
			case ToolType.None:
				throw new ArgumentException(string.Format("Resource type {0} cannot be used as an argument for GetResourceCount()", toolType), "resourceType");
			case ToolType.Axe:
				axes = count;
				break;
			case ToolType.Pickaxe:
				pickaxes = count;
				break;
			case ToolType.Hammer:
				hammers = count;
				break;
			case ToolType.Saw:
				saws = count;
				break;
			}
		}

		public void Clear()
		{
			this.axes = 0;
			this.pickaxes = 0;
			this.hammers = 0;
			this.saws = 0;
		}

		public int Axes
		{
			get {
				return this.axes;
			}
			set {
				axes = value;
			}
		}

		public int Pickaxes
		{
			get {
				return this.pickaxes;
			}
			set {
				pickaxes = value;
			}
		}

		public int Hammers
		{
			get {
				return this.hammers;
			}
			set {
				hammers = value;
			}
		}

		public int Saws
		{
			get {
				return this.saws;
			}
			set {
				saws = value;
			}
		}
	}
}

