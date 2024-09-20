using System.Collections.Generic;

namespace Test
{
	internal class TestScript
	{
		// [Field] Dictionary of Player
		private Dictionary<int, string> _playerDictionary = new();

		private void TestMethod()
		{
			string s1 = _playerDictionary.GetValueOrDefault(0);

			string s2 = _playerDictionary[0];

			_playerDictionary.TryGetValue(0, out string s3);
		}
	}
}