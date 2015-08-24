using UnityEngine;
using System.Collections;

public class GoodShip : MonoBehaviour {

	public GameObject shipExplosion;
    public float m_Health;
	public int crashDamage;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Explode () {
		GameObject explosion = Instantiate (shipExplosion, this.transform.position, Quaternion.identity) as GameObject;

		DestroyObject (explosion, Random.Range(0.1f, 0.2f));
		DestroyObject (gameObject);
	}

    void OnTriggerStay2D(Collider2D collider)
    {
        GameObject other = collider.gameObject;
        if (other.name == "EyeLaser")
        {
            m_Health -= 10.0f * Time.deltaTime;
            if ( m_Health <=0 )
            {
                Explode();
            }
        }
    }

}
