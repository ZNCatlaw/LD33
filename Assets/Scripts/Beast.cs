using UnityEngine;
using System.Collections;

public class Beast : MonoBehaviour {

	public float damageStep;
	public float creep;
	public int m_maxCosmicRage;
	public float m_calmDownRate;

	[HideInInspector] public bool isDead = false;
	[HideInInspector] public bool isEnraged = false;
    [HideInInspector] public int kills = 0;

	
	private float m_cosmicRage = 0;
	private int damage = 0;
	private float startingPosition;
	private float currentPosition;
	private Color hale = Color.white;
	private Color hot = Color.red;
	
	private SpriteRenderer spriteRenderer;
    private AudioSource beastRageSound;

	// Use this for initialization
	void Start () {
		startingPosition = this.transform.position.y;
		currentPosition = startingPosition;

		spriteRenderer = this.GetComponent<SpriteRenderer> ();
        beastRageSound = GetComponent<AudioSource>();
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

		// gradually calm down
		if (m_cosmicRage > 0) {
			if (this.isEnraged) {
				m_cosmicRage -= Time.deltaTime * this.m_calmDownRate * (this.m_maxCosmicRage/10);
			} else {
				m_cosmicRage -= Time.deltaTime;
			}

		}

		// reset the cosmic rage and DESTROY WORLDS
		if (this.isEnraged == false && m_cosmicRage > m_maxCosmicRage) {
			this.isEnraged = true;
            beastRageSound.Play();
		}

		// stops raging
		if (this.isEnraged == true && (m_cosmicRage < 0)) {
			this.isEnraged = false;
		}

		spriteRenderer.color = Color.Lerp(this.hale, this.hot, Mathf.Lerp(0, 1, (m_cosmicRage/m_maxCosmicRage)));
	}

	void OnTriggerEnter2D (Collider2D collider)
    {
		GameObject other = collider.gameObject;
		if (other.name == "ShipPuma") {

			GoodShip ship = other.GetComponent<GoodShip> ();
			ship.Explode ();
			this.damage += ship.crashDamage;

			this.PushBack();

		} else if (other.name == "SmallLaser") {
			Projectile projectile = other.GetComponent<Projectile> ();
			projectile.Explode ();

			if (this.isEnraged == false) {
				this.m_cosmicRage += projectile.cosmicRageDamage;
			}
		}
	}
}
