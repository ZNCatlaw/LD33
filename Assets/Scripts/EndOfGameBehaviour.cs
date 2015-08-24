using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EndOfGameBehaviour : MonoBehaviour {

    public Sprite statusText1;
    public Sprite statusText2;
    private int finalScore;

    // Use this for initialization
    void Start () {
        if (statusText1 != null) transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = statusText1;
        if (statusText2 != null) transform.GetChild(2).GetComponent<SpriteRenderer>().sprite = statusText2;
    }

    public void setFinalScore(int score)
    {
        finalScore = score;
        var uiText = transform.GetChild(0).GetChild(0).GetComponent<Text>();
        uiText.text = "CASUALTIES:\n" + finalScore;
        uiText.font.material.mainTexture.filterMode = FilterMode.Point;
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
