using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {

	public GameObject[] m_shipTypes;
	public float spawnTime;
	public float spawnDelay;

	// Use this for initialization
	void Start () {
	
		InvokeRepeating ("Spawn", spawnDelay, spawnTime);
	}

	void Spawn () {
		int ship_type_index = Random.Range (0, m_shipTypes.Length);
		float random_x = Random.Range (0f, 1f);

		Vector2 position = Camera.main.ViewportToWorldPoint(new Vector2(random_x, 1.1f));
		
		Instantiate (m_shipTypes [ship_type_index], position, Quaternion.identity);
	}

	// Update is called once per frame
	void FixedUpdate () {

	}
}
