
namespace TomatoTool
{
	public class StatusStringSet : ROMObject
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

		public const uint SIZE = 8;
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

		private StatusString statusString;
		public StatusString StatusString
		{
			get
			{
				return statusString;
			}

			set
			{
				statusString = value;
			}
		}

		private byte row;
		public byte Row
		{
			get
			{
				return row;
			}

			set
			{
				row = value;
			}
		}

		private byte column;
		public byte Column
		{
			get
			{
				return column;
			}

			set
			{
				column = value;
			}
		}

		public ushort Length
		{
			get
			{
				return (ushort)(row * column);
			}
		}

		public byte unknownValue0;
		public byte unknownValue1;
		
		public StatusStringSet(TomatoAdventure tomatoAdventure, uint address)
		{
			load(tomatoAdventure, address);
		}

		public void load(TomatoAdventure tomatoAdventure, uint address)
		{
			column = tomatoAdventure.read(address + 4);

			row = tomatoAdventure.read(address + 5);

			unknownValue0 = tomatoAdventure.read(address + 6);

			unknownValue1 = tomatoAdventure.read(address + 7);

			statusString = new StatusString(tomatoAdventure, tomatoAdventure.readAsAddress(address), Length);
		}

		public void save(TomatoAdventure tomatoAdventure, uint address)
		{
			tomatoAdventure.writeAsAddress(address, statusString.ObjectID);
			statusString.save(tomatoAdventure, address, Length);

			tomatoAdventure.write(address + 4, column);

			tomatoAdventure.write(address + 5, row);

			tomatoAdventure.write(address + 6, unknownValue0);

			tomatoAdventure.write(address + 7, unknownValue1);
		}
	}
}
