using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

using ProtoGOAP.Planning;
using ProtoGOAP.Planning.Preconditions;

public static class TestUtils
{
	public static void ValidatePlan(Plan plan, WorldState initialWorldState, Goal goal)
	{
		Debug.LogFormat("Validating plan ({0}) for goal {1}", plan, goal.Name);
		Debug.LogFormat("Initial state: {0}", initialWorldState);
		var currentWorldState = initialWorldState;
		foreach(var action in plan.Actions)
		{
			if(!action.IsAvailableIn(currentWorldState))
			{
				Debug.LogErrorFormat(
					"{0} (preconds: {1}) is not available in state {2}",
					action.Name,
					FormatPreconditions(action.Preconditions),
					currentWorldState
				);
				break;
			}

			currentWorldState = action.Apply(currentWorldState);

			Debug.LogFormat("After {0}: {1}", action.Name, currentWorldState);
		}

		if(goal.IsReachedIn(currentWorldState))
		{
			Debug.LogFormat("Plan for goal {0} (preconds: {1}) is valid", goal.Name, FormatPreconditions(goal.Preconditions));
		}
		else
		{
			Debug.LogErrorFormat(
				"Plan for goal {0} (preconds: {1}) is invalid: final state {2} does not satisfy the goal preconditions", 
				goal.Name, 
				FormatPreconditions(goal.Preconditions),
				currentWorldState
			);
		}
	}

	public static string FormatPreconditions(IEnumerable<IPrecondition> preconditions)
	{
		return preconditions.Aggregate(
			"",
			(soFar, precond) => soFar + ((soFar.Length == 0) ? "" : " AND ") + precond.ToString()
		);
	}
}

