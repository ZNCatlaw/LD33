using UnityEngine;
using System.Collections;

public class LinearFlightPattern : MonoBehaviour {

	public float m_speed;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		float dy = m_speed * Time.deltaTime;
		Vector2 targetDirection = this.GetTarget ().normalized;
		targetDirection.y *= dy;

		this.transform.Translate(targetDirection);
	}

	// returns a point directly ahead of the ship
	Vector2 GetTarget () {
		// bottom of the screen
		Vector2 target = Camera.main.ViewportToWorldPoint(new Vector2(0, -1));
		Vector2 position = this.transform.position;

		// correct so that the ship is pointing straight ahead
		target.x = this.transform.position.x;

		return target - position;
	}
}
