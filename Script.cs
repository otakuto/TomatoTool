
namespace TomatoTool
{
	public class Script : ROMObject
	{
		private uint address;
		public override uint ObjectID
		{
			get
			{
				return address;
			}

			set
			{
				address = value;
			}
		}

		private bool saved;
		public override bool Saved
		{
			get
			{
				return saved;
			}

			set
			{
				saved = value;
			}
		}

		public override uint Size
		{
			get
			{
				throw new System.NotImplementedException();
			}
		}

		public override uint Alignment
		{
			get
			{
				throw new System.NotImplementedException();
			}
		}

		//public List<String> script = new List<String>();

		public static readonly Script NULL;

		static Script()
		{
			NULL = new Script();

			NULL.address = TomatoTool.ROM.ADDRESS_NULL;
		}

		public Script()
		{
			initialize();
		}
		public Script(TomatoAdventure tomatoAdventure, uint address)
			
		{
			load(tomatoAdventure, address);
		}

		public void initialize()
		{
			
		}
		public void load(TomatoAdventure tomatoAdventure)
		{
			load(tomatoAdventure, address);
		}
		public void load(TomatoAdventure tomatoAdventure, uint address)
		{
			this.address = address;

			if (false && tomatoAdventure.isAddress(address))
			{
				uint i = 0;
				bool b = true;
				while (b)
				{
					switch (tomatoAdventure.read(address + i))
					{
						case 0xFD:
							switch (tomatoAdventure.read(address + i + 1))
							{
								case 0x35:
									//script.Add(String.Format("if (Flag[0x{0:X4}]) goto(0x{0:X8});", tomatoAdventure.readLittleEndian(address + 2, 2), tomatoAdventure.readAsAddress(address + 4)));
									i += 8;
									break;

								case 0x36:
									//script.Add(String.Format("if (!Flag[0x{0:X4}]) goto(0x{0:X8});", tomatoAdventure.readLittleEndian(address + 2, 2), tomatoAdventure.readAsAddress(address + 4)));
									i += 8;
									break;

								case 0x49:
									//script.Add(String.Format("Flag[0x{0:X4}] = true;", tomatoAdventure.readLittleEndian(address + 2, 2)));
									i += 4;
									break;

								case 0x4A:
									//script.Add(String.Format("Flag[0x{0:X4}] = false;", tomatoAdventure.readLittleEndian(address + 2, 2)));
									i += 4;
									break;

								case 0x71:
									//script.Add(String.Format("wait({0:D});", tomatoAdventure.readLittleEndian(address + 2, 2)));
									i += 4;
									break;

								case 0x7D:
									//script.Add("end();");
									b = false;
									i += 2;
									break;

								default:
									//script.Add(String.Format("{0:X2}", tomatoAdventure.read(address)));
									++i;
									break;
							}
							break;

						case 0xFE:
							switch (tomatoAdventure.read(address + i + 1))
							{
								case 0x07:
									//script.Add(String.Format("characterAction(0x{0:X2}, 0x{0:X8});", tomatoAdventure.read(address + 2), tomatoAdventure.readAsAddress(address + 3)));
									i += 7;
									break;

								case 0x0E:
									switch (tomatoAdventure.read(address + i + 2))
									{
										case 0xFD:
											//script.Add("characterStop();");
											i += 3;
											break;

										default:
											//script.Add(String.Format("{0:X2}", tomatoAdventure.read(address)));
											++i;
											break;
									}
									break;

								case 0x0F:
									switch (tomatoAdventure.read(address + i + 2))
									{
										case 0xFD:
											//script.Add("characterStart();");
											i += 3;
											break;

										default:
											//script.Add(String.Format("{0:X2}", tomatoAdventure.read(address)));
											++i;
											break;
									}
									break;

								case 0x1A:
									//script.Add("disableOperation();");
									i += 3;
									break;

								case 0xAA:
									//script.Add("BGMstop();");
									i += 2;
									break;

								case 0xB0:
									//script.Add(String.Format("soundEffect(0x{0:X2});", tomatoAdventure.read(address + 2)));
									i += 3;
									break;

								case 0x66:
									tomatoAdventure.add(new WindowString(tomatoAdventure, tomatoAdventure.readAsAddress(address + i + 2)));
									//script.Add(String.Format("showText(0x{0:X8}, none, top);", tomatoAdventure.readAsAddress(address + 2)) + windowString.Text);
									i += 6;
									break;

								case 0x67:
									tomatoAdventure.add(new WindowString(tomatoAdventure, tomatoAdventure.readAsAddress(address + i + 2)));
									//script.Add(String.Format("showText(0x{0:X8}, none, bottom);", tomatoAdventure.readAsAddress(address + 2)) + windowString.Text);
									i += 6;
									break;

								case 0xC8:

									tomatoAdventure.add(new WindowString(tomatoAdventure, tomatoAdventure.readAsAddress(address + i + 2)));
									//script.Add(String.Format("showText(0x{0:X8}, boy, top);", tomatoAdventure.readAsAddress(address + 2)) + windowString.Text);
									i += 6;
									break;

								case 0xC9:

									tomatoAdventure.add(new WindowString(tomatoAdventure, tomatoAdventure.readAsAddress(address + i + 2)));
									//script.Add(String.Format("showText(0x{0:X8}, boy, bottom);", tomatoAdventure.readAsAddress(address + 2)) + windowString.Text);
									i += 6;
									break;

								case 0xD2:

									tomatoAdventure.add(new WindowString(tomatoAdventure, tomatoAdventure.readAsAddress(address + i + 2)));
									//script.Add(String.Format("showText(0x{0:X8}, girl, top);", tomatoAdventure.readAsAddress(address + 2)) + windowString.Text);
									i += 6;
									break;

								case 0xD3:

									tomatoAdventure.add(new WindowString(tomatoAdventure, tomatoAdventure.readAsAddress(address + i + 2)));
									//script.Add(String.Format("showText(0x{0:X8}, girl, bottom);", tomatoAdventure.readAsAddress(address + 2)) + windowString.Text);
									i += 6;
									break;

								case 0xDB:

									tomatoAdventure.add(new WindowString(tomatoAdventure, tomatoAdventure.readAsAddress(address + i + 2)));
									//script.Add(String.Format("showText(0x{0:X8}, machine, top);", tomatoAdventure.readAsAddress(address + 2)) + windowString.Text);
									i += 6;
									break;

								default:
									//script.Add(String.Format("{0:X2}", tomatoAdventure.read(address)));
									++i;
									break;
							}
							break;

						default:
							//script.Add(String.Format("{0:X2}", tomatoAdventure.read(address)));
							++i;
							break;
					}
				}
			}

		}
		public void save(TomatoAdventure tomatoAdventure)
		{
		}
		public void save(TomatoAdventure tomatoAdventure, uint address)
		{
		}
	}
}
