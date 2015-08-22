using UnityEngine;
using System.Collections;

public class LinearAttackPattern : MonoBehaviour {

	public float shotDelay;
	public float shotPeriod;

	// Use this for initialization
	void Start () {
		InvokeRepeating ("Shoot", shotDelay, shotPeriod);
	}

	void Shoot () {
		// request a bullet with my position towards the target
	}

	// returns a point directly ahead of the ship
	Vector2 GetTarget () {
		return Camera.main.ViewportToWorldPoint(new Vector2(0, -1));
	}
}
