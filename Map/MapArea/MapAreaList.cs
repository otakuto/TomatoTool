using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;

namespace TomatoTool
{
	public class MapAreaList
		:
		ROMObject
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
				throw new System.NotImplementedException();
			}
		}

		public override uint Alignment
		{
			get
			{
				throw new System.NotImplementedException();
			}
		}

		public static List<uint> Pointer = new List<uint>()
		{
			TomatoTool.ROM.addBase(0x00033EAC)
		};
		public static uint SelectPointer = 0;

		private List<MapArea> mapArea = new List<MapArea>();
		
		public MapAreaList(TomatoAdventure tomatoAdventure)
		{
			load(tomatoAdventure);
		}

		public void load(TomatoAdventure tomatoAdventure)
		{
		}
		public void load(TomatoAdventure tomatoAdventure, uint address)
		{
			for (uint i = 0; address + (i * ROM.POINTER_SIZE) < tomatoAdventure.readAsAddress(address); ++i)
			{
				//mapArea.Add(tomatoAdventure.add<MapArea>(new MapArea(tomatoAdventure,)))
			}
		}

		public void save(TomatoAdventure tomatoAdventure, uint address)
		{
		}
	}
}
