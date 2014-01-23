using System.Collections.Generic;
using System.Drawing;

namespace TomatoTool
{
	public class Map
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
				return
					saved &&
					warpScriptList.Saved &&
					mapScriptList.Saved &&
					characterScriptList.Saved &&
					overrideTile.Saved &&
					mapArea.Saved;
			}

			set
			{
				saved = value;
			}
		}

		public const uint SIZE = 28;
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

		public static List<uint> MapTileTopAddressPointer = new List<uint>()
		{
			ROM.addBase(0x00034D70),
		};
		public static List<uint> PaletteTopAddressPointer = new List<uint>()
		{
			ROM.addBase(0x00031EF0),
			ROM.addBase(0x00033D80),
			ROM.addBase(0x0003F80C),
			ROM.addBase(0x0003F90C),
		};
		public static List<uint> ChipSetListTopAddressPointer = new List<uint>()
		{
			ROM.addBase(0x00033EA4),
		};
		public static List<uint> ChipSetTableAddressPointer = new List<uint>()
		{
			ROM.addBase(0x00033E9C),
		};
		public static List<uint> AnimationTileSetTopAddressPointer = new List<uint>()
		{
			ROM.addBase(0x00034080),
			ROM.addBase(0x00034E1C),
		};
		public static List<uint> TileTopAddressPointer = new List<uint>()
		{
			ROM.addBase(0x00034D70),
		};

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
		public const uint NUMBER_ADDRESS = 0;

		private byte saveType;
		public byte SaveType
		{
			get
			{
				return saveType;
			}

			set
			{
				saveType = value;
				saved = false;
			}
		}
		public const uint SAVE_TYPE_ADDRESS = 2;

		private byte width;
		public byte Width
		{
			get
			{
				return width;
			}

			set
			{
				width = value;
				saved = false;
			}
		}
		public const uint WIDTH_ADDRESS = 3;

		private byte height;
		public byte Height
		{
			get
			{
				return height;
			}

			set
			{
				height = value;
				saved = false;
			}
		}
		public const uint HEIGHT_ADDRESS = 4;

		private byte bgmNumber;
		public byte BGMNumber
		{
			get
			{
				return bgmNumber;
			}

			set
			{
				bgmNumber = value;
				saved = false;
			}
		}
		public const uint BGM_NUMBER_ADDRESS = 5;

		private byte battleBackgroundNumber;
		public byte BattleBackgroundNumber
		{
			get
			{
				return battleBackgroundNumber;
			}

			set
			{
				battleBackgroundNumber = value;
			}
		}
		public const uint BATTLE_BACKGROUND_NUMBER_ADDRESS = 6;

		private byte animationTileSetNumber;
		public byte AnimationTileSetNumber
		{
			get
			{
				return animationTileSetNumber;
			}

			set
			{
				animationTileSetNumber = value;
			}
		}
		public const uint ANIMATION_TILE_SET_NUMBER_ADDRESS = 7;

		private byte transitionPaletteNumber;
		public byte TransitionPaletteNumber
		{
			get
			{
				return transitionPaletteNumber;
			}

			set
			{
				transitionPaletteNumber = value;
			}
		}
		public const uint TRANSITION_PALETTE_NUMBER_ADDRESS = 8;

		private WarpScriptList warpScriptList;
		public WarpScriptList WarpScriptList
		{
			get
			{
				return warpScriptList;
			}

			set
			{
				warpScriptList = value;
			}
		}
		public const uint WARP_SCRIPT_LIST_ADDRESS = 12;

		private MapScriptList mapScriptList;
		public MapScriptList MapScriptList
		{
			get
			{
				return mapScriptList;
			}

			set
			{
				mapScriptList = value;
			}
		}
		public const uint MAP_SCRIPT_LIST_ADDRESS = 16;

		private CharacterScriptList characterScriptList;
		public CharacterScriptList CharacterScriptList
		{
			get
			{
				return characterScriptList;
			}

			set
			{
				characterScriptList = value;
			}
		}
		public const uint CHARACTER_SCRIPT_LIST_ADDRESS = 20;

		private OverrideTile overrideTile;
		public OverrideTile OverrideTile
		{
			get
			{
				return overrideTile;
			}

			set
			{
				overrideTile = value;
			}
		}
		public const uint OVERRIDE_TILE_ADDRESS = 24;

		private MapArea mapArea;
		public MapArea MapArea
		{
			get
			{
				return mapArea;
			}

			set
			{
				mapArea = value;
			}
		}

		private MapTile mapTile;
		public MapTile MapTile
		{
			get
			{
				return mapTile;
			}

			set
			{
				mapTile = value;
			}
		}

		public const uint CHIP_SET_MAP_LENGTH_0 = 3;
		private ChipSetTable[] chipSetTable;
		public ChipSetTable[] ChipSetTable
		{
			get
			{
				return chipSetTable;
			}

			set
			{
				chipSetTable = value;
			}
		}

		public const uint CHIP_SET_LIST_LENGTH_0 = 3;
		private ChipSetList[] chipSetList;
		public ChipSetList[] ChipSetList
		{
			get
			{
				return chipSetList;
			}

			set
			{
				chipSetList = value;
			}
		}

		public const uint PALETTE_LENGTH_0 = 15;
		private Palette[] palette;
		public Palette[] Palette
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

		private LZ77 tile;
		public LZ77 Tile
		{
			get
			{
				return tile;
			}

			set
			{
				tile = value;
			}
		}
		
		//部屋の横の最小サイズ
		private const uint MINIMUM_WIDTH = 15;
		//部屋の縦の最小サイズ
		private const uint MINIMUM_HEIGHT = 10;

		public const uint BLOCK_WIDTH = 16;
		public const uint BLOCK_HEIGHT = 16;
		
		//1/60秒
		public static ushort Frame = 0;
		
		public static Color SelectColor = Color.FromArgb(255, 0, 0);
		
		public Map(TomatoAdventure tomatoAdventure, uint address)
		{
			load(tomatoAdventure, address);
		}

		public void initialize()
		{
			throw new System.NotImplementedException();
		}
		public void load(TomatoAdventure tomatoAdventure)
		{
			load(tomatoAdventure, address);
		}
		public void load(TomatoAdventure tomatoAdventure, uint address)
		{
			this.address = address;

			number = (ushort)tomatoAdventure.readLittleEndian(address + NUMBER_ADDRESS, 2);
			saveType = tomatoAdventure.read(address + SAVE_TYPE_ADDRESS);
			width = tomatoAdventure.read(address + WIDTH_ADDRESS);
			height = tomatoAdventure.read(address + HEIGHT_ADDRESS);
			bgmNumber = tomatoAdventure.read(address + BGM_NUMBER_ADDRESS);

			battleBackgroundNumber = tomatoAdventure.read(address + BATTLE_BACKGROUND_NUMBER_ADDRESS);
			animationTileSetNumber = tomatoAdventure.read(address + ANIMATION_TILE_SET_NUMBER_ADDRESS);
			transitionPaletteNumber = tomatoAdventure.read(address + TRANSITION_PALETTE_NUMBER_ADDRESS);

			mapArea = new MapArea(tomatoAdventure, this);
			warpScriptList = tomatoAdventure.add(new WarpScriptList(tomatoAdventure, tomatoAdventure.readAsAddress(address + WARP_SCRIPT_LIST_ADDRESS)));
			mapScriptList = tomatoAdventure.add(new MapScriptList(tomatoAdventure, tomatoAdventure.readAsAddress(address + MAP_SCRIPT_LIST_ADDRESS)));
			characterScriptList = tomatoAdventure.add(new CharacterScriptList(tomatoAdventure, tomatoAdventure.readAsAddress(address + CHARACTER_SCRIPT_LIST_ADDRESS)));
			overrideTile = new OverrideTile(tomatoAdventure, tomatoAdventure.readAsAddress(address + OVERRIDE_TILE_ADDRESS));
			
			if (animationTileSetNumber != 0x00)
			{
				tomatoAdventure.add(new AnimationTileSet(tomatoAdventure, animationTileSetNumber));
			}

			if (battleBackgroundNumber != 0xFF)
			{
				tomatoAdventure.add(new BattleBackground(tomatoAdventure, battleBackgroundNumber));
			}
			
			palette = new Palette[PALETTE_LENGTH_0];
			for (uint i = 0; i < palette.GetLength(0); ++i)
			{
				palette[i] = tomatoAdventure.add(new Palette(tomatoAdventure, tomatoAdventure.readAsAddress(tomatoAdventure.readAsAddress(PaletteTopAddressPointer[0]) + (number * ROM.POINTER_SIZE)) + (i * TomatoTool.Palette.SIZE)));
			}

			chipSetTable = new ChipSetTable[CHIP_SET_MAP_LENGTH_0];
			for (uint i = 0; i < chipSetTable.GetLength(0); ++i)
			{
				if (tomatoAdventure.isAddress(tomatoAdventure.readAsAddress(tomatoAdventure.readAsAddress(ChipSetTableAddressPointer[0]) + (number * (ROM.POINTER_SIZE * CHIP_SET_MAP_LENGTH_0)) + (i * ROM.POINTER_SIZE))))
				{
					chipSetTable[i] = tomatoAdventure.add(new ChipSetTable(tomatoAdventure, tomatoAdventure.readAsAddress(tomatoAdventure.readAsAddress(ChipSetTableAddressPointer[0]) + (number * (ROM.POINTER_SIZE * CHIP_SET_MAP_LENGTH_0)) + (i * ROM.POINTER_SIZE)), this));
				}
			}

			chipSetList = new ChipSetList[CHIP_SET_LIST_LENGTH_0];
			for (uint i = 0; i < chipSetList.GetLength(0); ++i)
			{
				if (tomatoAdventure.isAddress(tomatoAdventure.readAsAddress(tomatoAdventure.readAsAddress(ChipSetListTopAddressPointer[0]) + (number * (ROM.POINTER_SIZE * CHIP_SET_LIST_LENGTH_0)) + (i * ROM.POINTER_SIZE))))
				{
					chipSetList[i] = tomatoAdventure.add(new ChipSetList(tomatoAdventure, tomatoAdventure.readAsAddress(tomatoAdventure.readAsAddress(ChipSetListTopAddressPointer[0]) + (number * (ROM.POINTER_SIZE * CHIP_SET_LIST_LENGTH_0)) + (i * ROM.POINTER_SIZE)), chipSetTable[i]));
				}
			}

			try
			{
				mapTile = new MapTile(tomatoAdventure, this);
			}
			catch
			{
			}
		}

		public void save(TomatoAdventure tomatoAdventure, uint address)
		{
			this.address = address;

			tomatoAdventure.writeLittleEndian(address + NUMBER_ADDRESS, 2, number);
			tomatoAdventure.write(address + SAVE_TYPE_ADDRESS, saveType);
			tomatoAdventure.write(address + WIDTH_ADDRESS, width);
			tomatoAdventure.write(address + HEIGHT_ADDRESS, height);
			tomatoAdventure.write(address + BGM_NUMBER_ADDRESS, bgmNumber);
			tomatoAdventure.write(address + BATTLE_BACKGROUND_NUMBER_ADDRESS, battleBackgroundNumber);
			tomatoAdventure.write(address + ANIMATION_TILE_SET_NUMBER_ADDRESS, animationTileSetNumber);
			tomatoAdventure.write(address + TRANSITION_PALETTE_NUMBER_ADDRESS, transitionPaletteNumber);

			//以下不明値
			tomatoAdventure.write(address + 9, 0x00);
			tomatoAdventure.write(address + 10, 0x00);
			tomatoAdventure.write(address + 11, 0x00);

			mapArea.save(tomatoAdventure, this);

			warpScriptList.save(tomatoAdventure, warpScriptList.ObjectID);
			tomatoAdventure.writeAsAddress(address + WARP_SCRIPT_LIST_ADDRESS, warpScriptList.ObjectID);

			mapScriptList.save(tomatoAdventure);
			tomatoAdventure.writeAsAddress(address + MAP_SCRIPT_LIST_ADDRESS, mapScriptList.ObjectID);

			characterScriptList.save(tomatoAdventure);
			tomatoAdventure.writeAsAddress(address + CHARACTER_SCRIPT_LIST_ADDRESS, characterScriptList.ObjectID);

			overrideTile.save(tomatoAdventure);
			tomatoAdventure.writeAsAddress(address + OVERRIDE_TILE_ADDRESS, overrideTile.ObjectID);

			for (uint i = 0; i < palette.GetLength(0); ++i)
			{
				palette[i].save(tomatoAdventure, palette[i].ObjectID);
			}
			tomatoAdventure.writeAsAddress(tomatoAdventure.readAsAddress(PaletteTopAddressPointer[0]) + (number * ROM.POINTER_SIZE), palette[0].ObjectID);

			for (uint i = 0; i < chipSetTable.GetLength(0); ++i)
			{
				if (chipSetTable[i] != null)
				{
					chipSetTable[i].save(tomatoAdventure, chipSetTable[i].ObjectID);
					tomatoAdventure.writeAsAddress(tomatoAdventure.readAsAddress(ChipSetTableAddressPointer[0]) + (number * (ROM.POINTER_SIZE * CHIP_SET_MAP_LENGTH_0)) + (i * ROM.POINTER_SIZE), chipSetTable[i].ObjectID);
				}
				else
				{
					tomatoAdventure.writeAsAddress(tomatoAdventure.readAsAddress(ChipSetTableAddressPointer[0]) + (number * (ROM.POINTER_SIZE * CHIP_SET_MAP_LENGTH_0)) + (i * ROM.POINTER_SIZE), ROM.ADDRESS_NULL);
				}
			}

			for (uint i = 0; i < chipSetList.GetLength(0); ++i)
			{
				if (chipSetList[i] != null)
				{
					chipSetList[i].save(tomatoAdventure);
					tomatoAdventure.writeAsAddress(tomatoAdventure.readAsAddress(ChipSetListTopAddressPointer[0]) + (number * (ROM.POINTER_SIZE * CHIP_SET_LIST_LENGTH_0)) + (i * ROM.POINTER_SIZE), chipSetList[i].ObjectID);
				}
				else
				{
					tomatoAdventure.writeAsAddress(tomatoAdventure.readAsAddress(ChipSetListTopAddressPointer[0]) + (number * (ROM.POINTER_SIZE * CHIP_SET_LIST_LENGTH_0)) + (i * ROM.POINTER_SIZE), ROM.ADDRESS_NULL);
				}
			}

			mapTile.save(tomatoAdventure, this);

			saved = true;
		}

		public static bool compatible(TomatoAdventure tomatoAdventure, uint address)
		{
			return
				tomatoAdventure.isAddress(tomatoAdventure.readAsAddress(address + WARP_SCRIPT_LIST_ADDRESS)) &&
				tomatoAdventure.isAddress(tomatoAdventure.readAsAddress(address + MAP_SCRIPT_LIST_ADDRESS)) &&
				tomatoAdventure.isAddress(tomatoAdventure.readAsAddress(address + CHARACTER_SCRIPT_LIST_ADDRESS)) &&
				tomatoAdventure.isAddress(tomatoAdventure.readAsAddress(address + OVERRIDE_TILE_ADDRESS));
		}

		public void resize(byte width, byte height)
		{
			mapArea.resize(width, height);

			for (int i = 0; i < chipSetTable.GetLength(0); ++i)
			{
				chipSetTable[i].resize(width, height);
			}
		}

		public void draw(Graphics graphics, bool bg1, bool bg2, bool bg3, TransitionPalette transitionPalette = null)
		{
			bool[] bg = new bool[]
			{
				bg1,
				bg2,
				bg3
			};

			//BG3だけ透過させないため
			graphics.FillRectangle(new SolidBrush(palette[0][0].toColor()), 0, 0, width * ChipSet.WIDTH, height * ChipSet.HEIGHT);

			if (updataFlag)
			{
				updataFlag = false;
				if (this.bg == null)
				{
					this.bg = new Bitmap[3];
				}

				for (int i = this.chipSetTable.GetLength(0) - 1; i >= 0; --i)
				{
					if (bg[i] && (this.chipSetTable[i] != null))
					{
						this.bg[i] = new Bitmap(this.chipSetTable[i].ChipSet.GetLength(0) * ChipSet.WIDTH, this.chipSetTable[i].ChipSet.GetLength(1) * ChipSet.HEIGHT);

						using (Graphics g = Graphics.FromImage(this.bg[i]))
						{
							ChipSetList chipSetList = this.chipSetList[i];
							ChipSetTable chipSetTable = this.chipSetTable[i];

							for (int y = 0; y < chipSetTable.ChipSet.GetLength(1); ++y)
							{
								for (int x = 0; x < chipSetTable.ChipSet.GetLength(0); ++x)
								{
									if (chipSetTable.ChipSet[x, y] < chipSetList.Count)
									{
										using (Bitmap bitmap = mapTile.toBitmap(chipSetList[chipSetTable.ChipSet[x, y]], palette, transitionPalette, true))
										{
											g.DrawImage(bitmap, x * ChipSet.WIDTH, y * ChipSet.HEIGHT);
										}
									}
								}
							}
						}
					}
				}
			}

			for (int i = bg.GetLength(0) - 1; i >= 0; i--)
			{
				if (this.bg[i] != null)
				{
					graphics.DrawImage(this.bg[i], 0, 0);
				}
			}
		}

		private Bitmap[] bg;
		private bool updataFlag = true;
		public void updata()
		{
			updataFlag = true;
		}
	}
}
