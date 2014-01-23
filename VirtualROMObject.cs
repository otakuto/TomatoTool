using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TomatoTool
{
	public abstract class VirtualROMObject
	{
		public abstract uint ObjectID
		{
			get;
			set;
		}

		public abstract bool Saved
		{
			get;
			set;
		}
	}
}
