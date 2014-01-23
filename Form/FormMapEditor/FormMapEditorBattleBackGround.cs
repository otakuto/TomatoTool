using System;
using System.Drawing;
using System.Windows.Forms;

namespace TomatoTool
{
	public partial class FormMapEditor : Form
	{

		private void pictureBoxBattleBackground_Paint(object sender, PaintEventArgs e)
		{
			((PictureBox)sender).Width = (int)BattleBackground.WIDTH;
			((PictureBox)sender).Height = (int)BattleBackground.HEIGHT;
			/*
			using(Bitmap bitmap = map.BattleBackground.toBitmap())
			{
				e.Graphics.DrawImage(bitmap, 0, 0);
			}*/
		}

		private void listBoxBattleBackground_SelectedIndexChanged(object sender, EventArgs e)
		{
			pictureBoxBattleBackground.Refresh();
		}
	}
}
