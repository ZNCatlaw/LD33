using UnityEngine;
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
    }

    // Update is called once per frame
    void Update()
    {
        foreach (Transform child in transform)
        {
            float speed = 10f * Time.deltaTime;
            child.transform.Translate(new Vector3(0, speed, 0));
        }
    }

    public void AddLaser( Vector3 position, Vector3 direction )
    {
        GameObject laser = Instantiate(m_LaserPrefab, position, transform.rotation) as GameObject;
        laser.transform.parent = m_Projectiles.transform;
        DestroyObject(laser, 2.0f);
    }
}
