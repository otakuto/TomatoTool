using System;
using System.Collections.Generic;
using System.Text;


namespace TomatoTool
{
	public class StatusString : ROMObject
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

		private string text;
		public string Text
		{
			get
			{
				return text;
			}

			set
			{
				if (canToByte(value))
				{
					text = value;
				}
				else
				{
					throw new ArgumentOutOfRangeException();
				}
			}
		}

		private static Dictionary<string, byte> statusStringToByte = new Dictionary<string, byte>()
		#region
		{
			{" ", 0x00},
			{"　", 0x00},

			{"A", 0x01},
			{"Ａ", 0x01},

			{"B", 0x02},
			{"Ｂ", 0x02},

			{"C", 0x03},
			{"Ｃ", 0x03},

			{"D", 0x04},
			{"Ｄ", 0x04},

			{"E", 0x05},
			{"Ｅ", 0x05},

			{"F", 0x06},
			{"Ｆ", 0x06},

			{"G", 0x07},
			{"Ｇ", 0x07},

			{"H", 0x08},
			{"Ｈ", 0x08},

			{"I", 0x09},
			{"Ｉ", 0x09},

			{"J", 0x0A},
			{"Ｊ", 0x0A},

			{"K", 0x0B},
			{"Ｋ", 0x0B},

			{"L", 0x0C},
			{"Ｌ", 0x0C},

			{"M", 0x0D},
			{"Ｍ", 0x0D},

			{"N", 0x0E},
			{"Ｎ", 0x0E},

			{"O", 0x0F},
			{"Ｏ", 0x0F},

			{"P", 0x10},
			{"Ｐ", 0x10},

			{"Q", 0x11},
			{"Ｑ", 0x11},

			{"R", 0x12},
			{"Ｒ", 0x12},

			{"S", 0x13},
			{"Ｓ", 0x13},

			{"T", 0x14},
			{"Ｔ", 0x14},

			{"U", 0x15},
			{"Ｕ", 0x15},

			{"V", 0x16},
			{"Ｖ", 0x16},

			{"W", 0x17},
			{"Ｗ", 0x17},

			{"X", 0x18},
			{"Ｘ", 0x18},

			{"Y", 0x19},
			{"Ｙ", 0x19},

			{"Z", 0x1A},
			{"Ｚ", 0x1A},

			{"ー", 0x1B},
			{"、", 0x1C},
			{"。", 0x1D},
			
			{"_", 0x1E},
			{"＿", 0x1E},

			{"ヴ", 0x1F},
			{"ァ", 0x20},
			{"ア", 0x21},
			{"イ", 0x22},
			{"ウ", 0x23},
			{"エ", 0x24},
			{"オ", 0x25},
			{"カ", 0x26},
			{"キ", 0x27},
			{"ク", 0x28},
			{"ケ", 0x29},
			{"コ", 0x2A},
			{"サ", 0x2B},
			{"シ", 0x2C},
			{"ス", 0x2D},
			{"セ", 0x2E},
			{"ソ", 0x2F},
			{"ィ", 0x30},
			{"タ", 0x31},
			{"チ", 0x32},
			{"ツ", 0x33},
			{"テ", 0x34},
			{"ト", 0x35},
			{"ナ", 0x36},
			{"ニ", 0x37},
			{"ヌ", 0x38},
			{"ネ", 0x39},
			{"ノ", 0x3A},
			{"ハ", 0x3B},
			{"ヒ", 0x3C},
			{"フ", 0x3D},
			{"ヘ", 0x3E},
			{"ホ", 0x3F},
			{"ゥ", 0x40},
			{"マ", 0x41},
			{"ミ", 0x42},
			{"ム", 0x43},
			{"メ", 0x44},
			{"モ", 0x45},
			{"ヤ", 0x46},
			{"ユ", 0x47},
			{"ヨ", 0x48},
			{"ワ", 0x49},
			{"ン", 0x4A},
			{"ラ", 0x4B},
			{"リ", 0x4C},
			{"ル", 0x4D},
			{"レ", 0x4E},
			{"ロ", 0x4F},
			{"ェ", 0x50},
			{"ガ", 0x51},
			{"ギ", 0x52},
			{"グ", 0x53},
			{"ゲ", 0x54},
			{"ゴ", 0x55},
			{"ザ", 0x56},
			{"ジ", 0x57},
			{"ズ", 0x58},
			{"ゼ", 0x59},
			{"ゾ", 0x5A},
			{"ダ", 0x5B},
			{"ヂ", 0x5C},
			{"ヅ", 0x5D},
			{"デ", 0x5E},
			{"ド", 0x5F},
			{"ォ", 0x60},
			{"バ", 0x61},
			{"ビ", 0x62},
			{"ブ", 0x63},
			{"ベ", 0x64},
			{"ボ", 0x65},
			{"パ", 0x66},
			{"ピ", 0x67},
			{"プ", 0x68},
			{"ペ", 0x69},
			{"ポ", 0x6A},
			{"ヲ", 0x6B},
			{"ャ", 0x6C},
			{"ュ", 0x6D},
			{"ョ", 0x6E},
			{"ッ", 0x6F},

			{"0", 0x70},
			{"０", 0x70},

			{"1", 0x71},
			{"１", 0x71},

			{"2", 0x72},
			{"２", 0x72},

			{"3", 0x73},
			{"３", 0x73},

			{"4", 0x74},
			{"４", 0x74},

			{"5", 0x75},
			{"５", 0x75},

			{"6", 0x76},
			{"６", 0x76},

			{"7", 0x77},
			{"７", 0x77},

			{"8", 0x78},
			{"８", 0x78},

			{"9", 0x79},
			{"９", 0x79},

			{"-", 0x7A},
			{"－", 0x7A},

			{"!", 0x7B},
			{"！", 0x7B},

			{"?", 0x7C},
			{"？", 0x7C},

			{"・", 0x7D},
			
			{":", 0x7E},
			{"：", 0x7E},

			{".", 0x7F},
			{"．", 0x7F},

			{"～", 0x80},

			{"a", 0x81},
			{"ａ", 0x81},

			{"b", 0x82},
			{"ｂ", 0x82},

			{"c", 0x83},
			{"ｃ", 0x83},

			{"d", 0x84},
			{"ｄ", 0x84},

			{"e", 0x85},
			{"ｅ", 0x85},

			{"f", 0x86},
			{"ｆ", 0x86},

			{"g", 0x87},
			{"ｇ", 0x87},

			{"h", 0x88},
			{"ｈ", 0x88},

			{"i", 0x89},
			{"ｉ", 0x89},

			{"j", 0x8A},
			{"ｊ", 0x8A},

			{"k", 0x8B},
			{"ｋ", 0x8B},

			{"l", 0x8C},
			{"ｌ", 0x8C},

			{"m", 0x8D},
			{"ｍ", 0x8D},

			{"n", 0x8E},
			{"ｎ", 0x8E},

			{"o", 0x8F},
			{"ｏ", 0x8F},

			{"p", 0x90},
			{"ｐ", 0x90},

			{"q", 0x91},
			{"ｑ", 0x91},

			{"r", 0x92},
			{"ｒ", 0x92},

			{"s", 0x93},
			{"ｓ", 0x93},

			{"t", 0x94},
			{"ｔ", 0x94},

			{"u", 0x95},
			{"ｕ", 0x95},

			{"v", 0x96},
			{"ｖ", 0x96},

			{"w", 0x97},
			{"ｗ", 0x97},

			{"x", 0x98},
			{"ｘ", 0x98},

			{"y", 0x99},
			{"ｙ", 0x99},

			{"z", 0x9A},
			{"ｚ", 0x9A},

			{"(", 0x9B},
			{"（", 0x9B},

			{")", 0x9C},
			{"）", 0x9C},
			
			{"「", 0x9D},
			{"」", 0x9E},
			{"…", 0x9F},
			{"ぁ", 0xA0},
			{"あ", 0xA1},
			{"い", 0xA2},
			{"う", 0xA3},
			{"え", 0xA4},
			{"お", 0xA5},
			{"か", 0xA6},
			{"き", 0xA7},
			{"く", 0xA8},
			{"け", 0xA9},
			{"こ", 0xAA},
			{"さ", 0xAB},
			{"し", 0xAC},
			{"す", 0xAD},
			{"せ", 0xAE},
			{"そ", 0xAF},
			{"ぃ", 0xB0},
			{"た", 0xB1},
			{"ち", 0xB2},
			{"つ", 0xB3},
			{"て", 0xB4},
			{"と", 0xB5},
			{"な", 0xB6},
			{"に", 0xB7},
			{"ぬ", 0xB8},
			{"ね", 0xB9},
			{"の", 0xBA},
			{"は", 0xBB},
			{"ひ", 0xBC},
			{"ふ", 0xBD},
			{"へ", 0xBE},
			{"ほ", 0xBF},
			{"ぅ", 0xC0},
			{"ま", 0xC1},
			{"み", 0xC2},
			{"む", 0xC3},
			{"め", 0xC4},
			{"も", 0xC5},
			{"や", 0xC6},
			{"ゆ", 0xC7},
			{"よ", 0xC8},
			{"わ", 0xC9},
			{"ん", 0xCA},
			{"ら", 0xCB},
			{"り", 0xCC},
			{"る", 0xCD},
			{"れ", 0xCE},
			{"ろ", 0xCF},
			{"ぇ", 0xD0},
			{"が", 0xD1},
			{"ぎ", 0xD2},
			{"ぐ", 0xD3},
			{"げ", 0xD4},
			{"ご", 0xD5},
			{"ざ", 0xD6},
			{"じ", 0xD7},
			{"ず", 0xD8},
			{"ぜ", 0xD9},
			{"ぞ", 0xDA},
			{"だ", 0xDB},
			{"ぢ", 0xDC},
			{"づ", 0xDD},
			{"で", 0xDE},
			{"ど", 0xDF},
			{"ぉ", 0xE0},
			{"ば", 0xE1},
			{"び", 0xE2},
			{"ぶ", 0xE3},
			{"べ", 0xE4},
			{"ぼ", 0xE5},
			{"ぱ", 0xE6},
			{"ぴ", 0xE7},
			{"ぷ", 0xE8},
			{"ぺ", 0xE9},
			{"ぽ", 0xEA},
			{"を", 0xEB},
			{"ゃ", 0xEC},
			{"ゅ", 0xED},
			{"ょ", 0xEE},
			{"っ", 0xEF},
			
			{"&", 0xF0},
			{"＆", 0xF0},

			{"/", 0xF1},
			{"／", 0xF1},

			{"%", 0xF2},
			{"％", 0xF2},

			{"[↑]", 0xF3},

			{"[Lv]", 0xF4},
			{"[Ｌｖ]", 0xF4},

			{"[A]", 0xF5},
			{"[Ａ]", 0xF5},

			{"[↓]", 0xF6},
			
			{"[B]", 0xF7},
			{"[Ｂ]", 0xF7},
			
			{"↑", 0xF8},
			{"→", 0xF9},
			{"↓", 0xFA},
			{"←", 0xFB},
			
			{"[+]", 0xFC},
			{"[＋]", 0xFC},

			{"‥", 0xFD},

			{"\"", 0xFE},
			{"゛", 0xFE},
			{"”", 0xFE},
		};
		#endregion
		private static Dictionary<byte, string> ByteToStatusString = new Dictionary<byte, string>()
		#region
		{
			{0x00, " "},

			{0x01, "A"},
			{0x02, "B"},
			{0x03, "C"},
			{0x04, "D"},
			{0x05, "E"},
			{0x06, "F"},
			{0x07, "G"},
			{0x08, "H"},
			{0x09, "I"},
			{0x0A, "J"},
			{0x0B, "K"},
			{0x0C, "L"},
			{0x0D, "M"},
			{0x0E, "N"},
			{0x0F, "O"},
			{0x10, "P"},
			{0x11, "Q"},
			{0x12, "R"},
			{0x13, "S"},
			{0x14, "T"},
			{0x15, "U"},
			{0x16, "V"},
			{0x17, "W"},
			{0x18, "X"},
			{0x19, "Y"},
			{0x1A, "Z"},
			{0x1B, "ー"},
			{0x1C, "、"},
			{0x1D, "。"},
			
			{0x1E, "_"},

			{0x1F, "ヴ"},
			{0x20, "ァ"},
			{0x21, "ア"},
			{0x22, "イ"},
			{0x23, "ウ"},
			{0x24, "エ"},
			{0x25, "オ"},
			{0x26, "カ"},
			{0x27, "キ"},
			{0x28, "ク"},
			{0x29, "ケ"},
			{0x2A, "コ"},
			{0x2B, "サ"},
			{0x2C, "シ"},
			{0x2D, "ス"},
			{0x2E, "セ"},
			{0x2F, "ソ"},
			{0x30, "ィ"},
			{0x31, "タ"},
			{0x32, "チ"},
			{0x33, "ツ"},
			{0x34, "テ"},
			{0x35, "ト"},
			{0x36, "ナ"},
			{0x37, "ニ"},
			{0x38, "ヌ"},
			{0x39, "ネ"},
			{0x3A, "ノ"},
			{0x3B, "ハ"},
			{0x3C, "ヒ"},
			{0x3D, "フ"},
			{0x3E, "ヘ"},
			{0x3F, "ホ"},
			{0x40, "ゥ"},
			{0x41, "マ"},
			{0x42, "ミ"},
			{0x43, "ム"},
			{0x44, "メ"},
			{0x45, "モ"},
			{0x46, "ヤ"},
			{0x47, "ユ"},
			{0x48, "ヨ"},
			{0x49, "ワ"},
			{0x4A, "ン"},
			{0x4B, "ラ"},
			{0x4C, "リ"},
			{0x4D, "ル"},
			{0x4E, "レ"},
			{0x4F, "ロ"},
			{0x50, "ェ"},
			{0x51, "ガ"},
			{0x52, "ギ"},
			{0x53, "グ"},
			{0x54, "ゲ"},
			{0x55, "ゴ"},
			{0x56, "ザ"},
			{0x57, "ジ"},
			{0x58, "ズ"},
			{0x59, "ゼ"},
			{0x5A, "ゾ"},
			{0x5B, "ダ"},
			{0x5C, "ヂ"},
			{0x5D, "ヅ"},
			{0x5E, "デ"},
			{0x5F, "ド"},
			{0x60, "ォ"},
			{0x61, "バ"},
			{0x62, "ビ"},
			{0x63, "ブ"},
			{0x64, "ベ"},
			{0x65, "ボ"},
			{0x66, "パ"},
			{0x67, "ピ"},
			{0x68, "プ"},
			{0x69, "ペ"},
			{0x6A, "ポ"},
			{0x6B, "ヲ"},
			{0x6C, "ャ"},
			{0x6D, "ュ"},
			{0x6E, "ョ"},
			{0x6F, "ッ"},
			{0x70, "0"},
			{0x71, "1"},
			{0x72, "2"},
			{0x73, "3"},
			{0x74, "4"},
			{0x75, "5"},
			{0x76, "6"},
			{0x77, "7"},
			{0x78, "8"},
			{0x79, "9"},
			{0x7A, "-"},

			{0x7B, "!"},

			{0x7C, "?"},

			{0x7D, "・"},

			{0x7E, ":"},

			{0x7F, "."},

			{0x80, "～"},
			{0x81, "a"},
			{0x82, "b"},
			{0x83, "c"},
			{0x84, "d"},
			{0x85, "e"},
			{0x86, "f"},
			{0x87, "g"},
			{0x88, "h"},
			{0x89, "i"},
			{0x8A, "j"},
			{0x8B, "k"},
			{0x8C, "l"},
			{0x8D, "m"},
			{0x8E, "n"},
			{0x8F, "o"},
			{0x90, "p"},
			{0x91, "q"},
			{0x92, "r"},
			{0x93, "s"},
			{0x94, "t"},
			{0x95, "u"},
			{0x96, "v"},
			{0x97, "w"},
			{0x98, "x"},
			{0x99, "y"},
			{0x9A, "z"},

			{0x9B, "("},

			{0x9C, ")"},

			{0x9D, "「"},
			{0x9E, "」"},
			{0x9F, "…"},
			{0xA0, "ぁ"},
			{0xA1, "あ"},
			{0xA2, "い"},
			{0xA3, "う"},
			{0xA4, "え"},
			{0xA5, "お"},
			{0xA6, "か"},
			{0xA7, "き"},
			{0xA8, "く"},
			{0xA9, "け"},
			{0xAA, "こ"},
			{0xAB, "さ"},
			{0xAC, "し"},
			{0xAD, "す"},
			{0xAE, "せ"},
			{0xAF, "そ"},
			{0xB0, "ぃ"},
			{0xB1, "た"},
			{0xB2, "ち"},
			{0xB3, "つ"},
			{0xB4, "て"},
			{0xB5, "と"},
			{0xB6, "な"},
			{0xB7, "に"},
			{0xB8, "ぬ"},
			{0xB9, "ね"},
			{0xBA, "の"},
			{0xBB, "は"},
			{0xBC, "ひ"},
			{0xBD, "ふ"},
			{0xBE, "へ"},
			{0xBF, "ほ"},
			{0xC0, "ぅ"},
			{0xC1, "ま"},
			{0xC2, "み"},
			{0xC3, "む"},
			{0xC4, "め"},
			{0xC5, "も"},
			{0xC6, "や"},
			{0xC7, "ゆ"},
			{0xC8, "よ"},
			{0xC9, "わ"},
			{0xCA, "ん"},
			{0xCB, "ら"},
			{0xCC, "り"},
			{0xCD, "る"},
			{0xCE, "れ"},
			{0xCF, "ろ"},
			{0xD0, "ぇ"},
			{0xD1, "が"},
			{0xD2, "ぎ"},
			{0xD3, "ぐ"},
			{0xD4, "げ"},
			{0xD5, "ご"},
			{0xD6, "ざ"},
			{0xD7, "じ"},
			{0xD8, "ず"},
			{0xD9, "ぜ"},
			{0xDA, "ぞ"},
			{0xDB, "だ"},
			{0xDC, "ぢ"},
			{0xDD, "づ"},
			{0xDE, "で"},
			{0xDF, "ど"},
			{0xE0, "ぉ"},
			{0xE1, "ば"},
			{0xE2, "び"},
			{0xE3, "ぶ"},
			{0xE4, "べ"},
			{0xE5, "ぼ"},
			{0xE6, "ぱ"},
			{0xE7, "ぴ"},
			{0xE8, "ぷ"},
			{0xE9, "ぺ"},
			{0xEA, "ぽ"},
			{0xEB, "を"},
			{0xEC, "ゃ"},
			{0xED, "ゅ"},
			{0xEE, "ょ"},
			{0xEF, "っ"},

			{0xF0, "&"},

			{0xF1, "/"},

			{0xF2, "%"},

			{0xF3, "[↑]"},
			{0xF4, "[Lv]"},
			{0xF5, "[A]"},
			{0xF6, "[↓]"},
			{0xF7, "[B]"},
			{0xF8, "↑"},
			{0xF9, "→"},
			{0xFA, "↓"},
			{0xFB, "←"},
			
			{0xFC, "[+]"},

			{0xFD, "‥"},

			{0xFE, "\""},
		};
		#endregion

