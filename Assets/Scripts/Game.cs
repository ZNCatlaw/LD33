﻿using UnityEngine;
using System.Collections;

public class Game : MonoBehaviour
{
    PlayerManager m_playerManager;
    ProjectileManager m_projectileManager;
    ShipManager m_shipManager;
	Spawner m_spawner;
	public GameObject gameOverScreen;

    [HideInInspector]
    public int kills = 0;

	private bool gameOver = false;
    private float timeGameEnded = 0.0f;

    // Use this for initialization
    void Start ()
    {
        m_playerManager = GetComponent<PlayerManager>();
        m_projectileManager = GetComponent<ProjectileManager>();
        m_shipManager = GetComponent<ShipManager>();
		m_spawner = GetComponent<Spawner>();
        kills = 0;
        timeGameEnded = 0.0f;
	}
	
	// Update is called once per frame
	void Update ()
    {
		// QUIT on ESCAPE
		if (Input.GetKey(KeyCode.Escape)) { Application.Quit(); }

		if (gameOver == false && m_playerManager.playerDead == true) {
			var gameOverObj = Instantiate(gameOverScreen) as GameObject;
            gameOverObj.GetComponent<EndOfGameBehaviour>().setFinalScore(kills);

			m_spawner.enabled = false;
			GameObject.Find("Background").GetComponent<BackgroundManager>().paused = true;
            StartCoroutine(Utils.Sound.FadeAudio(GetComponents<AudioSource>()[0], 3.0f, Utils.Sound.Fade.Out, Interpolate.EaseType.EaseOutExpo));
            gameOver = true;
            timeGameEnded = Time.time;
		}

        if (gameOver && Input.anyKeyDown && (Time.time - timeGameEnded) >= 4.0f)
        {
            Application.LoadLevel(Application.loadedLevel);
        }
	}
}
