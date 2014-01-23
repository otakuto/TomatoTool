namespace TomatoTool
{
	partial class WindowStringEditor
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.listBoxWindowString = new System.Windows.Forms.ListBox();
			this.buttonWindowStringSave = new System.Windows.Forms.Button();
			this.pictureBoxWindowString = new System.Windows.Forms.PictureBox();
			this.textBoxWindowString = new System.Windows.Forms.TextBox();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxWindowString)).BeginInit();
			this.SuspendLayout();
			// 
			// splitContainer1
			// 
			this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
			this.splitContainer1.Location = new System.Drawing.Point(0, 0);
			this.splitContainer1.Margin = new System.Windows.Forms.Padding(0);
			this.splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.listBoxWindowString);
			this.splitContainer1.Panel1MinSize = 0;
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.buttonWindowStringSave);
			this.splitContainer1.Panel2.Controls.Add(this.pictureBoxWindowString);
			this.splitContainer1.Panel2.Controls.Add(this.textBoxWindowString);
			this.splitContainer1.Panel2MinSize = 0;
			this.splitContainer1.Size = new System.Drawing.Size(892, 573);
			this.splitContainer1.SplitterDistance = 200;
			this.splitContainer1.TabIndex = 0;
			// 
			// listBoxWindowString
			// 
			this.listBoxWindowString.Dock = System.Windows.Forms.DockStyle.Fill;
			this.listBoxWindowString.FormattingEnabled = true;
			this.listBoxWindowString.ItemHeight = 12;
			this.listBoxWindowString.Location = new System.Drawing.Point(0, 0);
			this.listBoxWindowString.Margin = new System.Windows.Forms.Padding(0);
			this.listBoxWindowString.Name = "listBoxWindowString";
			this.listBoxWindowString.Size = new System.Drawing.Size(196, 569);
			this.listBoxWindowString.TabIndex = 0;
			this.listBoxWindowString.SelectedIndexChanged += new System.EventHandler(this.listBoxWindowString_SelectedIndexChanged);
			this.listBoxWindowString.Format += new System.Windows.Forms.ListControlConvertEventHandler(this.listBoxWindowString_Format);
			// 
			// buttonWindowStringSave
			// 
			this.buttonWindowStringSave.Location = new System.Drawing.Point(357, 430);
			this.buttonWindowStringSave.Name = "buttonWindowStringSave";
			this.buttonWindowStringSave.Size = new System.Drawing.Size(75, 23);
			this.buttonWindowStringSave.TabIndex = 2;
			this.buttonWindowStringSave.Text = "Save";
			this.buttonWindowStringSave.UseVisualStyleBackColor = true;
			// 
			// pictureBoxWindowString
			// 
			this.pictureBoxWindowString.Location = new System.Drawing.Point(41, 95);
			this.pictureBoxWindowString.Name = "pictureBoxWindowString";
			this.pictureBoxWindowString.Size = new System.Drawing.Size(100, 50);
			this.pictureBoxWindowString.TabIndex = 1;
			this.pictureBoxWindowString.TabStop = false;
			this.pictureBoxWindowString.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBoxWindowString_Paint);
			// 
			// textBoxWindowString
			// 
			this.textBoxWindowString.Location = new System.Drawing.Point(3, 312);
			this.textBoxWindowString.Multiline = true;
			this.textBoxWindowString.Name = "textBoxWindowString";
			this.textBoxWindowString.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.textBoxWindowString.Size = new System.Drawing.Size(512, 112);
			this.textBoxWindowString.TabIndex = 0;
			this.textBoxWindowString.TextChanged += new System.EventHandler(this.textBoxWindowString_TextChanged);
			// 
			// WindowStringEditor
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(892, 573);
			this.Controls.Add(this.splitContainer1);
			this.Name = "WindowStringEditor";
			this.Text = "WindowStringEditor";
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			this.splitContainer1.Panel2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxWindowString)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.ListBox listBoxWindowString;
		private System.Windows.Forms.PictureBox pictureBoxWindowString;
		private System.Windows.Forms.TextBox textBoxWindowString;
		private System.Windows.Forms.Button buttonWindowStringSave;
	}
}