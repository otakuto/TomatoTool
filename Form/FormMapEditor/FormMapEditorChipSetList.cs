using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;


namespace TomatoTool
{
	public partial class FormMapEditor : Form
	{
		#region コピーリスト

		private void radioButtonChipSetListBG_CheckedChanged(object sender, EventArgs e)
		{
			if (map.ChipSetList[radioButtonChipSetListBG()] != null)
			{
				listBoxChipSetList.DataSource = map.ChipSetList[radioButtonChipSetListBG()].ChipSet;
				listBoxChipSetList.SelectedItem = null;
			}
			else
			{
				listBoxChipSetList.DataSource = null;
				listBoxChipSetList.SelectedItem = null;
			}

			pictureBoxChipSetList.Refresh();
		}

		private void trackBarChipSetListCopyListPalette_Scroll(object sender, EventArgs e)
		{
			pictureBoxChipSetListCopyList.Refresh();
		}

		private int radioButtonChipSetListBG()
		{
			if (radioButtonChipSetListBG1.Checked)
			{
				return 0;
			}

			if (radioButtonChipSetListBG2.Checked)
			{
				return 1;
			}

			if (radioButtonChipSetListBG3.Checked)
			{
				return 2;
			}

			return -1;
		}

		private void checkBoxChipSetListFlip_CheckedChanged(object sender, EventArgs e)
		{
			pictureBoxChipSetListCopyList.Refresh();
		}

		//拡大してチップを表示
		private void pictureBoxChipSetListCopy_Paint(object sender, PaintEventArgs e)
		{
			//塗りつぶし透過させないため
			e.Graphics.FillRectangle(new SolidBrush(map.Palette[0][0].toColor()), 0, 0, ((PictureBox)sender).Width, ((PictureBox)sender).Height);

			//拡大描写の描写方法指定
			e.Graphics.InterpolationMode = InterpolationMode.NearestNeighbor;
			e.Graphics.PixelOffsetMode = PixelOffsetMode.Half;

			e.Graphics.DrawImage(map.MapTile.MainTile.toBitmap(selectChip, map.Palette), 0, 0, (Chip.WIDTH * 4), (Chip.HEIGHT * 4));
		}

		private void pictureBoxChipSetListCopyList_Paint(object sender, PaintEventArgs e)
		{
			//リサイズ
			((PictureBox)sender).Width = (Chip.WIDTH * 2) * 16;
			((PictureBox)sender).Height = (map.MapTile.MainTile.Count / (Chip.HEIGHT * 2)) * 16;

			Chip chip;
			//反転処理
			if (checkBoxChipSetListFlipX.Checked && checkBoxChipSetListFlipY.Checked)
			{
				chip = new Chip(0x0C00);
			}
			else if (checkBoxChipSetListFlipX.Checked)
			{
				chip = new Chip(0x0400);
			}
			else if (checkBoxChipSetListFlipY.Checked)
			{
				chip = new Chip(0x0800);
			}
			else
			{
				chip = new Chip(0x0000);
			}

			//拡大描写の描写方法指定
			e.Graphics.InterpolationMode = InterpolationMode.NearestNeighbor;
			e.Graphics.PixelOffsetMode = PixelOffsetMode.Half;

			//タイル描写
			for (int i = 0; i < map.MapTile.MainTile.Count; ++i)
			{
				using (Bitmap bitmap = map.MapTile.MainTile[i].toBitmap(map.Palette[trackBarChipSetListCopyListPalette.Value], chip.FlipX, chip.FlipY))
				{
					e.Graphics.DrawImage(bitmap, (i % 16) * (Chip.WIDTH * 2), (i / 16) * (Chip.HEIGHT * 2), (Chip.WIDTH * 2), (Chip.HEIGHT * 2));
				}
			}

			e.Graphics.InterpolationMode = InterpolationMode.Default;
			e.Graphics.PixelOffsetMode = PixelOffsetMode.Default;

			//グリッド描写
			if (toolStripMenuItemMapGrid.Checked)
			{
				gridDraw(sender, e.Graphics);
			}

			if (selectChip != null)
			{
				//選択されたマップチップを囲う
				e.Graphics.DrawRectangle(new Pen(Color.FromArgb(0, 0, 255), 1), (selectChip.TileNumber % 16) * (Chip.WIDTH * 2), (selectChip.TileNumber / 16) * (Chip.HEIGHT * 2), 15, 15);
				e.Graphics.DrawRectangle(new Pen(Color.FromArgb(0, 0, 255), 1), ((selectChip.TileNumber % 16) * (Chip.WIDTH * 2)) + 1, ((selectChip.TileNumber / 16) * (Chip.HEIGHT * 2)) + 1, 13, 13);
			}
		}
		private void pictureBoxChipSetListCopyList_MouseMove(object sender, MouseEventArgs e)
		{
			if ((e.Button == MouseButtons.Left) &&
				(0 <= e.X) && (e.X < ((PictureBox)sender).Width) &&
				(0 <= e.Y) && (e.Y < ((PictureBox)sender).Height) &&
				(0 <= (e.X / (Chip.WIDTH * 2)) + ((e.Y / (Chip.HEIGHT * 2)) * 16)) &&
				((e.X / (Chip.WIDTH * 2)) + ((e.Y / (Chip.HEIGHT * 2)) * 16) < map.MapTile.MainTile.Count))
			{
				selectChip.TileNumber = (ushort)((e.X / (Chip.WIDTH * 2)) + ((e.Y / (Chip.HEIGHT * 2)) * 16));
				selectChip.PaletteNumber = (byte)trackBarChipSetListCopyListPalette.Value;
				selectChip.FlipX = checkBoxChipSetListFlipX.Checked;
				selectChip.FlipY = checkBoxChipSetListFlipY.Checked;
			}

			pictureBoxChipSetListCopyList.Refresh();
			pictureBoxChipSetListCopy.Refresh();
		}

