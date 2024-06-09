using UnityEngine;

namespace Core.Features {
	public class ArenaCutter : MonoBehaviour {
		[SerializeField] private Camera mainCamera;
		[SerializeField] private SpriteRenderer arenaSpriteRenderer;
		[SerializeField] private BoxCollider2D left;
		[SerializeField] private BoxCollider2D right;
		[SerializeField] private BoxCollider2D top;
		[SerializeField] private BoxCollider2D bottom;

		private float initialSpriteWidth;
		private float initialSpriteHeight;
		private float initialSpriteBottom;
		private float initialSpriteLeft;
		private float initialSpriteRight;
		private float startSpriteWidth;
		private float startSpriteHeight;
		private float leftOffset;
		private float rightOffset;
		private float topOffset;
		private float bottomOffset;

		private int currentBoundary; // 1 - Top, 2 - Bottom, 3 - Left, 4 - Right

		private void Start() {
			initialSpriteWidth = arenaSpriteRenderer.size.x;
			startSpriteWidth = arenaSpriteRenderer.size.x;
			initialSpriteHeight = arenaSpriteRenderer.size.y;
			startSpriteHeight = arenaSpriteRenderer.size.y;
			initialSpriteBottom = arenaSpriteRenderer.bounds.min.y;
			initialSpriteLeft = arenaSpriteRenderer.bounds.min.x;
			initialSpriteRight = arenaSpriteRenderer.bounds.max.x;
			leftOffset = left.offset.x;
			rightOffset = right.offset.x;
			topOffset = top.offset.y;
			bottomOffset = bottom.offset.y;
		}
		private void Update() {
			// if (Input.GetKeyDown(KeyCode.Alpha1)) currentBoundary = 1;
			// if (Input.GetKeyDown(KeyCode.Alpha2)) currentBoundary = 2;
			// if (Input.GetKeyDown(KeyCode.Alpha3)) currentBoundary = 3;
			// if (Input.GetKeyDown(KeyCode.Alpha4)) currentBoundary = 4;

			// if (Input.GetMouseButtonUp(0)) {
			// 	Vector2 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
			//
			// 	switch (currentBoundary) {
			// 		case 1:
			// 			AdjustTopToMousePosition(mousePosition);
			// 			break;
			// 		case 2:
			// 			AdjustBottomToMousePosition(mousePosition);
			// 			break;
			// 		case 3:
			// 			AdjustLeftToMousePosition(mousePosition);
			// 			break;
			// 		case 4:
			// 			AdjustRightToMousePosition(mousePosition);
			// 			break;
			// 	}
			//
			// 	AdjustBoundaries();
			// }
		}
		public void AdjustBoundaries() {
			initialSpriteWidth = arenaSpriteRenderer.size.x;
			initialSpriteHeight = arenaSpriteRenderer.size.y;
			initialSpriteBottom = arenaSpriteRenderer.bounds.min.y;
			initialSpriteLeft = arenaSpriteRenderer.bounds.min.x;
			initialSpriteRight = arenaSpriteRenderer.bounds.max.x;

			left.offset = new Vector2(leftOffset * initialSpriteWidth / startSpriteWidth, 0);
			right.offset = new Vector2(rightOffset * initialSpriteWidth / startSpriteWidth, 0);
			top.offset = new Vector2(0, topOffset * initialSpriteHeight / startSpriteHeight);
			bottom.offset = new Vector2(0, bottomOffset * initialSpriteHeight / startSpriteHeight);
		}
		public void AdjustTopToMousePosition(Vector2 clickPosition) {
			// Текущая нижняя граница спрайта
			float spriteBottom = initialSpriteBottom;

			// Вычисляем новую высоту спрайта
			float newHeight = clickPosition.y - spriteBottom;
			newHeight = Mathf.Clamp(newHeight, 0, initialSpriteHeight * arenaSpriteRenderer.transform.localScale.y);

			// Устанавливаем новый размер спрайта
			Vector2 newSize = new Vector2(arenaSpriteRenderer.size.x, newHeight / arenaSpriteRenderer.transform.localScale.y);
			arenaSpriteRenderer.size = newSize;

			// Устанавливаем новую позицию спрайта, чтобы нижняя граница оставалась на месте
			float newYPosition = spriteBottom + newHeight / 2;
			arenaSpriteRenderer.transform.position = new Vector3(arenaSpriteRenderer.transform.position.x, newYPosition, arenaSpriteRenderer.transform.position.z);
			
			AdjustBoundaries();
		}
		public void AdjustBottomToMousePosition(Vector2 clickPosition) {
			// Текущая верхняя граница спрайта
			float spriteTop = initialSpriteBottom + initialSpriteHeight * arenaSpriteRenderer.transform.localScale.y;

			// Вычисляем новую высоту спрайта
			float newHeight = spriteTop - clickPosition.y;
			newHeight = Mathf.Clamp(newHeight, 0, initialSpriteHeight * arenaSpriteRenderer.transform.localScale.y);

			// Устанавливаем новый размер спрайта
			Vector2 newSize = new Vector2(arenaSpriteRenderer.size.x, newHeight / arenaSpriteRenderer.transform.localScale.y);
			arenaSpriteRenderer.size = newSize;

			// Устанавливаем новую позицию спрайта, чтобы верхняя граница оставалась на месте
			float newYPosition = spriteTop - newHeight / 2;
			arenaSpriteRenderer.transform.position = new Vector3(arenaSpriteRenderer.transform.position.x, newYPosition, arenaSpriteRenderer.transform.position.z);
			
			AdjustBoundaries();
		}
		public void AdjustLeftToMousePosition(Vector2 clickPosition) {
			// Текущая правая граница спрайта
			float spriteRight = initialSpriteRight;

			// Вычисляем новую ширину спрайта
			float newWidth = spriteRight - clickPosition.x;
			newWidth = Mathf.Clamp(newWidth, 0, initialSpriteWidth * arenaSpriteRenderer.transform.localScale.x);

			// Устанавливаем новый размер спрайта
			Vector2 newSize = new Vector2(newWidth / arenaSpriteRenderer.transform.localScale.x, arenaSpriteRenderer.size.y);
			arenaSpriteRenderer.size = newSize;

			// Устанавливаем новую позицию спрайта, чтобы правая граница оставалась на месте
			float newXPosition = spriteRight - newWidth / 2;
			arenaSpriteRenderer.transform.position = new Vector3(newXPosition, arenaSpriteRenderer.transform.position.y, arenaSpriteRenderer.transform.position.z);
			
			AdjustBoundaries();
		}
		public void AdjustRightToMousePosition(Vector2 clickPosition) {
			// Текущая левая граница спрайта
			float spriteLeft = initialSpriteLeft;

			// Вычисляем новую ширину спрайта
			float newWidth = clickPosition.x - spriteLeft;
			newWidth = Mathf.Clamp(newWidth, 0, initialSpriteWidth * arenaSpriteRenderer.transform.localScale.x);

			// Устанавливаем новый размер спрайта
			Vector2 newSize = new Vector2(newWidth / arenaSpriteRenderer.transform.localScale.x, arenaSpriteRenderer.size.y);
			arenaSpriteRenderer.size = newSize;

			// Устанавливаем новую позицию спрайта, чтобы левая граница оставалась на месте
			float newXPosition = spriteLeft + newWidth / 2;
			arenaSpriteRenderer.transform.position = new Vector3(newXPosition, arenaSpriteRenderer.transform.position.y, arenaSpriteRenderer.transform.position.z);
			
			AdjustBoundaries();
		}
	}
}