using UnityEngine;
using System.Collections;

public class StupidLinearShip : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Rigidbody2D body = this.GetComponent<Rigidbody2D> ();
		body.velocity = new Vector2 (0, -1);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
