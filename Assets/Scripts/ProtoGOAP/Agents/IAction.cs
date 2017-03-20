using System;

using ProtoGOAP.Planning;

namespace ProtoGOAP.Agents
{
	public interface IAction
	{
		//PlanningAction AsPlanningAction {get;}

		void StartExecution();
		void StartInterruption();

		void Update();

		ExecutionStatus Status {get;}
	}
}

