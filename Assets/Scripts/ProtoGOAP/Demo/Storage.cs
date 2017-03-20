using UnityEngine;
using System;

namespace ProtoGOAP.Demo
{
	public class Storage : MonoBehaviour
	{
		[SerializeField]
		private int logs;
		[SerializeField]
		private int planks;
		[SerializeField]
		private int ore;
		[SerializeField]
		private int iron;
		[SerializeField]
		private int stone;

		public int GetResourceCount(ResourceType resourceType)
		{
			switch(resourceType)
			{
			default:
				throw new ArgumentException(string.Format("unrecognized resource type {0}", resourceType), "resourceType");
			case ResourceType.None:
				throw new ArgumentException(string.Format("Resource type {0} cannot be used as an argument for GetResourceCount()", resourceType), "resourceType");
			case ResourceType.Logs:
				return logs;
			case ResourceType.Planks:
				return planks;
			case ResourceType.Ore:
				return ore;
			case ResourceType.Iron:
				return iron;
			case ResourceType.Stone:
				return stone;
			}
		}

		public void SetResourceCount(ResourceType resourceType, int count)
		{
			switch(resourceType)
			{
			default:
				throw new ArgumentException(string.Format("unrecognized resource type {0}", resourceType), "resourceType");
			case ResourceType.None:
				throw new ArgumentException(string.Format("Resource type {0} cannot be used as an argument for GetResourceCount()", resourceType), "resourceType");
			case ResourceType.Logs:
				logs = count;
				break;
			case ResourceType.Planks:
				planks = count;
				break;
			case ResourceType.Ore:
				ore = count;
				break;
			case ResourceType.Iron:
				iron = count;
				break;
			case ResourceType.Stone:
				stone = count;
				break;
			}
		}

		public void Clear()
		{
			this.logs = 0;
			this.planks = 0;
			this.ore = 0;
			this.iron = 0;
			this.stone = 0;
		}

		public int Logs
		{
			get {
				return this.logs;
			}
			set {
				logs = value;
			}
		}

		public int Planks
		{
			get {
				return this.planks;
			}
			set {
				planks = value;
			}
		}

		public int Ore
		{
			get {
				return this.ore;
			}
			set {
				ore = value;
			}
		}

		public int Iron
		{
			get {
				return this.iron;
			}
			set {
				iron = value;
			}
		}

		public int Stone
		{
			get {
				return this.stone;
			}
			set {
				stone = value;
			}
		}
	}
}

