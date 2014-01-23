using System;
using System.Drawing;
using System.Windows.Forms;


namespace TomatoTool
{
	public partial class FormMapEditor : Form
	{
		private ushort selectChipSetTable;

		#region コピーリスト

		private void pictureBoxChipSetTableCopyList_Paint(object sender, PaintEventArgs e)
		{
			int bg = radioButtonChipSetTableBG();

			ChipSetList chipSetList = map.ChipSetList[bg];

			if (chipSetList != null)
			{
				//リサイズ
				((PictureBox)sender).Width = ChipSet.WIDTH * 16;
				((PictureBox)sender).Height = ChipSet.HEIGHT * ((chipSetList.Count / 16) + (((chipSetList.Count % 16) == 0) ? 0 : 1));

				e.Graphics.FillRectangle(new SolidBrush(map.Palette[0][0].toColor()), 0, 0, ((PictureBox)sender).Width, ((PictureBox)sender).Height - ChipSet.HEIGHT);
				e.Graphics.FillRectangle(new SolidBrush(map.Palette[0][0].toColor()), 0, ((PictureBox)sender).Height - ChipSet.HEIGHT, (chipSetList.Count % 16 == 0 ? 16 : chipSetList.Count % 16) * ChipSet.WIDTH, ((PictureBox)sender).Height);

				for (int i = 0; i < chipSetList.Count; ++i)
				{
					using (Bitmap bitmap = map.MapTile.MainTile.toBitmap(chipSetList[i], map.Palette, true))
					{
						e.Graphics.DrawImage(bitmap, (i % 16) * ChipSet.WIDTH, (i / 16) * ChipSet.HEIGHT, ChipSet.WIDTH, ChipSet.HEIGHT);
					}
				}

				//グリッド描写
				if (toolStripMenuItemMapGrid.Checked)
				{
					gridDraw(sender, e.Graphics);
				}

				//枠描写
				using (Pen pen = new Pen(TomatoTool.Map.SelectColor, 2))
				{
					e.Graphics.DrawRectangle(pen, ((selectChipSetTable % 16) * ChipSet.WIDTH) + 1, ((selectChipSetTable / 16) * ChipSet.HEIGHT) + 1, ChipSet.WIDTH - pen.Width, ChipSet.HEIGHT - pen.Width);
				}
			}
		}
		private void pictureBoxChipSetTableCopyList_MouseMove(object sender, MouseEventArgs e)
		{
			int bg = radioButtonChipSetTableBG();

			ChipSetList chipSetList = map.ChipSetList[bg];
			ChipSetTable chipSetTable = map.ChipSetTable[bg];

			if ((e.Button == MouseButtons.Left) &&
			((0 <= e.X) && (e.X < ((PictureBox)sender).Width)) &&
			((0 <= e.Y) && (e.Y < ((PictureBox)sender).Height)) &&
			((e.X / TomatoTool.ChipSet.WIDTH) + ((e.Y / TomatoTool.ChipSet.HEIGHT) * 16)) < chipSetList.Count)
			{
				selectChipSetTable = (ushort)((e.X / TomatoTool.ChipSet.WIDTH) + ((e.Y / TomatoTool.ChipSet.HEIGHT) * 16));
			}

			pictureBoxChipSetTableCopyList.Refresh();
		}

		#endregion

		private void radioButtonChipSetTableBG_CheckedChanged(object sender, EventArgs e)
		{
			selectChipSetTable = 0;
			pictureBoxChipSetTable.Refresh();
			pictureBoxChipSetTableCopyList.Refresh();
		}
		public int radioButtonChipSetTableBG()
		{
			if (radioButtonChipSetTableBG1.Checked)
			{
				return 0;
			}

			if (radioButtonChipSetTableBG2.Checked)
			{
				return 1;
			}

			if (radioButtonChipSetTableBG3.Checked)
			{
				return 2;
			}

			return -1;
		}

		private void pictureBoxChipSetTable_Paint(object sender, PaintEventArgs e)
		{
			int bg = radioButtonChipSetTableBG();

			

			if (map.ChipSetTable[bg] != null)
			{
				ChipSetList chipSetList = map.ChipSetList[bg];
				ChipSetTable chipSetTable = map.ChipSetTable[bg];

				((PictureBox)sender).Width = chipSetTable.ChipSet.GetLength(0) * TomatoTool.ChipSet.WIDTH;
				((PictureBox)sender).Height = chipSetTable.ChipSet.GetLength(1) * TomatoTool.ChipSet.HEIGHT;

				//塗りつぶし透過させないため
				e.Graphics.FillRectangle(new SolidBrush(map.Palette[0][0].toColor()), 0, 0, ((PictureBox)sender).Width, ((PictureBox)sender).Height);

				for (int y = 0; y < chipSetTable.ChipSet.GetLength(1); ++y)
				{
					for (int x = 0; x < chipSetTable.ChipSet.GetLength(0); ++x)
					{
						try
						{
							e.Graphics.DrawImage(map.MapTile.MainTile.toBitmap(chipSetList[chipSetTable.ChipSet[x, y]], map.Palette, true), x * TomatoTool.ChipSet.WIDTH, y * TomatoTool.ChipSet.HEIGHT);
						}
						catch
						{
							chipSetTable.ChipSet[x, y] = (ushort)0x0000;
						}
					}
				}

				//マップエリアの描写
				if (toolStripMenuItemMapArea.Checked)
				{
					map.MapArea.draw(e.Graphics, true);
				}

				//グリッド描写
				if (toolStripMenuItemMapGrid.Checked)
				{
					gridDraw(sender, e.Graphics);
				}

				//ワープスクリプト描写
				if (toolStripMenuItemViewWarpScript.Checked)
				{
					map.WarpScriptList.draw(e.Graphics);
				}

				//マップスクリプト描写
				if (toolStripMenuItemMapScript.Checked)
				{
					map.MapScriptList.draw(e.Graphics);
				}

				//キャラクタースクリプト描写
				if (toolStripMenuItemCharacterScript.Checked)
				{
					map.CharacterScriptList.draw(e.Graphics);
				}
			}
		}
		private void pictureBoxChipSetTable_MouseMove(object sender, MouseEventArgs e)
		{
			int bg = radioButtonChipSetTableBG();

			ChipSetList chipSetList = map.ChipSetList[bg];
			ChipSetTable chipSetTable = map.ChipSetTable[bg];

			if (((0 <= e.X) && (e.X < ((PictureBox)sender).Width)) &&
				((0 <= e.Y) && (e.Y < ((PictureBox)sender).Height)) &&
				(e.X / TomatoTool.ChipSet.WIDTH) < chipSetTable.ChipSet.GetLength(0) &&
				(e.Y / TomatoTool.ChipSet.HEIGHT) < chipSetTable.ChipSet.GetLength(1))
			{
				switch (e.Button)
				{
					case MouseButtons.Left:

						chipSetTable.ChipSet[e.X / TomatoTool.ChipSet.WIDTH, e.Y / TomatoTool.ChipSet.HEIGHT] = selectChipSetTable;
						chipSetTable.Saved = false;
						map.updata();

						pictureBoxChipSetTable.Refresh();
						break;

					case MouseButtons.Right:

						selectChipSetTable = (ushort)chipSetTable.ChipSet[e.X / TomatoTool.ChipSet.WIDTH, e.Y / TomatoTool.ChipSet.HEIGHT];
						pictureBoxChipSetTableCopyList.Refresh();

						break;

					default:
						break;
				}
			}
		}
	}
}
