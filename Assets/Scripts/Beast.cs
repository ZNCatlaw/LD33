using UnityEngine;
using System.Collections;

public class Beast : MonoBehaviour {

	public float damageStep;
	public float creep;
	public int m_maxCosmicRage;
	
	public bool isDead = false;

	private float m_cosmicRage = 0;
	private int damage = 0;
	private float startingPosition;
	private float currentPosition;
	private Color hale = Color.white;
	private Color hot = Color.red;
	
	private SpriteRenderer spriteRenderer;

	// Use this for initialization
	void Start () {
		startingPosition = this.transform.position.y;
		currentPosition = startingPosition;

		spriteRenderer = this.GetComponent<SpriteRenderer> ();
	}

	void CreepForward () {
		if (this.transform.position.y < this.startingPosition) {
			this.transform.Translate(new Vector3(0, creep, 0));
		}
	}

	void PushBack() {
		this.transform.Translate (new Vector3 (0, -damageStep, 0));

		// if the beast has fallen off the screen, shut down its collider
		if (this.transform.position.y < (-startingPosition)) {
			this.isDead = true;
			this.GetComponent<PolygonCollider2D>().enabled = false;
		}
	}

	// Update is called once per frame
	void Update () {
		if (this.isDead) {
			return;
		}

		this.CreepForward ();

		if (m_cosmicRage > 0) {
			m_cosmicRage -= Time.deltaTime;
	
		} else if (m_cosmicRage > m_maxCosmicRage) {
			m_cosmicRage = 0;
		}

		Debug.Log (m_cosmicRage / m_maxCosmicRage);
		spriteRenderer.color = Color.Lerp(this.hale, this.hot, Mathf.Lerp(0, 1, (m_cosmicRage/m_maxCosmicRage)));
	}

	void OnTriggerEnter2D (Collider2D collider) {
		Debug.Log ("yes");
		GameObject other = collider.gameObject;
		if (other.name == "ShipPuma") {

			GoodShip ship = other.GetComponent<GoodShip> ();
			ship.Explode ();
			this.damage += ship.crashDamage;

			this.PushBack();

		} else if (other.name == "SmallLaser") {
			Projectile projectile = other.GetComponent<Projectile> ();
			projectile.Explode ();

			this.m_cosmicRage += projectile.cosmicRageDamage;
		}
	}
}
