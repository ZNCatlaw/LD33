using UnityEngine;
using System.Collections;

public class ProjectileManager : MonoBehaviour
{
    public GameObject m_LaserPrefab;



    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        foreach (Transform child in transform)
        {
            child.transform.Translate(new Vector3(0, 0.01f, 0));
        }
    }

    public void AddLaser( Vector3 position, Vector3 direction )
    {
        GameObject laser = Instantiate(m_LaserPrefab, position, transform.rotation) as GameObject;
        laser.transform.parent = transform;
    }
}
