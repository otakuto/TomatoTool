using System;
using System.Drawing;
using System.Windows.Forms;

namespace TomatoTool
{
	public partial class FormMapEditor : Form
	{
		private void comboBoxAnimationTileSetList_SelectedIndexChanged(object sender, EventArgs e)
		{
			map.MapTile.AnimationTileSet = (AnimationTileSet)comboBoxAnimationTileSetList.SelectedItem;

			listBoxAnimationTileTileList.SelectedIndexChanged -= listBoxAnimationTileTileList_SelectedIndexChanged;
			listBoxAnimationTileTileList.DataSource = map.MapTile.AnimationTileSet.AnimationTile[0].TileList;
			listBoxAnimationTileTileList.SelectedIndexChanged += listBoxAnimationTileTileList_SelectedIndexChanged;
			listBoxAnimationTileTileList.SelectedItem = null;
		}
		private void comboBoxAnimationTileSetList_Format(object sender, ListControlConvertEventArgs e)
		{
			e.Value = String.Format("{0:X8}", ((AnimationTileSet)e.ListItem).ObjectID);
		}

		private void listBoxAnimationTileTileList_SelectedIndexChanged(object sender, EventArgs e)
		{
			pictureBoxAnimationTileTileList.Refresh();

			comboBoxAnimationTileTile4BitList.SelectedIndexChanged -= comboBoxAnimationTileTile4BitList_SelectedIndexChanged;
			comboBoxAnimationTileTile4BitList.DataSource = tomatoAdventure.ObjectDictionary[typeof(Tile4BitList)];
			comboBoxAnimationTileTile4BitList.SelectedIndexChanged += comboBoxAnimationTileTile4BitList_SelectedIndexChanged;
			comboBoxAnimationTileTile4BitList.SelectedItem = listBoxAnimationTileTileList.SelectedItem;
		}
		private void listBoxAnimationTileTileList_Format(object sender, ListControlConvertEventArgs e)
		{
			e.Value = String.Format("{0:X8}", ((Tile4BitList)e.ListItem).ObjectID);
		}

		private void pictureBoxAnimationTileTileList_Paint(object sender, PaintEventArgs e)
		{
			if (listBoxAnimationTileTileList.SelectedItem != null)
			{
				Tile4BitList TileList = (Tile4BitList)listBoxAnimationTileTileList.SelectedItem;

				((PictureBox)sender).Width = TileList.Tile.Count * Tile.WIDTH;
				((PictureBox)sender).Height = Tile.HEIGHT;

				for (int i = 0; i < TileList.Tile.Count; ++i)
				{
					using (Bitmap bitmap = TileList.Tile[i].toBitmap(Palette.GrayScale))
					{
						e.Graphics.DrawImage(bitmap, i * Tile.WIDTH, 0);
					}
				}
			}
		}

		private void pictureBoxAnimationTileTile4BitList_Paint(object sender, PaintEventArgs e)
		{
			if (comboBoxAnimationTileTile4BitList.SelectedItem != null)
			{
				Tile4BitList TileList = (Tile4BitList)comboBoxAnimationTileTile4BitList.SelectedItem;

				((PictureBox)sender).Width = TileList.Tile.Count * Tile.WIDTH;
				((PictureBox)sender).Height = Tile.HEIGHT;

				for (int i = 0; i < TileList.Tile.Count; ++i)
				{
					using (Bitmap bitmap = TileList.Tile[i].toBitmap(Palette.GrayScale))
					{
						e.Graphics.DrawImage(bitmap, i * Tile.WIDTH, 0);
					}
				}
			}
		}

		private void comboBoxAnimationTileTile4BitList_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (listBoxAnimationTileTileList.SelectedItem != null)
			{
				map.MapTile.AnimationTileSet.AnimationTile[0].TileList[listBoxAnimationTileTileList.SelectedIndex] = (Tile4BitList)comboBoxAnimationTileTile4BitList.SelectedItem;

				pictureBoxAnimationTileTile4BitList.Refresh();
				pictureBoxAnimationTileTileList.Refresh();
			}
			else
			{
				comboBoxAnimationTileTile4BitList.SelectedIndexChanged -= comboBoxAnimationTileTile4BitList_SelectedIndexChanged;
				comboBoxAnimationTileTile4BitList.DataSource = null;
				comboBoxAnimationTileTile4BitList.SelectedIndexChanged += comboBoxAnimationTileTile4BitList_SelectedIndexChanged;

				pictureBoxAnimationTileTile4BitList.Refresh();
			}
		}
		private void comboBoxAnimationTileTile4BitList_Format(object sender, ListControlConvertEventArgs e)
		{
			e.Value = String.Format("{0:X8}", ((Tile4BitList)e.ListItem).ObjectID);
		}
	}
}
