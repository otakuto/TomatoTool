using System;
using System.Drawing;
using System.Windows.Forms;

namespace TomatoTool
{
	public partial class CharacterImageEditor : Form
	{
		private CharacterImage characterImage;

		private Palette palette;

		public CharacterImageEditor(CharacterImage characterImage, Palette palette)
		{
			InitializeComponent();
			initializeComponent(characterImage);

			this.characterImage = characterImage;
			this.palette = palette;
		}

		private void initializeComponent(CharacterImage characterImage)
		{
			pictureBoxOAMSet.Width = (int)OAMSet.WIDTH;
			pictureBoxOAMSet.Height = (int)OAMSet.HEIGHT;

			listBoxOAMSet.SelectedIndexChanged -= listBoxOAMSet_SelectedIndexChanged;
			listBoxOAMSet.DataSource = characterImage.OAMSetList.OAMSet;
			listBoxOAMSet.SelectedItem = null;
			listBoxOAMSet.SelectedIndexChanged += listBoxOAMSet_SelectedIndexChanged;

			componentOAMClear();
			componentOAMDisable();
		}

		private void pictureBoxOAMSet_Paint(object sender, PaintEventArgs e)
		{
			if (listBoxOAMSet.SelectedItem != null)
			{
				OAMSet oamSet = (OAMSet)listBoxOAMSet.SelectedItem;

				//OAMSetの描写
				using (Bitmap bitmap = oamSet.toBitmap(characterImage.TileList, palette, false))
				using (SolidBrush solidBrush = new SolidBrush(palette.Color[0].toColor()))
				{
					e.Graphics.FillRectangle(solidBrush, new Rectangle(0, 0, ((PictureBox)sender).Width, ((PictureBox)sender).Height));
					e.Graphics.DrawImage(bitmap, 0, 0);
				}

				//OAMに枠線を付ける
				e.Graphics.TranslateTransform(OAMSet.CENTER_X, OAMSet.CENTER_Y);
				for (int i = 0; i < oamSet.Count; ++i)
				{
					using (Pen pen = new Pen(Map.SelectColor, 1))
					{
						e.Graphics.DrawRectangle(pen, oamSet[i].X, oamSet[i].Y, (oamSet[i].Width * Tile.WIDTH) - 1, (oamSet[i].Height * Tile.HEIGHT) - 1);
					}
				}
				e.Graphics.ResetTransform();
			}
		}
		private void pictureBoxOAMSetTile_Paint(object sender, PaintEventArgs e)
		{
			if (listBoxOAM.SelectedItem != null)
			{
				((PictureBox)sender).Width = Tile.WIDTH * 32;
				((PictureBox)sender).Height = Tile.HEIGHT * (((characterImage.TileList.Count % 32) == 0) ? (characterImage.TileList.Count / 32) : ((characterImage.TileList.Count / 32) + 1));

				OAM oam = (OAM)listBoxOAM.SelectedItem;

				for (int i = 0; i < characterImage.TileList.Count; ++i)
				{
					using (Bitmap bitmap = characterImage.TileList[i].toBitmap(palette, oam.FlipX, oam.FlipY))
					{
						e.Graphics.DrawImage(bitmap, ((i % 32) * Tile.WIDTH), ((i / 32) * Tile.HEIGHT));
					}
				}

				using (Pen pen = new Pen(Color.FromArgb(0xFF, 0x00, 0x00)))
				{
					e.Graphics.DrawRectangle(pen, ((oam.TileNumber % 32) * Tile.WIDTH), ((oam.TileNumber / 32) * Tile.HEIGHT), (((oam.Width * oam.Height) % 32) * Tile.WIDTH), Tile.HEIGHT);
				}
			}
		}
		private void pictureBoxOAMSetTile_MouseMove(object sender, MouseEventArgs e)
		{
			if (listBoxOAM.SelectedItem != null)
			{
				if (((0 <= e.X) && (e.X < ((PictureBox)sender).Width)) &&
					((0 <= e.Y) && (e.Y < ((PictureBox)sender).Height)) &&
					((((e.Y / Tile.HEIGHT) * 32) + (e.X / Tile.WIDTH)) < characterImage.TileList.Count))
				{
					if (e.Button == MouseButtons.Left)
					{
						OAM oam = (OAM)listBoxOAM.SelectedItem;
						oam.TileNumber = (byte)(((e.Y / Tile.HEIGHT) * 32) + (e.X / Tile.WIDTH));

						pictureBoxOAM.Refresh();
						pictureBoxOAMSet.Refresh();
						pictureBoxOAMSetTile.Refresh();
					}
				}
			}
		}

		private void listBoxOAMSet_SelectedIndexChanged(object sender, EventArgs e)
		{
			listBoxOAM.SelectedIndexChanged -= listBoxOAM_SelectedIndexChanged;
			listBoxOAM.DataSource = ((OAMSet)listBoxOAMSet.SelectedItem).OAM;
			listBoxOAM.SelectedItem = null;
			listBoxOAM.SelectedIndexChanged += listBoxOAM_SelectedIndexChanged;

			pictureBoxOAMSet.Refresh();
		}
		private void listBoxOAMSet_Format(object sender, ListControlConvertEventArgs e)
		{
			e.Value = String.Format("{0:X4}", ((ListBox)sender).Items.IndexOf(e.ListItem));
		}

		private void listBoxOAM_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (listBoxOAM.SelectedItem != null)
			{
				componentOAMEnable();
				componentOAMLoad((OAM)listBoxOAM.SelectedItem);
			}
			else
			{
				componentOAMClear();
				componentOAMDisable();
			}

