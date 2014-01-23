using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace TomatoTool
{
	public partial class FormMapEditor
	{
		private void listBoxCharacterScript_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (listBoxCharacterScript.SelectedItem == null)
			{
				componentCharacterScriptClear();
				componentCharacterScriptDisable();
				componentCharacterScriptRefresh();
			}
			else
			{
				componentCharacterScriptEnable();
				componentCharacterScriptLoad((CharacterScript)listBoxCharacterScript.SelectedItem);
				componentCharacterScriptRefresh();
			}
		}
		private void listBoxCharacterScript_Format(object sender, ListControlConvertEventArgs e)
		{
			e.Value = String.Format("{0:X8}", ((ListBox)sender).Items.IndexOf(e.ListItem));
		}

		private void pictureBoxCharacterScript_Paint(object sender, PaintEventArgs e)
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

			//マップスクリプト描写
			if (toolStripMenuItemMapScript.Checked)
			{
				map.MapScriptList.draw(e.Graphics);
			}

			//キャラクタースクリプト描写
			map.CharacterScriptList.draw(e.Graphics, listBoxCharacterScript.SelectedIndex);
		}
		private void pictureBoxCharacterScript_MouseDown(object sender, MouseEventArgs e)
		{
			if (listBoxCharacterScript.Items != null)
			{
				switch (e.Button)
				{
					case MouseButtons.Left:
						for (int i = 0; i < listBoxCharacterScript.Items.Count; ++i)
						{
							CharacterScript characterScript = (CharacterScript)listBoxCharacterScript.Items[i];

							if (((e.X / Map.BLOCK_WIDTH) == characterScript.X) &&
								((e.Y / Map.BLOCK_HEIGHT) == characterScript.Y))
							{
								listBoxCharacterScript.SelectedItem = characterScript;
								return;
							}
						}
						listBoxCharacterScript.SelectedItem = null;
						break;

					default:
						break;
				}
			}
		}
		private void pictureBoxCharacterScript_MouseMove(object sender, MouseEventArgs e)
		{
			if (listBoxCharacterScript.SelectedItem != null)
			{
				switch (e.Button)
				{
					case MouseButtons.Left:
						if (((0 <= e.X) && (e.X < ((PictureBox)sender).Width)) &&
							((0 <= e.Y) && (e.Y < ((PictureBox)sender).Height)))
						{
							CharacterScript characterScript = (CharacterScript)listBoxCharacterScript.SelectedItem;

							characterScript.X = (byte)(e.X / Map.BLOCK_WIDTH);
							characterScript.Y = (byte)(e.Y / Map.BLOCK_HEIGHT);
							componentCharacterScriptLoad(characterScript);

							componentCharacterScriptRefresh();
						}
						break;

					default:
						break;
				}
			}
		}

		private void componentCharacterScriptLoad(CharacterScript characterScript)
		{
			textBoxCharacterScriptX.Text = characterScript.X.ToString();
			textBoxCharacterScriptY.Text = characterScript.Y.ToString();
			textBoxCharacterScriptTypeID.Text = String.Format("{0:X2}", characterScript.TypeID);
			textBoxCharacterScriptActionScriptAddress.Text = String.Format("{0:X8}", characterScript.ActionScript.ObjectID);
			textBoxCharacterScriptScriptAddress.Text = String.Format("{0:X8}", characterScript.Script.ObjectID);
			textBoxCharacterScriptID.Text = String.Format("{0:X4}", characterScript.ID);

			comboBoxCharacterScriptCharacterImageNumber.SelectedIndexChanged -= comboBoxCharacterScriptCharacterImageNumber_SelectedIndexChanged;
			comboBoxCharacterScriptCharacterImageNumber.DataSource = map.CharacterScriptList.CharacterImage;
			comboBoxCharacterScriptCharacterImageNumber.SelectedItem = map.CharacterScriptList.CharacterImage[characterScript.CharacterImageNumber];
			comboBoxCharacterScriptCharacterImageNumber.SelectedIndexChanged += comboBoxCharacterScriptCharacterImageNumber_SelectedIndexChanged;

			comboBoxCharacterScriptPalette.SelectedIndexChanged -= comboBoxCharacterScriptPalette_SelectedIndexChanged;
			comboBoxCharacterScriptPalette.DataSource = map.CharacterScriptList.Palette;
			comboBoxCharacterScriptPalette.SelectedIndex = characterScript.PaletteNumber;
			comboBoxCharacterScriptPalette.SelectedIndexChanged += comboBoxCharacterScriptPalette_SelectedIndexChanged;

			comboBoxCharacterScriptDirection.SelectedIndexChanged -= comboBoxCharacterScriptDirection_SelectedIndexChanged;
			comboBoxCharacterScriptDirection.DataSource = Enum.GetNames(typeof(CharacterScriptDirection));
			comboBoxCharacterScriptDirection.SelectedItem = characterScript.Direction.ToString();
			comboBoxCharacterScriptDirection.SelectedIndexChanged += comboBoxCharacterScriptDirection_SelectedIndexChanged;
		}
		private void componentCharacterScriptClear()
		{
			textBoxCharacterScriptX.Text = null;
			textBoxCharacterScriptY.Text = null;
			textBoxCharacterScriptTypeID.Text = null;

			comboBoxCharacterScriptCharacterImageNumber.DataSource = null;
			comboBoxCharacterScriptPalette.DataSource = null;
			comboBoxCharacterScriptDirection.DataSource = null;

			textBoxCharacterScriptActionScriptAddress.Text = null;
			textBoxCharacterScriptScriptAddress.Text = null;

			textBoxCharacterScriptID.Text = null;
		}
		private void componentCharacterScriptEnable()
		{
			textBoxCharacterScriptX.Enabled = true;
			textBoxCharacterScriptY.Enabled = true;
			textBoxCharacterScriptTypeID.Enabled = true;

			comboBoxCharacterScriptCharacterImageNumber.Enabled = true;
			comboBoxCharacterScriptPalette.Enabled = true;
			comboBoxCharacterScriptDirection.Enabled = true;

			textBoxCharacterScriptActionScriptAddress.Enabled = true;
			textBoxCharacterScriptScriptAddress.Enabled = true;

			textBoxCharacterScriptID.Enabled = true;
		}
		private void componentCharacterScriptDisable()
		{
			textBoxCharacterScriptX.Enabled = false;
			textBoxCharacterScriptY.Enabled = false;
			textBoxCharacterScriptTypeID.Enabled = false;

			comboBoxCharacterScriptCharacterImageNumber.Enabled = false;
			comboBoxCharacterScriptPalette.Enabled = false;
			comboBoxCharacterScriptDirection.Enabled = false;

			textBoxCharacterScriptActionScriptAddress.Enabled = false;
			textBoxCharacterScriptScriptAddress.Enabled = false;

			textBoxCharacterScriptID.Enabled = false;
		}
		private void componentCharacterScriptRefresh()
		{
			pictureBoxCharacterScript.Refresh();

			textBoxCharacterScriptX.Refresh();
			textBoxCharacterScriptY.Refresh();
			textBoxCharacterScriptTypeID.Refresh();
			textBoxCharacterScriptActionScriptAddress.Refresh();
			textBoxCharacterScriptScriptAddress.Refresh();
			textBoxCharacterScriptID.Refresh();

			comboBoxCharacterScriptCharacterImageNumber.Refresh();
			comboBoxCharacterScriptPalette.Refresh();
			comboBoxCharacterScriptDirection.Refresh();
		}

		private void characterScriptDelete()
		{
			if (listBoxCharacterScript.SelectedItem != null)
			{
				switch (MessageBox.Show("削除しますか", FormTomatoTool.applicationName, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation))
				{
					case DialogResult.Yes:
						map.CharacterScriptList.CharacterScript.RemoveAt(listBoxCharacterScript.SelectedIndex);

						listBoxCharacterScript.DataSource = null;
						listBoxCharacterScript.DataSource = map.CharacterScriptList.CharacterScript;

						componentCharacterScriptRefresh();

						break;

					default:
						break;
				}
			}
		}

		private void comboBoxCharacterScriptCharacterImageNumber_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (listBoxCharacterScript.SelectedItem != null)
			{
				((CharacterScript)listBoxCharacterScript.SelectedItem).CharacterImageNumber = (byte)comboBoxCharacterScriptCharacterImageNumber.SelectedIndex;
				pictureBoxCharacterScript.Refresh();
			}
		}
		private void comboBoxCharacterScriptCharacterImageNumber_Format(object sender, ListControlConvertEventArgs e)
		{
			e.Value = String.Format("{0:X8}", ((CharacterImage)e.ListItem).ObjectID);
		}

		private void comboBoxCharacterScriptPalette_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (listBoxCharacterScript.SelectedItem != null)
			{
				((CharacterScript)listBoxCharacterScript.SelectedItem).PaletteNumber = (byte)comboBoxCharacterScriptPalette.SelectedIndex;
				pictureBoxCharacterScript.Refresh();
			}
		}
		private void comboBoxCharacterScriptPalette_Format(object sender, ListControlConvertEventArgs e)
		{
			e.Value = String.Format("{0:X8}", ((Palette)e.ListItem).ObjectID);
		}

		private void comboBoxCharacterScriptDirection_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (listBoxCharacterScript.SelectedItem != null)
			{
				((CharacterScript)listBoxCharacterScript.SelectedItem).Direction = (CharacterScriptDirection)Enum.Parse(typeof(CharacterScriptDirection), ((ComboBox)sender).SelectedItem.ToString());
				pictureBoxCharacterScript.Refresh();
			}
		}
		private void comboBoxCharacterScriptDirection_SelectionChangeCommitted(object sender, EventArgs e)
		{
			if (listBoxCharacterScript.SelectedItem != null)
			{
				try
				{
					((CharacterScript)listBoxCharacterScript.SelectedItem).Direction = (CharacterScriptDirection)Enum.Parse(typeof(CharacterScriptDirection), ((ComboBox)sender).SelectedItem.ToString());
					pictureBoxCharacterScript.Refresh();
				}
				catch
				{
					((ComboBox)sender).SelectedItem = ((CharacterScript)listBoxCharacterScript.SelectedItem).Direction.ToString();
				}
			}
		}

		private void textBoxCharacterScriptX_Validating(object sender, CancelEventArgs e)
		{
			if (listBoxCharacterScript.SelectedItem != null)
			{
				TextBox textBox = (TextBox)sender;
				CharacterScript characterScript = (CharacterScript)listBoxCharacterScript.SelectedItem;

				try
				{
					characterScript.X = Convert.ToByte(textBox.Text);
					pictureBoxCharacterScript.Refresh();
				}
				catch
				{
					switch (MessageBox.Show("エラー", FormTomatoTool.applicationName, MessageBoxButtons.OKCancel, MessageBoxIcon.Error))
					{
						case DialogResult.Cancel:

							textBox.Text = characterScript.X.ToString();

							break;

						default:
							break;
					}

					textBox.SelectAll();
					e.Cancel = true;
				}
			}
		}
		private void textBoxCharacterScriptY_Validating(object sender, CancelEventArgs e)
		{

			if (listBoxCharacterScript.SelectedItem != null)
			{
				TextBox textBox = (TextBox)sender;
				CharacterScript characterScript = (CharacterScript)listBoxCharacterScript.SelectedItem;

				try
				{
					characterScript.Y = Convert.ToByte(textBox.Text);
					pictureBoxCharacterScript.Refresh();
				}
				catch
				{
					switch (MessageBox.Show("エラー", FormTomatoTool.applicationName, MessageBoxButtons.OKCancel, MessageBoxIcon.Error))
					{
						case DialogResult.Cancel:

							textBox.Text = characterScript.Y.ToString();

							break;

						default:
							break;
					}

					textBox.SelectAll();
					e.Cancel = true;
				}
			}
		}
		private void textBoxCharacterScriptTypeID_Validating(object sender, CancelEventArgs e)
		{

			if (listBoxCharacterScript.SelectedItem != null)
			{
				TextBox textBox = (TextBox)sender;
				CharacterScript characterScript = (CharacterScript)listBoxCharacterScript.SelectedItem;

				try
				{
					characterScript.TypeID = Convert.ToByte(textBox.Text, 16);
					pictureBoxCharacterScript.Refresh();
				}
				catch
				{
					switch (MessageBox.Show("エラー", FormTomatoTool.applicationName, MessageBoxButtons.OKCancel, MessageBoxIcon.Error))
					{
						case DialogResult.Cancel:

							textBox.Text = String.Format("{0:X2}", characterScript.TypeID);

							break;

						default:
							break;
					}

					textBox.SelectAll();
					e.Cancel = true;
				}
			}
		}
		private void textBoxCharacterScriptActionScriptAddress_Validating(object sender, CancelEventArgs e)
		{
			if (listBoxCharacterScript.SelectedItem != null)
			{
				TextBox textBox = (TextBox)sender;
				CharacterScript characterScript = (CharacterScript)listBoxCharacterScript.SelectedItem;

				try
				{
					characterScript.ActionScript.ObjectID = Convert.ToUInt32(textBox.Text, 16);
					pictureBoxCharacterScript.Refresh();
				}
				catch
				{
					switch (MessageBox.Show("エラー", FormTomatoTool.applicationName, MessageBoxButtons.OKCancel, MessageBoxIcon.Error))
					{
						case DialogResult.Cancel:

							textBox.Text = String.Format("{0:X8}", characterScript.ActionScript.ObjectID);

							break;

						default:
							break;
					}

					textBox.SelectAll();
					e.Cancel = true;
				}
			}
		}
		private void textBoxCharacterScriptScriptAddress_Validating(object sender, CancelEventArgs e)
		{
			if (listBoxCharacterScript.SelectedItem != null)
			{
				TextBox textBox = (TextBox)sender;
				CharacterScript characterScript = (CharacterScript)listBoxCharacterScript.SelectedItem;

				try
				{
					characterScript.Script.ObjectID = Convert.ToUInt32(textBox.Text, 16);
					pictureBoxCharacterScript.Refresh();
				}
				catch
				{
					switch (MessageBox.Show("エラー", FormTomatoTool.applicationName, MessageBoxButtons.OKCancel, MessageBoxIcon.Error))
					{
						case DialogResult.Cancel:

							textBox.Text = String.Format("{0:X8}", characterScript.Script.ObjectID);

							break;

						default:
							break;
					}

					textBox.SelectAll();
					e.Cancel = true;
				}
			}

		}
		private void textBoxCharacterScriptID_Validating(object sender, CancelEventArgs e)
		{
			if (listBoxCharacterScript.SelectedItem != null)
			{
				TextBox textBox = (TextBox)sender;
				CharacterScript characterScript = (CharacterScript)listBoxCharacterScript.SelectedItem;

				try
				{
					characterScript.ID = Convert.ToUInt16(textBox.Text, 16);
					pictureBoxCharacterScript.Refresh();
				}
				catch
				{
					switch (MessageBox.Show("エラー", FormTomatoTool.applicationName, MessageBoxButtons.OKCancel, MessageBoxIcon.Error))
					{
						case DialogResult.Cancel:

							textBox.Text = String.Format("{0:X4}", characterScript.ID);

							break;

						default:
							break;
					}

					textBox.SelectAll();
					e.Cancel = true;
				}
			}
		}

		private void textBoxCharacterScriptX_KeyDown(object sender, KeyEventArgs e)
		{
			if (listBoxCharacterScript.SelectedItem != null)
			{
				TextBox textBox = (TextBox)sender;
				CharacterScript characterScript = (CharacterScript)listBoxCharacterScript.SelectedItem;

				switch (e.KeyCode)
				{
					case Keys.Enter:
						tabControlMap.Focus();
						break;

					case Keys.Escape:
						textBox.Text = characterScript.X.ToString();
						tabControlMap.Focus();
						break;

					default:
						break;
				}
			}
		}
		private void textBoxCharacterScriptY_KeyDown(object sender, KeyEventArgs e)
		{
			if (listBoxCharacterScript.SelectedItem != null)
			{
				TextBox textBox = (TextBox)sender;
				CharacterScript characterScript = (CharacterScript)listBoxCharacterScript.SelectedItem;

				switch (e.KeyCode)
				{
					case Keys.Enter:
						tabControlMap.Focus();
						break;

					case Keys.Escape:
						textBox.Text = characterScript.Y.ToString();
						tabControlMap.Focus();
						break;

					default:
						break;
				}
			}
		}
		private void textBoxCharacterScriptTypeID_KeyDown(object sender, KeyEventArgs e)
		{
			if (listBoxCharacterScript.SelectedItem != null)
			{
				TextBox textBox = (TextBox)sender;
				CharacterScript characterScript = (CharacterScript)listBoxCharacterScript.SelectedItem;

				switch (e.KeyCode)
				{
					case Keys.Enter:
						tabControlMap.Focus();
						break;

					case Keys.Escape:
						textBox.Text = String.Format("{0:X2}", characterScript.TypeID);
						tabControlMap.Focus();
						break;

					default:
						break;
				}
			}
		}
		private void textBoxCharacterScriptActionScriptAddress_KeyDown(object sender, KeyEventArgs e)
		{
			if (listBoxCharacterScript.SelectedItem != null)
			{
				TextBox textBox = (TextBox)sender;
				CharacterScript characterScript = (CharacterScript)listBoxCharacterScript.SelectedItem;

				switch (e.KeyCode)
				{
					case Keys.Enter:
						tabControlMap.Focus();
						break;

					case Keys.Escape:
						textBox.Text = String.Format("{0:X8}", characterScript.ActionScript.ObjectID);
						tabControlMap.Focus();
						break;

					default:
						break;
				}
			}
		}
		private void textBoxCharacterScriptScriptAddress_KeyDown(object sender, KeyEventArgs e)
		{
			if (listBoxCharacterScript.SelectedItem != null)
			{
				TextBox textBox = (TextBox)sender;
				CharacterScript characterScript = (CharacterScript)listBoxCharacterScript.SelectedItem;

				switch (e.KeyCode)
				{
					case Keys.Enter:
						tabControlMap.Focus();
						break;

					case Keys.Escape:
						textBox.Text = String.Format("{0:X8}", characterScript.Script.ObjectID);
						tabControlMap.Focus();
						break;

					default:
						break;
				}
			}
		}
		private void textBoxCharacterScriptID_KeyDown(object sender, KeyEventArgs e)
		{
			if (listBoxCharacterScript.SelectedItem != null)
			{
				TextBox textBox = (TextBox)sender;
				CharacterScript characterScript = (CharacterScript)listBoxCharacterScript.SelectedItem;

				switch (e.KeyCode)
				{
					case Keys.Enter:
						tabControlMap.Focus();
						break;

					case Keys.Escape:
						textBox.Text = String.Format("{0:X4}", characterScript.ID);
						tabControlMap.Focus();
						break;

					default:
						break;
				}
			}
		}

		private void buttonCharacterScriptActionScriptAddressEdit_Click(object sender, EventArgs e)
		{
			if (listBoxCharacterScript.SelectedItem != null)
			{
				CharacterScript characterScript = (CharacterScript)listBoxCharacterScript.SelectedItem;

				ScriptEditor(tomatoAdventure, characterScript.ActionScript.ObjectID);
			}
		}
		private void buttonCharacterScriptScriptEdit_Click(object sender, EventArgs e)
		{
			if (listBoxCharacterScript.SelectedItem != null)
			{
				CharacterScript characterScript = (CharacterScript)listBoxCharacterScript.SelectedItem;

				ScriptEditor(tomatoAdventure, characterScript.Script.ObjectID);
			}
		}

		private void contextMenuStripListBoxCharacterScript_Opening(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if (map.CharacterScriptList.CharacterScript.Count == 0)
			{
				toolStripMenuItemListBoxCharacterScriptAllDelete.Enabled = false;
			}
			else
			{
				toolStripMenuItemListBoxCharacterScriptAllDelete.Enabled = true;
			}

			if (listBoxCharacterScript.SelectedItem != null)
			{
				toolStripMenuItemListBoxCharacterScriptInsert.Enabled = true;
				toolStripMenuItemListBoxCharacterScriptDelete.Enabled = true;
			}
			else
			{
				toolStripMenuItemListBoxCharacterScriptInsert.Enabled = false;
				toolStripMenuItemListBoxCharacterScriptDelete.Enabled = false;
			}
		}
		private void toolStripMenuItemListBoxCharacterScriptAdd_Click(object sender, EventArgs e)
		{
			map.CharacterScriptList.CharacterScript.Add(new CharacterScript());

			listBoxCharacterScript.DataSource = null;
			listBoxCharacterScript.DataSource = map.CharacterScriptList.CharacterScript;

			listBoxCharacterScript.SelectedIndex = listBoxCharacterScript.Items.Count - 1;

			componentCharacterScriptRefresh();
		}
		private void toolStripMenuItemListBoxCharacterScriptInsert_Click(object sender, EventArgs e)
		{
			if (listBoxCharacterScript.SelectedItem != null)
			{
				map.CharacterScriptList.CharacterScript.Insert(listBoxCharacterScript.SelectedIndex, new CharacterScript());

				listBoxCharacterScript.DataSource = null;
				listBoxCharacterScript.DataSource = map.CharacterScriptList.CharacterScript;

				componentCharacterScriptRefresh();
			}
		}
		private void toolStripMenuItemListBoxCharacterScriptDelete_Click(object sender, EventArgs e)
		{
			characterScriptDelete();
		}
		private void toolStripMenuItemListBoxCharacterScriptAllDelete_Click(object sender, EventArgs e)
		{
			switch (MessageBox.Show("全て削除しますか", FormTomatoTool.applicationName, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation))
			{
				case DialogResult.Yes:
					map.CharacterScriptList.CharacterScript.Clear();

					listBoxCharacterScript.DataSource = null;
					listBoxCharacterScript.DataSource = map.CharacterScriptList.CharacterScript;

					componentCharacterScriptRefresh();

					break;

				default:
					break;
			}
		}

		private void contextMenuStripPictureBoxCharacterScript_Opening(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if (listBoxCharacterScript.SelectedItem != null)
			{
				toolStripMenuItemPictureBoxCharacterScriptDelete.Enabled = true;
			}
			else
			{
				toolStripMenuItemPictureBoxCharacterScriptDelete.Enabled = false;
			}
		}
		private void toolStripMenuItemPictureBoxCharacterScriptAdd_Click(object sender, EventArgs e)
		{
			Point point = contextMenuStripPictureBoxCharacterScript.PointToClient(new Point(0, 0));
			point.X = -point.X;
			point.Y = -point.Y;
			point = contextMenuStripPictureBoxCharacterScript.SourceControl.PointToClient(point);

			CharacterScript characterScript = new CharacterScript();
			characterScript.X = (byte)(point.X / Map.BLOCK_WIDTH);
			characterScript.Y = (byte)(point.Y / Map.BLOCK_WIDTH);

			map.CharacterScriptList.CharacterScript.Add(characterScript);

			listBoxCharacterScript.DataSource = null;
			listBoxCharacterScript.DataSource = map.CharacterScriptList.CharacterScript;

			listBoxCharacterScript.SelectedIndex = listBoxCharacterScript.Items.Count - 1;

			pictureBoxCharacterScript.Refresh();
		}
		private void toolStripMenuItemPictureBoxCharacterScriptDelete_Click(object sender, EventArgs e)
		{
			characterScriptDelete();
		}

	}
}
