using System;
using TomatoTool;

namespace TomatoTool
{
	public class Chip : ICloneable
	{
		private ushort tileNumber;
		public ushort TileNumber
		{
			get
			{
				return tileNumber;
			}

			set
			{
				tileNumber = value;
			}
		}

		private byte paletteNumber;
		public byte PaletteNumber
		{
			get
			{
				return paletteNumber;
			}

			set
			{
				if ((0x00 <= value) && (value <= 0x0F))
				{
					paletteNumber = value;
				}
				else
				{
					throw new Exception();
				}
			}
		}

		private bool flipX;
		public bool FlipX
		{
			get
			{
				return flipX;
			}

			set
			{
				flipX = value;
			}
		}

		private bool flipY;
		public bool FlipY
		{
			get
			{
				return flipY;
			}

			set
			{
				flipY = value;
			}
		}

		public const int WIDTH = TomatoTool.Tile.WIDTH;
		public const int HEIGHT = TomatoTool.Tile.HEIGHT;

		public const uint SIZE = 2;

		public Chip()
		{
			initialize();
		}
		public Chip(ushort chip)
		{
			set(chip);
		}
		public Chip(TomatoAdventure tomatoAdventure, uint address)
		{
			load(tomatoAdventure, address);
		}

		public void initialize()
		{
			tileNumber = (ushort)0x03FF;
			paletteNumber = 0;

			flipX = false;
			flipY = false;
		}

		public void load(TomatoAdventure tomatoAdventure, uint address)
		{
			set((ushort)tomatoAdventure.readLittleEndian(address, SIZE));
		}

		public void save(TomatoAdventure tomatoAdventure, uint address)
		{
			tomatoAdventure.writeLittleEndian(address, SIZE, get());
		}

		public void set(ushort chip)
		{
			tileNumber = (ushort)(chip & 0x03FF);
			paletteNumber = (byte)((chip & 0xF000) >> 12);

			flipX = Convert.ToBoolean(chip & 0x0400);
			flipY = Convert.ToBoolean(chip & 0x0800);
		}
		public ushort get()
		{
			return (ushort)(tileNumber + (paletteNumber << 12) + (Convert.ToByte(flipX) << 10) + (Convert.ToByte(flipY) << 11));
		}

		object ICloneable.Clone()
		{
			return Clone();
		}
		public object Clone()
		{
			return new Chip(this.get());
		}
	}
}
