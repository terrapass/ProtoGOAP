using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;

using Terrapass.Debug;
using Terrapass.Time;

using Terrapass.GameAi.Goap.Planning;
using Terrapass.GameAi.Goap.Agents;
using Terrapass.GameAi.Goap.Planning.Preconditions;
using Terrapass.GameAi.Goap.Planning.Effects;

namespace ProtoGOAP.Demo
{
	public partial class Townsman
	{
		private IActionFactory GetActionFactory()
		{
			var actions = new Dictionary<PlanningAction, Func<IAction>>() {
				{
					new PlanningAction(
						"BuildHouse",
						new List<IPrecondition>() {
							new IsEqual(new SymbolId("MT"), (int)ToolType.Hammer),
							new IsNotSmaller(new SymbolId("CPlanks"), 4),
							new IsNotSmaller(new SymbolId("CIron"), 2)
						},
						new List<IEffect>() {
							new SetTrue(new SymbolId("HouseBuilt")),
							new Subtract(new SymbolId("CPlanks"), 4),
							new Subtract(new SymbolId("CIron"), 2)
						},
						30
					),
					() => new ActionBuildHouse(this)
				},
				{
					new PlanningAction(
						"GetPlanksFromStorage",
						new List<IPrecondition>() {
							new IsEqual(new SymbolId("MR"), 0),
							new IsNotSmaller(new SymbolId("SPlanks"), 1)
						},
						new List<IEffect>() {
							new SetValue(new SymbolId("MR"), (int)ResourceType.Planks),
							new Subtract(new SymbolId("SPlanks"), 1),
						},
						2
					),
					() => new ActionGetResource(this, ResourceType.Planks, this.town.MainStorage)
				},
				{
					new PlanningAction(
						"GetIronFromStorage",
						new List<IPrecondition>() {
							new IsEqual(new SymbolId("MR"), 0),
							new IsNotSmaller(new SymbolId("SIron"), 1)
						},
						new List<IEffect>() {
							new SetValue(new SymbolId("MR"), (int)ResourceType.Iron),
							new Subtract(new SymbolId("SIron"), 1),
						},
						2
					),
					() => new ActionGetResource(this, ResourceType.Iron, this.town.MainStorage)
				},
				{
					new PlanningAction(
						"PutPlanksIntoStorage",
						new List<IPrecondition>() {
							new IsEqual(new SymbolId("MR"), (int)ResourceType.Planks)
						},
						new List<IEffect>() {
							new SetValue(new SymbolId("MR"), (int)ResourceType.None),
							new Add(new SymbolId("SPlanks"), 1),
						},
						1
					),
					() => new ActionPutResource(this, ResourceType.Planks, this.town.MainStorage)
				},
				{
					new PlanningAction(
						"PutIronIntoStorage",
						new List<IPrecondition>() {
							new IsEqual(new SymbolId("MR"), (int)ResourceType.Iron)
						},
						new List<IEffect>() {
							new SetValue(new SymbolId("MR"), (int)ResourceType.None),
							new Add(new SymbolId("SIron"), 1),
						},
						1
					),
					() => new ActionPutResource(this, ResourceType.Iron, this.town.MainStorage)
				},
				{
					new PlanningAction(
						"PutPlanksIntoConstruction",
						new List<IPrecondition>() {
							new IsEqual(new SymbolId("MR"), (int)ResourceType.Planks)
						},
						new List<IEffect>() {
							new SetValue(new SymbolId("MR"), (int)ResourceType.None),
							new Add(new SymbolId("CPlanks"), 1),
						},
						1
					),
					() => new ActionPutResource(this, ResourceType.Planks, this.town.ConstructionStorage)
				},
				{
					new PlanningAction(
						"PutIronIntoConstruction",
						new List<IPrecondition>() {
							new IsEqual(new SymbolId("MR"), (int)ResourceType.Iron)
						},
						new List<IEffect>() {
							new SetValue(new SymbolId("MR"), (int)ResourceType.None),
							new Add(new SymbolId("CIron"), 1),
						},
						1
					),
					() => new ActionPutResource(this, ResourceType.Iron, this.town.ConstructionStorage)
				},
				{
					new PlanningAction(
						"CutTree",
						new List<IPrecondition>() {
							new IsEqual(new SymbolId("MT"), (int)ToolType.Axe),
							new IsEqual(new SymbolId("MR"), (int)ResourceType.None)
						},
						new List<IEffect>() {
							new SetValue(new SymbolId("MR"), (int)ResourceType.Planks)
						},
						5
					),
					() => new ActionCutTree(this)
				},
				{
					new PlanningAction(
						"TakeAxe",
						new List<IPrecondition>() {
							new IsEqual(new SymbolId("MT"), (int)ToolType.None),
							new IsNotSmaller(new SymbolId("BAxe"), 1)
						},
						new List<IEffect>() {
							new SetValue(new SymbolId("MT"), (int)ToolType.Axe),
							new Subtract(new SymbolId("BAxe"), 1)
						},
						1
					),
					() => new ActionGetTool(this, ToolType.Axe, this.town.ToolBench)
				},
				{
					new PlanningAction(
						"TakeHammer",
						new List<IPrecondition>() {
							new IsEqual(new SymbolId("MT"), (int)ToolType.None),
							new IsNotSmaller(new SymbolId("BHammer"), 1)
						},
						new List<IEffect>() {
							new SetValue(new SymbolId("MT"), (int)ToolType.Hammer),
							new Subtract(new SymbolId("BHammer"), 1)
						},
						1
					),
					() => new ActionGetTool(this, ToolType.Hammer, this.town.ToolBench)
				},
				{
					new PlanningAction(
						"PutbackAxe",
						new List<IPrecondition>() {
							new IsEqual(new SymbolId("MT"), (int)ToolType.Axe),
						},
						new List<IEffect>() {
							new SetValue(new SymbolId("MT"), (int)ToolType.None),
							new Add(new SymbolId("BAxe"), 1)
						},
						1
					),
					() => new ActionPutTool(this, ToolType.Axe, this.town.ToolBench)
				},
				{
					new PlanningAction(
						"PutbackHammer",
						new List<IPrecondition>() {
							new IsEqual(new SymbolId("MT"), (int)ToolType.Hammer),
						},
						new List<IEffect>() {
							new SetValue(new SymbolId("MT"), (int)ToolType.None),
							new Add(new SymbolId("BHammer"), 1)
						},
						1
					),
					() => new ActionPutTool(this, ToolType.Hammer, this.town.ToolBench)
				}
			};

			return new SimpleActionFactory(actions);
		}

