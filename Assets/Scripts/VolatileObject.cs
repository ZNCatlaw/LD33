using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class VolatileObject : MonoBehaviour {

    public float maxLifespan = 30.0f;
    public bool destroyAfterAudio;
    public bool destroyAfterAnim;

    private List<float> times = new List<float>();

    // Use this for initialization
    void Start() {
        if (destroyAfterAudio)
        {
            var audioSource = gameObject.GetComponent<AudioSource>();
            if (audioSource != null)
            {
                if (audioSource.playOnAwake)
                {
                    times.Add(audioSource.clip.length);
                }
            }
        }

        if (destroyAfterAnim)
        {
            var animator = gameObject.GetComponent<Animator>();
            if (animator != null)
            {
                times.Add(animator.GetCurrentAnimatorClipInfo(0)[0].clip.length);
            }
        }

        var lifeSpan = Mathf.Min(times.Max(), maxLifespan);
        Debug.Log(lifeSpan);
        Destroy(gameObject, lifeSpan);
    }
	
	// Update is called once per frame
	void Update () {
	}
}
