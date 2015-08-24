using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour
{
    public Vector3 m_Velocity;
	public GameObject bulletExplosion;
	public int cosmicRageDamage;

    // Use this for initialization
    void Start ()
    {
    }

// Update is called once per frame
    void Update ()
    {
	}

	public void Explode () {
		GameObject explosion = Instantiate (bulletExplosion, this.transform.position, Quaternion.identity) as GameObject;
		
		DestroyObject (explosion, Random.Range(0.1f, 0.2f));
		DestroyObject (gameObject);
	}
}
