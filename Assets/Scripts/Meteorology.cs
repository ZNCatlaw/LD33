using UnityEngine;
using System.Collections;

public class Meteorology : MonoBehaviour {

    public int pixelsPerUnit = 20;
    public bool pixelSnap = false;
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
        if (pixelSnap)
        {
            newPosition.x = Utils.Math.RoundToPixel(desiredPos.x, pixelsPerUnit);
            newPosition.y = Utils.Math.RoundToPixel(desiredPos.y, pixelsPerUnit);
            newPosition.z = Utils.Math.RoundToPixel(desiredPos.z, pixelsPerUnit);
        }
        else
        {
            newPosition.x = desiredPos.x;
            newPosition.y = desiredPos.y;
            newPosition.z = desiredPos.z;
        }
        
        transform.position = newPosition;
    }
}
