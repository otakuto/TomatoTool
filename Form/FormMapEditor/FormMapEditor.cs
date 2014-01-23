using System;
using System.Drawing;
using System.Windows.Forms;

namespace TomatoTool
{
	public partial class FormMapEditor : Form
	{
		private ToolStripMenuItem[] toolStripMenuItemMapChipBG;

		private TomatoAdventure tomatoAdventure;

		private Map map;

		private byte selectMapArea;
		private Chip selectChip = new Chip(0x0000);

		public FormMapEditor(TomatoAdventure tomatoAdventure, Map map)
		{
			InitializeComponent();

			toolStripMenuItemMapChipBG = new ToolStripMenuItem[3];
			toolStripMenuItemMapChipBG[0] = toolStripMenuItemViewChipBG1;
			toolStripMenuItemMapChipBG[1] = toolStripMenuItemViewChipBG2;
			toolStripMenuItemMapChipBG[2] = toolStripMenuItemViewChipBG3;

			controlMapLoad(tomatoAdventure, map);
		}

		private void controlMapLoad(TomatoAdventure tomatoAdventure, Map map)
		{
			this.tomatoAdventure = tomatoAdventure;
			this.map = map;

			if (map != null)
			{
				textBoxMapNumber.Text = String.Format("{0:X4}", map.Number);

				textBoxMapSaveType.Text = String.Format("{0:X2}", map.SaveType);

				textBoxMapWidth.Text = map.Width.ToString();
				textBoxMapHeight.Text = map.Height.ToString();

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

				//ワープスクリプト
				comboBoxWarpScriptList.SelectedIndexChanged -= comboBoxWarpScriptList_SelectedIndexChanged;
				comboBoxWarpScriptList.DataSource = tomatoAdventure.ObjectDictionary[typeof(WarpScriptList)];
				comboBoxWarpScriptList.SelectedItem = null;
				comboBoxWarpScriptList.SelectedIndexChanged += comboBoxWarpScriptList_SelectedIndexChanged;
				comboBoxWarpScriptList.SelectedItem = map.WarpScriptList;

				//マップスクリプト
				comboBoxMapScriptList.SelectedIndexChanged -= comboBoxMapScriptList_SelectedIndexChanged;
				comboBoxMapScriptList.DataSource = tomatoAdventure.ObjectDictionary[typeof(MapScriptList)];
				comboBoxMapScriptList.SelectedItem = null;
				comboBoxMapScriptList.SelectedIndexChanged += comboBoxMapScriptList_SelectedIndexChanged;
				comboBoxMapScriptList.SelectedItem = map.MapScriptList;

				//キャラクタースクリプト
				listBoxCharacterScript.DataSource = map.CharacterScriptList.CharacterScript;
				listBoxCharacterScript.SelectedItem = null;

				//キャラクターイメージのツリビュー関係
				listBoxCharacterImage.SelectedIndexChanged -= listBoxCharacterImage_SelectedIndexChanged;
				listBoxCharacterImage.DataSource = map.CharacterScriptList.CharacterImage;
				listBoxCharacterImage.SelectedIndexChanged += listBoxCharacterImage_SelectedIndexChanged;
				listBoxCharacterScript.SelectedItem = null;

				numericUpDownCharacterImageSize.Value = 1;

				//キャラクターパレット
				listBoxCharacterPalette.DataSource = map.CharacterScriptList.Palette;

				//アニメーションタイルセット
				comboBoxAnimationTileSetList.SelectedIndexChanged -= comboBoxAnimationTileSetList_SelectedIndexChanged;
				comboBoxAnimationTileSetList.DataSource = tomatoAdventure.ObjectDictionary[typeof(AnimationTileSet)];
				comboBoxAnimationTileSetList.SelectedIndexChanged += comboBoxAnimationTileSetList_SelectedIndexChanged;
				comboBoxAnimationTileSetList.SelectedItem = map.MapTile.AnimationTileSet;
			}
		}

