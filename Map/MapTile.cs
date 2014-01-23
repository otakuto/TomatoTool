using System.Drawing;
using System.Drawing.Imaging;

namespace TomatoTool
{
	public class MapTile
	{
		private LZ77 mainTile;
		public LZ77 MainTile
		{
			get
			{
				return mainTile;
			}

			set
			{
				mainTile = value;
			}
		}

		private AnimationTileSet animationTileSet;
		public AnimationTileSet AnimationTileSet
		{
			get
			{
				return animationTileSet;
			}

			set
			{
				animationTileSet = value;
			}
		}

		public static uint MainTileTopAddress;
		public static uint AnimationTileSetTopAddress;

		public MapTile(TomatoAdventure tomatoAdventure, Map map)
		{
			MainTileTopAddress = tomatoAdventure.readAsAddress(ROM.addBase(0x034D70));
			AnimationTileSetTopAddress = tomatoAdventure.readAsAddress(ROM.addBase(0x034080));

			load(tomatoAdventure, map);
		}

		public void load(TomatoAdventure tomatoAdventure, Map map)
		{
			mainTile = tomatoAdventure.add(new LZ77(tomatoAdventure, tomatoAdventure.readAsAddress(MainTileTopAddress + (map.Number * ROM.POINTER_SIZE))));

			if (map.AnimationTileSetNumber == 0x00)
			{
				animationTileSet = (AnimationTileSet)tomatoAdventure.add(AnimationTileSet.NULL);
			}
			else
			{
				animationTileSet = (AnimationTileSet)tomatoAdventure.add(new AnimationTileSet(tomatoAdventure, map.AnimationTileSetNumber));
			}

		}

		public void save(TomatoAdventure tomatoAdventure, Map map)
		{
			mainTile.save(tomatoAdventure);
			tomatoAdventure.writeAsAddress(MainTileTopAddress + (map.Number * ROM.POINTER_SIZE), mainTile.ObjectID);
		}

		public Bitmap toBitmap(Chip chip, Palette[] palette, TransitionPalette transitionPalette)
		{
			//transitionPaletteが実装されたらこれ消せ
			if (animationTileSet.existTile(chip))
			{
				return animationTileSet.getTile(chip).toBitmap(palette[chip.PaletteNumber], chip.FlipX, chip.FlipY);
			}
			else
			{
				if (chip.TileNumber < mainTile.Count)
				{
					return mainTile[chip.TileNumber].toBitmap(palette[chip.PaletteNumber], chip.FlipX, chip.FlipY);
				}
				else
				{
					return Tile4Bit.NotExistTile.toBitmap(palette[chip.PaletteNumber]);
				}
			}
			//transitionPaletteが実装されたらこれだけ残せ
			/*
			try
			{
				if (chip.TileNumber < 0x0300)
				{
					return mainTile[chip.TileNumber].toBitmap(((chip.PaletteNumber == transitionPalette.OverwritePaletteNumber) ? transitionPalette.getPalette() : palette[chip.PaletteNumber]), chip.FlipX, chip.FlipY);
				}
				else
				{
					//return animationTile.getTileList()[chip.TileNumber - 0x0300].toBitmap(((chip.PaletteNumber == transitionPalette.OverwritePaletteNumber) ? transitionPalette.getPalette() : palette[chip.PaletteNumber]), chip.FlipX, chip.FlipY);
				}
			}
			catch
			{
				return Tile4Bit.NotExistTile.toBitmap(palette[chip.PaletteNumber]);
			}
			*/
		}
		public Bitmap toBitmap(ChipSet chipSet, Palette[] palette, TransitionPalette transitionPalette, bool transparent)
		{
			Bitmap bitmap = new Bitmap(ChipSet.WIDTH, ChipSet.HEIGHT);

			if (transparent)
			{
				using (Graphics graphics = Graphics.FromImage(bitmap))
				using (ImageAttributes imageAttributes = new ImageAttributes())
				{
					for (int y = 0; y < chipSet.GetLength(1); ++y)
					{
						for (int x = 0; x < chipSet.GetLength(0); ++x)
						{
							Color color = palette[chipSet[x, y].PaletteNumber][0].toColor();
							imageAttributes.SetColorKey(color, color);

							using (Bitmap b = toBitmap(chipSet[x, y], palette, transitionPalette))
							{
								graphics.DrawImage(b, new Rectangle(x * Chip.WIDTH, y * Chip.HEIGHT, Chip.WIDTH, Chip.HEIGHT), 0, 0, Chip.WIDTH, Chip.HEIGHT, GraphicsUnit.Pixel, imageAttributes);
							}
						}
					}
				}
			}
			else
			{
				using (Graphics graphics = Graphics.FromImage(bitmap))
				{
					for (int y = 0; y < chipSet.GetLength(1); ++y)
					{
						for (int x = 0; x < chipSet.GetLength(0); ++x)
						{
							using (Bitmap b = toBitmap(chipSet[x, y], palette, transitionPalette))
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
