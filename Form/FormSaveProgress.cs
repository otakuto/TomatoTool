using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace TomatoTool
{
	public partial class FormSaveProgress : Form
	{
		public FormSaveProgress()
		{
			InitializeComponent();
		}

		public void setText(string text)
		{
			label1.Text = text;
		}

		public void performStep()
		{
			progressBar1.PerformStep();
		}

		public int Maximum
		{
			get
			{
				return progressBar1.Maximum;
			}

			set
			{
				progressBar1.Maximum = value;
			}
		}

	}
}
