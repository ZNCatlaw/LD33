using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    GameObject m_EyePrefab;
    GameObject m_Eyes;

    // Use this for initialization
    void Start ()
    {
        m_Eyes[0] = Instantiate( m_EyePrefab, transform.position, transform.rotation) as Eye;
        m_Eyes[1] = Instantiate(m_EyePrefab, transform.position, transform.rotation) as Eye;
    }

    // Update is called once per frame
    void Update ()
    {
	
	}
}
