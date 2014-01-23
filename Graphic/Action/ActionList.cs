using System;
using System.Collections;
using System.Collections.Generic;

namespace TomatoTool
{
	public class ActionList : ROMObject, IList
	{
		private uint address;
		public override uint ObjectID
		{
			get
			{
				return address;
			}

			set
			{
				address = value;
			}
		}

		private bool saved;
		public override bool Saved
		{
			get
			{
				return saved;
			}

			set
			{
				saved = value;
			}
		}
		public override uint Size
		{
			get
			{
				return (uint)(action.Count * ROM.POINTER_SIZE);
			}
		}

		public override uint Alignment
		{
			get
			{
				return ALIGNMENT;
			}
		}

		public Action this[CharacterScriptDirection characterScriptDirection]
		{
			get
			{
				switch (characterScriptDirection)
				{
					case CharacterScriptDirection.SP:
						return (Action)(((IList)this)[4]);
					case CharacterScriptDirection.Right:
						return (Action)(((IList)this)[1]);
					case CharacterScriptDirection.Left:
						return (Action)(((IList)this)[3]);
					case CharacterScriptDirection.Up:
						return (Action)(((IList)this)[0]);
					case CharacterScriptDirection.Down:
						return (Action)(((IList)this)[2]);
					default:
						return (Action)(((IList)this)[0]);
				}
			}

			set
			{
				switch (characterScriptDirection)
				{
					case CharacterScriptDirection.SP:
						((IList)this)[4] = value;
						break;
					case CharacterScriptDirection.Right:
						((IList)this)[1] = value;
						break;
					case CharacterScriptDirection.Left:
						((IList)this)[3] = value;
						break;
					case CharacterScriptDirection.Up:
						((IList)this)[0] = value;
						break;
					case CharacterScriptDirection.Down:
						((IList)this)[2] = value;
						break;
					default:
						((IList)this)[0] = value;
						break;
				}
			}
		}

		//上 4
		//右 1
		//左 2
		//下 8
		private List<Action> action;

		public const uint ALIGNMENT = 4;

		public ActionList()
		{
			initialize();
		}
		public ActionList(TomatoAdventure tomatoAdventure, uint address)
		{
			load(tomatoAdventure, address);
		}

		public void initialize()
		{
			action = new List<Action>();
		}
		public void load(TomatoAdventure tomatoAdventure, uint address)
		{
			this.address = address;

			action = new List<Action>();
			for (uint i = 0; tomatoAdventure.isAddress(tomatoAdventure.readAsAddress(address + (i * ROM.POINTER_SIZE))); ++i)
			{
				action.Add(new Action(tomatoAdventure, tomatoAdventure.readAsAddress(address + (i * ROM.POINTER_SIZE))));
			}
		}
		public void save(TomatoAdventure tomatoAdventure)
		{
			if (!saved)
			{
				save(tomatoAdventure, tomatoAdventure.malloc(Size, Alignment));
			}
		}
		public void save(TomatoAdventure tomatoAdventure, uint address)
		{
			this.address = address;

			for (uint i = 0; i < action.Count; ++i)
			{
				if (action[(int)i] != null)
				{
					action[(int)i].save(tomatoAdventure);
					tomatoAdventure.writeAsAddress(address + (i * ROM.POINTER_SIZE), action[(int)i].ObjectID);
				}
				else
				{
					tomatoAdventure.writeAsAddress(address + (i * ROM.POINTER_SIZE), ROM.ADDRESS_NULL);
				}
			}

			saved = true;
		}

		bool IList.IsFixedSize
		{
			get
			{
				return ((IList)action).IsFixedSize;
			}
		}
		bool IList.IsReadOnly
		{
			get
			{
				return ((IList)action).IsReadOnly;
			}
		}
		object IList.this[int index]
		{
			get
			{
				return ((IList)action)[index];
			}

			set
			{
				((IList)action)[index] = value;
				saved = false;
			}
		}
		int IList.Add(object value)
		{
			saved = false;
			return ((IList)action).Add(value);
		}
		void IList.Clear()
		{
			((IList)action).Clear();
			saved = false;
		}
		bool IList.Contains(object value)
		{
			return ((IList)action).Contains(value);
		}
		int IList.IndexOf(object value)
		{
			return ((IList)action).IndexOf(value);
		}
		void IList.Insert(int index, object value)
		{
			((IList)action).Insert(index, value);
			saved = false;
		}
		void IList.Remove(object value)
		{
			((IList)action).Remove(value);
			saved = false;
		}
		void IList.RemoveAt(int index)
		{
			((IList)action).RemoveAt(index);
			saved = false;
		}
		int ICollection.Count
		{
			get
			{
				return ((ICollection)action).Count;
			}
		}
		bool ICollection.IsSynchronized
		{
			get
			{
				return ((ICollection)action).IsSynchronized;
			}
		}
		object ICollection.SyncRoot
		{
			get
			{
				return ((ICollection)action).SyncRoot;
			}
		}
		void ICollection.CopyTo(Array array, int index)
		{
			((ICollection)action).CopyTo(array, index);
		}
		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable)action).GetEnumerator();
		}
	}
}
