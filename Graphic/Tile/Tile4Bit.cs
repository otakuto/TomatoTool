using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace TomatoTool
{
	public class Tile4Bit : Tile
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

		public const uint ALIGNMENT = 4;
		public override uint Alignment
		{
			get
			{
				return ALIGNMENT;
			}
		}

		public const uint TILE_LENGTH_0 = 4;
		public const uint TILE_LENGTH_1 = 8;
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
				if ((value.Width == WIDTH) && (value.Height == HEIGHT) && (value.PixelFormat == PixelFormat.Format4bppIndexed))
				{
				}
				else
				{
					throw new Exception();
				}
			}
		}

		static Tile4Bit()
		{
			NotExistTile = new Tile4Bit(new byte[TILE_LENGTH_0, TILE_LENGTH_1]);
		}

		public Tile4Bit(TomatoAdventure tomatoAdventure, uint address)
		{
			load(tomatoAdventure, address);
		}
		public Tile4Bit(byte[] tile)
		{
			set(tile);
		}
		public Tile4Bit(byte[,] tile)
		{
			set(tile);
		}

		public void initialize()
		{
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

		public void set(byte[] tile)
		{
			if (tile.GetLength(0) == (TILE_LENGTH_0 * TILE_LENGTH_1))
			{
				this.tile = new byte[TILE_LENGTH_0, TILE_LENGTH_1];

				for (uint y = 0; y < tile.GetLength(1); ++y)
				{
					for (uint x = 0; x < tile.GetLength(0); ++x)
					{
						this.tile[x, y] = tile[(y * TILE_LENGTH_0) + x];
					}
				}
			}
			else
			{
				throw new Exception();
			}
		}
		public void set(byte[,] tile)
		{
			if ((tile.GetLength(0) == TILE_LENGTH_0) && (tile.GetLength(1) == TILE_LENGTH_1))
			{
				this.tile = tile;
			}
			else
			{
				throw new Exception();
			}
		}

		public byte[,] get()
		{
			return tile;
		}

		public override Bitmap toBitmap(Palette palette)
		{
			if (this.bitmap == null)
			{
				this.bitmap = new Bitmap(WIDTH, HEIGHT, PixelFormat.Format4bppIndexed);

				BitmapData bitmapData = this.bitmap.LockBits(new Rectangle(0, 0, this.bitmap.Width, this.bitmap.Height), ImageLockMode.ReadWrite, PixelFormat.Format4bppIndexed);
				IntPtr intPtr = bitmapData.Scan0;
				byte[] data = new byte[Math.Abs(bitmapData.Stride) * this.bitmap.Height];
				Marshal.Copy(intPtr, data, 0, data.GetLength(0));
				{
					for (int y = 0; y < TomatoTool.Tile4Bit.TILE_LENGTH_1; ++y)
					{
						for (int x = 0; x < TomatoTool.Tile4Bit.TILE_LENGTH_0; ++x)
						{
							data[(y * TomatoTool.Tile4Bit.TILE_LENGTH_0) + x] = (byte)(((tile[x, y] & 0xF0) >> 4) + ((tile[x, y] & 0x0F) << 4));
						}
					}
				}
				Marshal.Copy(data, 0, intPtr, data.GetLength(0));
				this.bitmap.UnlockBits(bitmapData);
			}

			Bitmap bitmap = (Bitmap)this.bitmap.Clone();

			ColorPalette colorPalette = bitmap.Palette;
			for (int i = 0; i < colorPalette.Entries.Length; ++i)
			{
				colorPalette.Entries[i] = palette[i].toColor();
			}
			bitmap.Palette = colorPalette;

			return bitmap;
		}
		public override Bitmap toBitmap(Palette palette, bool flipX, bool flipY)
		{
			Bitmap bitmap = toBitmap(palette);

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
