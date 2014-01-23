using System.Collections;
using System.Collections.Generic;

namespace TomatoTool
{
	public class GimmickList
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
				throw new System.NotImplementedException();
			}
		}

		public override uint Alignment
		{
			get
			{
				throw new System.NotImplementedException();
			}
		}

		private List<ROMObject> gimmick;
		public List<ROMObject> Gimmick
		{
			get
			{
				return gimmick;
			}

			set
			{
				gimmick = value;
			}
		}

		public List<uint> Pointer = new List<uint>()
		{
			ROM.addBase(0x00048634),
			ROM.addBase(0x0004867C),
			ROM.addBase(0x00048698),
			ROM.addBase(0x000486B4),
			ROM.addBase(0x000486D0),
			ROM.addBase(0x000486EC),
			ROM.addBase(0x00048708),
			ROM.addBase(0x00048724),
			ROM.addBase(0x00048740),
			ROM.addBase(0x000487C0),
			ROM.addBase(0x00048874),
			ROM.addBase(0x000488AC),
			ROM.addBase(0x000488BC),
			ROM.addBase(0x000488CC),
			ROM.addBase(0x000488DC),
			ROM.addBase(0x000488EC),
			ROM.addBase(0x000488FC),
			ROM.addBase(0x0004890C),
			ROM.addBase(0x0004891C),
			ROM.addBase(0x00048950),
			ROM.addBase(0x00049F2C),
			ROM.addBase(0x0004AB7C),
			ROM.addBase(0x0004CC78),
			ROM.addBase(0x0004E05C),
			ROM.addBase(0x0004E134),
			ROM.addBase(0x0004E1AC),
			ROM.addBase(0x0004E2E4),
			ROM.addBase(0x0004E430),
			ROM.addBase(0x0004E64C),
			ROM.addBase(0x0004E720),
			ROM.addBase(0x0004EA18),
			ROM.addBase(0x0004EA38),
			ROM.addBase(0x0006AD7C),
			ROM.addBase(0x0006AE0C),
			ROM.addBase(0x0007533C),
			ROM.addBase(0x000754CC),
			ROM.addBase(0x000755FC),
			ROM.addBase(0x000759D8),
			ROM.addBase(0x00075A14),
			ROM.addBase(0x00075A28),
			ROM.addBase(0x00075A3C),
			ROM.addBase(0x00075A50),
			ROM.addBase(0x00075A64),
			ROM.addBase(0x00075A78),
			ROM.addBase(0x00075A8C),
			ROM.addBase(0x00075AA0),
			ROM.addBase(0x00075AF0),
			ROM.addBase(0x00078D54),
			ROM.addBase(0x00080A8C),
			ROM.addBase(0x000811CC)
		};

		public GimmickList(TomatoAdventure tomatoAdventure)
		{
			load(tomatoAdventure);
		}

		public void load(TomatoAdventure tomatoAdventure)
		{
			address = tomatoAdventure.readAsAddress(Pointer[0]);

			for (uint i = 0; TomatoTool.Gimmick.compatible(tomatoAdventure, address + (i * TomatoTool.Gimmick.SIZE)); ++i)
			{
				tomatoAdventure.add(new Gimmick(tomatoAdventure, address + (i * TomatoTool.Gimmick.SIZE), tomatoAdventure.readAsAddress(ROM.addBase(0x0064A4AC) + (i * ROM.POINTER_SIZE)), tomatoAdventure.readAsAddress(ROM.addBase(0x0064A574) + (i * ROM.POINTER_SIZE))));
			}
			gimmick = tomatoAdventure.ObjectDictionary[typeof(Gimmick)];
		}
	}
}
