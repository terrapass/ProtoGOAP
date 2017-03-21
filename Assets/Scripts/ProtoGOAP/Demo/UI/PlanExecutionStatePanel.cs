using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using System.Linq;

using ProtoGOAP.Agents;

namespace ProtoGOAP.Demo.UI
{
	public class PlanExecutionStatePanel : MonoBehaviour
	{
		private const string CURRENT_GOAL_TEMPLATE = "Current goal: {0}";
		private const string NO_GOAL = "<none>";
		private const string NO_PLAN = "<no plan>";
		private const string EMPTY_PLAN = "<empty plan>";

		[SerializeField]
		private Text currentGoalText;
		[SerializeField]
		private Text planText;

		private IPlanExecution planExecution;
		private int lastActionIndex = -1;

		void Start()
		{

		}

		void Update()
		{
			if(this.planExecution != null && planExecution.CurrentActionIndex != lastActionIndex)
			{
				RefreshText();
				lastActionIndex = planExecution.CurrentActionIndex;
			}
		}

		public IPlanExecution PlanExecution
		{
			get {
				return this.planExecution;
			}
			set {
				this.planExecution = value;
				this.RefreshText();
			}
		}

		private void RefreshText()
		{
			if(planExecution == null || planExecution.Plan == null)
			{
				currentGoalText.text = string.Format(CURRENT_GOAL_TEMPLATE, NO_GOAL);
				planText.text = NO_PLAN;
			}
			else
			{
				currentGoalText.text = string.Format(CURRENT_GOAL_TEMPLATE, planExecution.Plan.Goal.Name);
				var planDescription = planExecution.Plan.Length > 0 ? "" : EMPTY_PLAN;
				for(int i = 0; i < planExecution.Plan.Length; i++)
				{
					planDescription += planExecution.Plan.Actions.ElementAt(i).Name;
					if(i < planExecution.CurrentActionIndex)
					{
						planDescription += " (Complete)";
					}
					else if(i == planExecution.CurrentActionIndex)
					{
						planDescription += " (In Progress)";
					}
					planDescription += "\n";
				}
				planText.text = planDescription;
			}
		}
	}
}

