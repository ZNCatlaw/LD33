using UnityEngine;
using System.Collections;

public class LinearAttackPattern : MonoBehaviour {

	public float m_shotPeriod;
	public float m_shotDelay;

	[HideInInspector]
	public ProjectileManager m_projectileManager;


	// Use this for initialization
	void Start () {
		InvokeRepeating ("Shoot", m_shotDelay, m_shotPeriod);
	}

	void Shoot () {
		// request a bullet with my position towards the target
		m_projectileManager.AddLaser (this.transform.position, this.GetTarget());
	}

	// returns a point directly ahead of the ship
	Vector3 GetTarget () {
		// bottom of the screen
		Vector2 target = Camera.main.ViewportToWorldPoint(new Vector2(0, -1));
		Vector2 position = this.transform.position;
		
		// correct so that the ship is pointing straight ahead
		target.x = position.x;
		return position - target;	
	}
}
