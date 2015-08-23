using UnityEngine;
using System.Collections;

public class LinearAttackPattern : MonoBehaviour {

	public float m_salvoPeriod;
	public float m_salvo_size;

	public float m_bullet_frequency;
	public float m_bullet_speed;


	[HideInInspector]
	public ProjectileManager m_projectileManager;


	// Use this for initialization
	void Start () {
		InvokeRepeating("StartShooting", 0, m_salvoPeriod); 
	}

	void StartShooting () {
		StartCoroutine (Shooting ());
	}

	IEnumerator Shooting () {
		int count = 0;

		while(count < m_salvo_size) { 
			count++;
			yield return new WaitForSeconds(Random.Range(m_bullet_frequency - 0.1f, m_bullet_frequency + 0.1f));
			Shoot(); 
		} 
	}

	void Shoot () {
		// request a bullet with my position towards the target
		m_projectileManager.AddLaser (this.transform.position, this.GetTarget().normalized, this.m_bullet_speed);
	}

	// returns a point directly ahead of the ship
	Vector3 GetTarget () {
		// bottom of the screen
		Vector2 target = Camera.main.ViewportToWorldPoint(new Vector2(0, -1));
		Vector2 position = this.transform.position;
		
		// correct so that the ship is pointing straight ahead
		target.x = position.x;
		return target - position;
	}
}
