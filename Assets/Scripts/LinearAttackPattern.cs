using UnityEngine;
using System.Collections;

public class LinearAttackPattern : MonoBehaviour {

	public float m_salvoPeriod;
	public float m_salvo_size;

	public float m_bullet_frequency;
	public float m_bullet_speed;
	public int m_bullet_y_direction;

	public GameObject bulletType;

	[HideInInspector]
	public ProjectileManager m_projectileManager;

    private AudioSource shootSound;


	// Use this for initialization
	void Start () {
		InvokeRepeating("StartShooting", 0.5f, m_salvoPeriod);
        shootSound = gameObject.GetComponent<AudioSource>();
	}

	void StartShooting () {
		StartCoroutine (Shooting ());
	}

	IEnumerator Shooting () {
		int count = 0;
		float wait = Random.Range (m_bullet_frequency - 0.1f, m_bullet_frequency + 0.1f);

		while(count < m_salvo_size) { 
			count++;
			yield return new WaitForSeconds(wait);
			Shoot(); 
		}
        if (shootSound != null && !shootSound.isPlaying) shootSound.Play();
    }

	void Shoot () {
		// request a bullet with my position towards the target
		m_projectileManager.AddBullet(this.bulletType, this.transform.position, this.GetTarget().normalized, this.m_bullet_speed);
	}

	// returns a point directly ahead of the ship
	Vector3 GetTarget () {

		// bottom of the screen
		Vector2 target = Camera.main.ViewportToWorldPoint(new Vector2(0, m_bullet_y_direction));
		Vector2 position = this.transform.position;
		
		// correct so that the ship is pointing straight ahead
		target.x = position.x;
		// with bit of an angle, for fun
		target.x += Mathf.Cos (Mathf.PI/32) * Random.Range(-1, 1);

		return target - position;
	}
}
