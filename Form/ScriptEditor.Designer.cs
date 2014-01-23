namespace TomatoTool
{
	partial class ScriptEditor
	{
		/// <summary>
		/// 必要なデザイナー変数です。
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// 使用中のリソースをすべてクリーンアップします。
		/// </summary>
		/// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows フォーム デザイナーで生成されたコード

		/// <summary>
		/// デザイナー サポートに必要なメソッドです。このメソッドの内容を
		/// コード エディターで変更しないでください。
		/// </summary>
		private void InitializeComponent()
		{
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.DataGridViewScript = new System.Windows.Forms.DataGridView();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.DataGridViewScriptCopyList = new System.Windows.Forms.DataGridView();
			((System.ComponentModel.ISupportInitialize)(this.DataGridViewScript)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.DataGridViewScriptCopyList)).BeginInit();
			this.SuspendLayout();
			// 
			// statusStrip1
			// 
			this.statusStrip1.Location = new System.Drawing.Point(0, 551);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new System.Drawing.Size(892, 22);
			this.statusStrip1.TabIndex = 0;
			this.statusStrip1.Text = "statusStrip1";
			// 
			// DataGridViewScript
			// 
			this.DataGridViewScript.AllowUserToAddRows = false;
			this.DataGridViewScript.AllowUserToDeleteRows = false;
			this.DataGridViewScript.AllowUserToResizeColumns = false;
			this.DataGridViewScript.AllowUserToResizeRows = false;
			this.DataGridViewScript.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.DataGridViewScript.Dock = System.Windows.Forms.DockStyle.Fill;
			this.DataGridViewScript.Location = new System.Drawing.Point(0, 0);
			this.DataGridViewScript.Margin = new System.Windows.Forms.Padding(0);
			this.DataGridViewScript.Name = "DataGridViewScript";
			this.DataGridViewScript.RowTemplate.Height = 21;
			this.DataGridViewScript.Size = new System.Drawing.Size(696, 547);
			this.DataGridViewScript.TabIndex = 1;
			// 
			// splitContainer1
			// 
			this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
			this.splitContainer1.Location = new System.Drawing.Point(0, 0);
			this.splitContainer1.Margin = new System.Windows.Forms.Padding(0);
			this.splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.DataGridViewScript);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.DataGridViewScriptCopyList);
			this.splitContainer1.Size = new System.Drawing.Size(892, 551);
			this.splitContainer1.SplitterDistance = 700;
			this.splitContainer1.TabIndex = 2;
			// 
			// DataGridViewScriptCopyList
			// 
			this.DataGridViewScriptCopyList.AllowUserToAddRows = false;
			this.DataGridViewScriptCopyList.AllowUserToDeleteRows = false;
			this.DataGridViewScriptCopyList.AllowUserToResizeColumns = false;
			this.DataGridViewScriptCopyList.AllowUserToResizeRows = false;
			this.DataGridViewScriptCopyList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.DataGridViewScriptCopyList.Dock = System.Windows.Forms.DockStyle.Fill;
			this.DataGridViewScriptCopyList.Location = new System.Drawing.Point(0, 0);
			this.DataGridViewScriptCopyList.Margin = new System.Windows.Forms.Padding(0);
			this.DataGridViewScriptCopyList.Name = "DataGridViewScriptCopyList";
			this.DataGridViewScriptCopyList.RowTemplate.Height = 21;
			this.DataGridViewScriptCopyList.Size = new System.Drawing.Size(184, 547);
			this.DataGridViewScriptCopyList.TabIndex = 0;
			// 
			// ScriptEditor
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(892, 573);
			this.Controls.Add(this.splitContainer1);
			this.Controls.Add(this.statusStrip1);
			this.Name = "ScriptEditor";
			this.Text = "ScriptEditor";
			((System.ComponentModel.ISupportInitialize)(this.DataGridViewScript)).EndInit();
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.DataGridViewScriptCopyList)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.DataGridView DataGridViewScript;
		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.DataGridView DataGridViewScriptCopyList;
	}
}