		private abstract class AbstractAction<Subject> : IAction
		{
			protected readonly Subject subject;

			public AbstractAction(Subject subject)
			{
				this.subject = subject;
			}

			#region IAction implementation

			public void StartExecution()
			{
				Debug.LogFormat("Executing {0}...", this.GetType().Name);
				try
				{
					this.StartExecutionImpl();
				}
				catch(InvalidOperationException e)
				{
					Debug.LogWarningFormat("{0} failed: {1}", this.GetType().Name, e.Message);
					Status = ExecutionStatus.Failed;
				}
				ReportStatus();
			}

			public void StartInterruption()
			{
				Debug.LogFormat("Interrupting {0}...", this.GetType().Name);
				this.StartInterruptionImpl();
				ReportStatus();
			}

			public void Update()
			{
				try
				{
					this.UpdateImpl();
					if(this.Status.IsFinal())
					{
						ReportStatus();
					}
				}
				catch(InvalidOperationException e)
				{
					Debug.LogWarningFormat("{0} failed: {1}", this.GetType().Name, e.Message);
					Status = ExecutionStatus.Failed;
				}
			}

			public ExecutionStatus Status
			{
				get;
				protected set;
			}

			#endregion

			private void ReportStatus()
			{
				Debug.LogFormat("{0} is {1}", this.GetType().Name, Status);
			}

			protected abstract void StartExecutionImpl();

			protected abstract void StartInterruptionImpl();

			protected abstract void UpdateImpl();
		}

		private class ActionBuildHouse : AbstractAction<Townsman>
		{
			public const int PLANKS_REQUIRED = 4;
			public const int IRON_REQUIRED = 2;

			private const int EXECUTION_TIME = 5;

			private readonly ITimer timer;

