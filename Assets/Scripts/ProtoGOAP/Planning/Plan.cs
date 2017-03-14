using System;
using System.Linq;
using System.Collections.Generic;

using Terrapass.Debug;

namespace ProtoGOAP.Planning
{
	public struct Plan
	{
		public IEnumerable<string> ActionNames { get; }	// no need to store actual Action instances
		// TODO: This should actually be an ordered collection of action ids,
		//public IEnumerable<ActionId> ActionIds {get;}

		public int Length
		{
			get {
				return ActionNames.Count();
			}
		}

		public Plan(IEnumerable<string> actionNames)
		{
			this.ActionNames = new List<string>(PreconditionUtils.EnsureNotNull(actionNames, "actionNames")).AsReadOnly();
		}

		public override string ToString()
		{
			string result = "";
			bool first = true;
			foreach(string actionName in this.ActionNames)
			{
				if(!first)
				{
					result += " -> ";
				}
				result += actionName;
				first = false;
			}
			return result;
		}
	}
}

