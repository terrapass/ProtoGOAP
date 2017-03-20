using System;
using System.Collections.Generic;

using ProtoGOAP.Planning;

namespace ProtoGOAP.Agents
{
	public interface IActionFactory
	{
		IEnumerable<PlanningAction> SupportedPlanningActions {get;}

		IAction FromPlanningAction(/*ActionId*/string planningActionName);
	}
}

