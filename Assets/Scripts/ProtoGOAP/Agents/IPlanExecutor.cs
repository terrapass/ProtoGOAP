using System;
using System.Collections.Generic;

using ProtoGOAP.Planning;

namespace ProtoGOAP.Agents
{
	public interface IPlanExecutor
	{
		void SubmitForExecution(Plan plan);
		void InterruptExecution();

		IPlanExecution CurrentExecution {get;}

//		void AddAction(/*ActionId*/string name, IAction action);
//		void RemoveAction(/*ActionId*/string name);

		void Update();

		IEnumerable<PlanningAction> SupportedPlanningActions { get; }
	}
}

