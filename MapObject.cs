
namespace TomatoTool
{
	public class MapObject
	{
		private byte x;
		public byte X
		{
			get
			{
				return x;
			}
			set
			{
				x = value;
			}
		}

		private byte y;
		public byte Y
		{
			get
			{
				return y;
			}
			set
			{
				y = value;
			}
		}

		public MapObject(byte x, byte y)
		{
			this.x = x;
			this.y = y;
		}

		public void move(byte x, byte y)
		{
			this.x = x;
			this.y = y;
		}
	}
}
