using System.Drawing;

namespace TomatoTool
{
	public class WindowFont : Font
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

		public const uint SIZE = 32;
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

		public WindowFont(TomatoAdventure tomatoAdventure, uint address)
		{
			load(tomatoAdventure, address);
		}

		public void load(TomatoAdventure tomatoAdventure, uint address)
		{
			paleBlueLayer = new byte[16];
			for (uint i = 0; i < paleBlueLayer.GetLength(0); ++i)
			{
				paleBlueLayer[i] = tomatoAdventure.read(address + (i * 2));
			}

			whiteLayer = new byte[16];
			for (uint i = 0; i < whiteLayer.GetLength(0); ++i)
			{
				whiteLayer[i] = tomatoAdventure.read(address + (i * 2) + 1);
			}
		}

		public void save(TomatoAdventure tomatoAdventure, uint address)
		{
			for (uint i = 0; i < paleBlueLayer.GetLength(0); ++i)
			{
				tomatoAdventure.write(address + (i * 2), paleBlueLayer[i]);
			}

			for (uint i = 0; i < whiteLayer.GetLength(0); ++i)
			{
				tomatoAdventure.write(address + (i * 2) + 1, whiteLayer[i]);
			}
		}

		public Bitmap toBitmap()
		{
			Bitmap bitmap = new Bitmap(8, 16);

			Color16Bit[] color = new Color16Bit[16];

			for (uint i = 0; i < color.GetLength(0); ++i)
			{
				color[i] = new Color16Bit(0, 0, 0);
			}

			color[BLUE] = new Color16Bit(3, 5, 14);
			color[WHITE] = new Color16Bit(31, 31, 31);
			color[PALEBLUE] = new Color16Bit(24, 25, 29);

			for (int y = 0; y < bitmap.Height; ++y)
			{
				for (int x = 0; x < bitmap.Width; ++x)
				{
					bitmap.SetPixel(x, y, color[BLUE].toColor());
				}
			}

			for (int y = 0; y < paleBlueLayer.GetLength(0); ++y)
			{
				for (int x = 0; x < 8; ++x)
				{
					if (((paleBlueLayer[y] & (0x80 >> x)) >> (7 - x)) == 0x01)
					{
						bitmap.SetPixel(x, y, color[PALEBLUE].toColor());
					}
				}
			}

			for (int y = 0; y < whiteLayer.GetLength(0); ++y)
			{
				for (int x = 0; x < 8; ++x)
				{
					if (((whiteLayer[y] & (0x80 >> x)) >> (7 - x)) == 0x01)
					{
						bitmap.SetPixel(x, y, color[WHITE].toColor());
					}
				}
			}

			return bitmap;
		}
	}
}
