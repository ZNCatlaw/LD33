using UnityEngine;
using System.Collections;

public class ColorLerp : MonoBehaviour {
	public Material material1;
	public Material material2;
	public float duration = 2.0F;

	private IEnumerator toGreyscale;
	private bool toGreyscaleIsRunning = false;
	private IEnumerator toFullColor;
	private bool toFullColorIsRunning = false;
	
	public void ToFullColor () {
		if (toGreyscale != null) {
			StopCoroutine(toGreyscale);
		}

		if (toFullColorIsRunning == false) {
			toFullColorIsRunning = true;
			toFullColor = Lerping (0, 1);
			StartCoroutine (toFullColor);
		}

	}

	public void ToGreyscale () {	
		if (toFullColor != null) {
			StopCoroutine(toFullColor);
		}

		if (toGreyscaleIsRunning == false) {
			toGreyscaleIsRunning = true;
			toGreyscale = Lerping (1, 0);
			StartCoroutine (toGreyscale);	
		}

	}
	
	IEnumerator Lerping (float from, float to) {
		SpriteRenderer rend = GetComponent<SpriteRenderer> ();
		float lerp ;
		float time = 0;

		do {
			lerp = Mathf.Lerp(from, to, time/duration);
			rend.material.Lerp(material1, material2, lerp);
			time += Time.deltaTime;
			yield return null;
		} while (lerp != to);

		// we just finished one of the coroutines
		if (from > to) {
			toGreyscaleIsRunning = false;
		} else {
			toFullColorIsRunning = false;
		}
	}

}