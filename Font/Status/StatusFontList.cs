using System.Drawing;

namespace TomatoTool
{
	public class StatusFontList : ROMObject
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

		private StatusFont[] statusFont;
		public StatusFont[] StatusFont
		{
			get
			{
				return statusFont;
			}

			set
			{
				statusFont = value;
			}
		}


		public StatusFontList(TomatoAdventure tomatoAdventure, uint address)
		{
			load(tomatoAdventure, address);
		}

		public void load(TomatoAdventure tomatoAdventure, uint address)
		{
			statusFont = new StatusFont[256];

			for (uint i = 0; i < statusFont.GetLength(0); ++i)
			{
				statusFont[i] = new StatusFont(tomatoAdventure, address + (i * TomatoTool.StatusFont.SIZE));
			}
		}

		public void save(TomatoAdventure tomatoAdventure, uint address)
		{
			for (uint i = 0; i < statusFont.GetLength(0); ++i)
			{
				statusFont[i].save(tomatoAdventure, address + (i * TomatoTool.StatusFont.SIZE));
			}
		}

		public Bitmap toBitmap(StatusString statusString)
		{
			Bitmap bitmap = new Bitmap(statusString.Text.Length * 8, 8);

			using (Graphics graphics = Graphics.FromImage(bitmap))
			{
				byte[] data = statusString.toByteArray();
				for (uint i = 0; i < data.GetLength(0); ++i)
				{
					graphics.DrawImage(statusFont[data[i]].toBitmap(), i * 8, 0);
				}
			}

			return bitmap;
		}
	}
}
