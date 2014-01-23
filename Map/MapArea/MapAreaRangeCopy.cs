using System.Drawing;

namespace TomatoTool
{
	public class MapAreaRangeCopy
	{
		public byte this[int i, int j]
		{
			get
			{
				return rangeCopy[i, j];
			}

			set
			{
				rangeCopy[i, j] = value;
			}
		}
		public int GetLength(int dimension)
		{
			return rangeCopy.GetLength(dimension);
		}

		private byte[,] rangeCopy;

		public MapAreaRangeCopy(MapArea mapArea, Rectangle rectangle)
		{
			copy(mapArea, rectangle);
		}

		public void copy(MapArea mapArea, Rectangle rectangle)
		{
			rangeCopy = new byte[rectangle.Width + 1, rectangle.Height + 1];

			for (int i = 0; (i < rangeCopy.GetLength(0)) && ((rectangle.X + i) < mapArea.GetLength(0)); ++i)
			{
				for (int j = 0; (j < rangeCopy.GetLength(1)) && ((rectangle.Y + j) < mapArea.GetLength(1)); ++j)
				{
					rangeCopy[i, j] = mapArea[rectangle.X + i, rectangle.Y + j];
				}
			}
		}

		public void paste(MapArea mapArea, byte x, byte y)
		{
			for (int i = x; (i < rangeCopy.GetLength(0)) && (i < mapArea.GetLength(0)); ++i)
			{
				for (int j = y; (j < rangeCopy.GetLength(1)) && (j < mapArea.GetLength(1)); ++j)
				{
					mapArea[i, j] = rangeCopy[i, j];
				}
			}
		}

		public void draw(Graphics graphics)
		{
			for (int y = 0; y < rangeCopy.GetLength(1); ++y)
			{
				for (int x = 0; x < rangeCopy.GetLength(0); ++x)
				{
					graphics.DrawImage(MapArea.image[rangeCopy[x, y]], x * Map.BLOCK_WIDTH, y * Map.BLOCK_HEIGHT);
				}
			}
		}

	}
}
