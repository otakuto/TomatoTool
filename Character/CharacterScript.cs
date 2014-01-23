
using System;

namespace TomatoTool
{
	public class CharacterScript : ROMObject
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

		public const uint SIZE = 16;
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

		private byte x;
		public byte X
		{
			get
			{
				return x;
			}

			set
			{
				x = value;
				saved = false;
			}
		}

		private byte y;
		public byte Y
		{
			get
			{
				return y;
			}

			set
			{
				y = value;
				saved = false;
			}
		}

		private byte typeID;
		public byte TypeID
		{
			get
			{
				return typeID;
			}

			set
			{
				typeID = value;
				saved = false;
			}
		}

		private sbyte addX;
		public sbyte AddX
		{
			get
			{
				return addX;
			}

			set
			{
				addX = value;
				saved = false;
			}
		}

		private sbyte addY;
		public sbyte AddY
		{
			get
			{
				return addY;
			}

			set
			{
				addY = value;
				saved = false;
			}
		}

		private byte characterImageNumber;
		public byte CharacterImageNumber
		{
			get
			{
				return characterImageNumber;
			}

			set
			{
				if (value <= CHARACTER_IMAGE_NUMBER_MAX)
				{
					characterImageNumber = value;
					saved = false;
				}
				else
				{
					throw new Exception();
				}
			}
		}

		private byte paletteNumber;
		public byte PaletteNumber
		{
			get
			{
				return paletteNumber;
			}

			set
			{
				if (value <= PALETTE_NUMBER_MAX)
				{
					paletteNumber = value;
					saved = false;
				}
				else
				{
					throw new Exception();
				}
			}
		}

		private CharacterScriptDirection direction;
		public CharacterScriptDirection Direction
		{
			get
			{
				return direction;
			}

			set
			{
				direction = value;
				saved = false;
			}
		}

		private Script actionScript;
		public Script ActionScript
		{
			get
			{
				return actionScript;
			}

			set
			{
				actionScript = value;
				saved = false;
			}
		}

		private Script script;
		public Script Script
		{
			get
			{
				return script;
			}

			set
			{
				script = value;
				saved = false;
			}
		}
		
		private ushort id;
		public ushort ID
		{
			get
			{
				return id;
			}

			set
			{
				id = value;
				saved = false;
			}
		}

		public const byte CHARACTER_IMAGE_NUMBER_MAX = 0x0F;
		public const byte PALETTE_NUMBER_MAX = 0x0F;

		public CharacterScript()
		{
			initialize();
		}
		public CharacterScript(TomatoAdventure tomatoAdventure, uint address)
		{
			load(tomatoAdventure, address);
		}

		public void initialize()
		{
			x = 0;
			y = 0;

			typeID = 0;
			addX = 0;
			addY = 0;

			characterImageNumber = 0;
			paletteNumber = 0;

			direction = CharacterScriptDirection.None;

			script = new Script();
			actionScript = new Script();

			id = 0;
		}
		public void load(TomatoAdventure tomatoAdventure, uint address)
		{
			typeID = tomatoAdventure.read(address);

			addX = (sbyte)(tomatoAdventure.readTopBit(address + 5) - (((tomatoAdventure.read(address + 1) & 0x40) != 0) ? 16 : 0));
			addY = (sbyte)(tomatoAdventure.readBottomBit(address + 5) - (((tomatoAdventure.read(address + 1) & 0x20) != 0) ? 17 : 0));

			characterImageNumber = tomatoAdventure.readBottomBit(address + 1);

			paletteNumber = tomatoAdventure.readTopBit(address + 2);

			switch (tomatoAdventure.readBottomBit(address + 2))
			{
				case 0x01:
					direction = CharacterScriptDirection.Right;
					break;

				case 0x02:
					direction = CharacterScriptDirection.Left;
					break;

				case 0x03:
					direction = CharacterScriptDirection.SP;
					break;

				case 0x04:
					direction = CharacterScriptDirection.Up;
					break;

				case 0x08:
					direction = CharacterScriptDirection.Down;
					break;

				default:
					direction = CharacterScriptDirection.None;
					break;
			}

			x = tomatoAdventure.read(address + 3);
			y = tomatoAdventure.read(address + 4);

			actionScript = new Script(tomatoAdventure, tomatoAdventure.readAsAddress(address + 6));

			script = new Script(tomatoAdventure, tomatoAdventure.readAsAddress(address + 10));

			id = (ushort)tomatoAdventure.readLittleEndian(address + 14, 2);
		}
		public void save(TomatoAdventure tomatoAdventure, uint address)
		{
			tomatoAdventure.write(address, typeID);

			tomatoAdventure.write(address + 1, (byte)((((addX >= 0) ? 0 : (1 << 6)) + ((addY >= 0) ? 0 : (1 << 5)) + characterImageNumber)));

			tomatoAdventure.writeTopBit(address + 2, paletteNumber);

			tomatoAdventure.writeBottomBit(address + 2, (byte)direction);

			tomatoAdventure.write(address + 3, x);
			tomatoAdventure.write(address + 4, y);

			tomatoAdventure.write(address + 5, (byte)((((addX >= 0) ? addX : (addX + 16)) << 4) + ((addY >= 0) ? addY : (addY + 17))));

			//tomatoAdventure.writeTopBit(address + 5, (byte)((addX >= 0) ? addX : (addX + 12)));
			//tomatoAdventure.writeBottomBit(address + 5, (byte)((addY >= 0) ? addY : (addY + 17)));

			tomatoAdventure.writeAsAddress(address + 6, actionScript.ObjectID);

			tomatoAdventure.writeAsAddress(address + 10, script.ObjectID);

			tomatoAdventure.writeLittleEndian(address + 14, 2, id);

			saved = true;
		}
	}
}
