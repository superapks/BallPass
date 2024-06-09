using System;
using Core.UI;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Core.Features {
	public class BallMovement : MonoBehaviour {
		[SerializeField] private float force;
		[SerializeField] private Rigidbody2D rb;
		[SerializeField] private Hud hud;
		
		private void Start() => Launch();
		public void Launch() {
			float x = Random.Range(0, 2) == 0 ? Random.Range(-0.75f, -0.25f) : Random.Range(0.25f, 0.75f);
			float y = 1 - Mathf.Abs(x);
			rb.AddForce(new Vector2(x, y) * force, ForceMode2D.Impulse);
		}
		private void OnTriggerEnter2D(Collider2D other) {
			if (other.CompareTag("Line")) hud.GameOver();
		}
	}
}