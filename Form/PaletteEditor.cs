using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;

namespace TomatoTool
{
	public partial class PaletteEditor : Form
	{
		private TomatoAdventure tomatoAdventure;
		private Palette palette;
		public Palette Palette
		{
			get
			{
				return palette;
			}

			set
			{
				palette = value;
			}
		}

		private Tile4BitList tileList;

		private uint selectColor;

		public PaletteEditor(Palette palette)
		{
			InitializeComponent();

			this.palette = palette;

			textBoxSelectColorRGB.Text = String.Format("{0:X4}", palette[(int)selectColor].get());
			numericUpDownSelectColorRed.Value = palette[(int)selectColor].Red;
			numericUpDownSelectColorGreen.Value = palette[(int)selectColor].Green;
			numericUpDownSelectColorBlue.Value = palette[(int)selectColor].Blue;
		}

		public PaletteEditor(TomatoAdventure tomatoAdventure, Palette palette, Tile4BitList tileList)
		{
			InitializeComponent();

			this.tomatoAdventure = tomatoAdventure;
			this.tileList = tileList;
			this.palette = palette;

			textBoxSelectColorRGB.Text = String.Format("{0:X4}", palette[(int)selectColor].get());
			numericUpDownSelectColorRed.Value = palette[(int)selectColor].Red;
			numericUpDownSelectColorGreen.Value = palette[(int)selectColor].Green;
			numericUpDownSelectColorBlue.Value = palette[(int)selectColor].Blue;

			listBoxPalette.SelectedIndexChanged -= listBoxPalette_SelectedIndexChanged;
			listBoxPalette.DataSource = tomatoAdventure.ObjectDictionary[typeof(Palette)];
			listBoxPalette.SelectedIndexChanged += listBoxPalette_SelectedIndexChanged;
			listBoxPalette.SelectedItem = palette;
		}

		private void pictureBoxPalette_Paint(object sender, PaintEventArgs e)
		{
			palette.draw(e.Graphics, 32, 32);

			Color color = palette[(int)selectColor].toColor();
			Pen pen = new Pen(Color.FromArgb(color.R ^ 0xFF, color.G ^ 0xFF, color.B ^ 0xFF));

			e.Graphics.DrawRectangle(pen, selectColor * 32, 0, 31, 31);
			e.Graphics.DrawRectangle(pen, (selectColor * 32) + 1, 1, 29, 29);
		}

		private void pictureBoxPalette_MouseMove(object sender, MouseEventArgs e)
		{
			if ((0 <= e.X) && (e.X < (sender as PictureBox).Width) &&
				(0 <= e.Y) && (e.Y < (sender as PictureBox).Height) &&
				e.Button == MouseButtons.Left)
			{
				selectColor = (uint)e.X / 32;

				textBoxSelectColorRGB.Text = String.Format("{0:X4}", palette[(int)selectColor].get());
				numericUpDownSelectColorRed.Value = palette[(int)selectColor].Red;
				numericUpDownSelectColorGreen.Value = palette[(int)selectColor].Green;
				numericUpDownSelectColorBlue.Value = palette[(int)selectColor].Blue;

				pictureBoxSelectColor.Refresh();
				pictureBoxPalette.Refresh();
			}
		}

		private void pictureBoxSelectColor_Paint(object sender, PaintEventArgs e)
		{
			e.Graphics.FillRectangle(new SolidBrush(palette[(int)selectColor].toColor()), 0, 0, 64, 64);
		}

		private void textBoxSelectColorRGB_TextChanged(object sender, EventArgs e)
		{
			if (((sender as TextBox).Text != null) && ((sender as TextBox).Text != ""))
			{
				try
				{
					palette[(int)selectColor].set(Convert.ToUInt16((sender as TextBox).Text, 16));

					numericUpDownSelectColorRed.Value = palette[(int)selectColor].Red;

					numericUpDownSelectColorGreen.Value = palette[(int)selectColor].Green;

					numericUpDownSelectColorBlue.Value = palette[(int)selectColor].Blue;


					pictureBoxSelectColor.Refresh();
					pictureBoxPalette.Refresh();
					pictureBoxPreview.Refresh();
				}
				catch
				{
					(sender as TextBox).Text = String.Format("{0:X4}", palette[(int)selectColor].get());
				}
			}
		}

		private void numericUpDownSelectColorRed_ValueChanged(object sender, EventArgs e)
		{
			palette[(int)selectColor].Red = (byte)(sender as NumericUpDown).Value;

			pictureBoxSelectColor.Refresh();
			pictureBoxPalette.Refresh();
			pictureBoxPreview.Refresh();
		}

		private void numericUpDownSelectColorGreen_ValueChanged(object sender, EventArgs e)
		{
			palette[(int)selectColor].Green = (byte)(sender as NumericUpDown).Value;

			pictureBoxSelectColor.Refresh();
			pictureBoxPalette.Refresh();
			pictureBoxPreview.Refresh();
		}

		private void numericUpDownSelectColorBlue_ValueChanged(object sender, EventArgs e)
		{
			palette[(int)selectColor].Blue = (byte)(sender as NumericUpDown).Value;

			pictureBoxSelectColor.Refresh();
			pictureBoxPalette.Refresh();
			pictureBoxPreview.Refresh();
		}

		private void pictureBoxPreview_Paint(object sender, PaintEventArgs e)
		{
			if (tileList != null)
			{
				((PictureBox)sender).Width = 32 * Tile.WIDTH;
				((PictureBox)sender).Height = (tileList.Count / 32) * Tile.HEIGHT;

				for (uint i = 0; i < tileList.Count; ++i)
				{
					using (Bitmap bitmap = tileList.Tile[(int)i].toBitmap(palette))
					{
						e.Graphics.DrawImage(bitmap, (i % 32) * Tile.WIDTH, (i / 32) * Tile.HEIGHT);
					}
				}
			}
		}

		private void listBoxPalette_SelectedIndexChanged(object sender, EventArgs e)
		{
			palette = (Palette)listBoxPalette.SelectedItem;
		}
		private void listBoxPalette_Format(object sender, ListControlConvertEventArgs e)
		{
			e.Value = String.Format("{0:X8}", ((Palette)e.ListItem).ObjectID);
		}

		private void ToolStripMenuItemListBoxPaletteImport_Click(object sender, EventArgs e)
		{
			openFileDialog.ShowDialog();

			try
			{
				using (Bitmap bitmap = new Bitmap(openFileDialog.FileName))
				{
					if (bitmap.PixelFormat != PixelFormat.Format4bppIndexed)
					{
						throw new Exception();
					}

					((Palette)listBoxPalette.SelectedItem).set(bitmap.Palette.Entries);
				}
			}
			catch
			{
				try
				{
					((Palette)listBoxPalette.SelectedItem).set(File.ReadAllBytes(openFileDialog.FileName));
				}
				catch
				{
				}
			}
		}
		private void ToolStripMenuItemListBoxPaletteExport_Click(object sender, EventArgs e)
		{

		}
		private void ToolStripMenuItemListBoxPaletteAdd_Click(object sender, EventArgs e)
		{

		}
		private void ToolStripMenuItemListBoxPaletteDelete_Click(object sender, EventArgs e)
		{

		}
		
		private void buttonSelect_Click(object sender, EventArgs e)
		{
			Close();
		}
	}
}
