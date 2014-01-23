using System;

namespace TomatoTool
{
	public class MapRangeObject
	{
		//X座標始点
		private byte beginX;
		public byte BeginX
		{
			get
			{
				return beginX;
			}

			set
			{
				if (value <= endX)
				{
					beginX = value;
				}
				else
				{
					beginX = endX;
					endX = value;
				}
			}
		}

		//Y座標始点
		private byte beginY;
		public byte BeginY
		{
			get
			{
				return beginY;
			}

			set
			{
				if (value <= endY)
				{
					beginY = value;
				}
				else
				{
					beginY = endY;
					endY = value;
				}
			}
		}

		//X座標終点
		private byte endX;
		public byte EndX
		{
			get
			{
				return endX;
			}

			set
			{
				if (beginX <= value)
				{
					endX = value;
				}
				else
				{
					endX = beginX;
					beginX = value;
				}
			}
		}

		//Y座標終点
		private byte endY;
		public byte EndY
		{
			get
			{
				return endY;
			}

			set
			{
				if (beginY <= value)
				{
					endY = value;
				}
				else
				{
					endY = beginY;
					beginY = value;
				}
			}
		}


		public MapRangeObject()
		{
			initialize();
		}
		public MapRangeObject(byte beginX, byte beginY, byte endX, byte endY)
		{
			this.beginX = beginX;
			this.beginY = beginY;
			this.endX = endX;
			this.endY = endY;
		}

		public void initialize()
		{
			beginX = 0;
			beginY = 0;
			endX = 0;
			endY = 0;
		}

		public void move(byte startX, byte startY)
		{
			endX = (byte)(startX + endX - this.beginX);
			endY = (byte)(startY + endY - this.beginY);

			BeginX = startX;
			BeginY = startY;
		}
	}
}
