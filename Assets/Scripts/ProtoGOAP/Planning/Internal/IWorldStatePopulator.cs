using System;

namespace ProtoGOAP.Planning.Internal
{
	// This is a temporary interface for temporary classes,
	// which populate WorldState based on IKnowledgeProvider.
	internal interface IWorldStatePopulator
	{
		WorldState PopulateWorldState(IKnowledgeProvider knowledgeProvider, WorldState initialWorldState = default(WorldState));
	}
}

