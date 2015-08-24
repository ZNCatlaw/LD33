using UnityEngine;
using System.Collections;

public class Beast : MonoBehaviour {

	public float damageStep;
	public float creep;

	private int cosmic_rage = 0;
	private int damage = 0;
	private float startingPosition;
	private float currentPosition;

	// Use this for initialization
	void Start () {
		startingPosition = this.transform.position.y;
		currentPosition = startingPosition;
	}

	void CreepForward () {
		if (this.transform.position.y < this.startingPosition) {
			this.transform.Translate(new Vector3(0, creep, 0));
		}
	}

	void PushBack() {
		this.transform.Translate(new Vector3(0, -damageStep, 0));
	}

	// Update is called once per frame
	void Update () {
		this.CreepForward ();
	}

	void OnTriggerEnter2D (Collider2D collider) {
		GameObject other = collider.gameObject;
		if (other.name == "ShipPuma") {

			GoodShip ship = other.GetComponent<GoodShip> ();
			ship.Explode ();
			this.damage += ship.crashDamage;

			this.PushBack();

		} else if (other.name == "SmallLaser") {
			Projectile projectile = other.GetComponent<Projectile> ();
			projectile.Explode ();

			this.cosmic_rage += projectile.cosmic_rage;
		}
	}
}
