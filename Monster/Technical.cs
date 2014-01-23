
namespace TomatoTool
{
	public class Technical : ROMObject
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

		public const uint NAME_LENGTH = 8;
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

		//((attack * 2) - 防御力)で食らうダメージ
		//0でも無敵化していなければ1食らう
		private byte attack;
		public byte Attack
		{
			get
			{
				return attack;
			}

			set
			{
				attack = value;
			}
		}

		public byte unknownValue0;
		public byte unknownValue1;
		public byte unknownValue2;

		public uint unknownAddress;
		
		public Technical(TomatoAdventure tomatoAdventure, uint address) 
		{
			load(tomatoAdventure, address);
		}

		public void load(TomatoAdventure tomatoAdventure, uint address)
		{
			this.address = address;

			name = new StatusString(tomatoAdventure, address, NAME_LENGTH);

			attack = tomatoAdventure.read(address + 8);
			
			unknownValue0 = tomatoAdventure.read(address + 9);
			unknownValue1 = tomatoAdventure.read(address + 10);
			unknownValue2 = tomatoAdventure.read(address + 11);

			unknownAddress = tomatoAdventure.readAsAddress(address + 12);
		}

		public void save(TomatoAdventure tomatoAdventure, uint address)
		{

		}
	}
}
