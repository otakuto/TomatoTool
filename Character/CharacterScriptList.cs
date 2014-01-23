using System.Collections;
using System.Collections.Generic;
using System.Drawing;


namespace TomatoTool
{
	public class CharacterScriptList : ROMObject
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
				return (CHARACTER_IMAGE_LENGTH_0 * ROM.POINTER_SIZE) + (PALETTE_LENGTH_0 * ROM.POINTER_SIZE) + (uint)(characterScript.Count * TomatoTool.CharacterScript.SIZE) + FOOTER_SIZE;
			}
		}

		public override uint Alignment
		{
			get
			{
				return ALIGNMENT;
			}
		}

		public const uint PALETTE_LENGTH_0 = 12;
		private Palette[] palette;
		public Palette[] Palette
		{
			get
			{
				return palette;
			}
		}

		private List<CharacterScript> characterScript;
		public List<CharacterScript> CharacterScript
		{
			get
			{
				return characterScript;
			}
		}

		public const uint CHARACTER_IMAGE_LENGTH_0 = 16;
		private CharacterImage[] characterImage;
		public CharacterImage[] CharacterImage
		{
			get
			{
				return characterImage;
			}
		}
		
		public const uint FOOTER_SIZE = 4;

		public static Color Color = Color.FromArgb(0, 0xFF, 0);

		public const uint ALIGNMENT = 4;

		public CharacterScriptList(TomatoAdventure tomatoAdventure, uint address)
		{
			load(tomatoAdventure, address);
		}

		public void load(TomatoAdventure tomatoAdventure)
		{
			load(tomatoAdventure, address);
		}
		public void load(TomatoAdventure tomatoAdventure, uint address)
		{
			this.address = address;

			characterImage = new CharacterImage[CHARACTER_IMAGE_LENGTH_0];
			for (uint i = 0; i < characterImage.GetLength(0); ++i)
			{
				if (tomatoAdventure.isAddress(tomatoAdventure.readAsAddress(address + (i * ROM.POINTER_SIZE))))
				{
					characterImage[i] = (CharacterImage)tomatoAdventure.add(new CharacterImage(tomatoAdventure, tomatoAdventure.readAsAddress(address + (i * ROM.POINTER_SIZE))));
				}
				else
				{
					characterImage[i] = (CharacterImage)tomatoAdventure.add(TomatoTool.CharacterImage.NULL);
				}
			}

			palette = new Palette[PALETTE_LENGTH_0];
			for (uint i = 0; i < palette.GetLength(0); ++i)
			{
				if (tomatoAdventure.isAddress(tomatoAdventure.readAsAddress(address + (CHARACTER_IMAGE_LENGTH_0 * ROM.POINTER_SIZE) + (i * ROM.POINTER_SIZE))))
				{
					palette[i] = tomatoAdventure.add(new Palette(tomatoAdventure, tomatoAdventure.readAsAddress(address + (CHARACTER_IMAGE_LENGTH_0 * ROM.POINTER_SIZE) + (i * ROM.POINTER_SIZE))));
				}
			}

			characterScript = new List<CharacterScript>();
			for (uint i = 0; tomatoAdventure.read(address + (CHARACTER_IMAGE_LENGTH_0 * ROM.POINTER_SIZE) + (PALETTE_LENGTH_0 * ROM.POINTER_SIZE) + (i * TomatoTool.CharacterScript.SIZE)) != 0xFF; ++i)
			{
				characterScript.Add(new CharacterScript(tomatoAdventure, address + (CHARACTER_IMAGE_LENGTH_0 * ROM.POINTER_SIZE) + (PALETTE_LENGTH_0 * ROM.POINTER_SIZE) + (i * TomatoTool.CharacterScript.SIZE)));
			}

		}

		public void save(TomatoAdventure tomatoAdventure)
		{
			if (!saved)
			{
				save(tomatoAdventure, tomatoAdventure.malloc(Size, Alignment));
				saved = true;
			}
		}
		public void save(TomatoAdventure tomatoAdventure, uint address)
		{
			this.address = address;

			for (uint i = 0; i < characterImage.GetLength(0); ++i)
			{
				if ((characterImage[i] != null) && characterImage[i] != TomatoTool.CharacterImage.NULL)
				{
					characterImage[i].save(tomatoAdventure);
					tomatoAdventure.writeAsAddress(address + (i * ROM.POINTER_SIZE), characterImage[i].ObjectID);
				}
				else
				{
					tomatoAdventure.writeAsAddress(address + (i * ROM.POINTER_SIZE), ROM.ADDRESS_NULL);
				}
			}

			for (uint i = 0; i < palette.GetLength(0); ++i)
			{
				if (palette[i] != null)
				{
					palette[i].save(tomatoAdventure, palette[i].ObjectID);
					tomatoAdventure.writeAsAddress(address + (CHARACTER_IMAGE_LENGTH_0 * ROM.POINTER_SIZE) + (i * ROM.POINTER_SIZE), palette[i].ObjectID);
				}
				else
				{
					tomatoAdventure.writeAsAddress(address + (CHARACTER_IMAGE_LENGTH_0 * ROM.POINTER_SIZE) + (i * ROM.POINTER_SIZE), palette[0].ObjectID);
				}
			}

			for (uint i = 0; i < characterScript.Count; ++i)
			{
				if (characterScript[(int)i] != null)
				{
					characterScript[(int)i].save(tomatoAdventure, address + (CHARACTER_IMAGE_LENGTH_0 * ROM.POINTER_SIZE) + (PALETTE_LENGTH_0 * ROM.POINTER_SIZE) + (i * TomatoTool.CharacterScript.SIZE));
				}
			}

			//フッター
			tomatoAdventure.write((uint)(address + (CHARACTER_IMAGE_LENGTH_0 * ROM.POINTER_SIZE) + (PALETTE_LENGTH_0 * ROM.POINTER_SIZE) + (characterScript.Count * TomatoTool.CharacterScript.SIZE) + 0), 0xFF);
			tomatoAdventure.write((uint)(address + (CHARACTER_IMAGE_LENGTH_0 * ROM.POINTER_SIZE) + (PALETTE_LENGTH_0 * ROM.POINTER_SIZE) + (characterScript.Count * TomatoTool.CharacterScript.SIZE) + 1), 0x00);
			tomatoAdventure.write((uint)(address + (CHARACTER_IMAGE_LENGTH_0 * ROM.POINTER_SIZE) + (PALETTE_LENGTH_0 * ROM.POINTER_SIZE) + (characterScript.Count * TomatoTool.CharacterScript.SIZE) + 2), 0x00);
			tomatoAdventure.write((uint)(address + (CHARACTER_IMAGE_LENGTH_0 * ROM.POINTER_SIZE) + (PALETTE_LENGTH_0 * ROM.POINTER_SIZE) + (characterScript.Count * TomatoTool.CharacterScript.SIZE) + 3), 0x00);
		}

		public void draw(Graphics graphics)
		{
			draw(graphics, -1);
		}
		public void draw(Graphics graphics, int select)
		{
			if (characterScript != null)
			{
				graphics.TranslateTransform(Map.BLOCK_WIDTH / 2, Map.BLOCK_HEIGHT - 1);
				for (int i = 0; i < characterScript.Count; ++i)
				{
					try
					{
						using (Bitmap bitmap = toBitmap(characterScript[i], true))
						{
							graphics.DrawImage(bitmap, ((characterScript[i].X * (int)Map.BLOCK_WIDTH)) + characterScript[i].AddX - OAMSet.CENTER_X, (characterScript[i].Y * (int)Map.BLOCK_HEIGHT) + characterScript[i].AddY - OAMSet.CENTER_Y);
						}
					}
					catch
					{
					}
				}
				graphics.ResetTransform();

				//枠描写
				for (int i = 0; i < characterScript.Count; ++i)
				{
					using (Pen pen = new Pen((select == i) ? Map.SelectColor : Color, 2))
					{
						graphics.DrawRectangle(pen, (characterScript[i].X * Map.BLOCK_WIDTH) + 1, (characterScript[i].Y * Map.BLOCK_HEIGHT) + 1, Map.BLOCK_WIDTH - pen.Width, Map.BLOCK_HEIGHT - pen.Width);
					}
				}

			}
		}

		public Bitmap toBitmap(CharacterScript characterScript, bool isTransparent)
		{
			return characterImage[characterScript.CharacterImageNumber].OAMSetList.OAMSet[(ushort)(((IList)characterImage[characterScript.CharacterImageNumber].ActionList[characterScript.Direction])[0])].toBitmap(characterImage[characterScript.CharacterImageNumber].TileList, palette[characterScript.PaletteNumber], isTransparent);
			//return characterImage[characterScript.CharacterImageNumber].toBitmap(characterScript.Direction, 0, palette[characterScript.PaletteNumber], isTransparent);

		}
	}
}
