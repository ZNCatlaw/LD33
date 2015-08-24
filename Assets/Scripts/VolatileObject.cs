using UnityEngine;
using System.Collections;

public class VolatileObject : MonoBehaviour {

    public float maxLifespan = 30.0f;
    public bool destroyAfterAudio;
    public bool destroyAfterAnim;

    private float[] times = new float[2];

    // Use this for initialization
    void Start() {
        if (destroyAfterAudio)
        {
            var audioSource = gameObject.GetComponent<AudioSource>();
            if (audioSource != null)
            {
                if (audioSource.playOnAwake)
                {
                    times[0] = audioSource.clip.length;
                }
            }
        }

        if (destroyAfterAnim)
        {
            var animator = gameObject.GetComponent<Animator>();
            if (animator != null)
            {
                times[1] = animator.GetCurrentAnimatorStateInfo(0).length;
            }
        }

        var lifeSpan = Mathf.Min(Mathf.Max(times), maxLifespan);
        Destroy(gameObject, lifeSpan);
    }
	
	// Update is called once per frame
	void Update () {
	}
}
