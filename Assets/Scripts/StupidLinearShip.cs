using UnityEngine;
using System.Collections;

public class StupidLinearShip : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Rigidbody2D body = this.GetComponent<Rigidbody2D> ();
		body.velocity = new Vector2 (0, -(1 + Random.Range(-0.1f, 0.1f)));
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		//create rotation
		Vector2 target = Camera.main.ViewportToWorldPoint(new Vector2(0, -1));
		Vector2 position = transform.position;

		target.x = position.x; // ensure the ship is pointing straight ahead
		Quaternion wantedRotation = Quaternion.LookRotation(target - position);

		//then rotate
		transform.rotation = Quaternion.Lerp(transform.rotation, wantedRotation, Time.time * 1);
	}

	void OnCollisionEnter2D (Collision2D other) {
		Debug.Log("Collision detected with trigger object ");
	}
}
