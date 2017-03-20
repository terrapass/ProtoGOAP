using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;

using Terrapass.Extensions.Unity;

namespace ProtoGOAP.Demo
{
	public class House : MonoBehaviour 
	{
		[SerializeField]
		private bool isBuilt;
		[SerializeField]
		private GameObject constructionSitePrefab;
		[SerializeField]
		private GameObject housePrefab;

		private GameObject constructionSite;
		private GameObject house;

		void Start()
		{
			this.EnsureRequiredFieldsAreSetInEditor();

			this.constructionSite = (GameObject)Instantiate(constructionSitePrefab, this.transform.position, this.transform.rotation);
			this.house = (GameObject)Instantiate(housePrefab, this.transform.position, this.transform.rotation);
		}

		void Update()
		{
			this.constructionSite.GetComponentInChildren<Renderer>().enabled = !isBuilt;
			this.house.GetComponentInChildren<Renderer>().enabled = isBuilt;
		}

		public bool IsBuilt
		{
			get {
				return this.isBuilt;
			}
			set {
				isBuilt = value;
			}
		}
	}
}

