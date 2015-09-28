using System;
using System.Threading;

namespace Tynamix.ObjectFiller
{
	public static class ThreadSafeRandomProvider
	{
		private static int _seed = Environment.TickCount;

		private static readonly ThreadLocal<System.Random> _randomWrapper = new ThreadLocal<System.Random>(() =>
			new System.Random(Interlocked.Increment(ref _seed))
		);

		public static System.Random GetThreadRandom()
		{
			return _randomWrapper.Value;
		}
	}
}