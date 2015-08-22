using UnityEngine;
using System.Collections;

public class LinearFlightPattern : MonoBehaviour {


	// Use this for initialization
	void Start () {
		Rigidbody2D body = this.GetComponent<Rigidbody2D> ();
		body.velocity = new Vector2 (0, -(1 + Random.Range(-0.1f, 0.1f)));
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		//create rotation
		Vector2 target = this.GetTarget ();

		Quaternion wantedRotation = Quaternion.LookRotation(target);

		//then rotate
		transform.rotation = Quaternion.Lerp(transform.rotation, wantedRotation, Time.time * 1);
	}

	// returns a point directly ahead of the ship
	Vector2 GetTarget () {
		// bottom of the screen
		Vector2 target = Camera.main.ViewportToWorldPoint(new Vector2(0, -1));
		Vector2 position = this.transform.position;

		// correct so that the ship is pointing straight ahead
		target.x = position.x;
		return position - target;
	}
}
