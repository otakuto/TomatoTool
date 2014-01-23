using System;
using System.Drawing;
using System.Windows.Forms;

namespace TomatoTool
{
	public partial class FormTomatoTool
		:
		Form
	{
		private void listBoxMonster_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (listBoxMonster.Items != null)
			{
				Monster monster = listBoxMonster.SelectedItem as Monster;
				controlMonsterLoad(monster);
			}
			else
			{
				controlMonsterClear();
			}
		}
		private void listBoxMonster_Format(object sender, ListControlConvertEventArgs e)
		{
			e.Value = String.Format("{0:X4}", (e.ListItem as Monster).Name.Text);
		}

		private void controlMonsterLoad(Monster monster)
		{
			if (monster != null)
			{
				textBoxMonsterName.Text = monster.Name.Text;
				textBoxMonsterHP.Text = monster.HP.ToString();
				textBoxMonsterDiffence.Text = monster.Diffence.ToString();
				textBoxMonsterExperience.Text = monster.Experience.ToString();
				textBoxMonsterMoney.Text = monster.Money.ToString();
				textBoxMonsterSpeed.Text = monster.Speed.ToString();
				textBoxMonsterSize.Text = String.Format("{0:X2}", monster.ImageSize);
				pictureBox2.Refresh();
				pictureBox3.Refresh();
			}
		}

		private void controlMonsterClear()
		{
			textBoxMonsterName.Text = null;
			textBoxMonsterHP.Text = null;
			textBoxMonsterDiffence.Text = null;
			textBoxMonsterExperience.Text = null;
			textBoxMonsterMoney.Text = null;
			textBoxMonsterSpeed.Text = null;
		}

	}
}
