using System;
using System.Collections.Generic;

namespace TomatoTool
{
	public class LZ77
		:
		Tile4BitList
	{
		private uint size;
		public override uint Size
		{
			get
			{
				if (saved)
				{
					return size;
				}
				else
				{
					byte[] buffer = new byte[tile.Count * Tile4Bit.SIZE];

					for (int i = 0; i < tile.Count; ++i)
					{
						for (int y = 0; y < Tile4Bit.TILE_LENGTH_1; ++y)
						{
							for (int x = 0; x < Tile4Bit.TILE_LENGTH_0; ++x)
							{
								buffer[(i * Tile4Bit.SIZE) + (y * Tile4Bit.TILE_LENGTH_0) + x] = tile[i][x, y];
							}
						}
					}

					return size = (uint)LZ77.compression(buffer).GetLength(0);
				}
			}
		}

		public override uint Alignment
		{
			get
			{
				throw new System.NotImplementedException();
			}
		}

		public const uint COMPRESSION_BLOCK_LENGTH_0 = 8;

		public const byte HEADER = 0x10;

		public const uint MINIMUM_COMPRESSION_LENGTH_0 = 3;
		public const uint MAXIMUM_COMPRESSION_LENGTH_0 = 0x0F + MINIMUM_COMPRESSION_LENGTH_0;

		public LZ77(TomatoAdventure tomatoAdventure, uint address)
			:
			base(address)
		{
			load(tomatoAdventure, address);
		}

		public void load(TomatoAdventure tomatoAdventure, uint address)
		{
			this.address = address;

			if (tomatoAdventure.read(address) == HEADER)
			{
				uint size = tomatoAdventure.readLittleEndian(address + 1, 3);

				byte[] buffer = new byte[size];

				uint unCompressionPosition = 0;

				uint compressionPosition = address + 4;

				while (unCompressionPosition < size)
				{
					byte header = tomatoAdventure.read(compressionPosition);
					++compressionPosition;

					int i = 0;
					while ((i < COMPRESSION_BLOCK_LENGTH_0) && (unCompressionPosition < size))
					{
						if ((header & (0x80 >> i)) != 0)
						{
							for (int j = 0; j < ((tomatoAdventure.read(compressionPosition) >> 4) + MINIMUM_COMPRESSION_LENGTH_0); ++j)
							{
								buffer[unCompressionPosition] = buffer[unCompressionPosition - (tomatoAdventure.readBigEndian(compressionPosition, 2) & 0x0FFF) - 1];
								++unCompressionPosition;
							}
							compressionPosition += 2;
						}
						else
						{
							buffer[unCompressionPosition] = tomatoAdventure.read(compressionPosition);

							++unCompressionPosition;
							++compressionPosition;
						}
						++i;
					}
				}

				this.size = compressionPosition - address;

				Tile4Bit[] tile = new Tile4Bit[size / Tile4Bit.SIZE];

				for (uint i = 0; i < tile.GetLength(0); ++i)
				{
					byte[,] data = new byte[Tile4Bit.TILE_LENGTH_0, Tile4Bit.TILE_LENGTH_1];

					for (uint y = 0; y < data.GetLength(1); ++y)
					{
						for (uint x = 0; x < data.GetLength(0); ++x)
						{
							data[x, y] = buffer[(i * Tile4Bit.SIZE) + (y * Tile4Bit.TILE_LENGTH_0) + x];
						}
					}
					tile[i] = new Tile4Bit(data);
				}
				base.tile = new List<Tile4Bit>(tile);
			}
		}

		public void save(TomatoAdventure tomatoAdventure)
		{
			if (!saved)
			{
				saved = true;

				byte[] buffer = new byte[tile.Count * Tile4Bit.SIZE];

				for (int i = 0; i < tile.Count; ++i)
				{
					for (int y = 0; y < Tile4Bit.TILE_LENGTH_1; ++y)
					{
						for (int x = 0; x < Tile4Bit.TILE_LENGTH_0; ++x)
						{
							buffer[(i * Tile4Bit.SIZE) + (y * Tile4Bit.TILE_LENGTH_0) + x] = tile[i][x, y];
						}
					}
				}

				byte[] compression = LZ77.compression(buffer);

				address = tomatoAdventure.malloc(Size, Alignment);

				tomatoAdventure.writeArray(address, (uint)compression.GetLength(0), compression);
			}
		}
		public new void save(TomatoAdventure tomatoAdventure, uint address)
		{
			this.address = address;

			byte[] buffer = new byte[tile.Count * Tile4Bit.SIZE];

			for (int i = 0; i < tile.Count; ++i)
			{
				for (int y = 0; y < Tile4Bit.TILE_LENGTH_1; ++y)
				{
					for (int x = 0; x < Tile4Bit.TILE_LENGTH_0; ++x)
					{
						buffer[(i * Tile4Bit.SIZE) + (y * Tile4Bit.TILE_LENGTH_0) + x] = tile[i][x, y];
					}
				}
			}

			byte[] compression = LZ77.compression(buffer);
			tomatoAdventure.writeArray(address, (uint)compression.GetLength(0), compression);
		}

		public static byte[] compression(byte[] source)
		{
			List<byte> compression = new List<byte>();

			compression.Add(HEADER);

			//解凍後のサイズを追加
			compression.Add((byte)(source.GetLength(0) & 0xFF));
			compression.Add((byte)((source.GetLength(0) & 0xFF00) >> 8));
			compression.Add((byte)((source.GetLength(0) & 0xFF0000) >> 16));

			int unCompressionPosition = 0;

			while (unCompressionPosition + 1 < source.GetLength(0))
			{
				byte header = 0;
				ushort[] compressionBlock = new ushort[COMPRESSION_BLOCK_LENGTH_0];

				for (int i = 0; i < compressionBlock.GetLength(0); ++i)
				{
					int j;
					if (MAXIMUM_COMPRESSION_LENGTH_0 < (source.GetLength(0) - unCompressionPosition))
					{
						j = (int)MAXIMUM_COMPRESSION_LENGTH_0;
					}
					else
					{
						j = source.GetLength(0) - unCompressionPosition;
					}

					byte[] array = new byte[j];
					Buffer.BlockCopy(source, unCompressionPosition, array, 0, array.GetLength(0));

					while (true)
					{
						if (j >= MINIMUM_COMPRESSION_LENGTH_0)
						{
							int index = byteArraySearch(source, (((unCompressionPosition - 0x0FFF) > 0) ? (unCompressionPosition - 0x0FFF) : 0), unCompressionPosition - 1, array, j);

							if (index != -1)
							{
								header |= (byte)(0x80 >> i);
								compressionBlock[i] = (ushort)(((j - MINIMUM_COMPRESSION_LENGTH_0) << 12) + (unCompressionPosition - index - 1));

								unCompressionPosition += j;
								break;
							}
							else if (j <= MINIMUM_COMPRESSION_LENGTH_0)
							{
								compressionBlock[i] = source[unCompressionPosition];
								++unCompressionPosition;
							}

							--j;
						}
						else if (unCompressionPosition + 1 > source.GetLength(0))
						{
							break;
						}
						else if (MINIMUM_COMPRESSION_LENGTH_0 > (source.GetLength(0) - unCompressionPosition))
						{
							compressionBlock[i] = source[unCompressionPosition];
							++unCompressionPosition;
							break;
						}
						else
						{
							break;
						}
					}
				}

				compression.Add(header);
				for (int i = 0; i < compressionBlock.GetLength(0); ++i)
				{
					if ((header & (0x80 >> i)) != 0)
					{
						compression.Add((byte)(compressionBlock[i] >> 8));
						compression.Add((byte)(compressionBlock[i] & 0xFF));
					}
					else
					{
						compression.Add((byte)(compressionBlock[i] & 0xFF));
					}
				}
			}

			return compression.ToArray();
		}

		private static int byteArraySearch(byte[] array, int start, int end, byte[] value, int valueLength)
		{
			int j = 0;
			int k = 0;

			for (int i = start; i < end; ++i)
			{
				j = 0;
				k = 0;
				while (array[i + k] == value[j])
				{
					++k;
					if ((i + k) > end)
					{
						k = 0;
					}

					++j;
					if (valueLength <= j)
					{
						return i;
					}
				}
			}
			return -1;
		}

	}
}