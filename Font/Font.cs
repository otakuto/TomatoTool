
namespace TomatoTool
{
	public abstract class Font : ROMObject
	{
		protected byte[] whiteLayer;
		protected byte[] paleBlueLayer;

		public const int BLUE = 0;
		public const int WHITE = 1;
		public const int PALEBLUE = 2;
	}
}
