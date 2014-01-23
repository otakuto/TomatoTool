using System.Collections;
using System.Collections.Generic;

namespace TomatoTool
{
	public class MapList
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

		public override uint Size
		{
			get
			{
				return (uint)(map.Count * TomatoTool.Map.SIZE);
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

		private List<ROMObject> map;
		private List<ROMObject> Map
		{
			get
			{
				return map;
			}
		}

		public List<uint> Pointer = new List<uint>()
		{
			ROM.addBase(0x0002C2A8),
			ROM.addBase(0x00033CFC),
			ROM.addBase(0x0003F8F4),
			ROM.addBase(0x0004A028),
			ROM.addBase(0x0004A070),
			ROM.addBase(0x0004A0B0),
			ROM.addBase(0x0006C4F4)
		};

		public MapList(TomatoAdventure tomatoAdventure)
		{
			load(tomatoAdventure);
		}

		public void load(TomatoAdventure tomatoAdventure)
		{
			address = tomatoAdventure.readAsAddress(Pointer[0]);

			for (uint i = 0; TomatoTool.Map.compatible(tomatoAdventure, address + (i * TomatoTool.Map.SIZE)); ++i)
			{
				tomatoAdventure.add(new Map(tomatoAdventure, address + (i * TomatoTool.Map.SIZE)));
			}
			map = tomatoAdventure.ObjectDictionary[typeof(Map)];
		}

		public void save(TomatoAdventure tomatoAdventure)
		{
			address = tomatoAdventure.malloc(Size, Alignment);

			for (int i = 0; i < Pointer.Count; ++i)
			{
				tomatoAdventure.writeAsAddress(Pointer[i], address);
			}

			FormSaveProgress formSaveProgress = new FormSaveProgress();
			formSaveProgress.Show();
			formSaveProgress.Maximum = map.Count;
			for (uint i = 0; i < map.Count; ++i)
			{
				System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();
				stopwatch.Start();
				formSaveProgress.setText("Map" + System.String.Format("{0:X4}", i));
				formSaveProgress.Refresh();
				((Map)map[(int)i]).save(tomatoAdventure, address + (i * TomatoTool.Map.SIZE));
				formSaveProgress.performStep();
				stopwatch.Stop();
				System.Console.WriteLine(System.String.Format("{0:X4}", i) + ":" + stopwatch.Elapsed);
			}
			formSaveProgress.Close();
		}
	}
}
