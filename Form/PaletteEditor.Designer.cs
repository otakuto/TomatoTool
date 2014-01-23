namespace TomatoTool
{
	partial class PaletteEditor
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
			this.components = new System.ComponentModel.Container();
			this.pictureBoxPalette = new System.Windows.Forms.PictureBox();
			this.textBoxSelectColorRGB = new System.Windows.Forms.TextBox();
			this.numericUpDownSelectColorRed = new System.Windows.Forms.NumericUpDown();
			this.numericUpDownSelectColorGreen = new System.Windows.Forms.NumericUpDown();
			this.numericUpDownSelectColorBlue = new System.Windows.Forms.NumericUpDown();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.pictureBoxSelectColor = new System.Windows.Forms.PictureBox();
			this.pictureBoxPreview = new System.Windows.Forms.PictureBox();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.listBoxPalette = new System.Windows.Forms.ListBox();
			this.contextMenuStripListBoxPalette = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.ToolStripMenuItemListBoxPaletteImport = new System.Windows.Forms.ToolStripMenuItem();
			this.ToolStripMenuItemListBoxPaletteExport = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
			this.ToolStripMenuItemListBoxPaletteAdd = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
			this.ToolStripMenuItemListBoxPaletteDelete = new System.Windows.Forms.ToolStripMenuItem();
			this.buttonSelect = new System.Windows.Forms.Button();
			this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
			this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxPalette)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownSelectColorRed)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownSelectColorGreen)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownSelectColorBlue)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxSelectColor)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxPreview)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.contextMenuStripListBoxPalette.SuspendLayout();
			this.SuspendLayout();
			// 
			// pictureBoxPalette
			// 
			this.pictureBoxPalette.Location = new System.Drawing.Point(0, 0);
			this.pictureBoxPalette.Margin = new System.Windows.Forms.Padding(0);
			this.pictureBoxPalette.Name = "pictureBoxPalette";
			this.pictureBoxPalette.Size = new System.Drawing.Size(512, 32);
			this.pictureBoxPalette.TabIndex = 0;
			this.pictureBoxPalette.TabStop = false;
			this.pictureBoxPalette.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBoxPalette_Paint);
			this.pictureBoxPalette.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBoxPalette_MouseMove);
			this.pictureBoxPalette.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBoxPalette_MouseMove);
			// 
			// textBoxSelectColorRGB
			// 
			this.textBoxSelectColorRGB.Location = new System.Drawing.Point(110, 35);
			this.textBoxSelectColorRGB.MaxLength = 4;
			this.textBoxSelectColorRGB.Name = "textBoxSelectColorRGB";
			this.textBoxSelectColorRGB.Size = new System.Drawing.Size(48, 19);
			this.textBoxSelectColorRGB.TabIndex = 3;
			this.textBoxSelectColorRGB.TextChanged += new System.EventHandler(this.textBoxSelectColorRGB_TextChanged);
			// 
			// numericUpDownSelectColorRed
			// 
			this.numericUpDownSelectColorRed.Location = new System.Drawing.Point(110, 60);
			this.numericUpDownSelectColorRed.Maximum = new decimal(new int[] {
            31,
            0,
            0,
            0});
			this.numericUpDownSelectColorRed.Name = "numericUpDownSelectColorRed";
			this.numericUpDownSelectColorRed.Size = new System.Drawing.Size(48, 19);
			this.numericUpDownSelectColorRed.TabIndex = 4;
			this.numericUpDownSelectColorRed.ValueChanged += new System.EventHandler(this.numericUpDownSelectColorRed_ValueChanged);
			// 
			// numericUpDownSelectColorGreen
			// 
			this.numericUpDownSelectColorGreen.Location = new System.Drawing.Point(110, 85);
			this.numericUpDownSelectColorGreen.Maximum = new decimal(new int[] {
            31,
            0,
            0,
            0});
			this.numericUpDownSelectColorGreen.Name = "numericUpDownSelectColorGreen";
			this.numericUpDownSelectColorGreen.Size = new System.Drawing.Size(48, 19);
			this.numericUpDownSelectColorGreen.TabIndex = 5;
			this.numericUpDownSelectColorGreen.ValueChanged += new System.EventHandler(this.numericUpDownSelectColorGreen_ValueChanged);
			// 
			// numericUpDownSelectColorBlue
			// 
			this.numericUpDownSelectColorBlue.Location = new System.Drawing.Point(110, 110);
			this.numericUpDownSelectColorBlue.Maximum = new decimal(new int[] {
            31,
            0,
            0,
            0});
			this.numericUpDownSelectColorBlue.Name = "numericUpDownSelectColorBlue";
			this.numericUpDownSelectColorBlue.Size = new System.Drawing.Size(48, 19);
			this.numericUpDownSelectColorBlue.TabIndex = 6;
			this.numericUpDownSelectColorBlue.ValueChanged += new System.EventHandler(this.numericUpDownSelectColorBlue_ValueChanged);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(69, 38);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(29, 12);
			this.label1.TabIndex = 7;
			this.label1.Text = "RGB";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(69, 62);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(25, 12);
			this.label2.TabIndex = 8;
			this.label2.Text = "Red";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(69, 87);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(35, 12);
			this.label3.TabIndex = 9;
			this.label3.Text = "Green";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(69, 112);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(28, 12);
			this.label4.TabIndex = 10;
			this.label4.Text = "Blue";
			// 
			// pictureBoxSelectColor
			// 
			this.pictureBoxSelectColor.Location = new System.Drawing.Point(0, 32);
			this.pictureBoxSelectColor.Margin = new System.Windows.Forms.Padding(0);
			this.pictureBoxSelectColor.Name = "pictureBoxSelectColor";
			this.pictureBoxSelectColor.Size = new System.Drawing.Size(64, 64);
			this.pictureBoxSelectColor.TabIndex = 11;
			this.pictureBoxSelectColor.TabStop = false;
			this.pictureBoxSelectColor.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBoxSelectColor_Paint);
			// 
			// pictureBoxPreview
			// 
			this.pictureBoxPreview.Location = new System.Drawing.Point(0, 221);
			this.pictureBoxPreview.Margin = new System.Windows.Forms.Padding(0);
			this.pictureBoxPreview.Name = "pictureBoxPreview";
			this.pictureBoxPreview.Size = new System.Drawing.Size(16, 16);
			this.pictureBoxPreview.TabIndex = 15;
			this.pictureBoxPreview.TabStop = false;
			this.pictureBoxPreview.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBoxPreview_Paint);
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
			this.splitContainer1.Panel1.Controls.Add(this.listBoxPalette);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.buttonSelect);
			this.splitContainer1.Panel2.Controls.Add(this.pictureBoxPalette);
			this.splitContainer1.Panel2.Controls.Add(this.pictureBoxPreview);
			this.splitContainer1.Panel2.Controls.Add(this.textBoxSelectColorRGB);
			this.splitContainer1.Panel2.Controls.Add(this.pictureBoxSelectColor);
			this.splitContainer1.Panel2.Controls.Add(this.numericUpDownSelectColorRed);
			this.splitContainer1.Panel2.Controls.Add(this.label4);
			this.splitContainer1.Panel2.Controls.Add(this.numericUpDownSelectColorGreen);
			this.splitContainer1.Panel2.Controls.Add(this.label3);
			this.splitContainer1.Panel2.Controls.Add(this.numericUpDownSelectColorBlue);
			this.splitContainer1.Panel2.Controls.Add(this.label2);
			this.splitContainer1.Panel2.Controls.Add(this.label1);
			this.splitContainer1.Size = new System.Drawing.Size(894, 575);
			this.splitContainer1.SplitterDistance = 200;
			this.splitContainer1.TabIndex = 16;
			// 
			// listBoxPalette
			// 
			this.listBoxPalette.ContextMenuStrip = this.contextMenuStripListBoxPalette;
			this.listBoxPalette.Dock = System.Windows.Forms.DockStyle.Fill;
			this.listBoxPalette.FormattingEnabled = true;
			this.listBoxPalette.ItemHeight = 12;
			this.listBoxPalette.Location = new System.Drawing.Point(0, 0);
			this.listBoxPalette.Margin = new System.Windows.Forms.Padding(0);
			this.listBoxPalette.Name = "listBoxPalette";
			this.listBoxPalette.Size = new System.Drawing.Size(196, 571);
			this.listBoxPalette.TabIndex = 0;
			this.listBoxPalette.SelectedIndexChanged += new System.EventHandler(this.listBoxPalette_SelectedIndexChanged);
			this.listBoxPalette.Format += new System.Windows.Forms.ListControlConvertEventHandler(this.listBoxPalette_Format);
			// 
			// contextMenuStripListBoxPalette
			// 
			this.contextMenuStripListBoxPalette.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItemListBoxPaletteImport,
            this.ToolStripMenuItemListBoxPaletteExport,
            this.toolStripMenuItem3,
            this.ToolStripMenuItemListBoxPaletteAdd,
            this.toolStripMenuItem2,
            this.ToolStripMenuItemListBoxPaletteDelete});
			this.contextMenuStripListBoxPalette.Name = "contextMenuStripListBoxPalette";
			this.contextMenuStripListBoxPalette.Size = new System.Drawing.Size(142, 104);
			// 
			// ToolStripMenuItemListBoxPaletteImport
			// 
			this.ToolStripMenuItemListBoxPaletteImport.Name = "ToolStripMenuItemListBoxPaletteImport";
			this.ToolStripMenuItemListBoxPaletteImport.Size = new System.Drawing.Size(141, 22);
			this.ToolStripMenuItemListBoxPaletteImport.Text = "Import";
			this.ToolStripMenuItemListBoxPaletteImport.Click += new System.EventHandler(this.ToolStripMenuItemListBoxPaletteImport_Click);
			// 
			// ToolStripMenuItemListBoxPaletteExport
			// 
			this.ToolStripMenuItemListBoxPaletteExport.Name = "ToolStripMenuItemListBoxPaletteExport";
			this.ToolStripMenuItemListBoxPaletteExport.Size = new System.Drawing.Size(141, 22);
			this.ToolStripMenuItemListBoxPaletteExport.Text = "Export";
			this.ToolStripMenuItemListBoxPaletteExport.Click += new System.EventHandler(this.ToolStripMenuItemListBoxPaletteExport_Click);
			// 
			// toolStripMenuItem3
			// 
			this.toolStripMenuItem3.Name = "toolStripMenuItem3";
			this.toolStripMenuItem3.Size = new System.Drawing.Size(138, 6);
			// 
			// ToolStripMenuItemListBoxPaletteAdd
			// 
			this.ToolStripMenuItemListBoxPaletteAdd.Name = "ToolStripMenuItemListBoxPaletteAdd";
			this.ToolStripMenuItemListBoxPaletteAdd.Size = new System.Drawing.Size(141, 22);
			this.ToolStripMenuItemListBoxPaletteAdd.Text = "Add(&A)";
			this.ToolStripMenuItemListBoxPaletteAdd.Click += new System.EventHandler(this.ToolStripMenuItemListBoxPaletteAdd_Click);
			// 
			// toolStripMenuItem2
			// 
			this.toolStripMenuItem2.Name = "toolStripMenuItem2";
			this.toolStripMenuItem2.Size = new System.Drawing.Size(138, 6);
			// 
			// ToolStripMenuItemListBoxPaletteDelete
			// 
			this.ToolStripMenuItemListBoxPaletteDelete.Name = "ToolStripMenuItemListBoxPaletteDelete";
			this.ToolStripMenuItemListBoxPaletteDelete.ShortcutKeys = System.Windows.Forms.Keys.Delete;
			this.ToolStripMenuItemListBoxPaletteDelete.Size = new System.Drawing.Size(141, 22);
			this.ToolStripMenuItemListBoxPaletteDelete.Text = "Delete(&D)";
			this.ToolStripMenuItemListBoxPaletteDelete.Click += new System.EventHandler(this.ToolStripMenuItemListBoxPaletteDelete_Click);
			// 
			// buttonSelect
			// 
			this.buttonSelect.Location = new System.Drawing.Point(257, 112);
			this.buttonSelect.Name = "buttonSelect";
			this.buttonSelect.Size = new System.Drawing.Size(75, 23);
			this.buttonSelect.TabIndex = 16;
			this.buttonSelect.Text = "Select";
			this.buttonSelect.UseVisualStyleBackColor = true;
			this.buttonSelect.Click += new System.EventHandler(this.buttonSelect_Click);
			// 
			// openFileDialog
			// 
			this.openFileDialog.Filter = "*.png|*.png|*.bmp|*.bmp|*.pal|*.pal|*.bin|*.bin|*.*|*.*";
			// 
			// PaletteEditor
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(894, 575);
			this.Controls.Add(this.splitContainer1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Name = "PaletteEditor";
			this.Text = "PaletteEditor";
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxPalette)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownSelectColorRed)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownSelectColorGreen)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownSelectColorBlue)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxSelectColor)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxPreview)).EndInit();
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			this.splitContainer1.Panel2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			this.contextMenuStripListBoxPalette.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.PictureBox pictureBoxPalette;
		private System.Windows.Forms.TextBox textBoxSelectColorRGB;
		private System.Windows.Forms.NumericUpDown numericUpDownSelectColorRed;
		private System.Windows.Forms.NumericUpDown numericUpDownSelectColorGreen;
		private System.Windows.Forms.NumericUpDown numericUpDownSelectColorBlue;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.PictureBox pictureBoxSelectColor;
		private System.Windows.Forms.PictureBox pictureBoxPreview;
		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.ListBox listBoxPalette;
		private System.Windows.Forms.ContextMenuStrip contextMenuStripListBoxPalette;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
		private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemListBoxPaletteAdd;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
		private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemListBoxPaletteDelete;
		private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemListBoxPaletteImport;
		private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemListBoxPaletteExport;
		private System.Windows.Forms.SaveFileDialog saveFileDialog;
		private System.Windows.Forms.OpenFileDialog openFileDialog;
		private System.Windows.Forms.Button buttonSelect;
	}
}