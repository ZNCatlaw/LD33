using UnityEngine;
using System.Collections;

public class BackgroundDecoration : MonoBehaviour {

    public int pixelsPerUnit = 20;

    public Sprite[] decoratorSprites;
    [Range(0.0f, 5.0f)]
    public float scrollSpeed = 0.5f;

    private float desiredYTranslate = 0.0f;
    private float onePixel = 0.0f;

    // Use this for initialization
    void Start ()
    {
        GetComponent<SpriteRenderer>().sprite = decoratorSprites[Random.Range(0, decoratorSprites.Length)];
	}
	
	// Update is called once per frame
	void Update ()
    {
        onePixel = 1f / pixelsPerUnit;
        desiredYTranslate += (scrollSpeed * Time.deltaTime);
        while (desiredYTranslate >= onePixel)
        {
            var newPosition = transform.position;
            newPosition.y = (float) (System.Math.Round((newPosition.y - desiredYTranslate) * pixelsPerUnit, System.MidpointRounding.AwayFromZero) / pixelsPerUnit);
            transform.position = newPosition;
            desiredYTranslate -= onePixel;
        }
	}
}
