using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {

	public GameObject[] m_shipTypes;
	public float m_spawnTime;
	public float m_spawnDelay;

	private GameObject m_spawnedShips;

	// Use this for initialization
	void Start () {
	
		m_spawnedShips = new GameObject (); 
		m_spawnedShips.name = "SpawnedShips";
		InvokeRepeating ("Spawn", m_spawnDelay, m_spawnTime);
	}

	void Spawn () {
		int ship_type_index = Random.Range (0, m_shipTypes.Length);
		float random_x = Random.Range (0f, 1f);

		Vector2 position = Camera.main.ViewportToWorldPoint(new Vector2(random_x, 1.1f));

		GameObject ship = Instantiate (m_shipTypes [ship_type_index], position, Quaternion.identity) as GameObject;
		ship.transform.parent = m_spawnedShips.transform;
	}

	// Update is called once per frame
	void FixedUpdate () {

	}
}