		#endregion

		private void listBoxChipSetList_SelectedIndexChanged(object sender, EventArgs e)
		{
			pictureBoxChipSetList.Refresh();
		}
		private void listBoxChipSetList_Format(object sender, ListControlConvertEventArgs e)
		{
			e.Value = String.Format("{0:X8}", ((ListBox)sender).Items.IndexOf(e.ListItem));
		}

		private void pictureBoxChipSetList_Paint(object sender, PaintEventArgs e)
		{
			int bg = radioButtonChipSetListBG();


			ChipSetList chipSetList = map.ChipSetList[bg];

			if (chipSetList != null)
			{
				//リサイズ
				((PictureBox)sender).Width = (ChipSet.WIDTH * 2) * 16;
				((PictureBox)sender).Height = (ChipSet.HEIGHT * 2) * ((chipSetList.Count / 16) + (((chipSetList.Count % 16) == 0) ? 0 : 1));

				e.Graphics.FillRectangle(new SolidBrush(map.Palette[0][0].toColor()), 0, 0, ((PictureBox)sender).Width, ((PictureBox)sender).Height - (ChipSet.HEIGHT * 2));
				e.Graphics.FillRectangle(new SolidBrush(map.Palette[0][0].toColor()), 0, ((PictureBox)sender).Height - (ChipSet.HEIGHT * 2), (chipSetList.Count % 16 == 0 ? 16 : chipSetList.Count % 16) * (ChipSet.WIDTH * 2), ((PictureBox)sender).Height);

				//拡大描写の描写方法指定
				e.Graphics.InterpolationMode = InterpolationMode.NearestNeighbor;
				e.Graphics.PixelOffsetMode = PixelOffsetMode.Half;

				for (int i = 0; i < chipSetList.Count; ++i)
				{
					using (Bitmap bitmap = map.MapTile.MainTile.toBitmap(chipSetList[i], map.Palette, true))
					{
						e.Graphics.DrawImage(bitmap, (i % 16) * (ChipSet.WIDTH * 2), (i / 16) * (ChipSet.HEIGHT * 2), (ChipSet.WIDTH * 2), (ChipSet.HEIGHT * 2));
					}
				}

				e.Graphics.InterpolationMode = InterpolationMode.Default;
				e.Graphics.PixelOffsetMode = PixelOffsetMode.Default;

				//グリッド描写
				if (toolStripMenuItemMapGrid.Checked)
				{
					gridDraw(sender, e.Graphics);
				}

				//拡大描写の描写方法指定
				e.Graphics.InterpolationMode = InterpolationMode.NearestNeighbor;
				e.Graphics.PixelOffsetMode = PixelOffsetMode.Half;

				//枠描写
				using (Pen pen = new Pen(TomatoTool.Map.SelectColor, 2))
				{
					e.Graphics.DrawRectangle(pen, ((listBoxChipSetList.SelectedIndex % 16) * (ChipSet.WIDTH * 2)) + 1, ((listBoxChipSetList.SelectedIndex / 16) * (ChipSet.HEIGHT * 2)) + 1, (ChipSet.WIDTH * 2) - pen.Width, (ChipSet.HEIGHT * 2) - pen.Width);
				}

				e.Graphics.InterpolationMode = InterpolationMode.Default;
				e.Graphics.PixelOffsetMode = PixelOffsetMode.Default;
			}
		}
		private void pictureBoxChipSetList_MouseMove(object sender, MouseEventArgs e)
		{
			int bg = radioButtonChipSetListBG();


			ChipSetList chipSetList = map.ChipSetList[bg];

			if (chipSetList != null)
			{
				if ((0 <= e.X) && (e.X < ((PictureBox)sender).Width) &&
					(0 <= e.Y) && (e.Y < ((PictureBox)sender).Height) &&
					(((e.X / 32) + ((e.Y / 32) * 16)) < chipSetList.Count))
				{

					if (e.Button == MouseButtons.Left)
					{
						chipSetList[(e.X / 32) + ((e.Y / 32) * 16)][(e.X / 16) % 2, (e.Y / 16) % 2] = (Chip)selectChip.Clone();
						chipSetList.Saved = false;
						map.updata();

						pictureBoxChipSetList.Refresh();
					}

					if (e.Button == MouseButtons.Right)
					{
						selectChip = (Chip)chipSetList[(e.X / 32) + ((e.Y / 32) * 16)][(e.X / 16) % 2, (e.Y / 16) % 2].Clone();
						checkBoxChipSetListFlipX.Checked = selectChip.FlipX;
						checkBoxChipSetListFlipY.Checked = selectChip.FlipY;
						trackBarChipSetListCopyListPalette.Value = selectChip.PaletteNumber;

						pictureBoxChipSetListCopy.Refresh();
						pictureBoxChipSetListCopyList.Refresh();
					}
				}
			}
		}

