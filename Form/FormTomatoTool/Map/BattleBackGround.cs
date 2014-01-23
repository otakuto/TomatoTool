using System;
using System.Drawing;
using System.Windows.Forms;

namespace TomatoTool
{
	public partial class FormTomatoTool
		:
		Form
	{
		private void initializeBattleBackground()
		{
			listBoxBattleBackground.DataSource = tomatoAdventure.ObjectDictionary[typeof(BattleBackground)];
		}

		private void pictureBoxBattleBackground_Paint(object sender, PaintEventArgs e)
		{
			((PictureBox)sender).Width = (int)BattleBackground.WIDTH;
			((PictureBox)sender).Height = (int)BattleBackground.HEIGHT;

			using (Bitmap bitmap = ((BattleBackground)tomatoAdventure.ObjectDictionary[typeof(BattleBackground)][((Map)listBoxMap.SelectedItem).BattleBackgroundNumber]).toBitmap())
			{
				e.Graphics.DrawImage(bitmap, 0, 0);
			}
		}

		private void listBoxBattleBackground_SelectedIndexChanged(object sender, EventArgs e)
		{
			((Map)listBoxMap.SelectedItem).BattleBackgroundNumber = (byte)listBoxBattleBackground.SelectedIndex;

			pictureBoxBattleBackground.Refresh();
		}

		private void listBoxBattleBackground_Format(object sender, ListControlConvertEventArgs e)
		{
			e.Value = String.Format("{0:X2}", ((BattleBackground)e.ListItem).ObjectID);
		}
	}
}
