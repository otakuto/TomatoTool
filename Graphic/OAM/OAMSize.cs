using System;

namespace TomatoTool
{
	public enum OAMSize : byte
	{
		_8x8 = 0x00,
		_16x16 = 0x10,
		_32x32 = 0x20,
		_64x64 = 0x30,

		_16x8 = 0x01,
		_32x8 = 0x11,
		_32x16 = 0x21,
		_64x32 = 0x31,

		_8x16 = 0x02,
		_8x32 = 0x12,
		_16x32 = 0x22,
		_32x64 = 0x32
	}
}
