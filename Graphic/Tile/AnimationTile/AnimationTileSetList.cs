using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace TomatoTool
{
	public class AnimationTileSetList : ROMObject
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

		public List<uint> Pointer = new List<uint>()
		{
			ROM.addBase(0x00034080),
			ROM.addBase(0x00034E1C),
		};

		private Dictionary<byte, AnimationTileSet> animationTileSet;
		public Dictionary<byte, AnimationTileSet> AnimationTileSet
		{
			get
			{
				return animationTileSet;
			}
		}

		public AnimationTileSetList()
		{
			initialize();
		}
		public void initialize()
		{
			animationTileSet = new Dictionary<byte, AnimationTileSet>();
		}

		public void load(TomatoAdventure tomatoAdventure)
		{
		}

		public void add(TomatoAdventure tomatoAdventure, byte index)
		{
			if ((index == 0x00) || (animationTileSet.ContainsKey(index)))
			{
			}
			else
			{
				animationTileSet.Add(index, new AnimationTileSet(tomatoAdventure, tomatoAdventure.readAsAddress(Pointer[0]) + (index * ROM.POINTER_SIZE)));
			}
		}

	}
}
