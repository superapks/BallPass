using UnityEngine;
using Zenject;

namespace Core.Services {
	public class AudioPlayer : MonoBehaviour {
		[SerializeField] private AudioSource music;
		[SerializeField] private AudioSource click;
		[SerializeField] private AudioSource click2;
		[SerializeField] private AudioSource gameOver;
		public void PlayClickSound() => Play(click);
		public void PlayClick2Sound() => Play(click2);
		public void PlayGameOverSound() => Play(gameOver);
		private void Play(AudioSource source) => source.Play();
	}
}