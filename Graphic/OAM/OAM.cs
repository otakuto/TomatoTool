using System;
using System.Drawing;

namespace TomatoTool
{
	public class OAM : ROMObject
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

		public const uint SIZE = 6;
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

		private ushort x;
		public short X
		{
			get
			{
				return ((x & 0x0100) == 0x0100) ? (short)(-(OAMSet.WIDTH - x)) : (short)x;
			}

			set
			{
				x = (value > 0) ? (ushort)value : (ushort)(OAMSet.WIDTH + x);
				saved = false;
			}
		}

		private byte y;
		public sbyte Y
		{
			get
			{
				return ((y & 0xF0) == 0xF0) ? (sbyte)(-(OAMSet.HEIGHT - y)) : (sbyte)y;
			}

			set
			{
				y = (value > 0) ? (byte)value : (byte)(OAMSet.HEIGHT + y);
				saved = false;
			}
		}

		private OAMSize oamSize;
		public OAMSize OAMSize
		{
			get
			{
				return oamSize;
			}

			set
			{
				oamSize = value;
				saved = false;
			}
		}

		public byte Width
		{
			get
			{
				switch (oamSize)
				{
					case OAMSize._8x8:
						return 1;

					case OAMSize._16x16:
						return 2;

					case OAMSize._32x32:
						return 4;

					case OAMSize._64x64:
						return 8;


					case OAMSize._16x8:
						return 2;

					case OAMSize._32x8:
						return 4;

					case OAMSize._32x16:
						return 4;

					case OAMSize._64x32:
						return 8;


					case OAMSize._8x16:
						return 1;

					case OAMSize._8x32:
						return 1;

					case OAMSize._16x32:
						return 2;

					case OAMSize._32x64:
						return 4;


					default:
						throw new Exception();
				}
			}
		}

		public byte Height
		{
			get
			{
				switch (oamSize)
				{
					case OAMSize._8x8:
						return 1;

					case OAMSize._16x16:
						return 2;

					case OAMSize._32x32:
						return 4;

					case OAMSize._64x64:
						return 8;


					case OAMSize._16x8:
						return 1;

					case OAMSize._32x8:
						return 1;

					case OAMSize._32x16:
						return 2;

					case OAMSize._64x32:
						return 4;


					case OAMSize._8x16:
						return 2;

					case OAMSize._8x32:
						return 4;

					case OAMSize._16x32:
						return 4;

					case OAMSize._32x64:
						return 8;


					default:
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
				saved = false;
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
				saved = false;
			}
		}

		private byte tileNumber;
		public byte TileNumber
		{
			get
			{
				return tileNumber;
			}

			set
			{
				tileNumber = value;
				saved = false;
			}
		}

		private byte bgPriority;

		private byte paletteNumber;

		public OAM(TomatoAdventure tomatoAdventure, uint address)
			
		{
			load(tomatoAdventure, address);
		}

		public void initialize()
		{
			
		}
		public void load(TomatoAdventure tomatoAdventure, uint address)
		{
			this.address = address;

			x = (ushort)(((tomatoAdventure.read(address + 3) & 0x01) << 8) + tomatoAdventure.read(address + 2));
			y = tomatoAdventure.read(address);

			tileNumber = tomatoAdventure.read(address + 4);

			flipX = (tomatoAdventure.read(address + 3) & 0x10) != 0;
			flipY = (tomatoAdventure.read(address + 3) & 0x20) != 0;

			bgPriority = (byte)((tomatoAdventure.read(address + 5) & 0x0C) >> 2);
			paletteNumber = (byte)((tomatoAdventure.read(address + 5) & 0xF0) >> 4);

			oamSize = (OAMSize)((tomatoAdventure.read(address + 1) >> 6) + ((tomatoAdventure.read(address + 3) >> 6) << 4));
		}

		public void save(TomatoAdventure tomatoAdventure, uint address)
		{
			this.address = address;

			tomatoAdventure.write(address, y);
			tomatoAdventure.write(address + 1, (byte)(((byte)oamSize & 0x03) << 6));
			tomatoAdventure.write(address + 2, (byte)(x & 0xFF));
			tomatoAdventure.write(address + 3, (byte)((((byte)oamSize & 0x30) << 2) + (Convert.ToByte(flipY) << 5) + (Convert.ToByte(flipX) << 4) + ((x & 0x0100) >> 8)));
			tomatoAdventure.write(address + 4, tileNumber);
			tomatoAdventure.write(address + 5, (byte)((paletteNumber << 4) + (bgPriority << 2)));
		}

		public Bitmap toBitmap(Tile4BitList tileList, Palette palette)
		{
			Bitmap bitmap = new Bitmap(Width * Tile.WIDTH, Height * Tile.HEIGHT);

			using (Graphics graphics = Graphics.FromImage(bitmap))
			{
				int i = 0;

				for (int y = 0; y < Height; ++y)
				{
					for (int x = 0; x < Width; ++x)
					{
						using (Bitmap b = tileList[tileNumber + i].toBitmap(palette))
						{
							graphics.DrawImage(b, (x * Tile.WIDTH), (y * Tile.HEIGHT));
						}
						++i;
					}
				}
			}

			if (flipX && flipY)
			{
				bitmap.RotateFlip(RotateFlipType.RotateNoneFlipXY);
			}
			else if (flipX)
			{
				bitmap.RotateFlip(RotateFlipType.RotateNoneFlipX);
			}
			else if (flipY)
			{
				bitmap.RotateFlip(RotateFlipType.RotateNoneFlipY);
			}

			return bitmap;
		}
	}
}
