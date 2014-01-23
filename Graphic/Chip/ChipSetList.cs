using System.Collections.Generic;

namespace TomatoTool
{
	public class ChipSetList : ROMObject
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
				return (uint)(chipSet.Count * TomatoTool.ChipSet.SIZE);
			}
		}

		public override uint Alignment
		{
			get
			{
				throw new System.NotImplementedException();
			}
		}

		public ChipSet this[int i]
		{
			get
			{
				return chipSet[i];
			}

			set
			{
				chipSet[i] = value;
			}
		}
		public int Count
		{
			get
			{
				return chipSet.Count;
			}
		}

		private List<ChipSet> chipSet;
		public List<ChipSet> ChipSet
		{
			get
			{
				return chipSet;
			}

			set
			{
				chipSet = value;
			}
		}

		public ChipSetList(TomatoAdventure tomatoAdventure, uint address, ChipSetTable chipSetTable)
		{
			load(tomatoAdventure, address, chipSetTable);
		}

		public void load(TomatoAdventure tomatoAdventure, uint address, ChipSetTable chipSetTable)
		{
			this.address = address;

			chipSet = new List<ChipSet>();

			for (uint i = 0; i < chipSetTable.useChipSetNumber; ++i)
			{
				chipSet.Add(new ChipSet(tomatoAdventure, address + (i * TomatoTool.ChipSet.SIZE)));
			}
		}

		public void save(TomatoAdventure tomatoAdventure)
		{
			if (!saved)
			{
				save(tomatoAdventure, tomatoAdventure.malloc(Size, Alignment));
				saved = true;
			}
		}
		public void save(TomatoAdventure tomatoAdventure, uint address)
		{
			this.address = address;

			for (uint i = 0; i < chipSet.Count; ++i)
			{
				chipSet[(int)i].save(tomatoAdventure, address + (i * TomatoTool.ChipSet.SIZE));
			}
		}

	}
}
