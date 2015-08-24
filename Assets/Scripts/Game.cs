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
		// QUIT on ESCAPE
		if (Input.GetKey(KeyCode.Escape)) { Application.Quit(); }

		if (gameOver == false && m_playerManager.playerDead == true) {
			Instantiate(this.gameOverScreen);

			m_spawner.enabled = false;
			GameObject.Find("Background").GetComponent<BackgroundManager>().paused = true;
            StartCoroutine(Utils.Sound.FadeAudio(GetComponent<AudioSource>(), 2.0f, Utils.Sound.Fade.Out));
            gameOver = true;
		}
	}
}
