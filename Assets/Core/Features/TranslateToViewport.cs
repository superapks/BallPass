using UnityEngine;

namespace Core.Features {
	public class TranslateToViewport : MonoBehaviour {
		[SerializeField] private Camera mainCamera;
		[SerializeField] private Vector3 viewportPoint;
		[SerializeField] private Vector3 offset;

		private void Awake() {
			Vector3 target = mainCamera.ViewportToWorldPoint(viewportPoint) + offset;
			target.z = 0;
			transform.position = target;
		}
	}
}