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
		Vector2 position = transform.position;

		// ensure the ship is pointing straight ahead
		target.x = position.x; 
		Quaternion wantedRotation = Quaternion.LookRotation(target - position);

		//then rotate
		transform.rotation = Quaternion.Lerp(transform.rotation, wantedRotation, Time.time * 1);
	}

	// returns a point directly ahead of the ship
	Vector2 GetTarget () {
		return Camera.main.ViewportToWorldPoint(new Vector2(0, -1));
	}
}
