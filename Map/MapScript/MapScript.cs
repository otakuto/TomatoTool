using System;

namespace TomatoTool
{
	public class MapScript : ROMObject
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
				return SIZE;
			}
		}

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

		private bool hasTrigger;
		public bool HasTrigger
		{
			get
			{
				return hasTrigger;
			}

			set
			{
				hasTrigger = value;
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

		public const uint SIZE = 4 + TomatoTool.ROM.POINTER_SIZE;
		public const uint ALIGNMENT = 4;

		public MapScript()
		{
			initialize();
		}
		public MapScript(TomatoAdventure tomatoAdventure, uint address)
		{
			load(tomatoAdventure, address);
		}

		public void initialize()
		{
			mapRangeObject = new MapRangeObject();
			hasTrigger = false;
			script = Script.NULL;
		}
		public void load(TomatoAdventure tomatoAdventure, uint address)
		{
			mapRangeObject = new MapRangeObject(tomatoAdventure.read(address), tomatoAdventure.read(address + 1), (byte)(tomatoAdventure.read(address + 2) & (~0x80)), tomatoAdventure.read(address + 3));
			hasTrigger = (tomatoAdventure.read(address + 2) & 0x80) != 0;
			script = (Script)tomatoAdventure.add(new Script(tomatoAdventure, tomatoAdventure.readAsAddress(address + 4)));
		}
		public void save(TomatoAdventure tomatoAdventure, uint address)
		{
			tomatoAdventure.write(address, mapRangeObject.BeginX);
			tomatoAdventure.write(address + 1, mapRangeObject.BeginY);
			tomatoAdventure.write(address + 2, (byte)(mapRangeObject.EndX + (Convert.ToByte(hasTrigger) << 7)));
			tomatoAdventure.write(address + 3, mapRangeObject.EndY);

			script.save(tomatoAdventure);
			tomatoAdventure.writeAsAddress(address, script.ObjectID);

			saved = true;
		}
		
		public void move(byte startX, byte startY)
		{
			mapRangeObject.move(startX, startY);
		}
	}
}
