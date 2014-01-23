namespace TomatoTool
{
	public class Item : ROMObject 
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

		public const uint ALIGNMENT = 4;
		public override uint Alignment
		{
			get
			{
				return ALIGNMENT;
			}
		}

		//8文字まで
		private StatusString name;
		public StatusString Name
		{
			get
			{
				return name;
			}

			set
			{
				name = value;
			}
		}

		private ushort price;
		public ushort Price
		{
			get
			{
				return price;
			}

			set
			{
				price = value;
			}
		}

		public byte unknownValue0;
		public byte unknownValue1;

		public uint unknownAddress;

		public const uint NAME_LENGTH_0 = 8;
			
		public Item(TomatoAdventure tomatoAdventure, uint address)	
        {
            load(tomatoAdventure, address);
        }

		public void load(TomatoAdventure tomatoAdventure, uint address)
		{
			this.address = address;
		}
	}
}
