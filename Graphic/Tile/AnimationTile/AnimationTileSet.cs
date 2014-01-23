using System.Collections.Generic;

namespace TomatoTool
{
	public class AnimationTileSet : ROMObject
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
				uint size = 0;
				for (uint i = 0; i < animationTile.Count; ++i)
				{
					size += animationTile[(int)i].Size;
				}

				return size;
			}
		}

		public override uint Alignment
		{
			get
			{
				throw new System.NotImplementedException();
			}
		}

		public static readonly AnimationTileSet NULL;

		public List<uint> Pointer = new List<uint>()
		{
			ROM.addBase(0x00034080),
			ROM.addBase(0x00034E1C),
		};

		private List<AnimationTile> animationTile;
		public List<AnimationTile> AnimationTile
		{
			get
			{
				return animationTile;
			}

			set
			{
				animationTile = value;
			}
		}

		private byte number;
		public byte Number
		{
			get
			{
				return number;
			}

			set
			{
				number = value;
			}
		}

		static AnimationTileSet()
		{
			NULL = new AnimationTileSet();
		}

		public AnimationTileSet()
		{
			initialize();
		}

		public AnimationTileSet(TomatoAdventure tomatoAdventure, uint address)
		{
			load(tomatoAdventure, address);
		}

		public void initialize()
		{
			animationTile = new List<AnimationTile>();
		}

		public void load(TomatoAdventure tomatoAdventure, uint address)
		{
			this.address = address;

			animationTile = new List<AnimationTile>();

			{
				int i = 0;
				uint size = 0;

				do
				{
					animationTile.Add(new AnimationTile(tomatoAdventure, tomatoAdventure.readAsAddress(tomatoAdventure.readAsAddress(Pointer[0]) + ((address - 1) * ROM.POINTER_SIZE)) + size));
					size += animationTile[i].Size;
					++i;
				}
				while (tomatoAdventure.read(tomatoAdventure.readAsAddress(tomatoAdventure.readAsAddress(Pointer[0]) + ((address - 1) * ROM.POINTER_SIZE)) + size) != 0xFF);
			}
		}
		public void save(TomatoAdventure tomatoAdventure, uint address)
		{
			uint size = 0;
			for (uint i = 0; i < animationTile.Count; ++i)
			{
				animationTile[(int)i].save(tomatoAdventure, address + size);
				size += animationTile[(int)i].Size;
			}
		}

		public bool existTile(Chip chip)
		{
			for (int i = 0; i < animationTile.Count; ++i)
			{
				if ((animationTile[i].AddAddress <= chip.TileNumber + 0x400) && (chip.TileNumber + 0x400 <= animationTile[i].AddAddress + animationTile[i].TileSize))
				{
					return true;
				}
			}

			return false;
		}

		public Tile getTile(Chip chip)
		{
			for (int i = 0; i < animationTile.Count; ++i)
			{
				if ((animationTile[i].AddAddress <= chip.TileNumber + 0x400) && (chip.TileNumber + 0x400 <= animationTile[i].AddAddress + animationTile[i].TileSize))
				{
					return animationTile[i].getTileList()[chip.TileNumber + 0x400 - animationTile[i].AddAddress];
				}
			}

			return null;
		}
	}
}
