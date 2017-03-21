using UnityEngine;
using UnityEngine.AI;
using System;
using System.Collections.Generic;
using System.Linq;

using Terrapass.Extensions.Unity;
using Terrapass.Time;

using ProtoGOAP.Agents;
using ProtoGOAP.Planning;
using ProtoGOAP.Planning.Preconditions;
using Terrapass.Debug;

namespace ProtoGOAP.Demo
{
	// TODO: Encapsulate view logic in a separate TownsmanView class
	public partial class Townsman : MonoBehaviour
	{
		private const float TOOL_ANIMATION_PERIOD = 0.5f;
		private const float TOOL_ANIMATION_TO_IDLE_LERP = 0.25f;
		private const float IDLE_TOOL_ANGLE = 75f;
		private const float MAX_TOOL_ANGLE = 190f;

		[SerializeField]
		private Town town;
		[SerializeField]
		private Transform toolTransform;
		[SerializeField]
		private Transform resourceTransform;

		[SerializeField]
		private ToolType toolType;
		[SerializeField]
		private ResourceType resourceType;

		private Agent agent;

		private NavMeshAgent navMeshAgent;

		private IDictionary<ToolType, GameObject> toolViews;
		private IDictionary<ResourceType, GameObject> resourceViews;

		private GameObject currentToolView;
		private GameObject currentResourceView;

		private bool dirtyView = true;
		private bool animateTool = false;
		//private float currentToolAngle = IDLE_TOOL_ANGLE;
		//private float targetToolAngle;

		private IResettableTimer toolAnimationTimer;

		void Start()
		{
			this.EnsureRequiredFieldsAreSetInEditor();

			// Init AI
			var knowledgeProvider = new KnowledgeProvider(this);
			var goalSelector = new GoalSelector.Builder()
				.WithReevaluationPeriod(3.0f)
				.WithGoal(
					new Goal(
						"BuildHouse",
						new List<IPrecondition>() {
							new IsTrue(new SymbolId("HouseBuilt"))
						}
					),
					() => town.House.IsBuilt ? -1 : 1
				)
				.Build();
			var actionFactory = this.GetActionFactory();
			var planner = new RegressivePlanner(20, 5);
			var planExecutor = new PlanExecutor(actionFactory);

			this.agent = new Agent(
				new AgentEnvironment.Builder()
				.WithKnowledgeProvider(knowledgeProvider)
				.WithGoalSelector(goalSelector)
				.WithPlanner(planner)
				.WithPlanExecutor(planExecutor)
				.Build()
			);

			this.navMeshAgent = this.GetComponent<NavMeshAgent>();
			DebugUtils.Assert(this.navMeshAgent != null, "{0} requires {1} to be present", this.GetType(), typeof(NavMeshAgent));

			// Init view
			this.toolAnimationTimer = new ResettableExecutionTimer(true);
		}

		void Update()
		{
			// Update AI
			this.agent.Update();

			// Init view (if needed)
			if(toolViews == null)
			{
				toolViews = new Dictionary<ToolType, GameObject>();
				foreach(var kvp in town.ToolPrefabs)
				{
					var view = (GameObject)Instantiate(kvp.Value, toolTransform.position, toolTransform.rotation);
					view.transform.parent = toolTransform;
					toolViews.Add(
						kvp.Key,
						view
					);
					SetVisible(view, false);
				}
			}
			if(resourceViews == null)
			{
				resourceViews = new Dictionary<ResourceType, GameObject>();
				foreach(var kvp in town.ResourcePrefabs)
				{
					var view = (GameObject)Instantiate(kvp.Value, resourceTransform.position, resourceTransform.rotation);
					view.transform.parent = resourceTransform;
					resourceViews.Add(
						kvp.Key,
						view
					);
					SetVisible(view, false);
				}
			}

			// Update view
			if(dirtyView)
			{
				foreach(var kvp in toolViews)
				{
					SetVisible(kvp.Value, (kvp.Key == toolType));
				}
				foreach(var kvp in resourceViews)
				{
					SetVisible(kvp.Value, (kvp.Key == resourceType));
				}
				dirtyView = false;
			}

			var animSeconds = toolAnimationTimer.ElapsedSeconds;
			float animSecondsRemainder = (float)(animSeconds - Math.Floor(animSeconds / (TOOL_ANIMATION_PERIOD))*TOOL_ANIMATION_PERIOD);
			var currentToolAngle = Mathf.Lerp(
				IDLE_TOOL_ANGLE,
				MAX_TOOL_ANGLE, 
				1 - Math.Abs(2*(animSecondsRemainder / TOOL_ANIMATION_PERIOD) - 1)
			);
			toolTransform.localRotation = Quaternion.Euler(currentToolAngle, 0, -90);
		}

		private void SetVisible(GameObject gameObject, bool visible)
		{
			var renderers = gameObject.GetComponentsInChildren<Renderer>();
			foreach(var renderer in renderers)
			{
				renderer.enabled = visible;
			}
		}

		private GameObject FindClosestTree()
		{
			return DemoUtils.FindClosestWithTag(transform.position, "src_tree");
		}

		private void MoveTo(Vector3 position)
		{
			//this.navMeshAgent.ResetPath();
			this.navMeshAgent.destination = position;
		}

		private bool ReachedDestination
		{
			get {
				return !this.navMeshAgent.pathPending && this.navMeshAgent.remainingDistance < 0.0001f;
			}
		}

		public ToolType CurrentTool
		{
			get {
				return toolType;
			}
			set {
				toolType = value;
				dirtyView = true;
			}
		}

		public ResourceType CurrentResource
		{
			get {
				return resourceType;
			}
			set {
				resourceType = value;
				dirtyView = true;
			}
		}

		public bool AnimateTool
		{
			get {
				return this.animateTool;
			}
			set {
				if(!this.animateTool && value)
				{
					this.toolAnimationTimer.Reset(false);
				}
				else if(this.animateTool && !value)
				{
					this.toolAnimationTimer.Reset(true);
				}
				this.animateTool = value;
			}
		}
	}
}

