using Lidgren.Network;
using Xunit;

namespace UnitTests
{
	public class NetQueueTests : BaseTest
	{
		[Fact]
		public void NetQueue_EnqueueAndToArray_ShouldReturnCorrectArray()
		{
			var queue = new NetQueue<int>(4);

			queue.Enqueue(1);
			queue.Enqueue(2);
			queue.Enqueue(3);

			int[] arr = queue.ToArray();

			Assert.Equal(3, arr.Length);
			Assert.Equal(1, arr[0]);
			Assert.Equal(2, arr[1]);
			Assert.Equal(3, arr[2]);
		}

		[Fact]
		public void NetQueue_Contains_ShouldReturnCorrectResults()
		{
			var queue = new NetQueue<int>(4);
			queue.Enqueue(1);
			queue.Enqueue(2);
			queue.Enqueue(3);

			Assert.False(queue.Contains(4), "NetQueue.Contains should return false for an absent item.");
			Assert.True(queue.Contains(2), "NetQueue.Contains should return true for a present item.");
		}

		[Fact]
		public void NetQueue_TryDequeue_ShouldReturnCorrectResults()
		{
			var queue = new NetQueue<int>(4);
			queue.Enqueue(1);
			queue.Enqueue(2);
			queue.Enqueue(3);

			Assert.Equal(3, queue.Count);

			Assert.True(queue.TryDequeue(out int a));
			Assert.Equal(1, a);
			Assert.Equal(2, queue.Count);

			queue.EnqueueFirst(42);
			Assert.Equal(3, queue.Count);

			Assert.True(queue.TryDequeue(out a));
			Assert.Equal(42, a);

			Assert.True(queue.TryDequeue(out a));
			Assert.Equal(2, a);

			Assert.True(queue.TryDequeue(out a));
			Assert.Equal(3, a);

			Assert.False(queue.TryDequeue(out a));
			Assert.False(queue.TryDequeue(out a));
		}

		[Fact]
		public void NetQueue_Clear_ShouldEmptyTheQueue()
		{
			var queue = new NetQueue<int>(4);
			queue.Enqueue(78);

			Assert.Equal(1, queue.Count);

			Assert.True(queue.TryDequeue(out int a));
			Assert.Equal(78, a);

			queue.Clear();
			Assert.Equal(0, queue.Count);

			int[] arr2 = queue.ToArray();
			Assert.Empty(arr2);
		}
	}
}
