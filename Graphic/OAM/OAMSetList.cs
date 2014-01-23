using System.Collections.Generic;

namespace TomatoTool
{
	public class OAMSetList : ROMObject
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
				return (uint)(oamSet.Count * ROM.POINTER_SIZE);
			}
		}

		public const uint ALIGNMENT = 4;
		public override uint Alignment
		{
			get
			{
				return ALIGNMENT;
			}
		}

		public OAMSet this[int i]
		{
			get
			{
				return oamSet[i];
			}

			set
			{
				oamSet[i] = value;
			}
		}
		public int Count
		{
			get
			{
				return oamSet.Count;
			}
		}

		private List<OAMSet> oamSet;
		public List<OAMSet> OAMSet
		{
			get
			{
				return oamSet;
			}
		}

		public OAMSetList(TomatoAdventure tomatoAdventure, uint address)
		{
			load(tomatoAdventure, address);
		}

		public void load(TomatoAdventure tomatoAdventure, uint address)
		{
			this.address = address;

			oamSet = new List<OAMSet>();
			for (uint i = 0; tomatoAdventure.isAddress(tomatoAdventure.readAsAddress(address + (i * ROM.POINTER_SIZE))); ++i)
			{
				oamSet.Add(new OAMSet(tomatoAdventure, (address) + (i * ROM.POINTER_SIZE)));
			}
		}

		public void save(TomatoAdventure tomatoAdventure, uint address)
		{
			this.address = address;

			for (uint i = 0; i < oamSet.Count; ++i)
			{
				oamSet[(int)i].save(tomatoAdventure, oamSet[(int)i].ObjectID);
				tomatoAdventure.writeAsAddress(address + (i * ROM.POINTER_SIZE), oamSet[(int)i].ObjectID);
			}
		}
	}
}
