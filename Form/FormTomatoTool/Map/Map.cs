using System;
using System.Drawing;
using System.Windows.Forms;

namespace TomatoTool
{
	public partial class FormTomatoTool
		:
		Form
	{
		private void listBoxMap_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (listBoxMap.SelectedItem != null)
			{
				initializeMap();

				pictureBoxMapRefresh();
			}
		}
		private void listBoxMap_Format(object sender, ListControlConvertEventArgs e)
		{
			e.Value = String.Format("{0:X4}", (e.ListItem as Map).Number);
		}
		private void listBoxMap_Add(object sender, EventArgs e)
		{
		}
		private void listBoxMap_Delete(object sender, EventArgs e)
		{
			if (listBoxMap.SelectedItem != null)
			{
				switch (MessageBox.Show("削除しますか", applicationName, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation))
				{
					case DialogResult.Yes:
						try
						{
							int SelectedIndex = listBoxMap.SelectedIndex;
							listBoxMap.DataSource = null;
							tomatoAdventure.ObjectDictionary[typeof(Map)].RemoveAt(SelectedIndex);
							listBoxMap.DataSource = tomatoAdventure.ObjectDictionary[typeof(Map)];
							listBoxMap.SelectedItem = null;
							pictureBoxMapRefresh();
						}
						catch
						{
						}
						break;
				}
			}
		}

		private void initializeMap()
		{
			initializeMapView();

			//キャラクターパレット
			listBoxCharacterPalette.DataSource = ((Map)listBoxMap.SelectedItem).CharacterScriptList.Palette;

			Map map = (Map)listBoxMap.SelectedItem;


			//チップセットリスト
			if (map.ChipSetList[radioButtonChipSetListBG()] != null)
			{
				listBoxChipSetList.DataSource = map.ChipSetList[radioButtonChipSetListBG()].ChipSet;
				listBoxChipSetList.SelectedItem = null;
			}

			//マップパレット
			listBoxMapPalette.SelectedIndexChanged -= listBoxMapPalette_SelectedIndexChanged;
			listBoxMapPalette.DataSource = map.Palette;
			listBoxMapPalette.SelectedIndexChanged += listBoxMapPalette_SelectedIndexChanged;
			listBoxMapPalette.SelectedItem = null;

			//キャラクターイメージのツリビュー関係
			listBoxCharacterImage.SelectedIndexChanged -= listBoxCharacterImage_SelectedIndexChanged;
			listBoxCharacterImage.DataSource = map.CharacterScriptList.CharacterImage;
			listBoxCharacterImage.SelectedIndexChanged += listBoxCharacterImage_SelectedIndexChanged;
			listBoxCharacterScript.SelectedItem = null;

			numericUpDownCharacterImageSize.Value = 1;

			initializeMapArea();
			initializeMapScript();
			initializeWarpScript();
			initializeCharacterScript();
			initializeAnimationTileSet();
			initializeBattleBackground();
			pictureBoxMapRefresh();
		}

		private void initializeMapView()
		{
			Map map = (Map)listBoxMap.SelectedItem;

			textBoxMapSaveType.Text = String.Format("{0:X2}", map.SaveType);
			textBoxMapNumber.Text = String.Format("{0:X4}", map.Number);

			textBoxMapWarpScriptAddress.Text = String.Format("{0:X8}", map.WarpScriptList.ObjectID);
			textBoxMapMainScriptAddress.Text = String.Format("{0:X8}", map.MapScriptList.MainScript.ObjectID);
			textBoxMapObjectScriptAddress.Text = String.Format("{0:X8}", map.CharacterScriptList.ObjectID);
			textBoxMapAreaAddress.Text = String.Format("{0:X8}", map.MapArea.ObjectID);

			textBoxMapX.Text = map.Width.ToString();
			textBoxMapY.Text = map.Height.ToString();

			comboBoxMapBGM.SelectedIndex = map.BGMNumber;
		}

		private void saveMap()
		{
			if (listBoxMap.SelectedItem != null)
			{
				Map map = listBoxMap.SelectedItem as Map;

				//設定
				//mapSetting.SaveType = (byte)numericUpDownSaveType.Value;
				//mapSetting.Number = (ushort)numericUpDownMapNumber.Value;

				//tomatoAdventure.Map[Index].warpScript.address = Convert.ToInt32(TextBoxMapWarpScriptAddress.Text, 16);
				map.MapScriptList.MainScript.ObjectID = Convert.ToUInt32(textBoxMapMainScriptAddress.Text, 16);
				//tomatoAdventure.Map[Index].characterScript.address = Convert.ToInt32(TextBoxMapObjectScriptAddress.Text, 16);
				map.MapArea.ObjectID = Convert.ToUInt32(textBoxMapAreaAddress.Text, 16);
			}
		}

		private void PictureBoxMapView_Paint(object sender, PaintEventArgs e)
		{
			if (listBoxMap.SelectedItem != null)
			{
				Map map = listBoxMap.SelectedItem as Map;

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
				if (toolStripMenuItemMapWarpScript.Checked)
				{
					map.WarpScriptList.draw(e.Graphics);
				}

				//マップスクリプト
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

		}

		//グリッド
		private void gridDraw(object sender, Graphics graphics)
		{
			PictureBox pictureBox = sender as PictureBox;

			//色指定
			Pen pen = new Pen(Color.FromArgb(255, 0, 0), 1);

			//縦線
			for (int x = 0; x < (int)(pictureBox.Width / 16); ++x)
			{
				graphics.DrawLine(pen, x * 16, 0, x * 16, pictureBox.Height);
			}

			//横線
			for (int y = 0; y < (int)(pictureBox.Height / 16); ++y)
			{
				graphics.DrawLine(pen, 0, y * 16, pictureBox.Width, y * 16);
			}
		}

		private void pictureBoxResize(object sender, Map map)
		{
			(sender as PictureBox).Width = map.Width * 16;
			(sender as PictureBox).Height = map.Height * 16;
		}

		//全てのMapのPictureBoxをRefresh
		private void pictureBoxMapRefresh()
		{
			pictureBoxMapView.Refresh();
		}

		private void ToolStripMenuItemMap_CheckedChanged(object sender, EventArgs e)
		{
			pictureBoxMapRefresh();
		}

		//マップスクリプト編集
		private void ButtonMapScriptEdit_Click(object sender, EventArgs e)
		{
			if (listBoxMap.SelectedItem != null)
			{
				ScriptEditor(tomatoAdventure, ((Map)tomatoAdventure.ObjectDictionary[typeof(Map)][listBoxMap.SelectedIndex]).MapScriptList.MainScript.ObjectID);
			}
		}

		private void textBoxMapSaveType_TextChanged(object sender, EventArgs e)
		{
			if (listBoxMap.SelectedItem != null)
			{
				try
				{
					((Map)listBoxMap.SelectedItem).SaveType = Convert.ToByte(textBoxMapSaveType.Text, 16);
				}
				catch
				{
				}
			}
		}

		private void textBoxMapNumber_TextChanged(object sender, EventArgs e)
		{
			if (listBoxMap.SelectedItem != null)
			{
				try
				{
					((Map)listBoxMap.SelectedItem).Number = Convert.ToByte(textBoxMapNumber.Text, 16);
				}
				catch
				{
				}
			}
		}
	}
}
