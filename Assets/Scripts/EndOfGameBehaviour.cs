using UnityEngine;
using System.Collections;

public class EndOfGameBehaviour : MonoBehaviour {

    public Sprite statusText;

	// Use this for initialization
	void Start () {
        if (statusText != null) transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = statusText;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
