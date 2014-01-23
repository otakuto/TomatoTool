using System;
using System.Collections;
using System.Collections.Generic;

namespace TomatoTool
{
	public class TomatoAdventure
		:
		ROM
	{
		private StatusFontList statusCharacterFontList;
		public StatusFontList StatusCharacterFontList
		{
			get
			{
				return statusCharacterFontList;
			}

			set
			{
				statusCharacterFontList = value;
			}
		}

		private WindowFontList windowCharacterFontList;
		public WindowFontList WindowCharacterFontList
		{
			get
			{
				return windowCharacterFontList;
			}

			set
			{
				windowCharacterFontList = value;
			}
		}

		private Dictionary<Type, List<ROMObject>> objectDictionary;
		public Dictionary<Type, List<ROMObject>> ObjectDictionary
		{
			get
			{
				return objectDictionary;
			}

			set
			{
				objectDictionary = value;
			}
		}

		public TomatoAdventure(byte[] rom)
			: base(rom)
		{
			objectDictionary = new Dictionary<Type, List<ROMObject>>();

			load(rom);
		}

		public void load(byte[] rom)
		{
			add(new MapList(this));
			add(new GimmickList(this));
			addMonster(this, ROM.addBase(0x00634F50));

			statusCharacterFontList = new StatusFontList(this, ROM.addBase(0x00648748));
			windowCharacterFontList = new WindowFontList(this, ROM.addBase(0x0064274E));
		}

		public void save()
		{
			((MapList)objectDictionary[typeof(MapList)][0]).save(this);
			++saved;
		}

		public void rebuild()
		{
			/*
			const uint rebuildAddress = 0x650000;
			uint position = 0;

			//アドレスの変更
			for (uint i = 0; i < TomatoTool.Map.topAddress.Count; ++i)
			{
				tomatoAdventure.writeAsAddress((int)TomatoTool.Map.topAddress[i], rebuildAddress + position);
			}

			//マップの分だけ移動
			position = map.Count * TomatoTool.Map.SIZE;

			for (uint i = 0; i < map.Count; ++i)
			{
				map[i].WarpScriptList.save(tomatoAdventure, rebuildAddress + position);
				position += map[i].WarpScriptList.getSize();
			}

			position = 0;
			for (uint i = 0; i < map.Count; ++i)
			{
				position += map[i].save(tomatoAdventure, rebuildAddress + position);
			}
			*/
		}

		public T add<T>(T t)
		{
			if (t != null)
			{
				Type type = t.GetType();
				ROMObject romObject = t as ROMObject;

				if (objectDictionary.ContainsKey(type))
				{
					int index = objectDictionary[type].IndexOf(romObject);

					if (index == -1)
					{
						objectDictionary[type].Add(romObject);
						return t;
					}
					else
					{
						return (T)Convert.ChangeType(objectDictionary[type][index], typeof(T));
					}
				}
				else
				{
					objectDictionary.Add(type, new List<ROMObject>());
					objectDictionary[type].Add(romObject);
					return t;
				}
			}
			else
			{
				throw new Exception();
			}
		}

		public void objectListSort()
		{
			foreach (KeyValuePair<Type, List<ROMObject>> listROMObject in objectDictionary)
			{
				listROMObject.Value.Sort();
			}
		}

		private void addMonster(TomatoAdventure tomatoAdventure, uint address)
		{
			for (uint i = 0; Monster.compatible(tomatoAdventure, address + (i * TomatoTool.Monster.SIZE)); ++i)
			{
				tomatoAdventure.add(new Monster(tomatoAdventure, address + (i * TomatoTool.Monster.SIZE)));
			}
		}

		private void addClothes(TomatoAdventure tomatoAdventure, uint address)
		{
			for
			(
				uint i = 0;
				tomatoAdventure.isAddress(tomatoAdventure.readAsAddress(address + 20 + (i * TomatoTool.Monster.SIZE))) &&
				tomatoAdventure.isAddress(tomatoAdventure.readAsAddress(address + 24 + (i * TomatoTool.Monster.SIZE))) &&
				tomatoAdventure.isAddress(tomatoAdventure.readAsAddress(address + 28 + (i * TomatoTool.Monster.SIZE)));
				++i
			)
			{
				tomatoAdventure.add(new Clothes(tomatoAdventure, address + (i * TomatoTool.Monster.SIZE)));
			}
		}
	}
}
