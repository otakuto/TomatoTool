using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace TomatoTool
{
	public partial class FormTomatoTool
		:
		Form
	{
		public static readonly string applicationName = "TomatoTool" + ' ' + Application.ProductVersion;
		private string filePath;
		private bool modified = false;
		private TomatoAdventure tomatoAdventure;

		public FormTomatoTool()
		{
			using (SplashScreen splashScreen = new SplashScreen())
			{
				splashScreen.Show();
				splashScreen.Refresh();
				{
					Text = applicationName;

					InitializeComponent();

					textBoxGimmickLv = new TextBox[9];
					textBoxGimmickLv[0] = textBoxGimmickLv1;
					textBoxGimmickLv[1] = textBoxGimmickLv2;
					textBoxGimmickLv[2] = textBoxGimmickLv3;
					textBoxGimmickLv[3] = textBoxGimmickLv4;
					textBoxGimmickLv[4] = textBoxGimmickLv5;
					textBoxGimmickLv[5] = textBoxGimmickLv6;
					textBoxGimmickLv[6] = textBoxGimmickLv7;
					textBoxGimmickLv[7] = textBoxGimmickLv8;
					textBoxGimmickLv[8] = textBoxGimmickLv9;

					toolStripMenuItemMapChipBG = new ToolStripMenuItem[3];
					toolStripMenuItemMapChipBG[0] = toolStripMenuItemMapChipBG1;
					toolStripMenuItemMapChipBG[1] = toolStripMenuItemMapChipBG2;
					toolStripMenuItemMapChipBG[2] = toolStripMenuItemMapChipBG3;
				}

				{
					Bitmap errorBitmap = new Bitmap(16, 16);
					using (Graphics graphics = Graphics.FromImage(errorBitmap))
					{
						graphics.FillRectangle(new SolidBrush(Color.FromArgb(0, 0, 255)), 0, 0, 16, 16);
					}

					System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
					Bitmap[] bitmap = new Bitmap[256];
					for (uint i = 0; i < bitmap.GetLength(0); ++i)
					{
						using (Stream stream = assembly.GetManifestResourceStream(String.Format("TomatoTool.Resources.MapArea.{0:X2}.png", i)))
						{
							if (stream == null)
							{
								bitmap[i] = errorBitmap;
							}
							else
							{
								bitmap[i] = new Bitmap(stream);
							}
						}
					}

					MapArea.image = Array.AsReadOnly<Bitmap>(bitmap);
				}

				{
#if DEBUG
					openFileDialog.FileName = @"D:\Users\otakuto\Desktop\TomatoAdventure\TomatoAdventure.gba";
					openFileDialog_FileOk(null, null);
#else
					openFileDialog.ShowDialog();
#endif
					test();
				}
				splashScreen.Close();
			}

			if (tomatoAdventure == null)
			{
				Close();
			}
			else
			{
				Activate();
			}
		}
		
		private void FormTomatoTool_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (modified)
			{
				switch (MessageBox.Show(Path.GetFileName(filePath) + "への変更内容を保存しますか?", applicationName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation))
				{
					case DialogResult.Yes:
						toolStripMenuItemSave_Click(sender, e);

						break;

					case DialogResult.No:

						break;

					case DialogResult.Cancel:
						e.Cancel = true;

						break;
				}
			}

		}

		private void openFileDialog_FileOk(object sender, CancelEventArgs e)
		{
			try
			{
				filePath = openFileDialog.FileName;

				tomatoAdventure = null;
				tomatoAdventure = new TomatoAdventure(File.ReadAllBytes(filePath));
				tomatoAdventure.objectListSort();

				listBoxMap.DataSource = tomatoAdventure.ObjectDictionary[typeof(Map)];
				listBoxGimmick.DataSource = tomatoAdventure.ObjectDictionary[typeof(Gimmick)];
				listBoxMonster.DataSource = tomatoAdventure.ObjectDictionary[typeof(Monster)];

				initializeMap();
				loadEtc();

				foreach (KeyValuePair<Type, List<ROMObject>> listROMObject in tomatoAdventure.ObjectDictionary)
				{
					TreeNode treeNode = treeViewObjectList.Nodes.Add(((Type)listROMObject.Key).Name);

					for (int i = 0; i < listROMObject.Value.Count; ++i)
					{
						treeNode.Nodes.Add(String.Format("{0:X8}", listROMObject.Value[i].ObjectID));
					}
				}

				pictureBoxMapRefresh();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.ToString(), applicationName + " - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
		private void saveFileDialog_FileOk(object sender, CancelEventArgs e)
		{
			try
			{
				//マップの保存
				saveMap();

				tomatoAdventure.save();

				filePath = saveFileDialog.FileName;
				File.WriteAllBytes(filePath, tomatoAdventure.get());
				//setText("");
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.ToString(), applicationName + " - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void toolStripMenuItemOpen_Click(object sender, EventArgs e)
		{
			if (modified)
			{
				switch (MessageBox.Show(Path.GetFileName(filePath) + "への変更内容を保存しますか?", applicationName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation))
				{
					case DialogResult.Yes:

						toolStripMenuItemSave_Click(sender, e);
						openFileDialog.ShowDialog();

						break;

					case DialogResult.No:

						openFileDialog.ShowDialog();

						break;

					case DialogResult.Cancel:

						break;
				}
			}
			else
			{
				openFileDialog.ShowDialog();
			}
		}
		private void toolStripMenuItemSave_Click(object sender, EventArgs e)
		{
			try
			{
				saveMap();

				tomatoAdventure.save();


				File.WriteAllBytes(filePath, tomatoAdventure.get());
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				toolStripMenuItemSaveAs_Click(sender, e);
			}
		}
		private void toolStripMenuItemSaveAs_Click(object sender, EventArgs e)
		{
			saveFileDialog.ShowDialog();
		}
		private void toolStripMenuItemExit_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void test()
		{
			if (false)
			{
				tomatoAdventure.test();

				Bitmap bitmap;
				System.Drawing.Imaging.BitmapData bitmapData;
				byte[] data;

				bitmap = new Bitmap(1024, tomatoAdventure.get().GetLength(0) / 1024);
				bitmapData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), System.Drawing.Imaging.ImageLockMode.ReadWrite, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
				data = new byte[Math.Abs(bitmapData.Stride) * bitmap.Height * 4];
				bitmap.UnlockBits(bitmapData);

				Random random = new Random(0);

				foreach (KeyValuePair<Type, List<ROMObject>> listROMObject in tomatoAdventure.ObjectDictionary)
				{
					byte r = (byte)random.Next(0, 256);
					byte g = (byte)random.Next(0, 256);
					byte b = (byte)random.Next(0, 256);

					if (listROMObject.Key == typeof(Palette))
					{
						r = 0;
						g = 0;
						b = 0xFF;
					}

					try
					{
						for (int i = 0; i < listROMObject.Value.Count; ++i)
						{
							uint size = listROMObject.Value[i].Size;
							uint address = listROMObject.Value[i].ObjectID;
							if (address != ROM.ADDRESS_NULL)
							{
								uint ba = (((address - ROM.ADDRESS_BASE) / 1024) * 1024 * 4) + ((address - ROM.ADDRESS_BASE) % 1024) * 4;
								for (uint j = 0; j < size; ++j, ba += 4)
								{
									data[ba + 0] = 0xFF;
									data[ba + 1] = r;
									data[ba + 2] = g;
									data[ba + 3] = b;
								}
							}

						}
					}
					catch
					{
					}
				}
				{
					uint ba = (((0x64B4EC) / 1024) * 1024 * 4) + ((0x64B4EC) % 1024) * 4;
					for (uint i = 0x64B4EC; i < tomatoAdventure.get().GetLength(0); ++i, ba += 4)
					{

						data[ba + 0] = 0xFF;
						data[ba + 1] = 0xFF;
						data[ba + 2] = 0xFF;
						data[ba + 3] = 0xFF;
					}
				}

				bitmapData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), System.Drawing.Imaging.ImageLockMode.ReadWrite, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
				int offset = 0;
				long ptr = bitmapData.Scan0.ToInt64();
				for (int i = 0; i < bitmapData.Height; ++i)
				{
					Marshal.Copy(data, offset, new IntPtr(ptr), bitmapData.Width * 4);
					offset += bitmapData.Width * 4;
					ptr += bitmapData.Stride;
				}
				bitmap.UnlockBits(bitmapData);
				bitmap.Save(@"tomato.png", System.Drawing.Imaging.ImageFormat.Png);
				bitmap.Dispose();
			}
		}

		//スクリプトエディター呼び出し
		public void ScriptEditor(TomatoAdventure tomatoAdventure, uint address)
		{
			ScriptEditor scriptEditor = new ScriptEditor(tomatoAdventure, address);
			scriptEditor.Show();
			scriptEditor.Refresh();
			scriptEditor.Activate();
		}

		private void button2_Click(object sender, EventArgs e)
		{
			FormMapEditor mapEditor = new FormMapEditor(tomatoAdventure, listBoxMap.SelectedItem as Map);
			mapEditor.ShowDialog();

		}

		private void button3_Click(object sender, EventArgs e)
		{
			WindowStringEditor windowStringEditor = new WindowStringEditor(tomatoAdventure, null);
			windowStringEditor.Show();
			windowStringEditor.Refresh();
			windowStringEditor.Activate();
		}

		private void pictureBox2_Paint(object sender, PaintEventArgs e)
		{
			Monster monster = listBoxMonster.SelectedItem as Monster;

			if (monster.tileList != null && monster.Palette != null)
			{
				/*
				(sender as PictureBox).Width = monster.tileList[0].Tile.Count * 8;
				(sender as PictureBox).Height = 8;
				for (int i = 0; i < monster.tileList[0].Count; ++i)
				{
					Bitmap b = monster.tileList[0][i].toBitmap(monster.palette);
					e.Graphics.DrawImage(b, i * 8, 0);
					b.Dispose();
				}
				*/
				(sender as PictureBox).Width = 256;
				(sender as PictureBox).Height = 256;

				monster.draw(e.Graphics);
			}
			else
			{
				e.Graphics.DrawImage(MapArea.image[0], 0, 0);
			}
		}

		private void pictureBox3_Paint(object sender, PaintEventArgs e)
		{
			Monster monster = listBoxMonster.SelectedItem as Monster;

			if (monster.tileList != null && monster.tileList[1] != null && monster.Palette != null)
			{
				(sender as PictureBox).Width = monster.tileList[1].Tile.Count * 8;
				(sender as PictureBox).Height = 8;

				for (int i = 0; i < monster.tileList[0].Count; ++i)
				{
					Bitmap b = monster.tileList[1][i].toBitmap(monster.Palette);
					e.Graphics.DrawImage(b, i * 8, 0);
					b.Dispose();
				}
			}
			else
			{
				e.Graphics.DrawImage(MapArea.image[0], 0, 0);
			}
		}

		private void loadEtc()
		{
			textBoxROMSize.Text = String.Format("{0:X8}", tomatoAdventure.get().Length);
		}

		private void buttonROMResize_Click(object sender, EventArgs e)
		{
			try
			{
				tomatoAdventure.resize(Convert.ToInt32(textBoxROMSize.Text, 16));
				MessageBox.Show("リサイズ成功", applicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.ToString(), applicationName + " - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void splitContainer_SplitterMoved(object sender, SplitterEventArgs e)
		{
			SplitContainer[] splitContainerList = new SplitContainer[]
			{
				splitContainer1,
				splitContainer2,
				splitContainer3,
				splitContainer5,
			};

			int splitterDistance = ((SplitContainer)sender).SplitterDistance;

			for (int i = 0; i < splitContainerList.Length; ++i)
			{
				splitContainerList[i].SplitterMoved -= splitContainer_SplitterMoved;
				splitContainerList[i].SplitterDistance = splitterDistance;
				splitContainerList[i].SplitterMoved += splitContainer_SplitterMoved;
			}

		}

	}
}