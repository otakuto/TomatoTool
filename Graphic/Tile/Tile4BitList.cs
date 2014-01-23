using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;

namespace TomatoTool
{
	public class Tile4BitList
		:
		ROMObject
	{
		protected uint address;
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

		protected bool saved;
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
				return (uint)tile.Count * Tile4Bit.SIZE;
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

		public Tile4Bit this[int i]
		{
			get
			{
				return tile[i];
			}

			set
			{
				tile[i] = value;
			}
		}
		public int Count
		{
			get
			{
				return tile.Count;
			}
		}

		protected List<Tile4Bit> tile;
		public List<Tile4Bit> Tile
		{
			get
			{
				return tile;
			}

			set
			{
				tile = value;
			}
		}

		protected Tile4BitList(uint address)
		{
		}
		public Tile4BitList(Tile4Bit[] tile)
		{
			this.tile = new List<Tile4Bit>(tile);
		}
		public Tile4BitList(TomatoAdventure tomatoAdventure, uint address, int size)
		{
			load(tomatoAdventure, address, size);
		}

		public void load(TomatoAdventure tomatoAdventure, uint address, int size)
		{
			this.address = address;

			tile = new List<Tile4Bit>();
			for (uint i = 0; i < size; ++i)
			{
				tile.Add(new Tile4Bit(tomatoAdventure, address + (i * TomatoTool.Tile4Bit.SIZE)));
			}
		}

		public void save(TomatoAdventure tomatoAdventure, uint address)
		{
			for (uint i = 0; i < tile.Count; ++i)
			{
				tile[(int)i].save(tomatoAdventure, address + (i * Tile4Bit.SIZE));
			}
		}

		public Bitmap toBitmap(Chip chip, Palette[] palette)
		{
			if (chip.TileNumber < tile.Count)
			{
				return tile[chip.TileNumber].toBitmap(palette[chip.PaletteNumber], chip.FlipX, chip.FlipY);
			}
			else
			{
				return Tile4Bit.NotExistTile.toBitmap(palette[chip.PaletteNumber]);
			}
		}

		public Bitmap toBitmap(ChipSet chipSet, Palette[] palette, bool transparent)
		{
			Bitmap bitmap = new Bitmap(ChipSet.WIDTH, ChipSet.HEIGHT);

			using (Graphics graphics = Graphics.FromImage(bitmap))
			{
				if (transparent)
				{
					using (ImageAttributes imageAttributes = new ImageAttributes())
					{
						for (int y = 0; y < chipSet.GetLength(1); ++y)
						{
							for (int x = 0; x < chipSet.GetLength(0); ++x)
							{
								Color color = palette[chipSet[x, y].PaletteNumber][0].toColor();
								imageAttributes.SetColorKey(color, color);

								using (Bitmap b = toBitmap(chipSet[x, y], palette))
								{
									graphics.DrawImage(b, new Rectangle(x * Chip.WIDTH, y * Chip.HEIGHT, Chip.WIDTH, Chip.HEIGHT), 0, 0, Chip.WIDTH, Chip.HEIGHT, GraphicsUnit.Pixel, imageAttributes);
								}
							}
						}
					}
				}
				else
				{
					for (int y = 0; y < chipSet.GetLength(1); ++y)
					{
						for (int x = 0; x < chipSet.GetLength(0); ++x)
						{
							using (Bitmap b = toBitmap(chipSet[x, y], palette))
							{
								graphics.DrawImage(b, x * Chip.WIDTH, y * Chip.HEIGHT);
							}
						}
					}

				}
			}

			return bitmap;
		}
	}
}
