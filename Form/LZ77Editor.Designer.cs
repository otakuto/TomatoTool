namespace TomatoTool
{
	partial class LZ77Editor
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
			this.listBoxLZ77 = new System.Windows.Forms.ListBox();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.splitContainer2 = new System.Windows.Forms.SplitContainer();
			this.pictureBoxLZ77 = new System.Windows.Forms.PictureBox();
			this.buttonSelect = new System.Windows.Forms.Button();
			this.buttonDelete = new System.Windows.Forms.Button();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.pictureBoxLZ77Palette = new System.Windows.Forms.PictureBox();
			this.checkBoxLZ77PaletteGrayScale = new System.Windows.Forms.CheckBox();
			this.comboBoxLZ77Palette = new System.Windows.Forms.ComboBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.radioButtonLZ774Bit = new System.Windows.Forms.RadioButton();
			this.radioButtonLZ778Bit = new System.Windows.Forms.RadioButton();
			this.buttonLZ77Add = new System.Windows.Forms.Button();
			this.buttonLZ77Import = new System.Windows.Forms.Button();
			this.buttonLZ77Export = new System.Windows.Forms.Button();
			this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
			this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
			this.statusStrip = new System.Windows.Forms.StatusStrip();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
			this.splitContainer2.Panel1.SuspendLayout();
			this.splitContainer2.Panel2.SuspendLayout();
			this.splitContainer2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxLZ77)).BeginInit();
			this.groupBox2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxLZ77Palette)).BeginInit();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// listBoxLZ77
			// 
			this.listBoxLZ77.Dock = System.Windows.Forms.DockStyle.Fill;
			this.listBoxLZ77.FormattingEnabled = true;
			this.listBoxLZ77.ItemHeight = 12;
			this.listBoxLZ77.Location = new System.Drawing.Point(0, 0);
			this.listBoxLZ77.Margin = new System.Windows.Forms.Padding(0);
			this.listBoxLZ77.Name = "listBoxLZ77";
			this.listBoxLZ77.Size = new System.Drawing.Size(196, 569);
			this.listBoxLZ77.TabIndex = 0;
			this.listBoxLZ77.SelectedIndexChanged += new System.EventHandler(this.listBoxLZ77_SelectedIndexChanged);
			this.listBoxLZ77.Format += new System.Windows.Forms.ListControlConvertEventHandler(this.listBoxLZ77_Format);
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
			this.splitContainer1.Panel1.Controls.Add(this.listBoxLZ77);
			this.splitContainer1.Panel1MinSize = 0;
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
			this.splitContainer1.Panel2MinSize = 0;
			this.splitContainer1.Size = new System.Drawing.Size(892, 573);
			this.splitContainer1.SplitterDistance = 200;
			this.splitContainer1.TabIndex = 1;
			// 
			// splitContainer2
			// 
			this.splitContainer2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
			this.splitContainer2.Location = new System.Drawing.Point(0, 0);
			this.splitContainer2.Margin = new System.Windows.Forms.Padding(0);
			this.splitContainer2.Name = "splitContainer2";
			// 
			// splitContainer2.Panel1
			// 
			this.splitContainer2.Panel1.AutoScroll = true;
			this.splitContainer2.Panel1.Controls.Add(this.pictureBoxLZ77);
			this.splitContainer2.Panel1MinSize = 0;
			// 
			// splitContainer2.Panel2
			// 
			this.splitContainer2.Panel2.Controls.Add(this.buttonSelect);
			this.splitContainer2.Panel2.Controls.Add(this.buttonDelete);
			this.splitContainer2.Panel2.Controls.Add(this.groupBox2);
			this.splitContainer2.Panel2.Controls.Add(this.groupBox1);
			this.splitContainer2.Panel2.Controls.Add(this.buttonLZ77Add);
			this.splitContainer2.Panel2.Controls.Add(this.buttonLZ77Import);
			this.splitContainer2.Panel2.Controls.Add(this.buttonLZ77Export);
			this.splitContainer2.Panel2MinSize = 0;
			this.splitContainer2.Size = new System.Drawing.Size(688, 573);
			this.splitContainer2.SplitterDistance = 400;
			this.splitContainer2.TabIndex = 3;
			// 
			// pictureBoxLZ77
			// 
			this.pictureBoxLZ77.Location = new System.Drawing.Point(0, 0);
			this.pictureBoxLZ77.Margin = new System.Windows.Forms.Padding(0);
			this.pictureBoxLZ77.Name = "pictureBoxLZ77";
			this.pictureBoxLZ77.Size = new System.Drawing.Size(16, 16);
			this.pictureBoxLZ77.TabIndex = 2;
			this.pictureBoxLZ77.TabStop = false;
			this.pictureBoxLZ77.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBoxLZ77_Paint);
			// 
			// buttonSelect
			// 
			this.buttonSelect.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.buttonSelect.Location = new System.Drawing.Point(3, 174);
			this.buttonSelect.Name = "buttonSelect";
			this.buttonSelect.Size = new System.Drawing.Size(75, 23);
			this.buttonSelect.TabIndex = 10;
			this.buttonSelect.Text = "Select";
			this.buttonSelect.UseVisualStyleBackColor = true;
			// 
			// buttonDelete
			// 
			this.buttonDelete.Location = new System.Drawing.Point(84, 145);
			this.buttonDelete.Name = "buttonDelete";
			this.buttonDelete.Size = new System.Drawing.Size(75, 23);
			this.buttonDelete.TabIndex = 9;
			this.buttonDelete.Text = "Delete";
			this.buttonDelete.UseVisualStyleBackColor = true;
			// 
			// groupBox2
			// 
			this.groupBox2.AutoSize = true;
			this.groupBox2.Controls.Add(this.pictureBoxLZ77Palette);
			this.groupBox2.Controls.Add(this.checkBoxLZ77PaletteGrayScale);
			this.groupBox2.Controls.Add(this.comboBoxLZ77Palette);
			this.groupBox2.Location = new System.Drawing.Point(3, 10);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(268, 100);
			this.groupBox2.TabIndex = 8;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "パレット";
			// 
			// pictureBoxLZ77Palette
			// 
			this.pictureBoxLZ77Palette.Location = new System.Drawing.Point(6, 66);
			this.pictureBoxLZ77Palette.Name = "pictureBoxLZ77Palette";
			this.pictureBoxLZ77Palette.Size = new System.Drawing.Size(256, 16);
			this.pictureBoxLZ77Palette.TabIndex = 11;
			this.pictureBoxLZ77Palette.TabStop = false;
			this.pictureBoxLZ77Palette.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBoxLZ77Palette_Paint);
			// 
			// checkBoxLZ77PaletteGrayScale
			// 
			this.checkBoxLZ77PaletteGrayScale.AutoSize = true;
			this.checkBoxLZ77PaletteGrayScale.Location = new System.Drawing.Point(6, 18);
			this.checkBoxLZ77PaletteGrayScale.Name = "checkBoxLZ77PaletteGrayScale";
			this.checkBoxLZ77PaletteGrayScale.Size = new System.Drawing.Size(76, 16);
			this.checkBoxLZ77PaletteGrayScale.TabIndex = 4;
			this.checkBoxLZ77PaletteGrayScale.Text = "GrayScale";
			this.checkBoxLZ77PaletteGrayScale.UseVisualStyleBackColor = true;
			this.checkBoxLZ77PaletteGrayScale.Click += new System.EventHandler(this.checkBoxLZ77PaletteGrayScale_Click);
			// 
			// comboBoxLZ77Palette
			// 
			this.comboBoxLZ77Palette.FormattingEnabled = true;
			this.comboBoxLZ77Palette.Location = new System.Drawing.Point(6, 40);
			this.comboBoxLZ77Palette.Name = "comboBoxLZ77Palette";
			this.comboBoxLZ77Palette.Size = new System.Drawing.Size(121, 20);
			this.comboBoxLZ77Palette.TabIndex = 3;
			this.comboBoxLZ77Palette.SelectionChangeCommitted += new System.EventHandler(this.comboBoxLZ77Palette_SelectionChangeCommitted);
			this.comboBoxLZ77Palette.Format += new System.Windows.Forms.ListControlConvertEventHandler(this.comboBoxLZ77Palette_Format);
			// 
			// groupBox1
			// 
			this.groupBox1.AutoSize = true;
			this.groupBox1.Controls.Add(this.radioButtonLZ774Bit);
			this.groupBox1.Controls.Add(this.radioButtonLZ778Bit);
			this.groupBox1.Location = new System.Drawing.Point(3, 203);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(56, 74);
			this.groupBox1.TabIndex = 7;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "色数";
			// 
			// radioButtonLZ774Bit
			// 
			this.radioButtonLZ774Bit.AutoSize = true;
			this.radioButtonLZ774Bit.Checked = true;
			this.radioButtonLZ774Bit.Location = new System.Drawing.Point(6, 18);
			this.radioButtonLZ774Bit.Name = "radioButtonLZ774Bit";
			this.radioButtonLZ774Bit.Size = new System.Drawing.Size(44, 16);
			this.radioButtonLZ774Bit.TabIndex = 5;
			this.radioButtonLZ774Bit.TabStop = true;
			this.radioButtonLZ774Bit.Text = "4Bit";
			this.radioButtonLZ774Bit.UseVisualStyleBackColor = true;
			// 
			// radioButtonLZ778Bit
			// 
			this.radioButtonLZ778Bit.AutoSize = true;
			this.radioButtonLZ778Bit.Location = new System.Drawing.Point(6, 40);
			this.radioButtonLZ778Bit.Name = "radioButtonLZ778Bit";
			this.radioButtonLZ778Bit.Size = new System.Drawing.Size(44, 16);
			this.radioButtonLZ778Bit.TabIndex = 6;
			this.radioButtonLZ778Bit.Text = "8Bit";
			this.radioButtonLZ778Bit.UseVisualStyleBackColor = true;
			// 
			// buttonLZ77Add
			// 
			this.buttonLZ77Add.Location = new System.Drawing.Point(84, 116);
			this.buttonLZ77Add.Name = "buttonLZ77Add";
			this.buttonLZ77Add.Size = new System.Drawing.Size(75, 23);
			this.buttonLZ77Add.TabIndex = 2;
			this.buttonLZ77Add.Text = "Add";
			this.buttonLZ77Add.UseVisualStyleBackColor = true;
			this.buttonLZ77Add.Click += new System.EventHandler(this.buttonLZ77Add_Click);
			// 
			// buttonLZ77Import
			// 
			this.buttonLZ77Import.Location = new System.Drawing.Point(3, 145);
			this.buttonLZ77Import.Name = "buttonLZ77Import";
			this.buttonLZ77Import.Size = new System.Drawing.Size(75, 23);
			this.buttonLZ77Import.TabIndex = 1;
			this.buttonLZ77Import.Text = "Import";
			this.buttonLZ77Import.UseVisualStyleBackColor = true;
			this.buttonLZ77Import.Click += new System.EventHandler(this.buttonLZ77Import_Click);
			// 
			// buttonLZ77Export
			// 
			this.buttonLZ77Export.Location = new System.Drawing.Point(3, 116);
			this.buttonLZ77Export.Name = "buttonLZ77Export";
			this.buttonLZ77Export.Size = new System.Drawing.Size(75, 23);
			this.buttonLZ77Export.TabIndex = 0;
			this.buttonLZ77Export.Text = "Export";
			this.buttonLZ77Export.UseVisualStyleBackColor = true;
			this.buttonLZ77Export.Click += new System.EventHandler(this.buttonLZ77Export_Click);
			// 
			// saveFileDialog
			// 
			this.saveFileDialog.Filter = "PNG(*.png)|*.png";
			this.saveFileDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.saveFileDialog_FileOk);
			// 
			// openFileDialog
			// 
			this.openFileDialog.Filter = "PNG(*.png)|*.png";
			this.openFileDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog_FileOk);
			// 
			// statusStrip
			// 
			this.statusStrip.Location = new System.Drawing.Point(0, 551);
			this.statusStrip.Name = "statusStrip";
			this.statusStrip.Size = new System.Drawing.Size(892, 22);
			this.statusStrip.TabIndex = 2;
			// 
			// LZ77Editor
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(892, 573);
			this.Controls.Add(this.statusStrip);
			this.Controls.Add(this.splitContainer1);
			this.Name = "LZ77Editor";
			this.Text = "LZ77Editor";
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			this.splitContainer2.Panel1.ResumeLayout(false);
			this.splitContainer2.Panel2.ResumeLayout(false);
			this.splitContainer2.Panel2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
			this.splitContainer2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxLZ77)).EndInit();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxLZ77Palette)).EndInit();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ListBox listBoxLZ77;
		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.PictureBox pictureBoxLZ77;
		private System.Windows.Forms.SplitContainer splitContainer2;
		private System.Windows.Forms.RadioButton radioButtonLZ778Bit;
		private System.Windows.Forms.RadioButton radioButtonLZ774Bit;
		private System.Windows.Forms.CheckBox checkBoxLZ77PaletteGrayScale;
		private System.Windows.Forms.ComboBox comboBoxLZ77Palette;
		private System.Windows.Forms.Button buttonLZ77Add;
		private System.Windows.Forms.Button buttonLZ77Import;
		private System.Windows.Forms.Button buttonLZ77Export;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Button buttonSelect;
		private System.Windows.Forms.Button buttonDelete;
		private System.Windows.Forms.PictureBox pictureBoxLZ77Palette;
		private System.Windows.Forms.SaveFileDialog saveFileDialog;
		private System.Windows.Forms.OpenFileDialog openFileDialog;
		private System.Windows.Forms.StatusStrip statusStrip;
	}
}