			pictureBoxOAMSet.Refresh();
			pictureBoxOAM.Refresh();
			pictureBoxOAMSetTile.Refresh();
		}
		private void listBoxOAM_Format(object sender, ListControlConvertEventArgs e)
		{
			e.Value = String.Format("{0:X2}", ((ListBox)sender).Items.IndexOf(e.ListItem));
		}

		private void pictureBoxOAM_Paint(object sender, PaintEventArgs e)
		{
			if (listBoxOAM.SelectedItem != null)
			{
				OAM oam = (OAM)listBoxOAM.SelectedItem;

				((PictureBox)sender).Width = oam.Width * Tile.WIDTH;
				((PictureBox)sender).Height = oam.Height * Tile.HEIGHT;

				using (Bitmap bitmap = oam.toBitmap(characterImage.TileList, palette))
				{
					e.Graphics.DrawImage(bitmap, 0, 0);
				}
			}
		}

		private void componentOAMLoad(OAM oam)
		{
			numericUpDownX.Value = oam.X;
			numericUpDownY.Value = oam.Y;

			textBoxOAMTileNumber.Text = oam.TileNumber.ToString();

			comboBoxOAMSize.SelectedIndexChanged -= comboBoxOAMSize_SelectedIndexChanged;
			comboBoxOAMSize.DataSource = Enum.GetNames(typeof(OAMSize));
			comboBoxOAMSize.SelectedItem = oam.OAMSize.ToString();
			comboBoxOAMSize.SelectedIndexChanged += comboBoxOAMSize_SelectedIndexChanged;

			checkBoxFlipX.Checked = oam.FlipX;
			checkBoxFlipY.Checked = oam.FlipY;
		}
		private void componentOAMClear()
		{
			numericUpDownX.Text = null;
			numericUpDownY.Text = null;

			textBoxOAMTileNumber.Text = null;

			comboBoxOAMSize.DataSource = null;
			comboBoxOAMSize.SelectedItem = null;

			checkBoxFlipX.Checked = false;
			checkBoxFlipY.Checked = false;
		}
		private void componentOAMEnable()
		{
			numericUpDownX.Enabled = true;
			numericUpDownY.Enabled = true;

			textBoxOAMTileNumber.Enabled = true;

			comboBoxOAMSize.Enabled = true;

			checkBoxFlipX.Enabled = true;
			checkBoxFlipY.Enabled = true;
		}
		private void componentOAMDisable()
		{
			numericUpDownX.Enabled = false;
			numericUpDownY.Enabled = false;

			textBoxOAMTileNumber.Enabled = false;

			comboBoxOAMSize.Enabled = false;

			checkBoxFlipX.Enabled = false;
			checkBoxFlipY.Enabled = false;
		}

		private void numericUpDownX_ValueChanged(object sender, EventArgs e)
		{
			OAM oam = (OAM)listBoxOAM.SelectedItem;
			oam.X = (short)numericUpDownX.Value;

			pictureBoxOAMSet.Refresh();
		}
		private void numericUpDownY_ValueChanged(object sender, EventArgs e)
		{
			OAM oam = (OAM)listBoxOAM.SelectedItem;
			oam.Y = (sbyte)numericUpDownY.Value;

			pictureBoxOAMSet.Refresh();
		}
		private void checkBoxFlipX_CheckedChanged(object sender, EventArgs e)
		{
			if (listBoxOAM.SelectedItem != null)
			{
				OAM oam = (OAM)listBoxOAM.SelectedItem;
				oam.FlipX = checkBoxFlipX.Checked;

				pictureBoxOAM.Refresh();
				pictureBoxOAMSet.Refresh();
				pictureBoxOAMSetTile.Refresh();
			}
		}
		private void checkBoxFlipY_CheckedChanged(object sender, EventArgs e)
		{
			if (listBoxOAM.SelectedItem != null)
			{
				OAM oam = (OAM)listBoxOAM.SelectedItem;
				oam.FlipY = checkBoxFlipY.Checked;

				pictureBoxOAM.Refresh();
				pictureBoxOAMSet.Refresh();
				pictureBoxOAMSetTile.Refresh();
			}
		}

		private void comboBoxOAMSize_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (listBoxOAM.SelectedItem != null)
			{
				OAM oam = (OAM)listBoxOAM.SelectedItem;

				oam.OAMSize = (OAMSize)Enum.Parse(typeof(OAMSize), (string)comboBoxOAMSize.SelectedItem);

				pictureBoxOAM.Refresh();
				pictureBoxOAMSet.Refresh();
				pictureBoxOAMSetTile.Refresh();
			}
		}
		private void pictureBoxCharacterImageSize_Paint(object sender, PaintEventArgs e)
		{
			((PictureBox)sender).Width = (int)OAMSet.WIDTH;
			((PictureBox)sender).Height = (int)OAMSet.HEIGHT;

			using (Bitmap bitmap = characterImage.toBitmap(CharacterScriptDirection.Down, 0, palette, false))
			using (Pen pen = new Pen(Color.FromArgb(0xFF, 0x00, 0x00), 1))
			{
				e.Graphics.DrawImage(bitmap, 0, 0);
				e.Graphics.DrawRectangle(pen, OAMSet.CENTER_X - characterImage.Width, OAMSet.CENTER_Y - characterImage.Height, (characterImage.Width * 2) - 1, characterImage.Height - 1);
			}
		}


	}
}
