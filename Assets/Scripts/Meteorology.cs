using UnityEngine;
using System.Collections;

public class Meteorology : MonoBehaviour {

    public int pixelsPerUnit = 20;
    public int direction;
    public Sprite[] cloudSprites;

    [HideInInspector]
    public Vector3 desiredPos;

    // Use this for initialization
    void Start () {
        desiredPos = transform.position;
        GetComponent<SpriteRenderer>().sprite = cloudSprites[Random.Range(0, cloudSprites.Length)];
    }
	
	// Update is called once per frame
	void Update () {
        var newPosition = transform.position;
        newPosition.x = Utils.Math.RoundToPixel(desiredPos.x, pixelsPerUnit);
        newPosition.y = Utils.Math.RoundToPixel(desiredPos.y, pixelsPerUnit);
        newPosition.z = Utils.Math.RoundToPixel(desiredPos.z, pixelsPerUnit);
        transform.position = newPosition;
    }
}
