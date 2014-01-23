using System;
using System.Windows.Forms;


namespace TomatoTool
{
	public partial class ScriptEditor : Form
	{
		public Script Script;

		public ScriptEditor(TomatoAdventure tomatoAdventure, uint address)
		{
			InitializeComponent();

			Script = new Script(tomatoAdventure, address);

			DataGridViewScript.Columns.Add("Address", "Address");
			DataGridViewScript.Columns.Add("Scripts", "Scripts");
			DataGridViewScript.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

			for (uint i = address; i < address + 0x100; ++i)
			{
				switch (tomatoAdventure.read(address))
				{
					case 0xFD:
						switch (tomatoAdventure.read(address + 1))
						{
							case 0x35:
								DataGridViewScript.Rows.Add(String.Format("0x{0:X8}", address), String.Format("if (Flag[0x{0:X4}]) goto(0x{1:X8});", tomatoAdventure.readLittleEndian(address + 2, 2), tomatoAdventure.readAsAddress(address + 4)));
								address += 8;
								break;

							case 0x36:
								DataGridViewScript.Rows.Add(String.Format("0x{0:X8}", address), String.Format("if (!Flag[0x{0:X4}]) goto(0x{1:X8});", tomatoAdventure.readLittleEndian(address + 2, 2), tomatoAdventure.readAsAddress(address + 4)));
								address += 8;
								break;

							case 0x39:
								DataGridViewScript.Rows.Add(String.Format("0x{0:X8}", address), String.Format("if (Var[0x{0:X2}] == 0x{1:X2}) goto(0x{2:X8});", tomatoAdventure.read(address + 2), tomatoAdventure.read(address + 3), tomatoAdventure.readAsAddress(address + 4)));
								address += 8;
								break;

							case 0x49:
								DataGridViewScript.Rows.Add(String.Format("0x{0:X8}", address), String.Format("Flag[0x{0:X4}] = true;", tomatoAdventure.readLittleEndian(address + 2, 2)));
								address += 4;
								break;

							case 0x4A:
								DataGridViewScript.Rows.Add(String.Format("0x{0:X8}", address), String.Format("Flag[0x{0:X4}] = false;", tomatoAdventure.readLittleEndian(address + 2, 2)));
								address += 4;
								break;

							case 0x4F:
								DataGridViewScript.Rows.Add(String.Format("0x{0:X8}", address), String.Format("Var[0x{0:X2}]++;", tomatoAdventure.read(address + 2)));
								address += 3;
								break;

							case 0x71:
								DataGridViewScript.Rows.Add(String.Format("0x{0:X8}", address), String.Format("wait({0:D});", tomatoAdventure.readLittleEndian(address + 2, 2)));
								address += 4;
								break;

							case 0x7D:
								DataGridViewScript.Rows.Add(String.Format("0x{0:X8}", address), "end();");
								address += 2;
								break;

							default:
								DataGridViewScript.Rows.Add(String.Format("0x{0:X8}", address), String.Format("{0:X2}", tomatoAdventure.read(address)));
								address++;
								break;
						}
						break;

					case 0xFE:
						WindowString windowString;
						switch (tomatoAdventure.read(address + 1))
						{
							case 0x07:
								DataGridViewScript.Rows.Add(String.Format("0x{0:X8}", address), String.Format("characterAction(0x{0:X2}, 0x{1:X8});", tomatoAdventure.read(address + 2), tomatoAdventure.readAsAddress(address + 3)));
								address += 7;
								break;

							case 0x0E:
								switch (tomatoAdventure.read(address + 2))
								{
									case 0xFD:
										DataGridViewScript.Rows.Add(String.Format("0x{0:X8}", address), "characterStop();");
										address += 3;
										break;

									default:
										DataGridViewScript.Rows.Add(String.Format("0x{0:X8}", address), String.Format("{0:X2}", tomatoAdventure.read(address)));
										address++;
										break;
								}
								break;

							case 0x0F:
								switch (tomatoAdventure.read(address + 2))
								{
									case 0xFD:
										DataGridViewScript.Rows.Add(String.Format("0x{0:X8}", address), "characterStart();");
										address += 3;
										break;

									default:
										DataGridViewScript.Rows.Add(String.Format("0x{0:X8}", address), String.Format("{0:X2}", tomatoAdventure.read(address)));
										address++;
										break;
								}
								break;

							case 0x1A:
								DataGridViewScript.Rows.Add(String.Format("0x{0:X8}", address), "disableOperation();");
								address += 3;
								break;

							case 0xAA:
								DataGridViewScript.Rows.Add(String.Format("0x{0:X8}", address), "BGMstop();");
								address += 2;
								break;

							case 0xB0:
								DataGridViewScript.Rows.Add(String.Format("0x{0:X8}", address), String.Format("soundEffect(0x{0:X2});", tomatoAdventure.read(address + 2)));
								address += 3;
								break;

							case 0x66:
								windowString = new WindowString(tomatoAdventure, tomatoAdventure.readAsAddress(address + 2));
								DataGridViewScript.Rows.Add(String.Format("0x{0:X8}", address), String.Format("showText(0x{0:X8}, none, top);", tomatoAdventure.readAsAddress(address + 2)) + windowString.Text);
								address += 6;
								break;

							case 0x67:
								windowString = new WindowString(tomatoAdventure, tomatoAdventure.readAsAddress(address + 2));
								DataGridViewScript.Rows.Add(String.Format("0x{0:X8}", address), String.Format("showText(0x{0:X8}, none, bottom);", tomatoAdventure.readAsAddress(address + 2)) + windowString.Text);
								address += 6;
								break;

							case 0x89:
								DataGridViewScript.Rows.Add(String.Format("0x{0:X8}", address), "brighten();");
								address += 2;
								break;

							case 0x8B:
								DataGridViewScript.Rows.Add(String.Format("0x{0:X8}", address), "darken();");
								address += 2;
								break;

							case 0xC8:
								windowString = new WindowString(tomatoAdventure, tomatoAdventure.readAsAddress(address + 2));
								DataGridViewScript.Rows.Add(String.Format("0x{0:X8}", address), String.Format("showText(0x{0:X8}, boy, top);", tomatoAdventure.readAsAddress(address + 2)) + windowString.Text);
								address += 6;
								break;

							case 0xC9:
								windowString = new WindowString(tomatoAdventure, tomatoAdventure.readAsAddress(address + 2));
								DataGridViewScript.Rows.Add(String.Format("0x{0:X8}", address), String.Format("showText(0x{0:X8}, boy, bottom);", tomatoAdventure.readAsAddress(address + 2)) + windowString.Text);
								address += 6;
								break;

							case 0xD2:
								windowString = new WindowString(tomatoAdventure, tomatoAdventure.readAsAddress(address + 2));
								DataGridViewScript.Rows.Add(String.Format("0x{0:X8}", address), String.Format("showText(0x{0:X8}, girl, top);", tomatoAdventure.readAsAddress(address + 2)) + windowString.Text);
								address += 6;
								break;

							case 0xD3:
								windowString = new WindowString(tomatoAdventure, tomatoAdventure.readAsAddress(address + 2));
								DataGridViewScript.Rows.Add(String.Format("0x{0:X8}", address), String.Format("showText(0x{0:X8}, girl, bottom);", tomatoAdventure.readAsAddress(address + 2)) + windowString.Text);
								address += 6;
								break;

							case 0xDB:
								windowString = new WindowString(tomatoAdventure, tomatoAdventure.readAsAddress(address + 2));
								DataGridViewScript.Rows.Add(String.Format("0x{0:X8}", address), String.Format("showText(0x{0:X8}, machine, top);", tomatoAdventure.readAsAddress(address + 2)) + windowString.Text);
								address += 6;
								break;

							default:
								DataGridViewScript.Rows.Add(String.Format("0x{0:X8}", address), String.Format("{0:X2}", tomatoAdventure.read(address)));
								address++;
								break;
						}
						break;

					default:
						DataGridViewScript.Rows.Add(String.Format("0x{0:X8}", address), String.Format("{0:X2}", tomatoAdventure.read(address)));
						address++;
						break;
				}
				++i;
			}

		}
	}
}
