
namespace TomatoTool
{
	public class WindowFontList : ROMObject
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

		private WindowFont[] windowFont;
		public WindowFont[] WindowFont
		{
			get
			{
				return windowFont;
			}
		}

		private WindowChineseCharacterFont[] windowChineseCharacterFont;
		public WindowChineseCharacterFont[] WindowChineseCharacterFont
		{
			get
			{
				return windowChineseCharacterFont;
			}
		}

		public WindowFontList(TomatoAdventure tomatoAdventure, uint address)
		{
			load(tomatoAdventure, address);
		}

		public void load(TomatoAdventure tomatoAdventure, uint address)
		{
			windowFont = new WindowFont[2048];

			for (uint i = 0; i < windowFont.GetLength(0); ++i)
			{
				windowFont[i] = new WindowFont(tomatoAdventure, (uint)(address + (i * TomatoTool.WindowFont.SIZE)));
			}
		}

		public void save(TomatoAdventure tomatoAdventure, uint address)
		{
			for (uint i = 0; i < windowFont.GetLength(0); ++i)
			{
				windowFont[i].save(tomatoAdventure, (uint)(address + (i * TomatoTool.WindowFont.SIZE)));
			}
		}
	}
}
