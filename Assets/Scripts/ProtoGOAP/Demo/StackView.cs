using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProtoGOAP.Demo
{
	public class StackView : MonoBehaviour
	{
		[SerializeField]
		private GameObject singleViewPrefab;
		[SerializeField]
		private int count;
		[SerializeField]
		private int viewsPerRow = 3;
		[SerializeField]
		private float horizontalSpacing = 0.0f;
		[SerializeField]
		private float verticalSpacing = 0.0f;

		private bool dirtyView = true;
		private Stack<GameObject> views;

		void Start()
		{
			views = new Stack<GameObject>();
		}

		void Update()
		{
			if(dirtyView && singleViewPrefab != null)
			{
				if(count > views.Count)
				{
					var newViewsNeeded = count - views.Count;
					for(int i = 0; i < newViewsNeeded; i++)
					{
						var newView = (GameObject)Instantiate(singleViewPrefab);
						newView.transform.parent = this.transform;
						newView.transform.localRotation = Quaternion.identity;
						var newViewRenderer = newView.GetComponentInChildren<Renderer>();
						newView.transform.localPosition 
							= ((views.Count + 1) % viewsPerRow) * (newViewRenderer.bounds.size.x + horizontalSpacing)* Vector3.right
								+
							((views.Count + 1) / viewsPerRow) * (newViewRenderer.bounds.size.y + verticalSpacing) * Vector3.up;

						views.Push(newView);

					}
				}
				else
				{
					var extraViews = views.Count - count;
					for(int i = 0; i < extraViews; i++)
					{
						Destroy(views.Pop());
					}
				}
				dirtyView = false;
			}
		}

		public GameObject SingleViewPrefab
		{
			get {
				return this.singleViewPrefab;
			}
			set {
				singleViewPrefab = value;
				dirtyView = true;
				views.Clear();
			}
		}

		public int Count
		{
			get {
				return this.count;
			}
			set {
				count = value;
				dirtyView = true;
			}
		}

		public int ViewsPerRow
		{
			get {
				return this.viewsPerRow;
			}
			set {
				viewsPerRow = value;
				dirtyView = true;
			}
		}
	}
}

