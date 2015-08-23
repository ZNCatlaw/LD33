using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {

	public GameObject[] m_shipTypes;
	public float m_spawnTime;
	public float m_spawnDelay;

	private GameObject m_spawnedShips;
	private ProjectileManager m_projectileManager;

	// Use this for initialization
	void Start () {
		m_projectileManager = GetComponentInParent<ProjectileManager> ();
		m_spawnedShips = new GameObject (); 
		m_spawnedShips.name = "SpawnedShips";
		InvokeRepeating ("Spawn", m_spawnDelay, m_spawnTime);
	}

	void Spawn () {
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
