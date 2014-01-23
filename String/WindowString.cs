using System;
using System.Collections.Generic;
using System.Text;


namespace TomatoTool
{
	public class WindowString : ROMObject
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
				return (uint)toByteArray().GetLength(0);
			}
		}

		public override uint Alignment
		{
			get
			{
				return ALIGNMENT;
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

		public const uint ALIGNMENT = 1;

		private static Dictionary<string, byte> windowStringToByte = new Dictionary<string, byte>()
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
			
			{"=", 0x1E},
			{"＝", 0x1E},

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
			{"‥", 0x9F},
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
		};
		#endregion
		private static Dictionary<byte, string> byteToWindowString = new Dictionary<byte, string>()
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
			
			{0x1E, "="},

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
			{0x9F, "‥"},
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
		};
		#endregion

		private static Dictionary<string, byte> windowChineseCharacterStringToByte = new Dictionary<string, byte>()
		#region
		{
			{"気", 0x00},
			{"雨", 0x01},
			{"天", 0x02},
			{"空", 0x03},
			{"川", 0x04},
			{"山", 0x05},
			{"林", 0x06},
			{"森", 0x07},
			{"水", 0x08},
			{"火", 0x09},
			{"犬", 0x0A},
			{"虫", 0x0B},
			{"貝", 0x0C},
			{"竹", 0x0D},
			{"木", 0x0E},
			{"草", 0x0F},
			{"花", 0x10},
			{"名", 0x11},
			{"男", 0x12},
			{"女", 0x13},
			{"子", 0x14},
			{"人", 0x15},
			{"生", 0x16},
			{"目", 0x17},
			{"耳", 0x18},
			{"口", 0x19},
			{"手", 0x1A},
			{"足", 0x1B},
			{"糸", 0x1C},
			{"年", 0x1D},
			{"月", 0x1E},
			{"日", 0x1F},
			{"夕", 0x20},
			{"一", 0x21},
			{"二", 0x22},
			{"三", 0x23},
			{"四", 0x24},
			{"五", 0x25},
			{"六", 0x26},
			{"七", 0x27},
			{"八", 0x28},
			{"九", 0x29},
			{"十", 0x2A},
			{"百", 0x2B},
			{"千", 0x2C},
			{"左", 0x2D},
			{"右", 0x2E},
			{"先", 0x2F},
			{"中", 0x30},
			{"円", 0x31},
			{"玉", 0x32},
			{"大", 0x33},
			{"小", 0x34},
			{"早", 0x35},
			{"上", 0x36},
			{"下", 0x37},
			{"入", 0x38},
			{"出", 0x39},
			{"見", 0x3A},
			{"立", 0x3B},
			{"学", 0x3C},
			{"校", 0x3D},
			{"字", 0x3E},
			{"文", 0x3F},
			{"本", 0x40},
			{"力", 0x41},
			{"音", 0x42},
			{"休", 0x43},
			{"白", 0x44},
			{"赤", 0x45},
			{"青", 0x46},
			{"正", 0x47},
			{"車", 0x48},
			{"田", 0x49},
			{"土", 0x4A},
			{"石", 0x4B},
			{"金", 0x4C},
			{"町", 0x4D},
			{"村", 0x4E},
			{"王", 0x4F},
			{"春", 0x50},
			{"夏", 0x51},
			{"秋", 0x52},
			{"冬", 0x53},
			{"晴", 0x54},
			{"風", 0x55},
			{"雲", 0x56},
			{"雪", 0x57},
			{"星", 0x58},
			{"海", 0x59},
			{"池", 0x5A},
			{"谷", 0x5B},
			{"原", 0x5C},
			{"野", 0x5D},
			{"馬", 0x5E},
			{"牛", 0x5F},
			{"鳥", 0x60},
			{"魚", 0x61},
			{"羽", 0x62},
			{"鳴", 0x63},
			{"麦", 0x64},
			{"自", 0x65},
			{"友", 0x66},
			{"組", 0x67},
			{"親", 0x68},
			{"父", 0x69},
			{"母", 0x6A},
			{"兄", 0x6B},
			{"弟", 0x6C},
			{"姉", 0x6D},
			{"妹", 0x6E},
			{"体", 0x6F},
			{"毛", 0x70},
			{"頭", 0x71},
			{"顔", 0x72},
			{"首", 0x73},
			{"肉", 0x74},
			{"米", 0x75},
			{"食", 0x76},
			{"茶", 0x77},
			{"家", 0x78},
			{"室", 0x79},
			{"園", 0x7A},
			{"門", 0x7B},
			{"戸", 0x7C},
			{"台", 0x7D},
			{"活", 0x7E},
			{"週", 0x7F},
			{"曜", 0x80},
			{"時", 0x81},
			{"分", 0x82},
			{"朝", 0x83},
			{"昼", 0x84},
			{"午", 0x85},
			{"夜", 0x86},
			{"古", 0x87},
			{"今", 0x88},
			{"万", 0x89},
			{"数", 0x8A},
			{"算", 0x8B},
			{"計", 0x8C},
			{"半", 0x8D},
			{"何", 0x8E},
			{"番", 0x8F},
			{"前", 0x90},
			{"後", 0x91},
			{"内", 0x92},
			{"外", 0x93},
			{"角", 0x94},
			{"間", 0x95},
			{"東", 0x96},
			{"西", 0x97},
			{"南", 0x98},
			{"北", 0x99},
			{"方", 0x9A},
			{"形", 0x9B},
			{"場", 0x9C},
			{"点", 0x9D},
			{"線", 0x9E},
			{"直", 0x9F},
			{"丸", 0xA0},
			{"長", 0xA1},
			{"太", 0xA2},
			{"細", 0xA3},
			{"高", 0xA4},
			{"広", 0xA5},
			{"遠", 0xA6},
			{"近", 0xA7},
			{"多", 0xA8},
			{"少", 0xA9},
			{"強", 0xAA},
			{"弱", 0xAB},
			{"同", 0xAC},
			{"新", 0xAD},
			{"毎", 0xAE},
			{"交", 0xAF},
			{"引", 0xB0},
			{"合", 0xB1},
			{"歩", 0xB2},
			{"走", 0xB3},
			{"来", 0xB4},
			{"帰", 0xB5},
			{"通", 0xB6},
			{"止", 0xB7},
			{"回", 0xB8},
			{"会", 0xB9},
			{"行", 0xBA},
			{"切", 0xBB},
			{"楽", 0xBC},
			{"思", 0xBD},
			{"考", 0xBE},
			{"知", 0xBF},
			{"言", 0xC0},
			{"話", 0xC1},
			{"語", 0xC2},
			{"聞", 0xC3},
			{"答", 0xC4},
			{"教", 0xC5},
			{"才", 0xC6},
			{"当", 0xC7},
			{"読", 0xC8},
			{"書", 0xC9},
			{"記", 0xCA},
			{"紙", 0xCB},
			{"理", 0xCC},
			{"科", 0xCD},
			{"電", 0xCE},
			{"元", 0xCF},
			{"歌", 0xD0},
			{"声", 0xD1},
			{"絵", 0xD2},
			{"画", 0xD3},
			{"図", 0xD4},
			{"色", 0xD5},
			{"黒", 0xD6},
			{"黄", 0xD7},
			{"光", 0xD8},
			{"明", 0xD9},
			{"心", 0xDA},
			{"社", 0xDB},
			{"寺", 0xDC},
			{"道", 0xDD},
			{"汽", 0xDE},
			{"船", 0xDF},
			{"工", 0xE0},
			{"作", 0xE1},
			{"地", 0xE2},
			{"岩", 0xE3},
			{"用", 0xE4},
			{"売", 0xE5},
			{"買", 0xE6},
			{"店", 0xE7},
			{"国", 0xE8},
			{"京", 0xE9},
			{"市", 0xEA},
			{"里", 0xEB},
			{"弓", 0xEC},
			{"矢", 0xED},
			{"刀", 0xEE},
			{"公", 0xEF},
			{"↑", 0xF0},
			{"→", 0xF1},
			{"↓", 0xF2},
			{"←", 0xF3},
			{"[L]", 0xF4},
			{"[R]", 0xF5},
			{"[A]", 0xF6},
			{"[B]", 0xF7},
			{"[ハート]", 0xF8},
			{"♪", 0xF9},
			{"☆", 0xFA},
			{"…", 0xFB},
			{"□", 0xFC},
		};
		#endregion
		private static Dictionary<byte, string> byteToWindowChineseCharacterString = new Dictionary<byte, string>()
		#region
		{
			{0x00, "気"},
			{0x01, "雨"},
			{0x02, "天"},
			{0x03, "空"},
			{0x04, "川"},
			{0x05, "山"},
			{0x06, "林"},
			{0x07, "森"},
			{0x08, "水"},
			{0x09, "火"},
			{0x0A, "犬"},
			{0x0B, "虫"},
			{0x0C, "貝"},
			{0x0D, "竹"},
			{0x0E, "木"},
			{0x0F, "草"},
			{0x10, "花"},
			{0x11, "名"},
			{0x12, "男"},
			{0x13, "女"},
			{0x14, "子"},
			{0x15, "人"},
			{0x16, "生"},
			{0x17, "目"},
			{0x18, "耳"},
			{0x19, "口"},
			{0x1A, "手"},
			{0x1B, "足"},
			{0x1C, "糸"},
			{0x1D, "年"},
			{0x1E, "月"},
			{0x1F, "日"},
			{0x20, "夕"},
			{0x21, "一"},
			{0x22, "二"},
			{0x23, "三"},
			{0x24, "四"},
			{0x25, "五"},
			{0x26, "六"},
			{0x27, "七"},
			{0x28, "八"},
			{0x29, "九"},
			{0x2A, "十"},
			{0x2B, "百"},
			{0x2C, "千"},
			{0x2D, "左"},
			{0x2E, "右"},
			{0x2F, "先"},
			{0x30, "中"},
			{0x31, "円"},
			{0x32, "玉"},
			{0x33, "大"},
			{0x34, "小"},
			{0x35, "早"},
			{0x36, "上"},
			{0x37, "下"},
			{0x38, "入"},
			{0x39, "出"},
			{0x3A, "見"},
			{0x3B, "立"},
			{0x3C, "学"},
			{0x3D, "校"},
			{0x3E, "字"},
			{0x3F, "文"},
			{0x40, "本"},
			{0x41, "力"},
			{0x42, "音"},
			{0x43, "休"},
			{0x44, "白"},
			{0x45, "赤"},
			{0x46, "青"},
			{0x47, "正"},
			{0x48, "車"},
			{0x49, "田"},
			{0x4A, "土"},
			{0x4B, "石"},
			{0x4C, "金"},
			{0x4D, "町"},
			{0x4E, "村"},
			{0x4F, "王"},
			{0x50, "春"},
			{0x51, "夏"},
			{0x52, "秋"},
			{0x53, "冬"},
			{0x54, "晴"},
			{0x55, "風"},
			{0x56, "雲"},
			{0x57, "雪"},
			{0x58, "星"},
			{0x59, "海"},
			{0x5A, "池"},
			{0x5B, "谷"},
			{0x5C, "原"},
			{0x5D, "野"},
			{0x5E, "馬"},
			{0x5F, "牛"},
			{0x60, "鳥"},
			{0x61, "魚"},
			{0x62, "羽"},
			{0x63, "鳴"},
			{0x64, "麦"},
			{0x65, "自"},
			{0x66, "友"},
			{0x67, "組"},
			{0x68, "親"},
			{0x69, "父"},
			{0x6A, "母"},
			{0x6B, "兄"},
			{0x6C, "弟"},
			{0x6D, "姉"},
			{0x6E, "妹"},
			{0x6F, "体"},
			{0x70, "毛"},
			{0x71, "頭"},
			{0x72, "顔"},
			{0x73, "首"},
			{0x74, "肉"},
			{0x75, "米"},
			{0x76, "食"},
			{0x77, "茶"},
			{0x78, "家"},
			{0x79, "室"},
			{0x7A, "園"},
			{0x7B, "門"},
			{0x7C, "戸"},
			{0x7D, "台"},
			{0x7E, "活"},
			{0x7F, "週"},
			{0x80, "曜"},
			{0x81, "時"},
			{0x82, "分"},
			{0x83, "朝"},
			{0x84, "昼"},
			{0x85, "午"},
			{0x86, "夜"},
			{0x87, "古"},
			{0x88, "今"},
			{0x89, "万"},
			{0x8A, "数"},
			{0x8B, "算"},
			{0x8C, "計"},
			{0x8D, "半"},
			{0x8E, "何"},
			{0x8F, "番"},
			{0x90, "前"},
			{0x91, "後"},
			{0x92, "内"},
			{0x93, "外"},
			{0x94, "角"},
			{0x95, "間"},
			{0x96, "東"},
			{0x97, "西"},
			{0x98, "南"},
			{0x99, "北"},
			{0x9A, "方"},
			{0x9B, "形"},
			{0x9C, "場"},
			{0x9D, "点"},
			{0x9E, "線"},
			{0x9F, "直"},
			{0xA0, "丸"},
			{0xA1, "長"},
			{0xA2, "太"},
			{0xA3, "細"},
			{0xA4, "高"},
			{0xA5, "広"},
			{0xA6, "遠"},
			{0xA7, "近"},
			{0xA8, "多"},
			{0xA9, "少"},
			{0xAA, "強"},
			{0xAB, "弱"},
			{0xAC, "同"},
			{0xAD, "新"},
			{0xAE, "毎"},
			{0xAF, "交"},
			{0xB0, "引"},
			{0xB1, "合"},
			{0xB2, "歩"},
			{0xB3, "走"},
			{0xB4, "来"},
			{0xB5, "帰"},
			{0xB6, "通"},
			{0xB7, "止"},
			{0xB8, "回"},
			{0xB9, "会"},
			{0xBA, "行"},
			{0xBB, "切"},
			{0xBC, "楽"},
			{0xBD, "思"},
			{0xBE, "考"},
			{0xBF, "知"},
			{0xC0, "言"},
			{0xC1, "話"},
			{0xC2, "語"},
			{0xC3, "聞"},
			{0xC4, "答"},
			{0xC5, "教"},
			{0xC6, "才"},
			{0xC7, "当"},
			{0xC8, "読"},
			{0xC9, "書"},
			{0xCA, "記"},
			{0xCB, "紙"},
			{0xCC, "理"},
			{0xCD, "科"},
			{0xCE, "電"},
			{0xCF, "元"},
			{0xD0, "歌"},
			{0xD1, "声"},
			{0xD2, "絵"},
			{0xD3, "画"},
			{0xD4, "図"},
			{0xD5, "色"},
			{0xD6, "黒"},
			{0xD7, "黄"},
			{0xD8, "光"},
			{0xD9, "明"},
			{0xDA, "心"},
			{0xDB, "社"},
			{0xDC, "寺"},
			{0xDD, "道"},
			{0xDE, "汽"},
			{0xDF, "船"},
			{0xE0, "工"},
			{0xE1, "作"},
			{0xE2, "地"},
			{0xE3, "岩"},
			{0xE4, "用"},
			{0xE5, "売"},
			{0xE6, "買"},
			{0xE7, "店"},
			{0xE8, "国"},
			{0xE9, "京"},
			{0xEA, "市"},
			{0xEB, "里"},
			{0xEC, "弓"},
			{0xED, "矢"},
			{0xEE, "刀"},
			{0xEF, "公"},
			{0xF0, "↑"},
			{0xF1, "→"},
			{0xF2, "↓"},
			{0xF3, "←"},
			{0xF4, "[L]"},
			{0xF5, "[R]"},
			{0xF6, "[A]"},
			{0xF7, "[B]"},
			{0xF8, "[ハート]"},
			{0xF9, "♪"},
			{0xFA, "☆"},
			{0xFB, "…"},
			{0xFC, "□"},
		};
		#endregion

		private static Dictionary<string, byte> systemCodeStringToByte = new Dictionary<string, byte>()
		#region
		{
			{"[CHA0]", 0x81},
			{"[CHA1]", 0x82},
			{"[CHA2]", 0x83},
			{"[CHA3]", 0x84},
		};
		#endregion
		private static Dictionary<byte, string> byteToSystemCodeString = new Dictionary<byte, string>()
		#region
		{
			//エンド
			{0x00, "[END]"},
			//ボタンニューページ
			{0x01, "[BNP]"},
			//ボタンニューライン
			{0x02, "[BNL]"},

			//不明
			{0x04, "0x04"},

			//ニューライン
			{0x05, "[NL]"},

			//不明
			{0x06, "0x06"},

			//不明
			{0x11, "0x11"},

			//不明
			{0x12, "0x12"},

			//ビックウィズ
			{0x1C, "[BW]"},
			//ビックハイト
			{0x1D, "[BH]"},
			//ビックウィズハイト
			{0x1E, "[BWH]"},
			//ノーマルウィズハイト
			{0x1F, "[NWH]"},
			//センターカラム
			{0x24, "[CC]"},
			//センターライン
			{0x25, "[CL]"},
			//センターカラムライン
			{0x26, "[CCL]"},
			//レッドエンド
			{0x30, "[RE]"},
			//レッド
			{0x32, "[R]"},
			//ボタンスタート
			{0x40, "[BST]"},
			
			//不明
			{0x41, "0x41"},

			//不明
			{0x42, "0x42"},

			//不明
			{0x43, "0x43"},

			//不明
			{0x80, "0x80"},

			{0x81, "[CHA0]"},
			{0x82, "[CHA1]"},
			{0x83, "[CHA2]"},
			{0x84, "[CHA3]"},

			//不明
			{0x85, "0x85"},

		};
		#endregion

		public WindowString(string text)
		{
			Text = text;
		}
		public WindowString(TomatoAdventure tomatoAdventure, uint address)
			
		{
			load(tomatoAdventure, address);
		}
		public WindowString(TomatoAdventure tomatoAdventure, uint address, uint length)
			
		{
			load(tomatoAdventure, address, length);
		}

		public void load(TomatoAdventure tomatoAdventure, uint address)
		{
			this.address = address;
			if (tomatoAdventure.isAddress(address))
			{
				StringBuilder text = new StringBuilder();
				text.Capacity = 128;
				{
					uint i = 0;
					while (true)
					{
						if (tomatoAdventure.read(address + i) == 0xFE)
						{
							++i;
							text.Append(byteToWindowChineseCharacterString[tomatoAdventure.read(address + i)]);
						}
						else if (tomatoAdventure.read(address + i) == 0xFF)
						{
							++i;

							text.Append(byteToSystemCodeString[tomatoAdventure.read(address + i)]);

							if (tomatoAdventure.read(address + i) == 0x00)
							{
								break;
							}
						}
						else
						{
							text.Append(byteToWindowString[tomatoAdventure.read(address + i)]);
						}

						++i;
					}
				}

				this.text = text.ToString();
				this.text.TrimEnd();
			}
		}

		public void load(TomatoAdventure tomatoAdventure, uint address, uint length)
		{
			this.address = address;

			StringBuilder text = new StringBuilder();
			
			for (uint i = 0; i < length; ++i)
			{
				if (tomatoAdventure.read(address + i) == 0xFE)
				{
					++i;
					text.Append(byteToWindowChineseCharacterString[tomatoAdventure.read(address + i)]);
				}
				else if (tomatoAdventure.read(address + i) == 0xFF)
				{
					++i;
					text.Append(byteToSystemCodeString[tomatoAdventure.read(address + i)]);
				}
				else
				{
					text.Append(byteToWindowString[tomatoAdventure.read(address + i)]);
				}
			}

			this.text = text.ToString();
			this.text.TrimEnd();
		}

		public void save(TomatoAdventure tomatoAdventure, uint address)
		{
			byte[] data = toByteArray();

			for (uint i = 0; i < data.GetLength(0); ++i)
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
		public void save(TomatoAdventure tomatoAdventure, uint address, int length)
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
						if (windowChineseCharacterStringToByte.ContainsKey(text.Substring(i, text.IndexOf(']', i) - i + 1)))
						{
							data.Add(0xFE);
							data.Add(windowChineseCharacterStringToByte[text.Substring(i, text.IndexOf(']', i) - i + 1)]);
						}
						else if (systemCodeStringToByte.ContainsKey(text.Substring(i, text.IndexOf(']', i) - i + 1)))
						{
							data.Add(0xFF);
							data.Add(systemCodeStringToByte[text.Substring(i, text.IndexOf(']', i) - i + 1)]);
						}
						else
						{
							throw new System.Collections.Generic.KeyNotFoundException();
						}
						i = text.IndexOf(']', i) + 1;
					}
					else
					{
						if (windowStringToByte.ContainsKey(text.Substring(i, 1)))
						{
							data.Add(windowStringToByte[text.Substring(i, 1)]);
						}
						else if (windowChineseCharacterStringToByte.ContainsKey(text.Substring(i, 1)))
						{
							data.Add(0xFE);
							data.Add(windowChineseCharacterStringToByte[text.Substring(i, 1)]);
						}
						else
						{
							throw new System.Collections.Generic.KeyNotFoundException();
						}
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
					if (windowStringToByte.ContainsKey(text.Substring(i, text.IndexOf(']', i) - i + 1)))
					{
					}
					else if (systemCodeStringToByte.ContainsKey(text.Substring(i, text.IndexOf(']', i) - i + 1)))
					{
					}
					else
					{
						return false;
					}
					i = text.IndexOf(']', i) + 1;
				}
				else
				{
					if (windowStringToByte.ContainsKey(text.Substring(i, 1)))
					{
					}
					else if (windowChineseCharacterStringToByte.ContainsKey(text.Substring(i, 1)))
					{
					}
					else
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
