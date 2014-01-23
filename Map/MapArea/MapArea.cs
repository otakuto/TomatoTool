using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Drawing.Imaging;

namespace TomatoTool
{
	public class MapArea : ROMObject
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

		public override uint Size
		{
			get
			{
				return (uint)area.Length;
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

		public byte this[int i, int j]
		{
			get
			{
				return area[i, j];
			}

			set
			{
				area[i, j] = value;
			}
		}
		public int GetLength(int dimension)
		{
			return area.GetLength(dimension);
		}

		public static List<uint> TopAddressPointer = new List<uint>()
		{
			TomatoTool.ROM.addBase(0x00033EAC)
		};

		public static uint SelectTopAddressArrayDoublePointer = 0;

		private byte[,] area;
		public byte[,] Area
		{
			get
			{
				return area;
			}
		}

		public static ReadOnlyCollection<Bitmap> image;

		static MapArea()
		{
		}
		public MapArea(TomatoAdventure tomatoAdventure, Map map)
		{
			load(tomatoAdventure, map);
		}

		public void load(TomatoAdventure tomatoAdventure, Map map)
		{
			address = tomatoAdventure.readAsAddress(tomatoAdventure.readAsAddress(TopAddressPointer[(int)SelectTopAddressArrayDoublePointer]) + (map.Number * TomatoTool.ROM.POINTER_SIZE));

			area = new byte[map.Width, map.Height];

			for (uint y = 0; y < area.GetLength(1); ++y)
			{
				for (uint x = 0; x < area.GetLength(0); ++x)
				{
					area[x, y] = tomatoAdventure.read((uint)(address + (y * area.GetLength(0)) + x));
				}
			}
		}

		public void save(TomatoAdventure tomatoAdventure, Map map)
		{
			this.address = tomatoAdventure.malloc(Size, Alignment);
			tomatoAdventure.writeAsAddress(tomatoAdventure.readAsAddress(TopAddressPointer[(int)SelectTopAddressArrayDoublePointer]) + (map.Number * TomatoTool.ROM.POINTER_SIZE), address);

			for (uint y = 0; y < area.GetLength(1); ++y)
			{
				for (uint x = 0; x < area.GetLength(0); ++x)
				{
					tomatoAdventure.write((uint)(address + (y * area.GetLength(0)) + x), area[x, y]);
				}
			}
		}

		public void resize(byte width, byte height)
		{
			byte[,] area = new byte[width, height];

			for (int y = 0; y < this.area.GetLength(1); ++y)
			{
				for (int x = 0; x < this.area.GetLength(0); ++x)
				{
					area[x, y] = this.area[x, y];
				}
			}

			this.area = area;
		}

		/*public void draw(Graphics graphics)
		{
			draw(graphics, false);
		}*/
		public void draw(Graphics graphics, bool transparent)
		{
			if (area != null)
			{
				ColorMatrix cm = new System.Drawing.Imaging.ColorMatrix();
				cm.Matrix00 = 1;
				cm.Matrix11 = 1;
				cm.Matrix22 = 1;
				cm.Matrix33 = transparent ? (float)0.5 : (float)1;
				cm.Matrix44 = 1;

				ImageAttributes Attributes = new ImageAttributes();
				Attributes.SetColorMatrix(cm);

				for (int y = 0; y < area.GetLength(1); ++y)
				{
					for (int x = 0; x < area.GetLength(0); ++x)
					{
						graphics.DrawImage(image[area[x, y]], new Rectangle(x * (int)TomatoTool.Map.BLOCK_WIDTH, y * (int)TomatoTool.Map.BLOCK_HEIGHT, (int)TomatoTool.Map.BLOCK_WIDTH, (int)TomatoTool.Map.BLOCK_HEIGHT), 0, 0, (int)TomatoTool.Map.BLOCK_WIDTH, (int)TomatoTool.Map.BLOCK_HEIGHT, GraphicsUnit.Pixel, Attributes);
					}
				}
			}
		}
	}
}
