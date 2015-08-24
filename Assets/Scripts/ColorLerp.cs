using UnityEngine;
using System.Collections;

public class ColorLerp : MonoBehaviour {
	public Material material1;
	public Material material2;
	public float duration = 3.0F;

	private float from = 0;
	private float to = 1;
	private float current = 0;
	private float time = 0;

	private SpriteRenderer rend;

	private bool _isDone = true;
	public bool isDone {
		get { return _isDone; }
	}
	
	public void ToFullColor () {
		this.LerpTo (1);
	}

	public void ToGreyscale () {	
		this.LerpTo (0);
	}

	private void LerpTo(float value) {
		this.from = current;
		this.to = value;
		this.time = 0;
		this._isDone = false;
	}

	void Start () {
		rend = GetComponent<SpriteRenderer> ();
	}
	
	void Update () {
		float lerp ;

		if (this._isDone == false) {
			lerp = Mathf.SmoothStep(this.from, this.to, time/duration);
			rend.material.Lerp(material1, material2, lerp);
			time += Time.deltaTime;

			this.current = lerp;
			
			if (lerp == to) {
				this._isDone = true;
			}
		}
	}
}