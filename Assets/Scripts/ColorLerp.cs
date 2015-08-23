using UnityEngine;
using System.Collections;

public class ColorLerp : MonoBehaviour {
	public Material material1;
	public Material material2;
	public float duration = 3.0F;

	private IEnumerator toGreyscale;
	private bool toGreyscaleIsRunning = false;
	private IEnumerator toFullColor;
	private bool toFullColorIsRunning = false;

	public delegate void Callback ();

	private bool _isGreyscale = true;
	public bool isGreyscale {
		get { return _isGreyscale; }
	}

	private bool _isFullColor = false;
	public bool isFullColor {
		get { return _isFullColor; }
	}

	public void ToFullColor () {
		if (toGreyscale != null) {
			StopCoroutine(toGreyscale);

			// clean up after interrupting the coroutine
			toGreyscaleIsRunning = false;
			_isGreyscale = false;
			_isFullColor = false;
		}

		if (toFullColorIsRunning == false) {
			toFullColorIsRunning = true;

			toFullColor = Lerping (0, 1);
			StartCoroutine (toFullColor);
		}

	}

	public void ToGreyscale (Callback callback) {	

		if (toFullColor != null) {
			StopCoroutine(toFullColor);

			// clean up after interrupting the coroutine
			toFullColorIsRunning = false;
			_isGreyscale = false;
			_isFullColor = false;
		}

		if (toGreyscaleIsRunning == false) {
			toGreyscaleIsRunning = true;

			toGreyscale = Lerping (1, 0, callback);
			StartCoroutine (toGreyscale);	
		}

	}

	IEnumerator Lerping (float from, float to) {
		return this.Lerping (from, to, null);
	}

	IEnumerator Lerping (float from, float to, Callback callback) {
		SpriteRenderer rend = GetComponent<SpriteRenderer> ();
		float lerp ;
		float time = 0;

		do {
			lerp = Mathf.SmoothStep(from, to, time/duration);
			rend.material.Lerp(material1, material2, lerp);
			time += Time.deltaTime;
			yield return null;
		} while (lerp != to);


		// we just finished one of the coroutines
		if (from > to) {
			toGreyscaleIsRunning = false;
			_isGreyscale = true;
			_isFullColor = false;
		} else {
			toFullColorIsRunning = false;
			_isFullColor = true;
			_isGreyscale = false;
		}

		if (callback != null) { callback(); }
	}

}