			public ActionBuildHouse(Townsman subject)
				: base(subject)
			{
				this.timer = new ExecutionTimer(true);
			}

			protected override void StartExecutionImpl()
			{
				if(subject.town.House.IsBuilt)
				{
					throw new InvalidOperationException("already built");
				}

				if(subject.town.ConstructionStorage.GetResourceCount(ResourceType.Planks) < PLANKS_REQUIRED
				   || subject.town.ConstructionStorage.GetResourceCount(ResourceType.Iron) < IRON_REQUIRED)
				{
					throw new InvalidOperationException("not enough resources");
				}

				subject.MoveTo(subject.town.House.BuilderPosition.position);

				this.Status = ExecutionStatus.InProgress;
			}

			protected override void StartInterruptionImpl()
			{
				if(!this.Status.IsFinal() && this.Status != ExecutionStatus.InInterruption)
				{
					subject.AnimateTool = false;
					this.Status = ExecutionStatus.Interrupted;
				}
			}

			protected override void UpdateImpl()
			{
				if(Status == ExecutionStatus.InProgress)
				{
					if(timer.IsPaused && subject.ReachedDestination)
					{
						this.subject.town.ConstructionStorage.Planks -= PLANKS_REQUIRED;
						this.subject.town.ConstructionStorage.Iron -= IRON_REQUIRED;

						subject.AnimateTool = true;
						timer.Resume();
					}
					else if(!timer.IsPaused && timer.ElapsedSeconds > EXECUTION_TIME)
					{
						subject.AnimateTool = false;
						subject.town.House.IsBuilt = true;
						this.Status = ExecutionStatus.Complete;
					}
				}
			}
		}

		private class ActionGetResource : AbstractAction<Townsman>
		{
			private readonly ResourceType resourceType;
			private readonly Storage source;

			public ActionGetResource(Townsman subject, ResourceType resource, Storage source)
				: base(subject)
			{
				this.resourceType = resource;
				this.source = PreconditionUtils.EnsureNotNull(source, "source");
			}

			protected override void StartExecutionImpl()
			{
				if(source.GetResourceCount(resourceType) < 1)
				{
					throw new InvalidOperationException("not enough resources");
				}
				else if(subject.CurrentResource != ResourceType.None)
				{
					throw new InvalidOperationException("already carying another resource");
				}
				else
				{
					subject.MoveTo(source.transform.position);
					Status = ExecutionStatus.InProgress;
				}
			}

			protected override void StartInterruptionImpl()
			{
				if(!this.Status.IsFinal() && this.Status != ExecutionStatus.InInterruption)
				{
					this.Status = ExecutionStatus.Interrupted;
				}
			}

			protected override void UpdateImpl()
			{
				if(this.Status == ExecutionStatus.InProgress && subject.ReachedDestination)
				{
					this.subject.CurrentResource = resourceType;
					source.SetResourceCount(resourceType, source.GetResourceCount(resourceType) - 1);
					Status = ExecutionStatus.Complete;
				}
			}
		}

		private class ActionPutResource : AbstractAction<Townsman>
		{
			private readonly ResourceType resourceType;
			private readonly Storage target;

			public ActionPutResource(Townsman subject, ResourceType resource, Storage target)
				: base(subject)
			{
				this.resourceType = resource;
				this.target = PreconditionUtils.EnsureNotNull(target, "target");
			}

			protected override void StartExecutionImpl()
			{
				if(subject.CurrentResource != resourceType)
				{
					throw new InvalidOperationException("not carying the necessary resource");
				}
				else
				{
					subject.MoveTo(target.transform.position);
					this.Status = ExecutionStatus.InProgress;
				}
			}

			protected override void StartInterruptionImpl()
			{
				if(!this.Status.IsFinal() && this.Status != ExecutionStatus.InInterruption)
				{
					this.Status = ExecutionStatus.Interrupted;
				}
			}

			protected override void UpdateImpl()
			{
				if(Status == ExecutionStatus.InProgress && subject.ReachedDestination)
				{
					this.subject.CurrentResource = ResourceType.None;
					target.SetResourceCount(resourceType, target.GetResourceCount(resourceType) + 1);
					Status = ExecutionStatus.Complete;
				}
			}
		}

