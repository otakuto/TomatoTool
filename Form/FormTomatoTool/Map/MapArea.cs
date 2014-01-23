using System;
using System.Drawing;
using System.Windows.Forms;


namespace TomatoTool
{
	public partial class FormTomatoTool : Form
	{
		private bool mouseDownRight;
		private byte mapAreaCopyBeginX;
		private byte mapAreaCopyBeginY;

		private byte selectMapArea;

		private void initializeMapArea()
		{
		}

		//マップエリア
		private void pictureBoxMapArea_Paint(object sender, PaintEventArgs e)
		{
			Map map = (Map)listBoxMap.SelectedItem;

			pictureBoxResize(sender, map);

			//マップチップナンバーリスト
			map.draw(e.Graphics, toolStripMenuItemMapChipBG[0].Checked, toolStripMenuItemMapChipBG[1].Checked, toolStripMenuItemMapChipBG[2].Checked);

			//マップエリア描写
			map.MapArea.draw(e.Graphics, toolStripMenuItemMapChipBG[0].Checked || toolStripMenuItemMapChipBG[1].Checked || toolStripMenuItemMapChipBG[2].Checked);

			//グリッド描写
			if (toolStripMenuItemMapGrid.Checked)
			{
				gridDraw(sender, e.Graphics);
			}

			//ワープスクリプト描写
			if (toolStripMenuItemMapWarpScript.Checked)
			{
				map.WarpScriptList.draw(e.Graphics);
			}

			//マップスクリプト描写
			if (toolStripMenuItemMapScript.Checked)
			{
				map.MapScriptList.draw(e.Graphics);
			}

			//キャラクタースクリプト描写
			if (toolStripMenuItemMapCharacterScript.Checked)
			{
				map.CharacterScriptList.draw(e.Graphics);
			}
		}

		private void pictureBoxMapArea_MouseDown(object sender, MouseEventArgs e)
		{
			Map map = (Map)listBoxMap.SelectedItem;

			MapArea mapArea = map.MapArea;

			if (((0 <= e.X) && (e.X < ((PictureBox)sender).Width)) &&
				((0 <= e.Y) && (e.Y < ((PictureBox)sender).Height)) &&
				((e.X / 16) < mapArea.GetLength(0)) &&
				((e.Y / 16) < mapArea.GetLength(1)))
			{
				statusLabel.Text = String.Format("X={0:D3}  Y={1:D3}  Value={2:X2}", (e.X / 16), (e.Y / 16), mapArea[(e.X / 16), (e.Y / 16)]);

				switch (e.Button)
				{
					case MouseButtons.Right:

						mapAreaCopyBeginX = (byte)(e.X / 16);
						mapAreaCopyBeginY = (byte)(e.Y / 16);
						mouseDownRight = true;

						pictureBoxMapAreaCopyList.Refresh();

						break;

					default:
						break;
				}
			}
		}
		private void pictureBoxMapArea_MouseLeave(object sender, EventArgs e)
		{
			statusLabel.Text = null;
		}
		private void pictureBoxMapArea_MouseMove(object sender, MouseEventArgs e)
		{
			MapArea mapArea = ((Map)listBoxMap.SelectedItem).MapArea;

			if (((0 <= e.X) && (e.X < ((PictureBox)sender).Width)) &&
				((0 <= e.Y) && (e.Y < ((PictureBox)sender).Height)) &&
				((e.X / 16) < mapArea.GetLength(0)) &&
				((e.Y / 16) < mapArea.GetLength(1)))
			{
				statusLabel.Text = String.Format("X={0:D3}  Y={1:D3}  Value={2:X2}", (e.X / 16), (e.Y / 16), mapArea[(e.X / 16), (e.Y / 16)]);

				switch (e.Button)
				{
					case MouseButtons.Left:

						mapArea[(e.X / 16), (e.Y / 16)] = selectMapArea;
						pictureBoxMapArea.Refresh();

						break;

					case MouseButtons.Right:

						selectMapArea = mapArea[(e.X / 16), (e.Y / 16)];
						pictureBoxMapAreaCopyList.Refresh();
						textBoxMapAreaCopyCode.Text = String.Format("{0:X2}", selectMapArea);

						break;

					default:
						break;
				}
			}

		}
		private void pictureBoxMapArea_MouseUp(object sender, MouseEventArgs e)
		{

		}


		//マップエリアリスト
		private void pictureBoxMapAreaCopyList_Paint(object sender, PaintEventArgs e)
		{
			//マップエリアリスト描写
			for (int y = 0; y < 16; ++y)
			{
				for (int x = 0; x < 16; ++x)
				{
					e.Graphics.DrawImage(TomatoTool.MapArea.image[x + (y * 16)], x * 16, y * 16);
				}
			}

			//コピーされているマップエリアを枠で囲む
			e.Graphics.DrawRectangle(new Pen(Color.FromArgb(255, 0, 0), 1), (selectMapArea % 16) * 16, (selectMapArea / 16) * 16, 15, 15);
			e.Graphics.DrawRectangle(new Pen(Color.FromArgb(255, 0, 0), 1), ((selectMapArea % 16) * 16) + 1, ((selectMapArea / 16) * 16) + 1, 13, 13);

			pictureBoxMapAreaCopy.Refresh();
		}
		private void pictureBoxMapAreaCopyList_MouseMove(object sender, MouseEventArgs e)
		{
			if (((e.Button == MouseButtons.Left) || (e.Button == MouseButtons.Right)) &&
				((0 <= e.X) && (e.X < ((PictureBox)sender).Width)) &&
				((0 <= e.Y) && (e.Y < ((PictureBox)sender).Height)))
			{
				selectMapArea = (byte)((e.X / 16) + ((e.Y / 16) * 16));
				textBoxMapAreaCopyCode.Text = String.Format("{0:X2}", selectMapArea);
				pictureBoxMapAreaCopyList.Refresh();
			}
		}


		//マップエリアコピー
		private void pictureBoxMapAreaCopy_Paint(object sender, PaintEventArgs e)
		{
			e.Graphics.DrawImage(TomatoTool.MapArea.image[selectMapArea], 0, 0);
		}

		private void textBoxMapAreaCopyCode_TextChanged(object sender, EventArgs e)
		{
			if ((((TextBox)sender).Text != null) && (((TextBox)sender).Text != ""))
			{
				try
				{
					selectMapArea = Convert.ToByte(((TextBox)sender).Text, 16);
					pictureBoxMapAreaCopyList.Refresh();
					pictureBoxMapAreaCopy.Refresh();
				}
				catch
				{
					((TextBox)sender).Text = String.Format("{0:X2}", selectMapArea);
				}
			}
		}
	}
}
