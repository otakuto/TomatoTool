using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace TomatoTool
{
	public partial class FormMapEditor : Form
	{
		private void comboBoxMapScriptList_SelectedIndexChanged(object sender, EventArgs e)
		{
			map.MapScriptList = (MapScriptList)comboBoxMapScriptList.SelectedItem;
			listBoxMapScript.DataSource = map.MapScriptList;
			listBoxMapScript.SelectedItem = null;

			componentMapScriptClear();
			componentMapScriptDisable();
			pictureBoxMapScript.Refresh();

			comboBoxMapScriptListMainScript.SelectedIndexChanged -= comboBoxMapScriptListMainScript_SelectedIndexChanged;
			comboBoxMapScriptListMainScript.DataSource = tomatoAdventure.ObjectDictionary[typeof(Script)];
			comboBoxMapScriptListMainScript.SelectedIndexChanged += comboBoxMapScriptListMainScript_SelectedIndexChanged;
			comboBoxMapScriptListMainScript.SelectedItem = map.MapScriptList;
		}
		private void comboBoxMapScriptList_Format(object sender, ListControlConvertEventArgs e)
		{
			e.Value = String.Format("{0:X8}", ((MapScriptList)e.ListItem).ObjectID);
		}

		private void listBoxMapScript_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (listBoxMapScript.SelectedItem == null)
			{
				componentMapScriptClear();
				componentMapScriptDisable();
				pictureBoxMapScript.Refresh();
			}
			else
			{
				componentMapScriptEnable();
				componentMapScriptLoad((MapScript)listBoxMapScript.SelectedItem);
				pictureBoxMapScript.Refresh();
			}
		}
		private void listBoxMapScript_Format(object sender, ListControlConvertEventArgs e)
		{
			e.Value = String.Format("{0:X8}", ((ListBox)sender).Items.IndexOf(e.ListItem));
		}

		private void pictureBoxMapScript_Paint(object sender, PaintEventArgs e)
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

			//ワープスクリプト描写
			if (toolStripMenuItemViewWarpScript.Checked)
			{
				map.WarpScriptList.draw(e.Graphics);
			}

			//キャラクタースクリプト描写
			if (toolStripMenuItemCharacterScript.Checked)
			{
				map.CharacterScriptList.draw(e.Graphics);
			}

			//マップスクリプト描写
			map.MapScriptList.draw(e.Graphics, listBoxMapScript.SelectedIndex);

		}
		private void pictureBoxMapScript_MouseDown(object sender, MouseEventArgs e)
		{
			pictureBoxMapScript.Focus();

			if (listBoxMapScript.Items != null)
			{
				if (((0 <= e.X) && (e.X < ((PictureBox)sender).Width)) &&
					((0 <= e.Y) && (e.Y < ((PictureBox)sender).Height)))
				{
					for (int i = 0; i < listBoxMapScript.Items.Count; ++i)
					{
						MapScript mapScript = (MapScript)listBoxMapScript.Items[i];

						if ((mapScript.BeginX <= (e.X / Map.BLOCK_WIDTH)) && ((e.X / Map.BLOCK_WIDTH) <= mapScript.EndX) &&
							(mapScript.BeginY <= (e.Y / Map.BLOCK_HEIGHT)) && ((e.Y / Map.BLOCK_HEIGHT) <= mapScript.EndY))
						{
							listBoxMapScript.SelectedItem = listBoxMapScript.Items[i];
							return;
						}
					}
					listBoxMapScript.SelectedItem = null;
				}
			}
		}
		private void pictureBoxMapScript_MouseMove(object sender, MouseEventArgs e)
		{
			if (listBoxMapScript.SelectedItem != null)
			{
				MapScript mapScript = (MapScript)listBoxMapScript.SelectedItem;

				if (((0 <= e.X) && (e.X < ((PictureBox)sender).Width)) &&
					((0 <= e.Y) && (e.Y < ((PictureBox)sender).Height)))
				{
					switch (e.Button)
					{
						case MouseButtons.Left:
							if ((Control.ModifierKeys & Keys.Shift) == Keys.Shift)
							{
								if ((mapScript.BeginX <= (e.X / Map.BLOCK_WIDTH)) && ((e.X / Map.BLOCK_WIDTH) < map.Width) &&
								(mapScript.BeginY <= (e.Y / Map.BLOCK_HEIGHT)) && ((e.Y / Map.BLOCK_HEIGHT) < map.Height))
								{
									mapScript.EndX = (byte)(e.X / Map.BLOCK_WIDTH);
									mapScript.EndY = (byte)(e.Y / Map.BLOCK_HEIGHT);
									pictureBoxMapScript.Refresh();

									componentMapScriptLoad(mapScript);
									textBoxMapScriptBeginX.Refresh();
									textBoxMapScriptBeginY.Refresh();
									textBoxMapScriptEndX.Refresh();
									textBoxMapScriptEndY.Refresh();

									textBoxMapScriptScriptAddress.Refresh();
								}
							}
							else
							{
								if ((((e.X / Map.BLOCK_WIDTH) + (mapScript.EndX - mapScript.BeginX)) < map.Width) &&
									(((e.Y / Map.BLOCK_HEIGHT) + (mapScript.EndY - mapScript.BeginY)) < map.Height))
								{
									mapScript.move((byte)(e.X / Map.BLOCK_WIDTH), (byte)(e.Y / Map.BLOCK_HEIGHT));
									pictureBoxMapScript.Refresh();

									componentMapScriptLoad(mapScript);
									textBoxMapScriptBeginX.Refresh();
									textBoxMapScriptBeginY.Refresh();
									textBoxMapScriptEndX.Refresh();
									textBoxMapScriptEndY.Refresh();

									textBoxMapScriptScriptAddress.Refresh();
								}
							}
							break;

						default:
							break;
					}
				}
			}
		}

		private void componentMapScriptLoad(MapScript mapScript)
		{
			if (mapScript != null)
			{
				textBoxMapScriptBeginX.Text = mapScript.BeginX.ToString();
				textBoxMapScriptBeginY.Text = mapScript.BeginY.ToString();
				textBoxMapScriptEndX.Text = mapScript.EndX.ToString();
				textBoxMapScriptEndY.Text = mapScript.EndY.ToString();

				textBoxMapScriptScriptAddress.Text = String.Format("{0:X8}", mapScript.Script.ObjectID);

				checkBoxMapScriptHasTrigger.Checked = mapScript.HasTrigger;
			}
		}
		private void componentMapScriptClear()
		{
			textBoxMapScriptBeginX.Text = null;
			textBoxMapScriptBeginY.Text = null;

			textBoxMapScriptEndX.Text = null;
			textBoxMapScriptEndY.Text = null;

			textBoxMapScriptScriptAddress.Text = null;

			checkBoxMapScriptHasTrigger.Checked = false;
		}
		private void componentMapScriptEnable()
		{
			textBoxMapScriptBeginX.Enabled = true;
			textBoxMapScriptBeginY.Enabled = true;
			textBoxMapScriptEndX.Enabled = true;
			textBoxMapScriptEndY.Enabled = true;

			textBoxMapScriptScriptAddress.Enabled = true;

			checkBoxMapScriptHasTrigger.Enabled = true;
		}
		private void componentMapScriptDisable()
		{
			textBoxMapScriptBeginX.Enabled = false;
			textBoxMapScriptBeginY.Enabled = false;
			textBoxMapScriptEndX.Enabled = false;
			textBoxMapScriptEndY.Enabled = false;

			textBoxMapScriptScriptAddress.Enabled = false;

			checkBoxMapScriptHasTrigger.Enabled = false;
		}
		private void mapScriptDelete()
		{
			if (listBoxMapScript.SelectedItem != null)
			{
				switch (MessageBox.Show("削除しますか", FormTomatoTool.applicationName, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation))
				{
					case DialogResult.Yes:
						((IList)map.MapScriptList).RemoveAt(listBoxMapScript.SelectedIndex);

						listBoxMapScript.DataSource = null;
						listBoxMapScript.DataSource = map.MapScriptList;

						pictureBoxMapScript.Refresh();

						break;

					default:
						break;
				}
			}
		}

		private void comboBoxMapScriptListMainScript_SelectedIndexChanged(object sender, EventArgs e)
		{
			map.MapScriptList.MainScript = (Script)comboBoxMapScriptListMainScript.SelectedItem;
		}
		private void comboBoxMapScriptListMainScript_Format(object sender, ListControlConvertEventArgs e)
		{
			e.Value = String.Format("{0:X8}", ((Script)e.ListItem).ObjectID);
		}

		private void textBoxMapScriptScriptAddress_TextChanged(object sender, EventArgs e)
		{
			if ((listBoxMapScript.SelectedItem != null) && (((TextBox)sender).Text != null) && (((TextBox)sender).Text != ""))
			{
				try
				{
					((MapScript)listBoxMapScript.SelectedItem).Script.ObjectID = Convert.ToUInt32(((TextBox)sender).Text, 16);
				}
				catch
				{
					((TextBox)sender).Text = String.Format("{0:X8}", ((MapScript)listBoxMapScript.SelectedItem).Script.ObjectID);
				}
			}
		}

		private void checkBoxMapScriptHasTrigger_CheckedChanged(object sender, EventArgs e)
		{
			if (listBoxMapScript.SelectedItem != null)
			{
				((MapScript)listBoxMapScript.SelectedItem).HasTrigger = ((CheckBox)sender).Checked;
			}
		}
		
		private void textBoxMapScriptBeginX_Validating(object sender, CancelEventArgs e)
		{
			if (listBoxMapScript.SelectedItem != null)
			{
				TextBox textBox = (TextBox)sender;
				MapScript mapScript = (MapScript)listBoxMapScript.SelectedItem;

				try
				{
					mapScript.BeginX = Convert.ToByte(textBox.Text);
					pictureBoxMapScript.Refresh();
				}
				catch
				{
					switch (MessageBox.Show("エラー", FormTomatoTool.applicationName, MessageBoxButtons.OKCancel, MessageBoxIcon.Error))
					{
						case DialogResult.Cancel:

							textBox.Text = mapScript.BeginX.ToString();

							break;

						default:
							break;
					}

					textBox.SelectAll();
					e.Cancel = true;
				}
			}
		}
		private void textBoxMapScriptBeginY_Validating(object sender, CancelEventArgs e)
		{
			if (listBoxMapScript.SelectedItem != null)
			{
				TextBox textBox = (TextBox)sender;
				MapScript mapScript = (MapScript)listBoxMapScript.SelectedItem;

				try
				{
					mapScript.BeginY = Convert.ToByte(textBox.Text);
					pictureBoxMapScript.Refresh();
				}
				catch
				{
					switch (MessageBox.Show("エラー", FormTomatoTool.applicationName, MessageBoxButtons.OKCancel, MessageBoxIcon.Error))
					{
						case DialogResult.Cancel:

							textBox.Text = mapScript.BeginY.ToString();

							break;

						default:
							break;
					}

					textBox.SelectAll();
					e.Cancel = true;
				}
			}
		}
		private void textBoxMapScriptEndX_Validating(object sender, CancelEventArgs e)
		{
			if (listBoxMapScript.SelectedItem != null)
			{
				TextBox textBox = (TextBox)sender;
				MapScript mapScript = (MapScript)listBoxMapScript.SelectedItem;

				try
				{
					mapScript.EndX = Convert.ToByte(textBox.Text);
					pictureBoxMapScript.Refresh();
				}
				catch
				{
					switch (MessageBox.Show("エラー", FormTomatoTool.applicationName, MessageBoxButtons.OKCancel, MessageBoxIcon.Error))
					{
						case DialogResult.Cancel:

							textBox.Text = mapScript.EndX.ToString();

							break;

						default:
							break;
					}

					textBox.SelectAll();
					e.Cancel = true;
				}
			}
		}
		private void textBoxMapScriptEndY_Validating(object sender, CancelEventArgs e)
		{
			if (listBoxMapScript.SelectedItem != null)
			{
				TextBox textBox = (TextBox)sender;
				MapScript mapScript = (MapScript)listBoxMapScript.SelectedItem;

				try
				{
					mapScript.EndY = Convert.ToByte(textBox.Text);
					pictureBoxMapScript.Refresh();
				}
				catch
				{
					switch (MessageBox.Show("エラー", FormTomatoTool.applicationName, MessageBoxButtons.OKCancel, MessageBoxIcon.Error))
					{
						case DialogResult.Cancel:

							textBox.Text = mapScript.EndY.ToString();

							break;

						default:
							break;
					}

					textBox.SelectAll();
					e.Cancel = true;
				}
			}
		}

		private void textBoxMapScriptBeginX_KeyDown(object sender, KeyEventArgs e)
		{
			if (listBoxMapScript.SelectedItem != null)
			{
				TextBox textBox = (TextBox)sender;
				MapScript mapScript = (MapScript)listBoxMapScript.SelectedItem;

				switch (e.KeyCode)
				{
					case Keys.Enter:
						tabControlMap.Focus();
						break;

					case Keys.Escape:
						textBox.Text = mapScript.BeginX.ToString();
						tabControlMap.Focus();
						break;

					default:
						break;
				}
			}
		}
		private void textBoxMapScriptBeginY_KeyDown(object sender, KeyEventArgs e)
		{
			if (listBoxMapScript.SelectedItem != null)
			{
				TextBox textBox = (TextBox)sender;
				MapScript mapScript = (MapScript)listBoxMapScript.SelectedItem;

				switch (e.KeyCode)
				{
					case Keys.Enter:
						tabControlMap.Focus();
						break;

					case Keys.Escape:
						textBox.Text = mapScript.BeginY.ToString();
						tabControlMap.Focus();
						break;

					default:
						break;
				}
			}
		}
		private void textBoxMapScriptEndX_KeyDown(object sender, KeyEventArgs e)
		{
			if (listBoxMapScript.SelectedItem != null)
			{
				TextBox textBox = (TextBox)sender;
				MapScript mapScript = (MapScript)listBoxMapScript.SelectedItem;

				switch (e.KeyCode)
				{
					case Keys.Enter:
						tabControlMap.Focus();
						break;

					case Keys.Escape:
						textBox.Text = mapScript.EndX.ToString();
						tabControlMap.Focus();
						break;

					default:
						break;
				}
			}
		}
		private void textBoxMapScriptEndY_KeyDown(object sender, KeyEventArgs e)
		{
			if (listBoxMapScript.SelectedItem != null)
			{
				TextBox textBox = (TextBox)sender;
				MapScript mapScript = (MapScript)listBoxMapScript.SelectedItem;

				switch (e.KeyCode)
				{
					case Keys.Enter:
						tabControlMap.Focus();
						break;

					case Keys.Escape:
						textBox.Text = mapScript.EndY.ToString();
						tabControlMap.Focus();
						break;

					default:
						break;
				}
			}
		}

		private void contextMenuStripListBoxMapScript_Opening(object sender, CancelEventArgs e)
		{
			if (((IList)map.MapScriptList).Count == 0)
			{
				toolStripMenuItemListBoxMapScriptAllDelete.Enabled = false;
			}
			else
			{
				toolStripMenuItemListBoxMapScriptAllDelete.Enabled = true;
			}

			if (listBoxMapScript.SelectedItem != null)
			{
				toolStripMenuItemListBoxMapScriptInsert.Enabled = true;
				toolStripMenuItemListBoxMapScriptDelete.Enabled = true;
			}
			else
			{
				toolStripMenuItemListBoxMapScriptInsert.Enabled = false;
				toolStripMenuItemListBoxMapScriptDelete.Enabled = false;
			}
		}
		private void toolStripMenuItemListBoxMapScriptAdd_Click(object sender, EventArgs e)
		{
			((IList)map.MapScriptList).Add(new MapScript());

			listBoxMapScript.DataSource = null;
			listBoxMapScript.DataSource = map.MapScriptList;

			listBoxMapScript.SelectedIndex = listBoxMapScript.Items.Count - 1;

			pictureBoxMapScript.Refresh();
		}
		private void toolStripMenuItemListBoxMapScriptInsert_Click(object sender, EventArgs e)
		{
			if (listBoxMapScript.SelectedItem != null)
			{
				((IList)map.MapScriptList).Insert(listBoxMapScript.SelectedIndex, new MapScript());

				listBoxMapScript.DataSource = null;
				listBoxMapScript.DataSource = map.MapScriptList;

				pictureBoxMapScript.Refresh();
			}
		}
		private void toolStripMenuItemListBoxMapScriptDelete_Click(object sender, EventArgs e)
		{
			mapScriptDelete();
		}
		private void toolStripMenuItemListBoxMapScriptAllDelete_Click(object sender, EventArgs e)
		{
			switch (MessageBox.Show("全て削除しますか", FormTomatoTool.applicationName, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation))
			{
				case DialogResult.Yes:
					((IList)map.MapScriptList).Clear();

					listBoxMapScript.DataSource = null;
					listBoxMapScript.DataSource = map.MapScriptList;

					pictureBoxMapScript.Refresh();

					break;

				default:
					break;
			}
		}

		private void contextMenuStripPictureBoxMapScript_Opening(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if (listBoxMapScript.SelectedItem != null)
			{
				toolStripMenuItemPictureBoxMapScriptDelete.Enabled = true;
			}
			else
			{
				toolStripMenuItemPictureBoxMapScriptDelete.Enabled = false;
			}
		}
		private void toolStripMenuItemPictureBoxMapScriptAdd_Click(object sender, EventArgs e)
		{
			Point point = contextMenuStripPictureBoxMapScript.PointToClient(new Point(0, 0));
			point.X = -point.X;
			point.Y = -point.Y;
			point = contextMenuStripPictureBoxMapScript.SourceControl.PointToClient(point);

			MapScript mapScript = new MapScript();
			mapScript.EndX = (byte)(point.X / Map.BLOCK_WIDTH);
			mapScript.BeginX = (byte)(point.X / Map.BLOCK_WIDTH);
			mapScript.EndY = (byte)(point.Y / Map.BLOCK_HEIGHT);
			mapScript.BeginY = (byte)(point.Y / Map.BLOCK_HEIGHT);

			((IList)map.MapScriptList).Add(mapScript);

			listBoxMapScript.DataSource = null;
			listBoxMapScript.DataSource = map.MapScriptList;

			listBoxMapScript.SelectedIndex = listBoxMapScript.Items.Count - 1;

			pictureBoxMapScript.Refresh();
		}
		private void toolStripMenuItemPictureBoxMapScriptDelete_Click(object sender, EventArgs e)
		{
			mapScriptDelete();
		}
		
	}
}