		private class ActionGetTool : AbstractAction<Townsman>
		{
			private readonly ToolType toolType;
			private readonly ToolBench source;

			public ActionGetTool(Townsman subject, ToolType toolType, ToolBench source)
				: base(subject)
			{
				this.toolType = toolType;
				this.source = PreconditionUtils.EnsureNotNull(source, "source");
			}

			protected override void StartExecutionImpl()
			{
				if(source.GetToolCount(toolType) < 1)
				{
					throw new InvalidOperationException("not enough tools");
				}
				else if(subject.CurrentTool != ToolType.None)
				{
					throw new InvalidOperationException("already carying another tool");
				}
				else
				{
					subject.MoveTo(source.transform.position);
					Status = ExecutionStatus.InProgress;
				}
			}

			protected override void StartInterruptionImpl()
			{
				if(!this.Status.IsFinal() && this.Status != ExecutionStatus.InInterruption)
				{
					this.Status = ExecutionStatus.Interrupted;
				}
			}

			protected override void UpdateImpl()
			{
				if(Status == ExecutionStatus.InProgress && subject.ReachedDestination)
				{
					this.subject.CurrentTool = toolType;
					source.SetToolCount(toolType, source.GetToolCount(toolType) - 1);
					Status = ExecutionStatus.Complete;
				}
			}
		}

		private class ActionPutTool : AbstractAction<Townsman>
		{
			private readonly ToolType toolType;
			private readonly ToolBench target;

			public ActionPutTool(Townsman subject, ToolType toolType, ToolBench target)
				: base(subject)
			{
				this.toolType = toolType;
				this.target = PreconditionUtils.EnsureNotNull(target, "target");
			}

			protected override void StartExecutionImpl()
			{
				if(subject.CurrentTool != toolType)
				{
					throw new InvalidOperationException("not carying the necessary tool");
				}
				else
				{
					subject.MoveTo(target.transform.position);
					Status = ExecutionStatus.InProgress;
				}
			}

			protected override void StartInterruptionImpl()
			{
				if(!this.Status.IsFinal() && this.Status != ExecutionStatus.InInterruption)
				{
					this.Status = ExecutionStatus.Interrupted;
				}
			}

			protected override void UpdateImpl()
			{
				if(Status == ExecutionStatus.InProgress && subject.ReachedDestination)
				{
					this.subject.CurrentTool = ToolType.None;
					target.SetToolCount(toolType, target.GetToolCount(toolType) + 1);
					Status = ExecutionStatus.Complete;
				}
			}
		}

		private class ActionCutTree : AbstractAction<Townsman>
		{
			private const float EXECUTION_TIME = 3.0f;

			private readonly ITimer timer;

			private GameObject selectedTree;

			public ActionCutTree(Townsman subject)
				: base(subject)
			{
				this.timer = new ExecutionTimer(true);
			}

			#region implemented abstract members of AbstractAction

			protected override void StartExecutionImpl()
			{
				if(subject.toolType != ToolType.Axe)
				{
					throw new InvalidOperationException("has no axe");
				}

				this.selectedTree = subject.FindClosestTree();
				subject.MoveTo(selectedTree.transform.position);
				this.Status = ExecutionStatus.InProgress;
			}

			protected override void StartInterruptionImpl()
			{
				if(!this.Status.IsFinal() && this.Status != ExecutionStatus.InInterruption)
				{
					this.subject.AnimateTool = false;
					this.Status = ExecutionStatus.Interrupted;
				}
			}

			protected override void UpdateImpl()
			{
				if(Status == ExecutionStatus.InProgress)
				{
					if(timer.IsPaused && subject.ReachedDestination)
					{
						this.timer.Resume();
						this.subject.AnimateTool = true;
					}
					else if(!timer.IsPaused && timer.ElapsedSeconds > EXECUTION_TIME)
					{
						this.subject.AnimateTool = false;
						if(this.subject.CurrentResource != ResourceType.None)
						{
							throw new InvalidOperationException("already carying another resource");
						}
						this.subject.CurrentResource = ResourceType.Planks;
						Destroy(this.selectedTree);
						this.Status = ExecutionStatus.Complete;
					}
				}
			}

			#endregion


		}
	}
}

