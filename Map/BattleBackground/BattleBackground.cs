using System;
using System.Collections.Generic;
using System.Drawing;

namespace TomatoTool
{
	public class BattleBackground
		:
		ROMObject
	{
		public override uint ObjectID
		{
			get
			{
				return number;
			}

			set
			{
				number = (byte)value;
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

		public static List<uint> PaletteTopAddressPointer = new List<uint>()
		{
			ROM.addBase(0x0004A078),
			ROM.addBase(0x0004A0BC),
		};
		public static List<uint> LZ77TopAddressPointer = new List<uint>()
		{
			ROM.addBase(0x0004A030),
		};
		public static List<uint> ChipTopAddressPointer = new List<uint>()
		{
			ROM.addBase(0x0004A03C),
		};
		
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

		private LZ77 lz77;
		public LZ77 LZ77
		{
			get
			{
				return lz77;
			}

			set
			{
				lz77 = value;
			}
		}

		private Chip[,] chip;
		public Chip[,] Chip
		{
			get
			{
				return chip;
			}

			set
			{
				chip = value;
			}
		}

		private Palette[] palette;
		public Palette[] Palette
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

		public const uint CHIP_LENGTH_0 = 32;
		public const uint CHIP_LENGTH_1 = 16;

		public const uint PALETTE_LENGTH_0 = 4;

		public const uint WIDTH = 256;
		public const uint HEIGHT = 128;

		public BattleBackground()
		{
			initialize();
		}
		public BattleBackground(TomatoAdventure tomatoAdventure, byte number)
		{
			load(tomatoAdventure, number);
		}

		public void initialize()
		{

		}
		public void load(TomatoAdventure tomatoAdventure, byte number)
		{
			this.number = number;

			palette = new Palette[PALETTE_LENGTH_0];
			for (uint i = 0; i < palette.GetLength(0); ++i)
			{
				palette[i] = new Palette(tomatoAdventure, tomatoAdventure.readAsAddress(tomatoAdventure.readAsAddress(PaletteTopAddressPointer[0]) + (number * ROM.POINTER_SIZE)) + (i * TomatoTool.Palette.SIZE));
			}

			lz77 = new LZ77(tomatoAdventure, tomatoAdventure.readAsAddress(tomatoAdventure.readAsAddress(LZ77TopAddressPointer[0]) + (number * ROM.POINTER_SIZE)));

			chip = new Chip[CHIP_LENGTH_0, CHIP_LENGTH_1];
			for (uint y = 0; y < chip.GetLength(1); ++y)
			{
				for (uint x = 0; x < chip.GetLength(0); ++x)
				{
					chip[x, y] = new Chip(tomatoAdventure, tomatoAdventure.readAsAddress(tomatoAdventure.readAsAddress(ChipTopAddressPointer[0]) + (number * ROM.POINTER_SIZE)) + (x * TomatoTool.Chip.SIZE) + (y * CHIP_LENGTH_0 * TomatoTool.Chip.SIZE));
					chip[x, y].TileNumber -= 0x200;
				}
			}
		}
		public void save(TomatoAdventure tomatoAdventure, uint address)
		{

		}
		
		public Bitmap toBitmap()
		{
			Bitmap bitmap = new Bitmap((int)(CHIP_LENGTH_0 * Tile.WIDTH), (int)(CHIP_LENGTH_1 * Tile.WIDTH));
			using (Graphics graphics = Graphics.FromImage(bitmap))
			{
				Palette[] palette = new Palette[16]
				{
					TomatoTool.Palette.GrayScale,
					TomatoTool.Palette.GrayScale,
					TomatoTool.Palette.GrayScale,
					TomatoTool.Palette.GrayScale,
					TomatoTool.Palette.GrayScale,
					TomatoTool.Palette.GrayScale,
					TomatoTool.Palette.GrayScale,
					TomatoTool.Palette.GrayScale,
					TomatoTool.Palette.GrayScale,
					TomatoTool.Palette.GrayScale,
					this.palette[0],
					this.palette[1],
					this.palette[2],
					this.palette[3],
					TomatoTool.Palette.GrayScale,
					TomatoTool.Palette.GrayScale,
				};

				for (int y = 0; y < CHIP_LENGTH_1; ++y)
				{
					for (int x = 0; x < CHIP_LENGTH_0; ++x)
					{
						using (Bitmap b = lz77.toBitmap(chip[x, y], palette))
						{
							graphics.DrawImage(b, x * Tile.WIDTH, y * Tile.HEIGHT);
						}
					}
				}
			}

			return bitmap;
		}
	}
}
