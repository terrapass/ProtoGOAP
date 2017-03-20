using UnityEngine;
using System;
using System.Collections.Generic;

using Terrapass.Extensions.Unity;

namespace ProtoGOAP.Demo
{
	public class Town : MonoBehaviour
	{
		[Serializable]
		private struct ToolPrefabPair
		{
			public ToolType toolType;
			public GameObject prefab;
		}

		[Serializable]
		private struct ResourcePrefabPair
		{
			public ResourceType resourceType;
			public GameObject prefab;
		}

		[SerializeField]
		private Storage mainStorage;
		[SerializeField]
		private Storage constructionStorage;
		[SerializeField]
		private ToolBench toolBench;
		[SerializeField]
		private House house;
		[SerializeField]
		private ToolPrefabPair[] toolPrefabPairs;
		[SerializeField]
		private ResourcePrefabPair[] resourcePrefabPairs;

		private IDictionary<ToolType, GameObject> toolPrefabs;
		private IDictionary<ResourceType, GameObject> resourcePrefabs;

		void Start()
		{
			this.EnsureRequiredFieldsAreSetInEditor();

			this.toolPrefabs = new Dictionary<ToolType, GameObject>();
			foreach(var pair in toolPrefabPairs)
			{
				this.toolPrefabs.Add(pair.toolType, pair.prefab);
			}

			this.resourcePrefabs = new Dictionary<ResourceType, GameObject>();
			foreach(var pair in resourcePrefabPairs)
			{
				this.resourcePrefabs.Add(pair.resourceType, pair.prefab);
			}
		}

		public Storage MainStorage
		{
			get {
				return this.mainStorage;
			}
		}

		public Storage ConstructionStorage
		{
			get {
				return this.constructionStorage;
			}
		}

		public ToolBench ToolBench
		{
			get {
				return this.toolBench;
			}
			set {
				toolBench = value;
			}
		}

		public House House
		{
			get {
				return this.house;
			}
			set {
				house = value;
			}
		}

		public IDictionary<ToolType, GameObject> ToolPrefabs
		{
			get {
				return this.toolPrefabs;
			}
		}

		public IDictionary<ResourceType, GameObject> ResourcePrefabs
		{
			get {
				return this.resourcePrefabs;
			}
		}
	}
}