		private void contextMenuStripListBoxChipSetList_Opening(object sender, CancelEventArgs e)
		{

		}
		private void toolStripMenuItemContextMenuStripListBoxChipSetListAdd_Click(object sender, EventArgs e)
		{
			int bg = radioButtonChipSetListBG();
			ChipSetList chipSetList = map.ChipSetList[bg];

			chipSetList.ChipSet.Add(new ChipSet());

			listBoxChipSetList.DataSource = null;
			listBoxChipSetList.DataSource = chipSetList.ChipSet;

			listBoxChipSetList.SelectedIndex = listBoxChipSetList.Items.Count - 1;

			pictureBoxChipSetList.Refresh();
		}
		private void toolStripMenuItemContextMenuStripListBoxChipSetListInsert_Click(object sender, EventArgs e)
		{
			if (listBoxChipSetList.SelectedItem != null)
			{
				int bg = radioButtonChipSetListBG();
				ChipSetList chipSetList = map.ChipSetList[bg];

				int selectedIndex = listBoxChipSetList.SelectedIndex;
				chipSetList.ChipSet.Insert(selectedIndex, new ChipSet());

				listBoxChipSetList.DataSource = null;
				listBoxChipSetList.DataSource = chipSetList.ChipSet;

				listBoxChipSetList.SelectedIndex = selectedIndex;

				pictureBoxChipSetList.Refresh();
			}
		}
		private void toolStripMenuItemContextMenuStripListBoxChipSetListDelete_Click(object sender, EventArgs e)
		{
			if (listBoxChipSetList.SelectedItem != null)
			{
				switch (MessageBox.Show("削除しますか", FormTomatoTool.applicationName, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation))
				{
					case DialogResult.Yes:

						int bg = radioButtonChipSetListBG();
						ChipSetList chipSetList = map.ChipSetList[bg];

						int selectedIndex = listBoxChipSetList.SelectedIndex;
						chipSetList.ChipSet.RemoveAt(selectedIndex);

						listBoxChipSetList.DataSource = null;
						listBoxChipSetList.DataSource = chipSetList.ChipSet;

						pictureBoxChipSetList.Refresh();

						break;

					default:
						break;
				}
			}
		}
		private void toolStripMenuItemContextMenuStripListBoxChipSetListAllDelete_Click(object sender, EventArgs e)
		{
			switch (MessageBox.Show("全て削除しますか", FormTomatoTool.applicationName, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation))
			{
				case DialogResult.Yes:

					int bg = radioButtonChipSetListBG();
					ChipSetList chipSetList = map.ChipSetList[bg];

					chipSetList.ChipSet.Clear();

					listBoxChipSetList.DataSource = null;
					listBoxChipSetList.DataSource = chipSetList.ChipSet;

					pictureBoxChipSetList.Refresh();

					break;

				default:
					break;
			}
		}
		private void toolStripMenuItemContextMenuStripListBoxChipSetListNew_Click(object sender, EventArgs e)
		{

		}
	}
}
