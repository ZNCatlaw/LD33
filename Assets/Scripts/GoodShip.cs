using UnityEngine;
using System.Collections;

public class GoodShip : MonoBehaviour {

	public GameObject shipExplosion;
    public float m_Health;
	public int crashDamage;
	public bool chainExplosions;
    public Game m_Game;

	private float m_MaxHealth;
	private Color hale = Color.white;
	private Color hot = Color.red;
	private SpriteRenderer spriteRenderer;

	// Use this for initialization
	void Start () {
		spriteRenderer = this.GetComponent<SpriteRenderer> ();
		m_MaxHealth = m_Health;
	}
	
	// Update is called once per frame
	void Update () {
		m_Health += Time.deltaTime;
		spriteRenderer.color = Color.Lerp(this.hale, this.hot, Mathf.Lerp(0, 1, (1 - m_Health/m_MaxHealth)));
	}
	
	public void Explode () {
		GameObject explosion = Instantiate (shipExplosion, this.transform.position, Quaternion.identity) as GameObject;
		explosion.name = "ShipExplosion";

		DestroyObject (gameObject);
	}

    void OnTriggerStay2D(Collider2D collider)
    {
        GameObject other = collider.gameObject;
        if (other.name == "EyeLaserLine") {
			m_Health -= 10.0f * Time.deltaTime;

			if (m_Health <= 0) {
                m_Game.kills += 1;
                Explode ();
			}
		} else if (other.name == "BeastLaser") {
			Explode ();
		} else if (this.chainExplosions && other.name == "ShipExplosion") {
			Explode ();
		}
    }

}
