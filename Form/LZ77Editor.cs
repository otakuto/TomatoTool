using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace TomatoTool
{
	public partial class LZ77Editor : Form
	{
		private string filePath;

		private TomatoAdventure tomatoAdventure;

		private LZ77 lz77;
		public LZ77 LZ77
		{
			get
			{
				return lz77;
			}
		}

		public LZ77Editor(TomatoAdventure tomatoAdventure, LZ77 lz77, Palette palette)
		{
			InitializeComponent();
			controlLZ77Load(tomatoAdventure, lz77, palette);
		}

		private void controlLZ77Load(TomatoAdventure tomatoAdventure, LZ77 lz77, Palette palette)
		{
			this.tomatoAdventure = tomatoAdventure;
			this.lz77 = lz77;

			listBoxLZ77.DataSource = tomatoAdventure.ObjectDictionary[typeof(LZ77)];
			listBoxLZ77.SelectedItem = lz77;

			comboBoxLZ77Palette.DataSource = tomatoAdventure.ObjectDictionary[typeof(Palette)];
			comboBoxLZ77Palette.SelectedItem = palette;
		}

		private void pictureBoxLZ77_Paint(object sender, PaintEventArgs e)
		{
			if (listBoxLZ77.SelectedItem != null)
			{
				LZ77 lz77 = (LZ77)listBoxLZ77.SelectedItem;
				
				(sender as PictureBox).Width = 256;
				(sender as PictureBox).Height = (lz77.Count / 32) * 8;

				Palette palette;
				if (checkBoxLZ77PaletteGrayScale.Checked)
				{
					palette = Palette.GrayScale;
				}
				else
				{
					palette = (Palette)comboBoxLZ77Palette.SelectedItem;
				}

				for (int i = 0; i < lz77.Count; ++i)
				{
					Bitmap bitmap = lz77[i].toBitmap(palette);
					e.Graphics.DrawImage(bitmap, (i % 32) * 8, (i / 32) * 8);
					bitmap.Dispose();
				}
			}
		}

		private void listBoxLZ77_Format(object sender, ListControlConvertEventArgs e)
		{
			e.Value = String.Format("{0:X8}", ((LZ77)e.ListItem).ObjectID);
		}

		private void listBoxLZ77_SelectedIndexChanged(object sender, EventArgs e)
		{
			lz77 = (LZ77)listBoxLZ77.SelectedItem;
			pictureBoxLZ77.Refresh();
		}


		private void comboBoxLZ77Palette_Format(object sender, ListControlConvertEventArgs e)
		{
			e.Value = String.Format("{0:X8}", ((Palette)e.ListItem).ObjectID);
		}

		private void comboBoxLZ77Palette_SelectionChangeCommitted(object sender, EventArgs e)
		{
			pictureBoxLZ77.Refresh();
		}

		private void checkBoxLZ77PaletteGrayScale_Click(object sender, EventArgs e)
		{
			pictureBoxLZ77.Refresh();
		}


		private void buttonLZ77Export_Click(object sender, EventArgs e)
		{
			saveFileDialog.ShowDialog();
		}

		private void buttonLZ77Import_Click(object sender, EventArgs e)
		{
			try
			{
				openFileDialog.ShowDialog();

				LZ77 lz77 = ((LZ77)listBoxLZ77.SelectedItem);
				
				using (Bitmap bitmap = new Bitmap(filePath))
				{
					if (bitmap.PixelFormat != PixelFormat.Format4bppIndexed)
					{
						throw new Exception();
					}

					BitmapData bitmapData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadWrite, PixelFormat.Format4bppIndexed);
					IntPtr intPtr = bitmapData.Scan0;
					byte[] data = new byte[Math.Abs(bitmapData.Stride) * bitmap.Height];
					Marshal.Copy(intPtr, data, 0, data.GetLength(0));
					bitmap.UnlockBits(bitmapData);

					Tile4Bit[] tile = new Tile4Bit[(bitmap.Height / Tile.HEIGHT) * 32];

					for (int i = 0; i < tile.GetLength(0); ++i)
					{
						byte[,] buffer = new byte[Tile4Bit.TILE_LENGTH_0, Tile4Bit.TILE_LENGTH_1];
						for (int y = 0; y < Tile4Bit.TILE_LENGTH_1; ++y)
						{
							for (int x = 0; x < Tile4Bit.TILE_LENGTH_0; ++x)
							{
								buffer[x, y] = (byte)(((data[((i / 32) * 1024) + ((i % 32) * 4) + (y * 128) + x] & 0xF0) >> 4) + ((data[((i / 32) * 1024) + ((i % 32) * 4) + (y * 128) + x] & 0x0F) << 4));
							}
						}
						tile[i] = new Tile4Bit(buffer);
					}
					lz77.Tile = new List<Tile4Bit>(tile);
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.ToString(), this.Name + " - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void buttonLZ77Add_Click(object sender, EventArgs e)
		{
			openFileDialog.ShowDialog();
		}

		private void pictureBoxLZ77Palette_Paint(object sender, PaintEventArgs e)
		{
			Palette palette;
			if (checkBoxLZ77PaletteGrayScale.Checked)
			{
				palette = Palette.GrayScale;
			}
			else
			{
				palette = ((Palette)comboBoxLZ77Palette.SelectedItem);
			}

			palette.draw(e.Graphics, 16, 16);
		}

		private void openFileDialog_FileOk(object sender, CancelEventArgs e)
		{
			filePath = openFileDialog.FileName;
		}

		private void saveFileDialog_FileOk(object sender, CancelEventArgs e)
		{
			try
			{
				filePath = saveFileDialog.FileName;

				Tile4BitList tileList = (LZ77)listBoxLZ77.SelectedItem;
				Palette palette;
				if (checkBoxLZ77PaletteGrayScale.Checked)
				{
					palette = Palette.GrayScale;
				}
				else
				{
					palette = (Palette)comboBoxLZ77Palette.SelectedItem;
				}

				using (Bitmap bitmap = new Bitmap(Tile.WIDTH * 32, (tileList.Count / 32) * Tile.HEIGHT, PixelFormat.Format4bppIndexed))
				{
					BitmapData bitmapData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadWrite, PixelFormat.Format4bppIndexed);
					IntPtr intPtr = bitmapData.Scan0;
					byte[] data = new byte[Math.Abs(bitmapData.Stride) * bitmap.Height];

					for (int i = 0; i < tileList.Count; ++i)
					{
						for (int y = 0; y < Tile4Bit.TILE_LENGTH_1; ++y)
						{
							for (int x = 0; x < Tile4Bit.TILE_LENGTH_0; ++x)
							{
								data[((i / 32) * 1024) + ((i % 32) * 4) + (y * 128) + x] = (byte)(((tileList[i][x, y] & 0xF0) >> 4) + ((tileList[i][x, y] & 0x0F) << 4));
							}
						}
					}

					Marshal.Copy(data, 0, intPtr, data.GetLength(0));
					bitmap.UnlockBits(bitmapData);

					ColorPalette colorPalette = bitmap.Palette;
					for (int i = 0; i < colorPalette.Entries.Length; ++i)
					{
						colorPalette.Entries[i] = palette[i].toColor();
					}
					bitmap.Palette = colorPalette;

					bitmap.Save(filePath, ImageFormat.Png);
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.ToString(), this.Name + " - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
	}
}
