using System;
using System.Drawing;

namespace TomatoTool
{
	public class Color16Bit : ICloneable
	{
		private byte red;
		public byte Red
		{
			get
			{
				return red;
			}

			set
			{
				if (value <= MAX_COLOR_VALUE)
				{
					red = value;
				}
				else
				{
					throw new Exception();
				}
			}
		}

		private byte green;
		public byte Green
		{
			get
			{
				return green;
			}

			set
			{
				if (value <= MAX_COLOR_VALUE)
				{
					green = value;
				}
				else
				{
					throw new Exception();
				}
			}
		}

		private byte blue;
		public byte Blue
		{
			get
			{
				return blue;
			}

			set
			{
				if (value <= MAX_COLOR_VALUE)
				{
					blue = value;
				}
				else
				{
					throw new Exception();
				}
			}
		}

		public const uint MAX_COLOR_VALUE = 31;

		public const uint SIZE = 2;


		public Color16Bit(ushort rgb)
		{
			set(rgb);
		}
		public Color16Bit(byte red, byte green, byte blue)
		{
			set(red, green, blue);
		}
		public Color16Bit(Color color)
		{
			set(color);
		}
		public Color16Bit(TomatoAdventure tomatoAdventure, uint address)
		{
			load(tomatoAdventure, address);
		}

		public void load(TomatoAdventure tomatoAdventure, uint address)
		{
			set((ushort)tomatoAdventure.readLittleEndian(address, SIZE));
		}

		public void save(TomatoAdventure tomatoAdventure, uint address)
		{
			tomatoAdventure.writeLittleEndian(address, SIZE, get());
		}
		
		public void set(ushort rgb)
		{
			red = (byte)(rgb & MAX_COLOR_VALUE);

			green = (byte)((rgb >> 5) & MAX_COLOR_VALUE);

			blue = (byte)((rgb >> 10) & MAX_COLOR_VALUE);
		}
		public void set(byte red, byte green, byte blue)
		{
			Red = red;
			Green = green;
			Blue = blue;
		}
		public void set(Color color)
		{
			Red = (byte)(color.R >> 3);
			Green = (byte)(color.G >> 3);
			Blue = (byte)(color.B >> 3);
		}

		public ushort get()
		{
			return (ushort)((blue << 10) + (green << 5) + red);
		}

		public Color toColor()
		{
			return Color.FromArgb(red << 3, green << 3, blue << 3);
		}

		object ICloneable.Clone()
		{
			return Clone();
		}
		public Color16Bit Clone()
		{
			return new Color16Bit(red, green, blue);
		}
	}
}
