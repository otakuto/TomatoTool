using System.Drawing;

namespace TomatoTool
{
	public class Monster : ROMObject
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

		public const uint SIZE = 76;
		public override uint Size
		{
			get
			{
				return SIZE;
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

		public const uint NAME_LENGTH_0 = 8;
		private StatusString name;
		public StatusString Name
		{
			get
			{
				return name;
			}

			set
			{
				name = value;
			}
		}

		private ushort hp;
		public ushort HP
		{
			get
			{
				return hp;
			}

			set
			{
				hp = value;
			}
		}

		private ushort speed;
		public ushort Speed
		{
			get
			{
				return speed;
			}

			set
			{
				speed = value;
			}
		}

		private ushort diffence;
		public ushort Diffence
		{
			get
			{
				return diffence;
			}

			set
			{
				diffence = value;
			}
		}

		private ushort experience;
		public ushort Experience
		{
			get
			{
				return experience;
			}

			set
			{
				experience = value;
			}
		}

		private ushort money;
		public ushort Money
		{
			get
			{
				return money;
			}

			set
			{
				money = value;
			}
		}

		private byte imageSize;
		public byte ImageSize
		{
			get
			{
				return imageSize;
			}

			set
			{
				imageSize = value;
			}
		}

		public const int TECHNICAL_LENGTH_0 = 3;
		private Technical[] technical;
		public Technical[] Technical
		{
			get
			{
				return technical;
			}

			set
			{
				technical = value;
			}
		}

		private Palette palette;
		public Palette Palette
		{
			get
			{
				return palette;
			}

			set
			{
				palette = value;
			}
		}

		private OAMSetList oamSetList;
		public OAMSetList OAMSetList
		{
			get
			{
				return oamSetList;
			}

			set
			{
				oamSetList = value;
			}
		}

		private ActionList actionList;
		public ActionList ActionList
		{
			get
			{
				return actionList;
			}

			set
			{
				actionList = value;
			}
		}

		public const uint TILE_LIST_LENGTH_0 = 2;
		public Tile4BitList[] tileList;

		public Monster(TomatoAdventure tomatoAdventure, uint address)
		{
			load(tomatoAdventure, address);
		}

		public void load(TomatoAdventure tomatoAdventure, uint address)
		{
			this.address = address;

			name = tomatoAdventure.add(new StatusString(tomatoAdventure, address, NAME_LENGTH_0));
			hp = (ushort)tomatoAdventure.readLittleEndian(address + 10, 2);
			speed = (ushort)tomatoAdventure.readLittleEndian(address + 12, 2);
			diffence = (ushort)tomatoAdventure.readLittleEndian(address + 14, 2);
			experience = (ushort)tomatoAdventure.readLittleEndian(address + 40, 2);
			money = (ushort)tomatoAdventure.readLittleEndian(address + 42, 2);
			imageSize = tomatoAdventure.read(address + 47);

			technical = new Technical[TECHNICAL_LENGTH_0];
			for (uint i = 0; i < TECHNICAL_LENGTH_0; ++i)
			{
				technical[i] = tomatoAdventure.add(new Technical(tomatoAdventure, tomatoAdventure.readAsAddress(address + 28 + (i * ROM.POINTER_SIZE))));
			}

			if (tomatoAdventure.readAsAddress(address + 52) != ROM.ADDRESS_NULL)
			{
				palette = tomatoAdventure.add(new Palette(tomatoAdventure, tomatoAdventure.readAsAddress(address + 52)));
			}

			if (tomatoAdventure.readAsAddress(address + 68) != ROM.ADDRESS_NULL)
			{
				oamSetList = tomatoAdventure.add(new OAMSetList(tomatoAdventure, tomatoAdventure.readAsAddress(tomatoAdventure.readAsAddress(address + 68))));
			}

			if (tomatoAdventure.readAsAddress(address + 72) != ROM.ADDRESS_NULL)
			{
				actionList = tomatoAdventure.add(new ActionList(tomatoAdventure, tomatoAdventure.readAsAddress(tomatoAdventure.readAsAddress(address + 72))));
			}

			tileList = new Tile4BitList[TILE_LIST_LENGTH_0];
			for (uint i = 0; i < tileList.GetLength(0); ++i)
			{
				if (tomatoAdventure.readAsAddress(address + 56 + (i * ROM.POINTER_SIZE)) != ROM.ADDRESS_NULL)
				{
					if (tomatoAdventure.read(tomatoAdventure.readAsAddress(address + 56) + (i * ROM.POINTER_SIZE)) == LZ77.HEADER)
					{
						tileList[i] = tomatoAdventure.add(new LZ77(tomatoAdventure, tomatoAdventure.readAsAddress(address + 56) + (i * ROM.POINTER_SIZE)));
					}
					else
					{
					}
				}
			}
		}

		public void save(TomatoAdventure tomatoAdventure, uint address)
		{

		}

		public static bool compatible(TomatoAdventure tomatoAdventure, uint address)
		{
			return
				tomatoAdventure.isAddress(tomatoAdventure.readAsAddress(address + 20)) &&
				tomatoAdventure.isAddress(tomatoAdventure.readAsAddress(address + 24)) &&
				tomatoAdventure.isAddress(tomatoAdventure.readAsAddress(address + 28)) &&
				tomatoAdventure.isAddress(tomatoAdventure.readAsAddress(address + 32)) &&
				tomatoAdventure.isAddress(tomatoAdventure.readAsAddress(address + 36)) &&
				(tomatoAdventure.isAddress(tomatoAdventure.readAsAddress(address + 52)) || (ROM.ADDRESS_NULL == tomatoAdventure.readAsAddress(address + 52))) &&
				(tomatoAdventure.isAddress(tomatoAdventure.readAsAddress(address + 56)) || (ROM.ADDRESS_NULL == tomatoAdventure.readAsAddress(address + 56))) &&
				(tomatoAdventure.isAddress(tomatoAdventure.readAsAddress(address + 60)) || (ROM.ADDRESS_NULL == tomatoAdventure.readAsAddress(address + 60))) &&
				(tomatoAdventure.isAddress(tomatoAdventure.readAsAddress(address + 64)) || (ROM.ADDRESS_NULL == tomatoAdventure.readAsAddress(address + 64))) &&
				(tomatoAdventure.isAddress(tomatoAdventure.readAsAddress(address + 68)) || (ROM.ADDRESS_NULL == tomatoAdventure.readAsAddress(address + 68))) &&
				(tomatoAdventure.isAddress(tomatoAdventure.readAsAddress(address + 72)) || (ROM.ADDRESS_NULL == tomatoAdventure.readAsAddress(address + 72)));
		}

		public void draw(Graphics graphics)
		{
			int width = tileList[0].Count;
			int height = 1;
			switch (imageSize)
			{
				case 0:
					width = 5;
					height = 5;
					break;

				case 1:
					width = 7;
					height = 7;
					break;

				case 2:
					width = 10;
					height = 5;
					break;

				case 3:
					//width = 5;
					//height = 10;
					break;
			}

			for (int y = 0; y < height; ++y)
			{
				for (int x = 0; x < width; ++x)
				{
					using (Bitmap bitmap = tileList[0][(y * width) + x].toBitmap(palette))
					{
						graphics.DrawImage(bitmap, (x * Tile.WIDTH), (y * Tile.HEIGHT));
					}
				}
			}
		}
	}
}
