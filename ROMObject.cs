using System;
using System.Drawing;
using TomatoTool;

namespace TomatoTool
{
	public abstract class ROMObject
		:
		IComparable<ROMObject>
	{
		int IComparable<ROMObject>.CompareTo(ROMObject romObject)
		{
			return (int)(this.ObjectID - romObject.ObjectID);
		}

		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}

			ROMObject r = obj as ROMObject;
			if ((object)r == null)
			{
				return false;
			}

			return this.ObjectID == r.ObjectID;
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		public static bool operator ==(ROMObject left, ROMObject right)
		{
			if (((object)left != null) && ((object)right != null))
			{
				return left.ObjectID == right.ObjectID;
			}
			else
			{
				return (object)left == (object)right;
			}
		}
		public static bool operator !=(ROMObject left, ROMObject right)
		{
			return !(left == right);
		}

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

		public abstract uint Size
		{
			get;
		}

		public abstract uint Alignment
		{
			get;
		}

		public ROMObject()
		{
			Saved = true;
		}
	}
}
