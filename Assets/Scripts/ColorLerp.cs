using UnityEngine;
using System.Collections;

public class ColorLerp : MonoBehaviour {
	public Material material1;
	public Material material2;
	public float duration = 2.0F;
	public SpriteRenderer rend;

	void Start() {
		rend = GetComponent<SpriteRenderer>();
		rend.material = material1;
	}
	void Update() {
		float lerp = Mathf.PingPong(Time.time, duration) / duration;
		rend.material.Lerp(material1, material2, lerp);
	}
}