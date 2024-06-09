using Core.Services;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using Zenject;

namespace Core.UI {
	public class MainMenu : MonoBehaviour {
		[SerializeField] private UIDocument document;
		private VisualElement root;
		private ProgressSaver progressSaver;
		private AudioPlayer audioPlayer;

		[Inject]
		private void Construct(ProgressSaver progressSaver, AudioPlayer audioPlayer) {
			this.progressSaver = progressSaver;
			this.audioPlayer = audioPlayer;
		}
		private void Start() {
			root = document.rootVisualElement;
			
			var playButton = root.Q<Button>("play-button");
			
			playButton.clicked += () => {
				PlayClickSound();
				SceneManager.LoadScene("Game");
			};
		}
		// private void ToggleMusic() {
		// 	PlayClickSound();
		//
		// 	if (progressSaver.IsMusicOn) {
		// 		progressSaver.IsMusicOn = false;
		// 		audioPlayer.MuteMusic();
		// 		musicButton.style.backgroundImage = offImage;
		// 	}
		// 	else {
		// 		progressSaver.IsMusicOn = true;
		// 		audioPlayer.UnmuteMusic();
		// 		musicButton.style.backgroundImage = onImage;
		// 	}
		// }
		private void PlayClickSound() => audioPlayer.PlayClickSound();
		private void ShowElement(VisualElement element) => element.style.display = DisplayStyle.Flex;
		private void HideElement(VisualElement element) => element.style.display = DisplayStyle.None;
	}
}