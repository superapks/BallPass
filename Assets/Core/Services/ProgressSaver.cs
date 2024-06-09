using UnityEngine;

namespace Core.Services {
	public class ProgressSaver {
		private const string ScoreKey = "Score";
		private const string IsMusicOnKey = "IsMusicOn";
		private const string IsSoundsOnKey = "IsSoundsOnKey";

		public int Score {
			get => PlayerPrefs.GetInt(ScoreKey, 0);
			set => PlayerPrefs.SetInt(ScoreKey, value);
		}
		public bool IsMusicOn {
			get => PlayerPrefs.GetInt(IsMusicOnKey, 1) == 1;
			set => PlayerPrefs.SetInt(IsMusicOnKey, value ? 1 : 0);
		}
		public bool IsSoundsOn {
			get => PlayerPrefs.GetInt(IsSoundsOnKey, 1) == 1;
			set => PlayerPrefs.SetInt(IsSoundsOnKey, value ? 1 : 0);
		}
	}
}