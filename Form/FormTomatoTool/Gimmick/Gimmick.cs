using System;
using System.Drawing;
using System.Windows.Forms;

namespace TomatoTool
{
	public partial class FormTomatoTool
		:
		Form
	{
		private void listBoxGimmick_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (listBoxGimmick.SelectedItem != null)
			{
				Gimmick gimmick = listBoxGimmick.SelectedItem as Gimmick;
				pictureBoxGimmick.Refresh();
				controlGimmickLoad(gimmick);
			}
		}
		private void listBoxGimmick_Format(object sender, ListControlConvertEventArgs e)
		{
			e.Value = String.Format("{0:X4}", (e.ListItem as Gimmick).Name.Text);
		}

		private void pictureBoxGimmick_Paint(object sender, PaintEventArgs e)
		{
			if (listBoxGimmick.SelectedItem != null)
			{
				Gimmick gimmick = listBoxGimmick.SelectedItem as Gimmick;

				(sender as PictureBox).Width = 208;
				(sender as PictureBox).Height = 48;

				for (int y = 0; y < 4; ++y)
				{
					for (int x = 0; x < 4; ++x)
					{
						Bitmap b = gimmick.Icon[(y * 4) + x].toBitmap(gimmick.Palette);
						e.Graphics.DrawImage(b, x * TomatoTool.Tile.WIDTH, y * TomatoTool.Tile.HEIGHT);
						//b.Dispose();
					}
				}

				e.Graphics.DrawImage(tomatoAdventure.StatusCharacterFontList.toBitmap(gimmick.Name), 0, 32);
				e.Graphics.DrawImage(tomatoAdventure.StatusCharacterFontList.toBitmap(gimmick.DescriptionBattle), 0, 40);
			}
		}

		private void controlGimmickLoad(Gimmick gimmick)
		{
			if (gimmick != null)
			{
				textBoxGimmickName.Text = gimmick.Name.Text;
				textBoxGimmickUses.Text = gimmick.Uses.ToString();
				textBoxGimmickAttack.Text = gimmick.Attack.ToString();
				textBoxGimmickBattery.Text = gimmick.BatteryMax.ToString();
				textBoxGimmickDescriptionBattle.Text = gimmick.DescriptionBattle.Text;

				for (uint i = 0; i < textBoxGimmickLv.GetLength(0); ++i)
				{
					textBoxGimmickLv[i].Text = gimmick.Level[i].ToString();
				}
			}
		}

	}
}
