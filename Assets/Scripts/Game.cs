using UnityEngine;
using System.Collections;

public class Game : MonoBehaviour
{
    PlayerManager m_playerManager;
    ProjectileManager m_projectileManager;
    ShipManager m_shipManager;

    // Use this for initialization
    void Start ()
    {
        m_playerManager = GetComponent<PlayerManager>();
        m_projectileManager = GetComponent<ProjectileManager>();
        m_shipManager = GetComponent<ShipManager>();


	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}
}
