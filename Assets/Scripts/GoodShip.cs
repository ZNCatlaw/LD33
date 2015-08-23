using UnityEngine;
using System.Collections;

public class GoodShip : MonoBehaviour {

	public GameObject shipExplosion;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Explode () {
		GameObject explosion = Instantiate (shipExplosion, this.transform.position, Quaternion.identity) as GameObject;

		DestroyObject (explosion, Random.Range(0.1f, 0.2f));
		DestroyObject (gameObject);
	}

}
