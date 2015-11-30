using System.Security.Cryptography;
using System.Threading;

namespace Tynamix.ObjectFiller
{
	public static class ThreadSafeRandomProvider
	{
		private static readonly int _seed;

		static ThreadSafeRandomProvider()
		{
			_seed = GetRandomSeed();
		}

		private static int GetRandomSeed()
		{
			using (var rngCsp = new RNGCryptoServiceProvider())
			{
				var randomNumber = new byte[1];
				rngCsp.GetBytes(randomNumber);
				return randomNumber[0];
			}
		}

		private static readonly ThreadLocal<System.Random> _randomWrapper =
			new ThreadLocal<System.Random>(() => new System.Random(GetRandomSeed()));

		public static System.Random GetThreadRandom()
		{
			return _randomWrapper.Value;
		}
	}
}