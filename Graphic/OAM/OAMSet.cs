using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;

namespace TomatoTool
{
	public class OAMSet : ROMObject
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
				return HEADER_SIZE + (uint)(oam.Count * TomatoTool.OAM.SIZE);
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

		public OAM this[int i]
		{
			get
			{
				return oam[i];
			}

			set
			{
				oam[i] = value;
			}
		}
		public int Count
		{
			get
			{
				return oam.Count;
			}
		}

		private List<OAM> oam;
		public List<OAM> OAM
		{
			get
			{
				return oam;
			}
		}

		//座標指定できる範囲0x0000-0x01FF合計0x0200
		public const uint WIDTH = 0x0200;
		//座標指定できる範囲0x00-0xFF合計0x0100
		public const uint HEIGHT = 0x0100;

		public const uint HEADER_SIZE = 2;

		//座標指定できる範囲0x0000-0x01FF合計0x0200の中心
		public const uint CENTER_X = 0x0100;
		//座標指定できる範囲0x00-0xFF合計0x0100の中心
		public const uint CENTER_Y = 0x80;

		public OAMSet(TomatoAdventure tomatoAdventure, uint address)
			
		{
			load(tomatoAdventure, address);
		}

		public void load(TomatoAdventure tomatoAdventure, uint address)
		{
			this.address = address;

			oam = new List<OAM>();
			for (uint i = 0; i < tomatoAdventure.readLittleEndian(tomatoAdventure.readAsAddress(address), HEADER_SIZE); ++i)
			{
				oam.Add(new OAM(tomatoAdventure, tomatoAdventure.readAsAddress(address) + HEADER_SIZE + (i * TomatoTool.OAM.SIZE)));
			}

		}

		public void save(TomatoAdventure tomatoAdventure, uint address)
		{
			this.address = address;

			tomatoAdventure.writeLittleEndian(address, HEADER_SIZE, (uint)oam.Count);

			for (uint i = 0; i < oam.Count; ++i)
			{
				oam[(int)i].save(tomatoAdventure, address + HEADER_SIZE + (i * TomatoTool.OAM.SIZE));
			}
		}

		public void draw(Graphics graphics, Tile4BitList tileList, Palette palette)
		{
			for (int i = 0; i < oam.Count; ++i)
			{
				//oam[i].draw(graphics, (int)(oam[i].X + CENTER_X), (int)(oam[i].Y + CENTER_Y), tileList, palette);
			}
		}

		public Bitmap toBitmap(Tile4BitList tileList, Palette palette, bool isTransparent)
		{
			Bitmap bitmap = new Bitmap((int)WIDTH, (int)HEIGHT);

			using (Graphics graphics = Graphics.FromImage(bitmap))
			{
				graphics.TranslateTransform(CENTER_X, CENTER_Y);
				
				if (isTransparent)
				{
					using (ImageAttributes imageAttributes = new ImageAttributes())
					{
						Color color = palette[0].toColor();
						imageAttributes.SetColorKey(color, color);
							
						for (int i = 0; i < oam.Count; ++i)
						{
							using (Bitmap oamBitmap = oam[i].toBitmap(tileList, palette))
							{
								graphics.DrawImage(oamBitmap, new Rectangle(oam[i].X, oam[i].Y, (oam[i].Width * Tile.WIDTH), (oam[i].Height * Tile.HEIGHT)), 0, 0, (oam[i].Width * Tile.WIDTH), (oam[i].Height * Tile.HEIGHT), GraphicsUnit.Pixel, imageAttributes);
							}
						}
					}
				}
				else
				{
					for (int i = 0; i < oam.Count; ++i)
					{
						using (Bitmap oamBitmap = oam[i].toBitmap(tileList, palette))
						{
							graphics.DrawImage(oamBitmap, oam[i].X, oam[i].Y, (oam[i].Width * Tile.WIDTH), (oam[i].Height * Tile.HEIGHT));
						}
					}
				}
			}

			return bitmap;
		}
	}
}
