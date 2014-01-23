using System.Collections.Generic;

namespace TomatoTool
{
	public class AnimationTile : ROMObject
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
				return (uint)(6 + (tileList.Count * ROM.POINTER_SIZE));
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
		
		private List<Tile4BitList> tileList;
		public List<Tile4BitList> TileList
		{
			get
			{
				return tileList;
			}
		}

		private byte updateInterval;
		public byte UpdateInterval
		{
			get
			{
				return updateInterval;
			}

			set
			{
				updateInterval = value;
			}
		}

		private ushort addAddress;
		public ushort AddAddress
		{
			get
			{
				return addAddress;
			}

			set
			{
				addAddress = value;
			}
		}

		//タイルの大きさ
		//0x00 = 1
		//0x01 = 2
		//0xFF = 0x100
		private byte tileSize;
		public byte TileSize
		{
			get
			{
				return tileSize;
			}
		}

		//予想だと全部0x00
		private byte unknownValue1;
		
		public AnimationTile()
		{
			initialize();
		}
		public AnimationTile(TomatoAdventure tomatoAdventure, uint address)
		{
			load(tomatoAdventure, address);
		}

		public void initialize()
		{
			address = ROM.ADDRESS_NULL;

			tileList = new List<Tile4BitList>();

			updateInterval = 0;
			addAddress = 0;
			tileSize = 0;

			unknownValue1 = 0x00;
		}
		public void load(TomatoAdventure tomatoAdventure, uint address)
		{
			this.address = address;

			updateInterval = tomatoAdventure.read(address);
			addAddress = (ushort)tomatoAdventure.readLittleEndian(address + 2, 2);
			byte tileListCount = tomatoAdventure.read(address + 4);
			tileSize = tomatoAdventure.read(address + 5);

			unknownValue1 = tomatoAdventure.read(address + 1);

			tileList = new List<Tile4BitList>();

			for (uint i = 0; i <= tileListCount; ++i)
			{
				tileList.Add((Tile4BitList)tomatoAdventure.add(new Tile4BitList(tomatoAdventure, tomatoAdventure.readAsAddress(address + 6 + (i * ROM.POINTER_SIZE)), tileSize + 1)));
			}
		}
		public void save(TomatoAdventure tomatoAdventure, uint address)
		{
			this.address = address;

			tomatoAdventure.write(address, updateInterval);
			tomatoAdventure.writeLittleEndian(address + 2, 2, addAddress);
			tomatoAdventure.write(address + 4, (byte)(tileList.Count - 1));
			tomatoAdventure.write(address + 5, tileSize);

			for (uint i = 0; i < tileList.Count; ++i)
			{
				tomatoAdventure.writeAsAddress(address + 6 + (i * ROM.POINTER_SIZE), tileList[(int)i].ObjectID);
				tileList[(int)i].save(tomatoAdventure, tileList[(int)i].ObjectID);
			}
		}
		
		public Tile4BitList getTileList(ushort frame)
		{
			return tileList[(frame / updateInterval) % tileList.Count];
		}
		public Tile4BitList getTileList()
		{
			return getTileList(Map.Frame);
		}
	}
}
