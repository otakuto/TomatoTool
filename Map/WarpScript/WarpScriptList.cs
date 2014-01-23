using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;

namespace TomatoTool
{
	public class WarpScriptList : ROMObject, IList
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

		
		public override uint Size
		{
			get
			{
				return (uint)(WarpScript.SIZE * warpScript.Count) + FOOTER_SIZE;
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
		private List<WarpScript> warpScript;

		private bool saved;
		public override bool Saved
		{
			get
			{
				for (int i = 0; i < warpScript.Count; ++i)
				{
					if (!warpScript[i].Saved)
					{
						return warpScript[i].Saved;
					}
				}
				return saved;
			}

			set
			{
				saved = value;
			}
		}

		public static readonly byte[] FOOTER = new byte[]
		{
			0xFF,
			0x00,
			0x00,
			0x00
		};
		public const uint FOOTER_SIZE = 4;

		public static Color Color = Color.FromArgb(0, 0, 255);

		public WarpScriptList()
		{
			initialize();
		}
		public WarpScriptList(TomatoAdventure tomatoAdventure, uint address)
		{
			load(tomatoAdventure, address);
		}

		public void initialize()
		{
			address = TomatoTool.ROM.ADDRESS_NULL;
			warpScript = new List<WarpScript>();
		}
		public void load(TomatoAdventure tomatoAdventure)
		{
			load(tomatoAdventure, address);
		}
		public void load(TomatoAdventure tomatoAdventure, uint address)
		{
			this.address = address;

			warpScript = new List<WarpScript>();

			{
				uint i = 0;
				while ((tomatoAdventure.read((uint)(address + (i * WarpScript.SIZE))) != 0xFF))
				{
					warpScript.Add(new WarpScript(tomatoAdventure, address + (i * WarpScript.SIZE)));
					++i;
				}
			}
		}
		
		public void save(TomatoAdventure tomatoAdventure, uint address)
		{
			this.address = address;

			if (warpScript != null)
			{
				for (uint i = 0; i < warpScript.Count; ++i)
				{
					warpScript[(int)i].save(tomatoAdventure, address + (i * TomatoTool.WarpScript.SIZE));
				}

				tomatoAdventure.writeArray((uint)(address + (warpScript.Count * TomatoTool.WarpScript.SIZE)), (uint)FOOTER.GetLength(0), FOOTER);
			}

			saved = true;
		}
		
		bool IList.IsFixedSize
		{
			get
			{
				return ((IList)warpScript).IsFixedSize;
			}
		}
		bool IList.IsReadOnly
		{
			get
			{
				return ((IList)warpScript).IsReadOnly;
			}
		}
		object IList.this[int index]
		{
			get
			{
				return ((IList)warpScript)[index];
			}

			set
			{
				((IList)warpScript)[index] = value;
				saved = false;
			}
		}
		int IList.Add(object value)
		{
			saved = false;
			return ((IList)warpScript).Add(value);
		}
		void IList.Clear()
		{
			((IList)warpScript).Clear();
			saved = false;
		}
		bool IList.Contains(object value)
		{
			return ((IList)warpScript).Contains(value);
		}
		int IList.IndexOf(object value)
		{
			return ((IList)warpScript).IndexOf(value);
		}
		void IList.Insert(int index, object value)
		{
			saved = false;
			((IList)warpScript).Insert(index, value);
		}
		void IList.Remove(object value)
		{
			saved = false;
			((IList)warpScript).Remove(value);
		}
		void IList.RemoveAt(int index)
		{
			saved = false;
			((IList)warpScript).RemoveAt(index);
		}

		int ICollection.Count
		{
			get
			{
				return ((ICollection)warpScript).Count;
			}
		}
		bool ICollection.IsSynchronized
		{
			get
			{
				return ((ICollection)warpScript).IsSynchronized;
			}
		}
		object ICollection.SyncRoot
		{
			get
			{
				return ((ICollection)warpScript).SyncRoot;
			}
		}
		void ICollection.CopyTo(Array array, int index)
		{
			((ICollection)warpScript).CopyTo(array, index);
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return warpScript.GetEnumerator();
		}

		public void draw(Graphics graphics)
		{
			draw(graphics, -1);
		}
		public void draw(Graphics graphics, int select)
		{
			if (warpScript != null)
			{
				for (int i = 0; i < warpScript.Count; ++i)
				{
					using (Pen pen = new Pen((select == i) ? Map.SelectColor : Color, 2))
					{
						graphics.DrawRectangle(pen, (warpScript[i].BeginX * (int)Map.BLOCK_WIDTH) + 1, (warpScript[i].BeginY * (int)Map.BLOCK_HEIGHT) + 1, ((warpScript[i].EndX - warpScript[i].BeginX) * Map.BLOCK_WIDTH) + (Map.BLOCK_WIDTH - pen.Width), ((warpScript[i].EndY - warpScript[i].BeginY) * Map.BLOCK_HEIGHT) + (Map.BLOCK_HEIGHT - pen.Width));
					}
				}
			}
		}
	}
}
