using UnityEngine;
using System.Collections;

public class Beast : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D (Collider2D collider) {
		GameObject other = collider.gameObject;
		if (other.name == "ShipPuma") {
			Debug.Log ("BAM");

			GoodShip ship = other.GetComponent<GoodShip> ();
			ship.Explode ();
		} else if (other.name == "SmallLaser") {
			Projectile projectile = other.GetComponent<Projectile> ();
			projectile.Explode ();
		}

	}
}
