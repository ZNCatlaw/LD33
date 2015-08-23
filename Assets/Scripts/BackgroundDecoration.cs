using UnityEngine;
using System.Collections;

public class BackgroundDecoration : MonoBehaviour {

    public int pixelsPerUnit = 20;
    public Sprite[] decoratorSprites;

    [HideInInspector]
    public float desiredYPos = 0.0f;

    private float onePixel = 0.0f;

    // Use this for initialization
    void Start ()
    {
        GetComponent<SpriteRenderer>().sprite = decoratorSprites[Random.Range(0, decoratorSprites.Length)];
        onePixel = 1f / pixelsPerUnit;
    }
	
	// Update is called once per frame
	void Update ()
    {
        var newPosition = transform.position;
        newPosition.y = Utils.Math.RoundToPixel(desiredYPos, pixelsPerUnit);
        transform.position = newPosition;
	}
}
