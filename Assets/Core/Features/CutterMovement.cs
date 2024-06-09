using System;
using Core.Services;
using Core.UI;
using DG.Tweening;
using UnityEngine;
using Zenject;

namespace Core.Features {
	public class CutterMovement : MonoBehaviour {
		[SerializeField] private Camera mainCamera;
		[SerializeField] private ArenaCutter arenaCutter;
		[SerializeField] private Transform line;
		[SerializeField] private SpriteRenderer lineSpriteRenderer;
		[SerializeField] private SpriteRenderer arenaSpriteRenderer;
		[SerializeField] private BoxCollider2D lineCollider;
		[SerializeField] private LayerMask ballMask;
		[SerializeField] private Rigidbody2D ballRb;
		[SerializeField] private Hud hud;
		[SerializeField] private TrailRenderer ballTrail;
		private bool isHorizontal = true;
		private bool isDragging;
		private Vector3 startPosition;
		private AudioPlayer audioPlayer;

		[Inject]
		private void Construct(AudioPlayer audioPlayer) => this.audioPlayer = audioPlayer;
		private void Start() => startPosition = transform.position;
		private void OnMouseDrag() {
			Vector3 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
			mousePosition.z = 0;
			if (isDragging || Vector2.Distance(transform.position, mousePosition) > 0.5f) {
				isDragging = true;
				transform.position = mousePosition;
			}
		}
		private void OnMouseUp() {
			if (isDragging) {
				isDragging = false;

				if (IsSpriteInside(lineSpriteRenderer, arenaSpriteRenderer)) {
					ExpandLineToArenaBounds();
				}
				else {
					transform.position = startPosition;
				}
			}
			else {
				audioPlayer.PlayClick2Sound();
				if (line.transform.rotation == Quaternion.Euler(0, 0, 90)) {
					line.transform.rotation = Quaternion.Euler(0, 0, 0);
					isHorizontal = true;
				}
				else {
					line.transform.rotation = Quaternion.Euler(0, 0, 90);
					isHorizontal = false;
				}
			}
		}
		private bool IsSpriteInside(SpriteRenderer spriteA, SpriteRenderer spriteB) {
			Bounds boundsA = spriteA.bounds;
			Bounds boundsB = spriteB.bounds;

			return boundsB.Contains(boundsA.min) && boundsB.Contains(boundsA.max);
		}
		private void ExpandLineToArenaBounds() {
			// Получаем размеры арены
			Bounds arenaBounds = arenaSpriteRenderer.bounds;

			// Вычисляем целевые размеры для спрайта линии
			float targetWidth = (isHorizontal ? arenaBounds.size.x : arenaBounds.size.y) / 0.4f;

			// Используем DoTween для анимации изменения размеров линии до размеров арены за 2 секунды
			const float Duration = 0.45f;
			lineCollider.enabled = true;
			if (isHorizontal) lineSpriteRenderer.transform.DOMoveX(arenaSpriteRenderer.transform.position.x, Duration).SetEase(Ease.Linear);
			else lineSpriteRenderer.transform.DOMoveY(arenaSpriteRenderer.transform.position.y, Duration).SetEase(Ease.Linear);
			DOTween
				.To(() => lineCollider.size, x => lineCollider.size = x, new Vector2(targetWidth, lineCollider.size.y), Duration)
				.SetEase(Ease.Linear);
			DOTween
				.To(() => lineSpriteRenderer.size, x => lineSpriteRenderer.size = x, new Vector2(targetWidth, lineSpriteRenderer.size.y), Duration)
				.SetEase(Ease.Linear)
				.OnComplete(() => {
					if (isHorizontal) {
						if (Physics2D.BoxCast(transform.position + Vector3.up * 15, new Vector2(30, 30), 0, Vector2.up, distance: 0.1f, ballMask.value)) {
							arenaCutter.AdjustBottomToMousePosition(transform.position);
						}
						else {
							arenaCutter.AdjustTopToMousePosition(transform.position);
						}
					}
					else {
						if (Physics2D.BoxCast(transform.position + Vector3.right * 15, new Vector2(30, 30), 0, Vector2.right, 0, ballMask.value)) {
							arenaCutter.AdjustLeftToMousePosition(transform.position);
						}
						else {
							arenaCutter.AdjustRightToMousePosition(transform.position);
						}
					}

					lineCollider.enabled = false;
					lineSpriteRenderer.transform.localPosition = Vector3.zero;
					lineSpriteRenderer.size = new Vector2(2.25f, lineSpriteRenderer.size.y);
					lineCollider.size = new Vector2(2.25f, lineCollider.size.y);
					transform.position = startPosition;

					if (arenaSpriteRenderer.size is { x: < 5, y: < 5 }) {
						ballTrail.enabled = false;
						ballRb.velocity = Vector2.zero;
						
						while (arenaSpriteRenderer.size is { x: < 9, y: < 16 }) {
							arenaSpriteRenderer.size += Vector2.one * 0.1f;
						}
						arenaSpriteRenderer.transform.position = Vector3.zero;
						arenaCutter.AdjustBoundaries();

						ballRb.transform.position = Vector3.zero;
						ballRb.GetComponent<BallMovement>().Launch();
						ballTrail.enabled = true;

						hud.IncreaseScore();
					}
				});
		}
	}
}