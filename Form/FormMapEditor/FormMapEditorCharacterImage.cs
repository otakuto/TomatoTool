using System;
using System.Drawing;
using System.Windows.Forms;

namespace TomatoTool
{
	public partial class FormMapEditor
	{
		private void listBoxCharacterImage_SelectedIndexChanged(object sender, EventArgs e)
		{
			pictureBoxCharacterImage.Refresh();
			pictureBoxCharacterImageAction.Refresh();
		}
		private void listBoxCharacterImage_Format(object sender, ListControlConvertEventArgs e)
		{
			e.Value = String.Format("{0:X8}", ((CharacterImage)e.ListItem).ObjectID);
		}

		private void numericUpDownPictureBoxMapCharacterImageSize_ValueChanged(object sender, EventArgs e)
		{
			pictureBoxCharacterImage.Refresh();
		}

		private void trackBarCharacterImagePalette_Scroll(object sender, EventArgs e)
		{
			pictureBoxCharacterImage.Refresh();
		}

		private void pictureBoxCharacterImage_Paint(object sender, PaintEventArgs e)
		{
			if (listBoxCharacterImage.SelectedItem != null)
			{
				CharacterImage characterImage = (CharacterImage)listBoxCharacterImage.SelectedItem;

				numericUpDownCharacterImageSize.Maximum = characterImage.TileList.Count;
				int size = Convert.ToInt32(numericUpDownCharacterImageSize.Value);

				//テスト用横に広げるだけ
				pictureBoxCharacterImage.Height = (characterImage.TileList.Count / size) * 8 + 8;
				pictureBoxCharacterImage.Width = size * 8;

				for (int i = 0; i < characterImage.TileList.Count; ++i)
				{
					Bitmap b = characterImage.TileList[i].toBitmap(map.CharacterScriptList.Palette[trackBarCharacterImagePalette.Value]);
					e.Graphics.DrawImage(b, (i % size) * 8, (i / size) * 8);
					b.Dispose();
				}
			}
		}

		private void pictureBoxCharacterImageAction_Paint(object sender, PaintEventArgs e)
		{
			if (listBoxCharacterImage.SelectedItem != null)
			{
				((PictureBox)sender).Width = 0x0200;
				((PictureBox)sender).Height = 0x0100;

				CharacterImage characterImage = (CharacterImage)listBoxCharacterImage.SelectedItem;
				try
				{
					e.Graphics.DrawImage(characterImage.toBitmap(CharacterScriptDirection.Down, 0, map.CharacterScriptList.Palette[trackBarCharacterImagePalette.Value], true), 0, 0);
				}
				catch
				{
				}
			}
		}

		private void buttonCharacterImageEdit_Click(object sender, EventArgs e)
		{
			CharacterImageEditor characterImageEditor = new CharacterImageEditor((CharacterImage)listBoxCharacterImage.SelectedItem, map.CharacterScriptList.Palette[trackBarCharacterImagePalette.Value]);
			characterImageEditor.Show();
			characterImageEditor.Refresh();
			characterImageEditor.Activate();
		}

	}
}
