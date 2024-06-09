using System;
using System.Collections.Generic;
using Core.Features;
using Core.Services;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using Zenject;

namespace Core.UI {
	public class Hud : MonoBehaviour {
		[SerializeField] private UIDocument document;
		[SerializeField] private GameObject destroyAfterFail;
		[SerializeField] private NumberDisplay numberDisplay;
		private VisualElement root;
		private AudioPlayer audioPlayer;
		private VisualElement gameOverContainer;
		private VisualElement finalScoreNumbersContainer;
		private VisualElement scoreNumbersContainer;
		private int score;

		[Inject]
		private void Construct(AudioPlayer audioPlayer) => this.audioPlayer = audioPlayer;
		private void Start() {
			root = document.rootVisualElement;
			
			scoreNumbersContainer = root.Q<VisualElement>("score-numbers-container");
			var pauseButton = root.Q<Button>("pause-button");
			var pauseContainer = root.Q<VisualElement>("pause-container");
			var playButton = root.Q<Button>("play-button");
			var restartButton = root.Q<Button>("restart-button");
			var homeButton = root.Q<Button>("home-button");
			gameOverContainer = root.Q<VisualElement>("game-over-container");
			finalScoreNumbersContainer = root.Q<VisualElement>("final-score-numbers-container");
			var gameOverRestartButton = root.Q<Button>("game-over-restart-button");
			var gameOverHomeButton = root.Q<Button>("game-over-home-button");
			
			numberDisplay.DisplayNumber(score.ToString(), scoreNumbersContainer, 82, 116);
			
			pauseButton.clicked += () => {
				PlayClickSound();
				Time.timeScale = 0;
				ShowElement(pauseContainer);
			};
			playButton.clicked += () => {
				PlayClickSound();
				Time.timeScale = 1;
				HideElement(pauseContainer);
			};
			restartButton.clicked += () => {
				PlayClickSound();
				Time.timeScale = 1;
				SceneManager.LoadScene("Game");
			};
			gameOverRestartButton.clicked += () => {
				PlayClickSound();
				Time.timeScale = 1;
				SceneManager.LoadScene("Game");
			};
			homeButton.clicked += () => {
				PlayClickSound();
				Time.timeScale = 1;
				SceneManager.LoadScene("MainMenu");
			};
			gameOverHomeButton.clicked += () => {
				PlayClickSound();
				Time.timeScale = 1;
				SceneManager.LoadScene("MainMenu");
			};
		}
		public void IncreaseScore() {
			score++;
			numberDisplay.DisplayNumber(score.ToString(), scoreNumbersContainer, 82, 116);
		}
		public void GameOver() {
			audioPlayer.PlayGameOverSound();
			Time.timeScale = 0;
			DOTween.KillAll();
			StopAllCoroutines();
			numberDisplay.DisplayNumber(score.ToString(), finalScoreNumbersContainer, 82, 116);
			ShowElement(gameOverContainer);
		}
		private void HideElement(VisualElement element) => element.style.display = DisplayStyle.None;
		private void ShowElement(VisualElement element) => element.style.display = DisplayStyle.Flex;
		private void PlayClickSound() => audioPlayer.PlayClickSound();
	}
}