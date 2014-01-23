
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;

namespace TomatoTool
{
	public class MapScriptList : ROMObject, IList
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
				for (int i = 0; i < mapScript.Count; ++i)
				{
					if (!mapScript[i].Saved)
					{
						return mapScript[i].Saved;
					}
				}
				return mainScript.Saved && saved;
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
				return MAIN_SIZE + (uint)(mapScript.Count * TomatoTool.MapScript.SIZE) + FOOTER_SIZE;
			}
		}

		public override uint Alignment
		{
			get
			{
				return ALIGNMENT;
			}
		}


		private Script mainScript;
		public Script MainScript
		{
			get
			{
				return mainScript;
			}

			set
			{
				mainScript = value;
				saved = false;
			}
		}

		private List<MapScript> mapScript;

		public static Color Color = Color.FromArgb(0, 0xFF, 0xFF);

		public const uint MAIN_SIZE = 4;
		public const uint FOOTER_SIZE = 4;
		public const uint ALIGNMENT = 4;

		public MapScriptList()
		{
			initialize();
		}
		public MapScriptList(TomatoAdventure tomatoAdventure, uint address)
		{
			load(tomatoAdventure, address);
		}

		public void initialize()
		{
			mainScript = Script.NULL;
			mapScript = new List<MapScript>();
		}
		public void load(TomatoAdventure tomatoAdventure, uint address)
		{
			this.address = address;

			if (address >= 0)
			{
				mainScript = (Script)tomatoAdventure.add(new Script(tomatoAdventure, tomatoAdventure.readAsAddress(address)));

				{
					mapScript = new List<MapScript>();

					uint i = 0;
					while (tomatoAdventure.read(address + MAIN_SIZE + (i * TomatoTool.MapScript.SIZE)) != 0xFF)
					{
						mapScript.Add(new MapScript(tomatoAdventure, address + MAIN_SIZE + (i * TomatoTool.MapScript.SIZE)));
						++i;
					}
				}
			}
		}
		public void save(TomatoAdventure tomatoAdventure)
		{
			if (!Saved)
			{
				save(tomatoAdventure, tomatoAdventure.malloc(Size, Alignment));
				saved = true;
			}
		}
		public void save(TomatoAdventure tomatoAdventure, uint address)
		{
			this.address = address;

			mainScript.save(tomatoAdventure);
			tomatoAdventure.writeAsAddress(address, mainScript.ObjectID);

			for (uint i = 0; i < mapScript.Count; ++i)
			{
				mapScript[(int)i].save(tomatoAdventure, address + 4 + (i * TomatoTool.MapScript.SIZE));
			}

			tomatoAdventure.write((uint)(address + MAIN_SIZE + (mapScript.Count * TomatoTool.MapScript.SIZE) + 0), 0xFF);
			tomatoAdventure.write((uint)(address + MAIN_SIZE + (mapScript.Count * TomatoTool.MapScript.SIZE) + 1), 0x00);
			tomatoAdventure.write((uint)(address + MAIN_SIZE + (mapScript.Count * TomatoTool.MapScript.SIZE) + 2), 0x00);
			tomatoAdventure.write((uint)(address + MAIN_SIZE + (mapScript.Count * TomatoTool.MapScript.SIZE) + 3), 0x00);

			saved = true;
		}

		bool IList.IsFixedSize
		{
			get
			{
				return ((IList)mapScript).IsFixedSize;
			}
		}
		bool IList.IsReadOnly
		{
			get
			{
				return ((IList)mapScript).IsReadOnly;
			}
		}
		object IList.this[int index]
		{
			get
			{
				return ((IList)mapScript)[index];
			}

			set
			{
				((IList)mapScript)[index] = value;
				saved = false;
			}
		}
		int IList.Add(object value)
		{
			saved = false;
			return ((IList)mapScript).Add(value);
		}
		void IList.Clear()
		{
			((IList)mapScript).Clear();
			saved = false;
		}
		bool IList.Contains(object value)
		{
			return ((IList)mapScript).Contains(value);
		}
		int IList.IndexOf(object value)
		{
			return ((IList)mapScript).IndexOf(value);
		}
		void IList.Insert(int index, object value)
		{
			((IList)mapScript).Insert(index, value);
			saved = false;
		}
		void IList.Remove(object value)
		{
			((IList)mapScript).Remove(value);
			saved = false;
		}
		void IList.RemoveAt(int index)
		{
			((IList)mapScript).RemoveAt(index);
			saved = false;
		}
		int ICollection.Count
		{
			get
			{
				return ((ICollection)mapScript).Count;
			}
		}
		bool ICollection.IsSynchronized
		{
			get
			{
				return ((ICollection)mapScript).IsSynchronized;
			}
		}
		object ICollection.SyncRoot
		{
			get
			{
				return ((ICollection)mapScript).SyncRoot;
			}
		}
		void ICollection.CopyTo(Array array, int index)
		{
			((ICollection)mapScript).CopyTo(array, index);
		}
		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable)mapScript).GetEnumerator();
		}

		public void draw(Graphics graphics)
		{
			draw(graphics, -1);
		}
		public void draw(Graphics graphics, int select)
		{
			if (mapScript != null)
			{
				for (int i = 0; i < mapScript.Count; ++i)
				{
					using (Pen pen = new Pen((select == i) ? TomatoTool.Map.SelectColor : Color, 2))
					{
						graphics.DrawRectangle(pen, (mapScript[i].BeginX * (int)TomatoTool.Map.BLOCK_WIDTH) + 1, (mapScript[i].BeginY * (int)TomatoTool.Map.BLOCK_HEIGHT) + 1, ((mapScript[i].EndX - mapScript[i].BeginX) * TomatoTool.Map.BLOCK_WIDTH) + (TomatoTool.Map.BLOCK_WIDTH - pen.Width), ((mapScript[i].EndY - mapScript[i].BeginY) * TomatoTool.Map.BLOCK_HEIGHT) + (TomatoTool.Map.BLOCK_HEIGHT - pen.Width));
					}
				}
			}
		}
	}
}
