using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProtoGOAP.Demo
{
	public static class DemoUtils
	{
		public static GameObject FindClosestWithTag(GameObject origin, string tag)
		{
			GameObject[] gos = GameObject.FindGameObjectsWithTag(tag);
			GameObject closest = null;
			float distance = Mathf.Infinity;
			Vector3 position = origin.transform.position;
			foreach (GameObject go in gos) {
				Vector3 diff = go.transform.position - position;
				float curDistance = diff.sqrMagnitude;
				if (curDistance < distance) {
					closest = go;
					distance = curDistance;
				}
			}
			return closest;
		}
	}
}