		#region マップビュー

		private void pictureBoxMapView_Paint(object sender, PaintEventArgs e)
		{
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

			//マップスクリプト
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

		#endregion

		#region マップタイル

		private void pictureBoxMapTile_Paint(object sender, PaintEventArgs e)
		{
			((PictureBox)sender).Width = 256;
			((PictureBox)sender).Height = (map.MapTile.MainTile.Count / 32) * 8;

			for (int i = 0; i < map.MapTile.MainTile.Count; ++i)
			{
				Bitmap b = map.MapTile.MainTile[i].toBitmap(map.Palette[0]);
				e.Graphics.DrawImage(b, (i % 32) * 8, (i / 32) * 8);
				b.Dispose();
			}
		}

		private void button2_Click(object sender, EventArgs e)
		{
			using (LZ77Editor LZ77Editor = new LZ77Editor(tomatoAdventure, map.MapTile.MainTile, map.Palette[0]))
			{
				if (LZ77Editor.ShowDialog() == DialogResult.OK)
				{
					map.MapTile.MainTile = LZ77Editor.LZ77;
					pictureBoxMapTile.Refresh();
				}
			}

		}

		#endregion

		#region キャラクターパレット

		private void listBoxCharacterPalette_SelectedIndexChanged(object sender, EventArgs e)
		{
			pictureBoxCharacterPalette.Refresh();
		}
		private void listBoxCharacterPalette_Format(object sender, ListControlConvertEventArgs e)
		{
			e.Value = String.Format("{0:X8}", ((Palette)e.ListItem).ObjectID);
		}

		private void pictureBoxCharacterPalette_Paint(object sender, PaintEventArgs e)
		{
			if (listBoxCharacterPalette.SelectedItem != null)
			{
				Palette palette = (Palette)listBoxCharacterPalette.SelectedItem;
				palette.draw(e.Graphics, 32, 32);
			}
		}

		private void buttonCharacterPaletteEdit_Click(object sender, EventArgs e)
		{
			PaletteEditor paletteEditor = new PaletteEditor((Palette)listBoxCharacterPalette.SelectedItem);
			paletteEditor.Show();
			paletteEditor.Refresh();
			paletteEditor.Activate();
		}

		#endregion

		//グリッド
		private void gridDraw(object sender, Graphics graphics)
		{
			PictureBox pictureBox = (PictureBox)sender;

			//色指定
			using (Pen pen = new Pen(Color.FromArgb(255, 0, 0), 1))
			{
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
		}

		private void pictureBoxResize(object sender, Map map)
		{
			((PictureBox)sender).Width = map.Width * (int)Map.BLOCK_WIDTH;
			((PictureBox)sender).Height = map.Height * (int)Map.BLOCK_HEIGHT;
		}

		//スクリプトエディター呼び出し
		public void ScriptEditor(TomatoAdventure tomatoAdventure, uint address)
		{
			ScriptEditor scriptEditor = new ScriptEditor(tomatoAdventure, address);
			scriptEditor.Show();
			scriptEditor.Refresh();

			scriptEditor.Activate();
		}

		private void buttonMapResize_Click(object sender, EventArgs e)
		{
			try
			{
				map.resize(Convert.ToByte(textBoxMapWidth.Text), Convert.ToByte(textBoxMapHeight.Text));
				
				pictureBoxMapView.Refresh();
			}
			catch
			{
				textBoxMapWidth.Text = map.Width.ToString();
				textBoxMapHeight.Text = map.Height.ToString();
			}
		}

		
		private void button1_Click(object sender, EventArgs e)
		{
			ScriptEditor(tomatoAdventure, Convert.ToUInt32(textBoxMapScriptScriptAddress.Text, 16));
		}

		private void numericUpDownFrame_ValueChanged(object sender, EventArgs e)
		{
			Map.Frame = (ushort)numericUpDownFrame.Value;
			map.updata();
			pictureBoxMapView.Refresh();
		}
	}
}
