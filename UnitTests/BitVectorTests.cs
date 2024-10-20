using System;
using Lidgren.Network;
using Xunit;

namespace UnitTests
{
	public class BitVectorTests : BaseTest
	{
		[Fact]
		public void BitVector_SetAndGetBit_ShouldBehaveCorrectly()
		{
			var v = new NetBitVector(256);
			for (int i = 0; i < 256; i++)
			{
				v.Clear();
				if (i > 42 && i < 65)
					v = new NetBitVector(256);

				Assert.True(v.IsEmpty(), "bit vector fail 1");
				
				v.Set(i, true);

				Assert.True(v.Get(i), "bit vector fail 2");
				Assert.False(v.IsEmpty(), "bit vector fail 3");

				if (i != 79)
					Assert.False(v.Get(79), "bit vector fail 4");

				int f = v.GetFirstSetIndex();
				Assert.Equal(f, i);
			}
		}
	}
}
