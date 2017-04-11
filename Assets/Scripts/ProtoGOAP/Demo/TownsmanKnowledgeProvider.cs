using UnityEngine;
using System;

using Terrapass.Debug;

using Terrapass.GameAi.Goap.Planning;

namespace ProtoGOAP.Demo
{
	public partial class Townsman
	{
		private class KnowledgeProvider : IKnowledgeProvider
		{
			private readonly Townsman townsman;

			public KnowledgeProvider(Townsman townsman)
			{
				DebugUtils.Assert(townsman != null, "townsman must not be null");
				this.townsman = townsman;
			}

			#region IKnowledgeProvider implementation

			public int GetSymbolValue(SymbolId symbolId)
			{
				if(symbolId.Name[0] == 'M')
				{
					return symbolId.Name[1] == 'R'
						? (int)this.townsman.resourceType
						: (int)this.townsman.toolType;
				}

				if(symbolId.Name[0] == 'S')
				{
					return this.townsman.town.MainStorage.GetResourceCount((ResourceType)Enum.Parse(typeof(ResourceType), symbolId.Name.Substring(1)));
				}

				if(symbolId.Name[0] == 'C')
				{
					return this.townsman.town.ConstructionStorage.GetResourceCount((ResourceType)Enum.Parse(typeof(ResourceType), symbolId.Name.Substring(1)));
				}

				if(symbolId.Name[0] == 'B')
				{
					return this.townsman.town.ToolBench.GetToolCount((ToolType)Enum.Parse(typeof(ToolType), symbolId.Name.Substring(1)));
				}

				if(symbolId.Name == "HouseBuilt")
				{
					return townsman.town.House.IsBuilt ? 1 : 0;
				}

				throw new ArgumentException(string.Format("Unrecognized symbol {0}", symbolId), "symbolId");
			}

			#endregion
		}
	}
}
