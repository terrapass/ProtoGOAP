using System;
using System.Collections.Generic;

namespace ProtoGOAP.Planning
{
	public interface IPlanner
	{
		Plan FormulatePlan(IKnowledgeProvider knowledgeProvider, IEnumerable<PlanningAction> availableActions, Goal goal);
	}
}

