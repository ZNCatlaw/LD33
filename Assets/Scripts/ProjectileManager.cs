﻿using UnityEngine;
using System.Collections;

public class ProjectileManager : MonoBehaviour
{
    public GameObject m_LaserPrefab;

    GameObject m_Projectiles;

    // Use this for initialization
    void Start()
    {
        m_Projectiles = new GameObject();
        m_Projectiles.transform.parent = this.transform;
        m_Projectiles.name = "Projectiles";
    }

    // Update is called once per frame
    void Update()
    {
        foreach (Transform child in m_Projectiles.transform)
        {
            Projectile projectile = child.GetComponent<Projectile>();
            Vector3 delta = projectile.m_Velocity * Time.deltaTime;
            child.transform.Translate(delta, Space.World);
        }
    }

    public void AddLaser( Vector3 position, Vector3 direction )
    {
		AddLaser (position, direction, 10.0f);
    }

	public void AddLaser( Vector3 position, Vector3 direction, float bullet_speed )
	{
		Quaternion rot = new Quaternion();
		rot.SetLookRotation(direction, new Vector3( 0, 0, 1));
		GameObject laser = Instantiate(m_LaserPrefab, position, rot) as GameObject;
		laser.transform.parent = m_Projectiles.transform;
		
		Debug.Log (direction);
		Projectile projectile = laser.GetComponent<Projectile>();
		projectile.m_Velocity = direction * bullet_speed;
		
		DestroyObject(laser, 2.0f);
	}
}
