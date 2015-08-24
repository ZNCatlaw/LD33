using UnityEngine;
using System.Collections;


public class Spawner : MonoBehaviour
{
	public GameObject[] m_shipTypes;

    public float m_IntroTime;
    public float m_IntroStartRate;
    public float m_IntroEndRate;

    public float m_NormalSlopeRate;

    public float m_Period;
    public float m_Intensity;

    public float m_CurrentSpawnRate;
    public float m_CurrentWaveFactor;

    private GameObject m_spawnedShips;
	private ProjectileManager m_projectileManager;
    private PlayerManager m_playerManager;

	// Use this for initialization
	public void Start()
    {
		m_projectileManager = GetComponentInParent<ProjectileManager> ();
		m_spawnedShips = new GameObject (); 
		m_spawnedShips.name = "SpawnedShips";

        m_playerManager = GameObject.Find("Game").GetComponent<PlayerManager>();
    }

    public void Update()
    {
        float time = Time.timeSinceLevelLoad;

        float spawnRate = 0;
        if( time < m_IntroTime )
        {
            spawnRate = m_IntroStartRate + (m_IntroEndRate - m_IntroStartRate) * (time / m_IntroTime);
        }
        else
        {
            spawnRate = m_IntroEndRate + time * m_NormalSlopeRate;
        }

        //Find the current spawn rate and then integrate it over time to figure out how much to spawn this frame.
        int activePlayers = Mathf.Max(m_playerManager.GetCurrentNumberOfPlayers(), 1);

        m_CurrentWaveFactor = 1.0f + (Mathf.Sin( (time * 2.0f * Mathf.PI) / m_Period) * m_Intensity);
        m_CurrentSpawnRate = spawnRate * activePlayers * m_CurrentWaveFactor;

        //Integrate(poorly) the spawn rate to find how many ships to spawn this frame
        float spawnVolume = m_CurrentSpawnRate * Time.deltaTime;

        //Spawn whole ships
        while(spawnVolume > 1.0f)
        {
            Spawn();
            spawnVolume -= 1.0f;
        }
        //Spawn fractional ships
        if( spawnVolume > Random.Range(0.0f,1.0f))
        {
            Spawn();
        }
    }

    void Spawn ()
    {
		if(!this.enabled)
        {
			return;
		}

		int index = Random.Range (0, m_shipTypes.Length);
		float random_x = Random.Range (0f, 1f);

		Vector2 position = Camera.main.ViewportToWorldPoint(new Vector2(random_x, 1.1f));

		BuildShip (m_shipTypes[index], position);
	}

	void BuildShip (GameObject ship, Vector2 position) {
		ship = Instantiate (ship, position, Quaternion.identity) as GameObject;
		ship.transform.parent = m_spawnedShips.transform;
		ship.GetComponent<LinearAttackPattern>().m_projectileManager = m_projectileManager;
		ship.gameObject.name = "ShipPuma";
		DestroyObject (ship, 5f);
	}
	
}
