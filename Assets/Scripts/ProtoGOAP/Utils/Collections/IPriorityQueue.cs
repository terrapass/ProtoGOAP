using System;
using System.Collections.Generic;

namespace ProtoGOAP.Utils.Collections
{
	public interface IPriorityQueue<T> : ICollection<T>
	{
		T Front {get;}

		T PopFront();
	}
}

