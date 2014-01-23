using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace TomatoTool
{
	public partial class FormMapEditor : Form
	{
		private void comboBoxWarpScriptList_SelectedIndexChanged(object sender, EventArgs e)
		{
			map.WarpScriptList = (WarpScriptList)comboBoxWarpScriptList.SelectedItem;

			listBoxWarpScript.SelectedIndexChanged -= listBoxWarpScript_SelectedIndexChanged;
			listBoxWarpScript.DataSource = map.WarpScriptList;
			listBoxWarpScript.SelectedItem = null;
			listBoxWarpScript.SelectedIndexChanged += listBoxWarpScript_SelectedIndexChanged;
			
			componentWarpScriptClear();
			componentWarpScriptDisable();
			componentWarpScriptRefresh();
		}
		private void comboBoxWarpScriptList_Format(object sender, ListControlConvertEventArgs e)
		{
			e.Value = String.Format("{0:X8}", ((WarpScriptList)e.ListItem).ObjectID);
		}

		private void listBoxWarpScript_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (listBoxWarpScript.SelectedItem == null)
			{
				componentWarpScriptClear();
				componentWarpScriptDisable();
				componentWarpScriptRefresh();
			}
			else
			{
				componentWarpScriptEnable();
				componentWarpScriptLoad((WarpScript)listBoxWarpScript.SelectedItem);
				componentWarpScriptRefresh();
			}
		}
		private void listBoxWarpScript_Format(object sender, ListControlConvertEventArgs e)
		{
			e.Value = String.Format("{0:X8}", ((ListBox)sender).Items.IndexOf(e.ListItem));
		}

		private void pictureBoxWarpScript_Paint(object sender, PaintEventArgs e)
		{
			//リサイズ
			pictureBoxResize(sender, map);

			//マップチップナンバーリスト
			map.draw(e.Graphics, toolStripMenuItemMapChipBG[0].Checked, toolStripMenuItemMapChipBG[1].Checked, toolStripMenuItemMapChipBG[2].Checked);

			//マップエリアの描写
			if (toolStripMenuItemMapArea.Checked)
			{
				map.MapArea.draw(e.Graphics, toolStripMenuItemMapChipBG[0].Checked || toolStripMenuItemMapChipBG[1].Checked || toolStripMenuItemMapChipBG[2].Checked);
			}

			//グリッド描写
			if (toolStripMenuItemMapGrid.Checked)
			{
				gridDraw(sender, e.Graphics);
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

			//ワープスクリプトの描写
			map.WarpScriptList.draw(e.Graphics, listBoxWarpScript.SelectedIndex);

		}
		private void pictureBoxWarpScript_MouseDown(object sender, MouseEventArgs e)
		{
			pictureBoxWarpScript.Focus();

			if (listBoxWarpScript.Items != null)
			{
				if (((0 <= e.X) && (e.X < ((PictureBox)sender).Width)) &&
					((0 <= e.Y) && (e.Y < ((PictureBox)sender).Height)))
				{
					for (int i = 0; i < listBoxWarpScript.Items.Count; ++i)
					{
						WarpScript warpScript = (WarpScript)listBoxWarpScript.Items[i];

						if ((warpScript.BeginX <= (e.X / Map.BLOCK_WIDTH)) && ((e.X / Map.BLOCK_WIDTH) <= warpScript.EndX) &&
							(warpScript.BeginY <= (e.Y / Map.BLOCK_HEIGHT)) && ((e.Y / Map.BLOCK_HEIGHT) <= warpScript.EndY))
						{
							listBoxWarpScript.SelectedItem = listBoxWarpScript.Items[i];
							return;
						}
					}
					listBoxWarpScript.SelectedItem = null;
				}
			}
		}
		private void pictureBoxWarpScript_MouseMove(object sender, MouseEventArgs e)
		{
			if (listBoxWarpScript.SelectedItem != null)
			{
				WarpScript warpScript = (WarpScript)listBoxWarpScript.SelectedItem;

				if (((0 <= e.X) && (e.X < ((PictureBox)sender).Width)) &&
					((0 <= e.Y) && (e.Y < ((PictureBox)sender).Height)))
				{
					switch (e.Button)
					{
						case MouseButtons.Left:
							if ((Control.ModifierKeys & Keys.Shift) == Keys.Shift)
							{
								if ((warpScript.BeginX <= (e.X / Map.BLOCK_WIDTH)) && ((e.X / Map.BLOCK_WIDTH) < map.Width) &&
								(warpScript.BeginY <= (e.Y / Map.BLOCK_HEIGHT)) && ((e.Y / Map.BLOCK_HEIGHT) < map.Height))
								{
									warpScript.EndX = (byte)(e.X / Map.BLOCK_WIDTH);
									warpScript.EndY = (byte)(e.Y / Map.BLOCK_HEIGHT);
									componentWarpScriptLoad(warpScript);

									componentWarpScriptRefresh();
								}
							}
							else
							{
								if ((((e.X / Map.BLOCK_WIDTH) + (warpScript.EndX - warpScript.BeginX)) < map.Width) &&
									(((e.Y / Map.BLOCK_HEIGHT) + (warpScript.EndY - warpScript.BeginY)) < map.Height))
								{
									warpScript.move((byte)(e.X / Map.BLOCK_WIDTH), (byte)(e.Y / Map.BLOCK_HEIGHT));
									componentWarpScriptLoad(warpScript);

									componentWarpScriptRefresh();
								}
							}
							break;

						default:
							break;
					}
				}
			}
		}

		private void componentWarpScriptLoad(WarpScript warpScript)
		{
			if (warpScript != null)
			{
				textBoxWarpScriptBeginX.Text = warpScript.BeginX.ToString();
				textBoxWarpScriptBeginY.Text = warpScript.BeginY.ToString();
				textBoxWarpScriptEndX.Text = warpScript.EndX.ToString();
				textBoxWarpScriptEndY.Text = warpScript.EndY.ToString();

				textBoxWarpScriptMoveX.Text = warpScript.MoveX.ToString();
				textBoxWarpScriptMoveY.Text = warpScript.MoveY.ToString();
				textBoxWarpScriptAdjustX.Text = warpScript.AdjustX.ToString();
				textBoxWarpScriptAdjustY.Text = warpScript.AdjustY.ToString();

				textBoxWarpScriptNumber.Text = String.Format("{0:X4}", warpScript.Number);

				comboBoxWarpScriptMotion.SelectedIndexChanged -= comboBoxWarpScriptMotion_SelectedIndexChanged;
				comboBoxWarpScriptMotion.DataSource = Enum.GetNames(typeof(WarpScriptMotion));
				comboBoxWarpScriptMotion.SelectedItem = warpScript.Motion.ToString();
				comboBoxWarpScriptMotion.SelectedIndexChanged += comboBoxWarpScriptMotion_SelectedIndexChanged;
			}
		}
		private void componentWarpScriptClear()
		{
			textBoxWarpScriptBeginX.Text = null;
			textBoxWarpScriptBeginY.Text = null;
			textBoxWarpScriptEndX.Text = null;
			textBoxWarpScriptEndY.Text = null;
			textBoxWarpScriptAdjustX.Text = null;
			textBoxWarpScriptAdjustY.Text = null;

			textBoxWarpScriptMoveX.Text = null;
			textBoxWarpScriptMoveY.Text = null;
			textBoxWarpScriptNumber.Text = null;

			comboBoxWarpScriptMotion.DataSource = null;
		}
		private void componentWarpScriptEnable()
		{
			textBoxWarpScriptBeginX.Enabled = true;
			textBoxWarpScriptBeginY.Enabled = true;
			textBoxWarpScriptEndX.Enabled = true;
			textBoxWarpScriptEndY.Enabled = true;

			textBoxWarpScriptMoveX.Enabled = true;
			textBoxWarpScriptMoveY.Enabled = true;
			textBoxWarpScriptAdjustX.Enabled = true;
			textBoxWarpScriptAdjustY.Enabled = true;

			textBoxWarpScriptNumber.Enabled = true;
			comboBoxWarpScriptMotion.Enabled = true;
		}
		private void componentWarpScriptDisable()
		{
			textBoxWarpScriptBeginX.Enabled = false;
			textBoxWarpScriptBeginY.Enabled = false;
			textBoxWarpScriptEndX.Enabled = false;
			textBoxWarpScriptEndY.Enabled = false;

			textBoxWarpScriptMoveX.Enabled = false;
			textBoxWarpScriptMoveY.Enabled = false;
			textBoxWarpScriptAdjustX.Enabled = false;
			textBoxWarpScriptAdjustY.Enabled = false;

			textBoxWarpScriptNumber.Enabled = false;
			comboBoxWarpScriptMotion.Enabled = false;
		}
		private void componentWarpScriptRefresh()
		{
			pictureBoxWarpScript.Refresh();

			textBoxWarpScriptBeginX.Refresh();
			textBoxWarpScriptBeginY.Refresh();
			textBoxWarpScriptEndX.Refresh();
			textBoxWarpScriptEndY.Refresh();

			textBoxWarpScriptMoveX.Refresh();
			textBoxWarpScriptMoveY.Refresh();
			textBoxWarpScriptAdjustX.Refresh();
			textBoxWarpScriptAdjustY.Refresh();

			textBoxWarpScriptNumber.Refresh();
			comboBoxWarpScriptMotion.Refresh();
		}

		private void warpScriptDelete()
		{
			if (listBoxWarpScript.SelectedItem != null)
			{
				switch (MessageBox.Show("削除しますか", FormTomatoTool.applicationName, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation))
				{
					case DialogResult.Yes:
						((IList)map.WarpScriptList).RemoveAt(listBoxWarpScript.SelectedIndex);

						listBoxWarpScript.DataSource = null;
						listBoxWarpScript.DataSource = map.WarpScriptList;

						componentWarpScriptRefresh();

						break;

					default:
						break;
				}
			}
		}

		private void textBoxWarpScriptBeginX_Validating(object sender, CancelEventArgs e)
		{
			if (listBoxWarpScript.SelectedItem != null)
			{
				TextBox textBox = (TextBox)sender;
				WarpScript warpScript = (WarpScript)listBoxWarpScript.SelectedItem;

				try
				{
					warpScript.BeginX = Convert.ToByte(textBox.Text);
					pictureBoxWarpScript.Refresh();
				}
				catch
				{
					switch (MessageBox.Show("エラー", FormTomatoTool.applicationName, MessageBoxButtons.OKCancel, MessageBoxIcon.Error))
					{
						case DialogResult.Cancel:

							textBox.Text = warpScript.BeginX.ToString();

							break;

						default:
							break;
					}

					textBox.SelectAll();
					e.Cancel = true;
				}
			}
		}
		private void textBoxWarpScriptBeginY_Validating(object sender, CancelEventArgs e)
		{
			if (listBoxWarpScript.SelectedItem != null)
			{
				TextBox textBox = (TextBox)sender;
				WarpScript warpScript = (WarpScript)listBoxWarpScript.SelectedItem;

				try
				{
					warpScript.BeginY = Convert.ToByte(textBox.Text);
					pictureBoxWarpScript.Refresh();
				}
				catch
				{
					switch (MessageBox.Show("エラー", FormTomatoTool.applicationName, MessageBoxButtons.OKCancel, MessageBoxIcon.Error))
					{
						case DialogResult.Cancel:

							textBox.Text = warpScript.BeginY.ToString();

							break;

						default:
							break;
					}

					textBox.SelectAll();
					e.Cancel = true;
				}
			}
		}
		private void textBoxWarpScriptEndX_Validating(object sender, CancelEventArgs e)
		{
			if (listBoxWarpScript.SelectedItem != null)
			{
				TextBox textBox = (TextBox)sender;
				WarpScript warpScript = (WarpScript)listBoxWarpScript.SelectedItem;

				try
				{
					warpScript.EndX = Convert.ToByte(textBox.Text);
					pictureBoxWarpScript.Refresh();
				}
				catch
				{
					switch (MessageBox.Show("エラー", FormTomatoTool.applicationName, MessageBoxButtons.OKCancel, MessageBoxIcon.Error))
					{
						case DialogResult.Cancel:

							textBox.Text = warpScript.EndX.ToString();

							break;

						default:
							break;
					}

					textBox.SelectAll();
					e.Cancel = true;
				}
			}
		}
		private void textBoxWarpScriptEndY_Validating(object sender, CancelEventArgs e)
		{
			if (listBoxWarpScript.SelectedItem != null)
			{
				TextBox textBox = (TextBox)sender;
				WarpScript warpScript = (WarpScript)listBoxWarpScript.SelectedItem;

				try
				{
					warpScript.EndY = Convert.ToByte(textBox.Text);
					pictureBoxWarpScript.Refresh();
				}
				catch
				{
					switch (MessageBox.Show("エラー", FormTomatoTool.applicationName, MessageBoxButtons.OKCancel, MessageBoxIcon.Error))
					{
						case DialogResult.Cancel:

							textBox.Text = warpScript.EndY.ToString();

							break;

						default:
							break;
					}

					textBox.SelectAll();
					e.Cancel = true;
				}
			}
		}
		private void textBoxWarpScriptMoveX_Validating(object sender, CancelEventArgs e)
		{
			if (listBoxWarpScript.SelectedItem != null)
			{
				TextBox textBox = (TextBox)sender;
				WarpScript warpScript = (WarpScript)listBoxWarpScript.SelectedItem;

				try
				{
					warpScript.MoveX = Convert.ToByte(textBox.Text);
					pictureBoxWarpScript.Refresh();
				}
				catch
				{
					switch (MessageBox.Show("エラー", FormTomatoTool.applicationName, MessageBoxButtons.OKCancel, MessageBoxIcon.Error))
					{
						case DialogResult.Cancel:

							textBox.Text = warpScript.MoveX.ToString();

							break;

						default:
							break;
					}

					textBox.SelectAll();
					e.Cancel = true;
				}
			}
		}
		private void textBoxWarpScriptMoveY_Validating(object sender, CancelEventArgs e)
		{
			if (listBoxWarpScript.SelectedItem != null)
			{
				TextBox textBox = (TextBox)sender;
				WarpScript warpScript = (WarpScript)listBoxWarpScript.SelectedItem;

				try
				{
					warpScript.MoveY = Convert.ToByte(textBox.Text);
					pictureBoxWarpScript.Refresh();
				}
				catch
				{
					switch (MessageBox.Show("エラー", FormTomatoTool.applicationName, MessageBoxButtons.OKCancel, MessageBoxIcon.Error))
					{
						case DialogResult.Cancel:

							textBox.Text = warpScript.MoveY.ToString();

							break;

						default:
							break;
					}

					textBox.SelectAll();
					e.Cancel = true;
				}
			}
		}
		private void textBoxWarpScriptAdjustX_Validating(object sender, CancelEventArgs e)
		{
			if (listBoxWarpScript.SelectedItem != null)
			{
				TextBox textBox = (TextBox)sender;
				WarpScript warpScript = (WarpScript)listBoxWarpScript.SelectedItem;

				try
				{
					warpScript.AdjustX = Convert.ToByte(textBox.Text);
					pictureBoxWarpScript.Refresh();
				}
				catch
				{
					switch (MessageBox.Show("エラー", FormTomatoTool.applicationName, MessageBoxButtons.OKCancel, MessageBoxIcon.Error))
					{
						case DialogResult.Cancel:

							textBox.Text = warpScript.AdjustX.ToString();

							break;

						default:
							break;
					}

					textBox.SelectAll();
					e.Cancel = true;
				}
			}
		}
		private void textBoxWarpScriptAdjustY_Validating(object sender, CancelEventArgs e)
		{
			if (listBoxWarpScript.SelectedItem != null)
			{
				TextBox textBox = (TextBox)sender;
				WarpScript warpScript = (WarpScript)listBoxWarpScript.SelectedItem;

				try
				{
					warpScript.AdjustY = Convert.ToByte(textBox.Text);
					pictureBoxWarpScript.Refresh();
				}
				catch
				{
					switch (MessageBox.Show("エラー", FormTomatoTool.applicationName, MessageBoxButtons.OKCancel, MessageBoxIcon.Error))
					{
						case DialogResult.Cancel:

							textBox.Text = warpScript.AdjustY.ToString();

							break;

						default:
							break;
					}

					textBox.SelectAll();
					e.Cancel = true;
				}
			}
		}
		private void textBoxWarpScriptNumber_Validating(object sender, CancelEventArgs e)
		{
			if (listBoxWarpScript.SelectedItem != null)
			{
				TextBox textBox = (TextBox)sender;
				WarpScript warpScript = (WarpScript)listBoxWarpScript.SelectedItem;

				try
				{
					warpScript.Number = Convert.ToByte(textBox.Text);
					pictureBoxWarpScript.Refresh();
				}
				catch
				{
					switch (MessageBox.Show("エラー", FormTomatoTool.applicationName, MessageBoxButtons.OKCancel, MessageBoxIcon.Error))
					{
						case DialogResult.Cancel:

							textBox.Text = warpScript.Number.ToString();

							break;

						default:
							break;
					}

					textBox.SelectAll();
					e.Cancel = true;
				}
			}
		}

		private void textBoxWarpScriptBeginX_KeyDown(object sender, KeyEventArgs e)
		{
			if (listBoxWarpScript.SelectedItem != null)
			{
				TextBox textBox = (TextBox)sender;
				WarpScript warpScript = (WarpScript)listBoxWarpScript.SelectedItem;

				switch (e.KeyCode)
				{
					case Keys.Enter:
						tabControlMap.Focus();
						break;

					case Keys.Escape:
						textBox.Text = warpScript.BeginX.ToString();
						tabControlMap.Focus();
						break;

					default:
						break;
				}
			}
		}
		private void textBoxWarpScriptBeginY_KeyDown(object sender, KeyEventArgs e)
		{
			if (listBoxWarpScript.SelectedItem != null)
			{
				TextBox textBox = (TextBox)sender;
				WarpScript warpScript = (WarpScript)listBoxWarpScript.SelectedItem;

				switch (e.KeyCode)
				{
					case Keys.Enter:
						tabControlMap.Focus();
						break;

					case Keys.Escape:
						textBox.Text = warpScript.BeginY.ToString();
						tabControlMap.Focus();
						break;

					default:
						break;
				}
			}
		}
		private void textBoxWarpScriptEndX_KeyDown(object sender, KeyEventArgs e)
		{
			if (listBoxWarpScript.SelectedItem != null)
			{
				TextBox textBox = (TextBox)sender;
				WarpScript warpScript = (WarpScript)listBoxWarpScript.SelectedItem;

				switch (e.KeyCode)
				{
					case Keys.Enter:
						tabControlMap.Focus();
						break;

					case Keys.Escape:
						textBox.Text = warpScript.EndX.ToString();
						tabControlMap.Focus();
						break;

					default:
						break;
				}
			}
		}
		private void textBoxWarpScriptEndY_KeyDown(object sender, KeyEventArgs e)
		{
			if (listBoxWarpScript.SelectedItem != null)
			{
				TextBox textBox = (TextBox)sender;
				WarpScript warpScript = (WarpScript)listBoxWarpScript.SelectedItem;

				switch (e.KeyCode)
				{
					case Keys.Enter:
						tabControlMap.Focus();
						break;

					case Keys.Escape:
						textBox.Text = warpScript.EndY.ToString();
						tabControlMap.Focus();
						break;

					default:
						break;
				}
			}
		}
		private void textBoxWarpScriptMoveX_KeyDown(object sender, KeyEventArgs e)
		{
			if (listBoxWarpScript.SelectedItem != null)
			{
				TextBox textBox = (TextBox)sender;
				WarpScript warpScript = (WarpScript)listBoxWarpScript.SelectedItem;

				switch (e.KeyCode)
				{
					case Keys.Enter:
						tabControlMap.Focus();
						break;

					case Keys.Escape:
						textBox.Text = warpScript.MoveX.ToString();
						tabControlMap.Focus();
						break;

					default:
						break;
				}
			}
		}
		private void textBoxWarpScriptMoveY_KeyDown(object sender, KeyEventArgs e)
		{
			if (listBoxWarpScript.SelectedItem != null)
			{
				TextBox textBox = (TextBox)sender;
				WarpScript warpScript = (WarpScript)listBoxWarpScript.SelectedItem;

				switch (e.KeyCode)
				{
					case Keys.Enter:
						tabControlMap.Focus();
						break;

					case Keys.Escape:
						textBox.Text = warpScript.MoveY.ToString();
						tabControlMap.Focus();
						break;

					default:
						break;
				}
			}
		}
		private void textBoxWarpScriptAdjustX_KeyDown(object sender, KeyEventArgs e)
		{
			if (listBoxWarpScript.SelectedItem != null)
			{
				TextBox textBox = (TextBox)sender;
				WarpScript warpScript = (WarpScript)listBoxWarpScript.SelectedItem;

				switch (e.KeyCode)
				{
					case Keys.Enter:
						tabControlMap.Focus();
						break;

					case Keys.Escape:
						textBox.Text = warpScript.AdjustX.ToString();
						tabControlMap.Focus();
						break;

					default:
						break;
				}
			}
		}
		private void textBoxWarpScriptAdjustY_KeyDown(object sender, KeyEventArgs e)
		{
			if (listBoxWarpScript.SelectedItem != null)
			{
				TextBox textBox = (TextBox)sender;
				WarpScript warpScript = (WarpScript)listBoxWarpScript.SelectedItem;

				switch (e.KeyCode)
				{
					case Keys.Enter:
						tabControlMap.Focus();
						break;

					case Keys.Escape:
						textBox.Text = warpScript.AdjustY.ToString();
						tabControlMap.Focus();
						break;

					default:
						break;
				}
			}
		}
		private void textBoxWarpScriptNumber_KeyDown(object sender, KeyEventArgs e)
		{
			if (listBoxWarpScript.SelectedItem != null)
			{
				TextBox textBox = (TextBox)sender;
				WarpScript warpScript = (WarpScript)listBoxWarpScript.SelectedItem;

				switch (e.KeyCode)
				{
					case Keys.Enter:
						tabControlMap.Focus();
						break;

					case Keys.Escape:
						textBox.Text = warpScript.Number.ToString();
						tabControlMap.Focus();
						break;

					default:
						break;
				}
			}
		}

		private void comboBoxWarpScriptMotion_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (comboBoxWarpScriptMotion.SelectedItem != null)
			{
				((WarpScript)listBoxWarpScript.SelectedItem).Motion = (WarpScriptMotion)Enum.Parse(typeof(WarpScriptMotion), (string)comboBoxWarpScriptMotion.SelectedItem);
			}
		}

		private void contextMenuStripListBoxWarpScript_Opening(object sender, CancelEventArgs e)
		{
			if (((IList)map.WarpScriptList).Count == 0)
			{
				toolStripMenuItemListBoxWarpScriptAllDelete.Enabled = false;
			}
			else
			{
				toolStripMenuItemListBoxWarpScriptAllDelete.Enabled = true;
			}

			if (listBoxWarpScript.SelectedItem != null)
			{
				toolStripMenuItemListBoxWarpScriptInsert.Enabled = true;
				toolStripMenuItemListBoxWarpScriptDelete.Enabled = true;
			}
			else
			{
				toolStripMenuItemListBoxWarpScriptInsert.Enabled = false;
				toolStripMenuItemListBoxWarpScriptDelete.Enabled = false;
			}
		}
		private void toolStripMenuItemListBoxWarpScriptAdd_Click(object sender, EventArgs e)
		{
			((IList)map.WarpScriptList).Add(new WarpScript());

			listBoxWarpScript.DataSource = null;
			listBoxWarpScript.DataSource = map.WarpScriptList;

			listBoxWarpScript.SelectedIndex = listBoxWarpScript.Items.Count - 1;

			componentWarpScriptRefresh();
		}
		private void toolStripMenuItemListBoxWarpScriptInsert_Click(object sender, EventArgs e)
		{
			if (listBoxWarpScript.SelectedItem != null)
			{
				((IList)map.WarpScriptList).Insert(listBoxWarpScript.SelectedIndex, new WarpScript());

				listBoxWarpScript.DataSource = null;
				listBoxWarpScript.DataSource = map.WarpScriptList;

				componentWarpScriptRefresh();
			}
		}
		private void toolStripMenuItemListBoxWarpScriptDelete_Click(object sender, EventArgs e)
		{
			warpScriptDelete();
		}
		private void toolStripMenuItemListBoxWarpScriptAllDelete_Click(object sender, EventArgs e)
		{
			switch (MessageBox.Show("全て削除しますか", FormTomatoTool.applicationName, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation))
			{
				case DialogResult.Yes:
					((IList)map.WarpScriptList).Clear();

					listBoxWarpScript.DataSource = null;
					listBoxWarpScript.DataSource = map.WarpScriptList;

					componentWarpScriptRefresh();

					break;

				default:
					break;
			}
		}

		private void contextMenuStripPictureBoxWarpScript_Opening(object sender, CancelEventArgs e)
		{
			if (listBoxWarpScript.SelectedItem != null)
			{
				toolStripMenuItemPictureBoxWarpScriptDelete.Enabled = true;
			}
			else
			{
				toolStripMenuItemPictureBoxWarpScriptDelete.Enabled = false;
			}
		}
		private void toolStripMenuItemPictureBoxWarpScriptAdd_Click(object sender, EventArgs e)
		{
			Point point = contextMenuStripPictureBoxWarpScript.PointToClient(new Point(0, 0));
			point.X = -point.X;
			point.Y = -point.Y;
			point = contextMenuStripPictureBoxWarpScript.SourceControl.PointToClient(point);
			
			WarpScript warpScript = new WarpScript();
			warpScript.EndX = (byte)(point.X / Map.BLOCK_WIDTH);
			warpScript.BeginX = (byte)(point.X / Map.BLOCK_WIDTH);
			warpScript.EndY = (byte)(point.Y / Map.BLOCK_HEIGHT);
			warpScript.BeginY = (byte)(point.Y / Map.BLOCK_HEIGHT);

			((IList)map.WarpScriptList).Add(warpScript);

			listBoxWarpScript.DataSource = null;
			listBoxWarpScript.DataSource = map.WarpScriptList;

			listBoxWarpScript.SelectedIndex = listBoxWarpScript.Items.Count - 1;

			pictureBoxWarpScript.Refresh();
		}
		private void toolStripMenuItemPictureBoxWarpScriptDelete_Click(object sender, EventArgs e)
		{
			warpScriptDelete();
		}

		private void toolStripMenuItemComboBoxWarpScriptListAdd_Click(object sender, EventArgs e)
		{

		}
		private void toolStripMenuItemComboBoxWarpScriptListDelete_Click(object sender, EventArgs e)
		{

		}
		private void toolStripMenuItemComboBoxWarpScriptListAllDelete_Click(object sender, EventArgs e)
		{

		}
	}
}
