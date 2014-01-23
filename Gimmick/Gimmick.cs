
namespace TomatoTool
{
	public class Gimmick : ROMObject
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

		public const uint SIZE = 88;
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

		public const uint NAME_LENGTH_0 = 8;
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
		public const uint NAME_ADDRESS = 0;

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
		public const uint ATTACK_ADDRESS = 34;

		private byte batteryMax;
		public byte BatteryMax
		{
			get
			{
				return batteryMax;
			}

			set
			{
				batteryMax = value;
			}
		}
		public const uint BATTERY_MAX_ADDRESS = 35;

		private byte uses;
		public byte Uses
		{
			get
			{
				return uses;
			}

			set
			{
				uses = value;
			}
		}
		public const uint USES_ADDRESS = 37;

		public const uint LEVEL_LENGTH_0 = 9;
		private byte[] level;
		public byte[] Level
		{
			get
			{
				return level;
			}
		}
		public const uint LEVEL_ADDRESS = 40;

		public const uint DESCRIPTION_BATTLE_LENGTH_0 = 26;
		private StatusString descriptionBattle;
		public StatusString DescriptionBattle
		{
			get
			{
				return descriptionBattle;
			}

			set
			{
				descriptionBattle = value;
			}
		}
		public const uint DESCRIPTION_BATTLE_ADDRESS = 8;

		private StatusString descriptionStatus;
		public StatusString DescriptionStatus
		{
			get
			{
				return descriptionStatus;
			}

			set
			{
				descriptionStatus = value;
			}
		}

		private Palette palette;
		public Palette Palette
		{
			get
			{
				return palette;
			}

			set
			{
				palette = value;
			}
		}

		private LZ77 icon;
		public LZ77 Icon
		{
			get
			{
				return icon;
			}

			set
			{
				icon = value;
			}
		}


		public Gimmick(TomatoAdventure tomatoAdventure, uint address, uint iconAddress, uint paletteAddress)
		{
			load(tomatoAdventure, address, iconAddress, paletteAddress);
		}

		public void initialize()
		{
			name = new StatusString("");
			descriptionBattle = new StatusString("");
			level = new byte[LEVEL_LENGTH_0];
			attack = 0;
			batteryMax = 0;
			uses = 0;
		}
		public void load(TomatoAdventure tomatoAdventure, uint address, uint iconAddress, uint paletteAddress)
		{
			this.address = address;

			name = tomatoAdventure.add(new StatusString(tomatoAdventure, address + NAME_ADDRESS, NAME_LENGTH_0));
			descriptionBattle = tomatoAdventure.add(new StatusString(tomatoAdventure, address + DESCRIPTION_BATTLE_ADDRESS, DESCRIPTION_BATTLE_LENGTH_0));

			level = new byte[LEVEL_LENGTH_0];
			for (uint i = 0; i < level.GetLength(0); ++i)
			{
				level[i] = tomatoAdventure.read(address + LEVEL_ADDRESS + (i * 2));
			}

			attack = tomatoAdventure.read(address + ATTACK_ADDRESS);
			batteryMax = tomatoAdventure.read(address + BATTERY_MAX_ADDRESS);
			uses = tomatoAdventure.read(address + USES_ADDRESS);

			icon = tomatoAdventure.add(new LZ77(tomatoAdventure, iconAddress));
			palette = tomatoAdventure.add(new Palette(tomatoAdventure, paletteAddress));
		}
		public void save(TomatoAdventure tomatoAdventure, uint address, uint iconAddress, uint paletteAddress)
		{
			this.address = address;
			icon.save(tomatoAdventure, iconAddress);
			palette.save(tomatoAdventure, paletteAddress);
			name.save(tomatoAdventure, address, NAME_LENGTH_0);
			descriptionBattle.save(tomatoAdventure, address + DESCRIPTION_BATTLE_ADDRESS, DESCRIPTION_BATTLE_LENGTH_0);
			for (uint i = 0; i < level.GetLength(0); ++i)
			{
				tomatoAdventure.write(address + LEVEL_ADDRESS + (i * 2), level[i]);
			}
			tomatoAdventure.write(address + ATTACK_ADDRESS, attack);
			tomatoAdventure.write(address + BATTERY_MAX_ADDRESS, batteryMax);
			tomatoAdventure.write(address + USES_ADDRESS, uses);
		}

		public static bool compatible(TomatoAdventure tomatoAdventure, uint address)
		{
			return
				tomatoAdventure.isAddress(tomatoAdventure.readAsAddress(address + 60)) &&
				tomatoAdventure.isAddress(tomatoAdventure.readAsAddress(address + 64)) &&
				tomatoAdventure.isAddress(tomatoAdventure.readAsAddress(address + 68)) &&
				tomatoAdventure.isAddress(tomatoAdventure.readAsAddress(address + 72)) &&
				tomatoAdventure.isAddress(tomatoAdventure.readAsAddress(address + 76)) &&
				tomatoAdventure.isAddress(tomatoAdventure.readAsAddress(address + 80)) &&
				tomatoAdventure.isAddress(tomatoAdventure.readAsAddress(address + 84));
		}
	}
}
