using System;


namespace TomatoTool
{
	public class WarpScript
		:
		ROMObject
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

		public const uint SIZE = 10;
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

		private MapRangeObject mapRangeObject;

		public byte BeginX
		{
			get
			{
				return mapRangeObject.BeginX;
			}

			set
			{
				mapRangeObject.BeginX = value;
				saved = false;
			}
		}

		public byte BeginY
		{
			get
			{
				return mapRangeObject.BeginY;
			}

			set
			{
				mapRangeObject.BeginY = value;
				saved = false;
			}
		}

		public byte EndX
		{
			get
			{
				return mapRangeObject.EndX;
			}

			set
			{
				mapRangeObject.EndX = value;
				saved = false;
			}
		}

		public byte EndY
		{
			get
			{
				return mapRangeObject.EndY;
			}

			set
			{
				mapRangeObject.EndY = value;
				saved = false;
			}
		}

		private ushort number;
		public ushort Number
		{
			get
			{
				return number;
			}

			set
			{
				number = value;
				saved = false;
			}
		}

		private WarpScriptMotion motion;
		public WarpScriptMotion Motion
		{
			get
			{
				return motion;
			}

			set
			{
				motion = value;
				saved = false;
			}
		}

		private byte moveX;
		public byte MoveX
		{
			get
			{
				return moveX;
			}

			set
			{
				moveX = value;
				saved = false;
			}
		}

		private byte moveY;
		public byte MoveY
		{
			get
			{
				return moveY;
			}

			set
			{
				moveY = value;
				saved = false;
			}
		}

		private byte adjustX;
		public byte AdjustX
		{
			get
			{
				return adjustX;
			}

			set
			{
				if (value <= 16)
				{
					adjustX = value;
					saved = false;
				}
				else
				{
					throw new Exception();
				}
			}
		}

		private byte adjustY;
		public byte AdjustY
		{
			get
			{
				return adjustY;
			}

			set
			{
				if (value <= 16)
				{
					adjustY = value;
					saved = false;
				}
				else
				{
					throw new Exception();
				}
			}
		}

		public WarpScript()
		{
			initialize();
		}
		public WarpScript(TomatoAdventure tomatoAdventure, uint address)
		{
			load(tomatoAdventure, address);
		}

		public void initialize()
		{
			mapRangeObject = new MapRangeObject();

			number = 0;

			motion = 0;

			moveX = 0;
			moveY = 0;

			adjustX = 0;
			adjustY = 0;
		}
		public void load(TomatoAdventure tomatoAdventure, uint address)
		{
			mapRangeObject = new MapRangeObject(tomatoAdventure.read(address), tomatoAdventure.read(address + 1), tomatoAdventure.read(address + 2), tomatoAdventure.read(address + 3));

			number = (ushort)tomatoAdventure.readLittleEndian(address + 4, 2);

			motion = (WarpScriptMotion)tomatoAdventure.read(address + 6);

			moveX = tomatoAdventure.read(address + 7);
			moveY = tomatoAdventure.read(address + 8);

			adjustX = tomatoAdventure.readTopBit(address + 9);
			adjustY = tomatoAdventure.readBottomBit(address + 9);
		}
		public void save(TomatoAdventure tomatoAdventure, uint address)
		{
			tomatoAdventure.write(address, mapRangeObject.BeginX);
			tomatoAdventure.write(address + 1, mapRangeObject.BeginY);
			tomatoAdventure.write(address + 2, mapRangeObject.EndX);
			tomatoAdventure.write(address + 3, mapRangeObject.EndY);

			tomatoAdventure.writeLittleEndian(address + 4, 2, number);

			tomatoAdventure.write(address + 6, (byte)motion);

			tomatoAdventure.write(address + 7, moveX);
			tomatoAdventure.write(address + 8, moveY);

			tomatoAdventure.write(address + 9, (byte)((adjustX << 4) + adjustY));

			saved = true;
		}
		
		public void move(byte startX, byte startY)
		{
			mapRangeObject.move(startX, startY);
			saved = false;
		}
	}
}
