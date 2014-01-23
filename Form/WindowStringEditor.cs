using System;
using System.Windows.Forms;

namespace TomatoTool
{
	public partial class WindowStringEditor : Form
	{
		private WindowString windowString;

		public WindowStringEditor(TomatoAdventure tomatoAdventure, WindowString windowString)
		{
			InitializeComponent();
			controlWindowStringLoad(tomatoAdventure, windowString);
		}

		private void controlWindowStringLoad(TomatoAdventure tomatoAdventure, WindowString windowString)
		{
			this.windowString = windowString;

			listBoxWindowString.DataSource = tomatoAdventure.ObjectDictionary[typeof(WindowString)];
			
			if (windowString != null)
			{
				listBoxWindowString.SelectedItem = windowString;
			}
		}

		private void listBoxWindowString_Format(object sender, ListControlConvertEventArgs e)
		{
			e.Value = String.Format("{0:X8}", ((WindowString)e.ListItem).ObjectID);
		}

		private void listBoxWindowString_SelectedIndexChanged(object sender, EventArgs e)
		{
			textBoxWindowString.Text = ((WindowString)listBoxWindowString.SelectedItem).Text;
		}

		private void textBoxWindowString_TextChanged(object sender, EventArgs e)
		{
			pictureBoxWindowString.Refresh();
		}

		private void pictureBoxWindowString_Paint(object sender, PaintEventArgs e)
		{

		}
	}
}
