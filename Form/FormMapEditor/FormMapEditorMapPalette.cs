using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TomatoTool
{
	public partial class FormMapEditor : Form
	{
		private void listBoxMapPalette_SelectedIndexChanged(object sender, EventArgs e)
		{
			pictureBoxMapPalette.Refresh();
		}

		private void listBoxMapPalette_Format(object sender, ListControlConvertEventArgs e)
		{
			e.Value = String.Format("{0:X8}", ((Palette)e.ListItem).ObjectID);
		}

		private void pictureBoxMapPalette_Paint(object sender, PaintEventArgs e)
		{
			if (listBoxMapPalette.SelectedItem != null)
			{
				((PictureBox)sender).Width = 32 * 16;
				((PictureBox)sender).Height = 32;

				Palette palette = (Palette)listBoxMapPalette.SelectedItem;

				palette.draw(e.Graphics, 32, 32);
			}
		}

		private void buttonMapPaletteEdit_Click(object sender, EventArgs e)
		{
			if (listBoxMapPalette.SelectedItem != null)
			{
				PaletteEditor PaletteEditor = new PaletteEditor(tomatoAdventure, (Palette)listBoxMapPalette.SelectedItem, map.MapTile.MainTile);
				PaletteEditor.ShowDialog();
				map.Palette[listBoxMapPalette.SelectedIndex] = PaletteEditor.Palette;
			}
		}
	}
}
