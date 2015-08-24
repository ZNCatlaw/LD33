using UnityEngine;
using System.Collections;

public class Game : MonoBehaviour
{
    PlayerManager m_playerManager;
    ProjectileManager m_projectileManager;
    ShipManager m_shipManager;
	Spawner m_spawner;
	public GameObject gameOverScreen; 

	private bool gameOver = false;

    // Use this for initialization
    void Start ()
    {
        m_playerManager = GetComponent<PlayerManager>();
        m_projectileManager = GetComponent<ProjectileManager>();
        m_shipManager = GetComponent<ShipManager>();
		m_spawner = GetComponent<Spawner>();
	}
	
	// Update is called once per frame
	void Update ()
    {
		if (gameOver == false && m_playerManager.playerDead == true) {
			Instantiate(this.gameOverScreen);

			m_spawner.enabled = false;
			GameObject.Find("Background").GetComponent<BackgroundManager>().paused = true;
			gameOver = true;
		}
	}
}
