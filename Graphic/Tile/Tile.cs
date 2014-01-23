using System.Drawing;

namespace TomatoTool
{
	public abstract class Tile : ROMObject
	{
		protected byte[,] tile;

		public const int WIDTH = 8;
		public const int HEIGHT = 8;

		public abstract Bitmap toBitmap(Palette palette);

		public abstract Bitmap toBitmap(Palette palette, bool flipX, bool flipY);
	}
}