using UnityEngine;
using System.Collections;

public class ScreenShake : MonoBehaviour 
{
	public Camera camera;
	public float shakeMagnitude;

	private float shake = 0;

	private Vector3 originalCameraPosition;

	void Start () {
		this.originalCameraPosition = this.camera.transform.position;
	}

	void OnTriggerEnter2D (Collider2D collider) {
		GameObject other = collider.gameObject;

		if (other.name == "ShipPuma") {
			shake = 0.1f;
		}
	}

	void Update () {
		if (shake > 0) {
			camera.transform.position = camera.transform.position + (Random.insideUnitSphere * shakeMagnitude);
			shake -= Time.deltaTime;
		} else if (shake < 0) {
			this.camera.transform.position = originalCameraPosition;
			shake = 0;
		}
	}
}