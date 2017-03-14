using System;

namespace ProtoGOAP.Planning
{
	public interface IKnowledgeProvider
	{
		int GetSymbolValue(SymbolId symbolId);
		// TODO: Generalize, if needed
		//Value GetSymbolValue<Value>(SymbolId symbolId);
	}
}

