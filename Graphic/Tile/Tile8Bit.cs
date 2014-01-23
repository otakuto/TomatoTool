using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace TomatoTool
{
	public class Tile8Bit : Tile
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

		public const uint SIZE = TILE_LENGTH_0 * TILE_LENGTH_1;
		public override uint Size
		{
			get
			{
				return SIZE;
			}
		}

		public override uint Alignment
		{
			get
			{
				throw new System.NotImplementedException();
			}
		}

		public byte this[int i, int j]
		{
			get
			{
				return tile[i, j];
			}

			set
			{
				if (value < 16)
				{
					tile[i, j] = value;
				}
				else
				{
					throw new Exception();
				}
			}
		}

		//空のタイル
		public static readonly Tile4Bit NotExistTile;

		private Bitmap bitmap;

		public Bitmap Bitmap
		{
			get
			{
				return bitmap;
			}

			set
			{
				if ((value.Width == WIDTH) && (value.Height == HEIGHT) && (value.PixelFormat == PixelFormat.Format8bppIndexed))
				{
				}
				else
				{
					throw new Exception();
				}
			}
		}

		public const uint TILE_LENGTH_0 = 8;
		public const uint TILE_LENGTH_1 = 8;
		
		static Tile8Bit()
		{
			NotExistTile = new Tile4Bit(new byte[TILE_LENGTH_0, TILE_LENGTH_1]);
		}

		public Tile8Bit(TomatoAdventure tomatoAdventure, uint address)
		{
			load(tomatoAdventure, address);
		}

		public void load(TomatoAdventure tomatoAdventure, uint address)
		{
			tile = new byte[TILE_LENGTH_0, TILE_LENGTH_1];

			for (uint y = 0; y < tile.GetLength(1); ++y)
			{
				for (uint x = 0; x < tile.GetLength(0); ++x)
				{
					tile[x, y] = tomatoAdventure.read(address + (y * TILE_LENGTH_0) + x);
				}
			}
		}

		public void save(TomatoAdventure tomatoAdventure, uint address)
		{
			for (uint y = 0; y < tile.GetLength(1); ++y)
			{
				for (uint x = 0; x < tile.GetLength(0); ++x)
				{
					tomatoAdventure.write(address + (y * TILE_LENGTH_0) + x, tile[x, y]);
				}
			}
		}

		public override Bitmap toBitmap(Palette palette)
		{
			throw new System.NotImplementedException();
		}

		public override Bitmap toBitmap(Palette palette, bool flipX, bool flipY)
		{
			throw new System.NotImplementedException();
		}
	}
}
