using UnityEngine;
using System.Collections;

public class EndOfGameBehaviour : MonoBehaviour {

    public Sprite statusText1;
    public Sprite statusText2;

    // Use this for initialization
    void Start () {
        if (statusText1 != null) transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = statusText1;
        if (statusText2 != null) transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = statusText2;
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