		public StatusString(string text)
		{
			Text = text;
		}
		public StatusString(TomatoAdventure tomatoAdventure, uint address, uint length)
		{
			load(tomatoAdventure, address, length);
		}

		public void load(TomatoAdventure tomatoAdventure, uint length)
		{
			load(tomatoAdventure, address, length);
		}
		public void load(TomatoAdventure tomatoAdventure, uint address, uint length)
		{
			this.address = address;

			StringBuilder text = new StringBuilder();
			text.Capacity = (int)length;

			for (uint i = 0; i < length; ++i)
			{
				text.Append(ByteToStatusString[tomatoAdventure.read(address + i)]);
			}

			this.text = text.ToString();
			this.text.TrimEnd();
		}

		public void save(TomatoAdventure tomatoAdventure, uint length)
		{
			save(tomatoAdventure, address, length);
		}
		public void save(TomatoAdventure tomatoAdventure, uint address, uint length)
		{
			byte[] data = toByteArray();

			for (uint i = 0; i < length; ++i)
			{
				if (i < data.GetLength(0))
				{
					tomatoAdventure.write(address + i, data[i]);
				}
				else
				{
					tomatoAdventure.write(address + i, 0x00);
				}
			}
		}

		public byte[] toByteArray()
		{
			List<byte> data = new List<byte>();
			{
				int i = 0;
				while (i < text.Length)
				{
					if (text.Substring(i, 1) == "[")
					{
						string test = text.Substring(i, text.IndexOf(']', i) - i + 1);
						data.Add(statusStringToByte[text.Substring(i, text.IndexOf(']', i) - i + 1)]);
						i = text.IndexOf(']', i) + 1;
					}
					else
					{
						data.Add(statusStringToByte[text.Substring(i, 1)]);
						++i;
					}
				}
			}
			return data.ToArray();
		}

		private static bool canToByte(string text)
		{
			int i = 0;
			while (i < text.Length)
			{
				if (text.Substring(i, 1) == "[")
				{
					string test = text.Substring(i, text.IndexOf(']', i) - i + 1);
					if (!statusStringToByte.ContainsKey(text.Substring(i, text.IndexOf(']', i) - i + 1)))
					{
						return false;
					}
					i = text.IndexOf(']', i) + 1;
				}
				else
				{
					if (!statusStringToByte.ContainsKey(text.Substring(i, 1)))
					{
						return false;
					}
					++i;
				}
			}
			return true;
		}
	}
}
