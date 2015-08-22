using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    public GameObject m_EyePrefab;
    Eye[] m_Eyes;

    // Use this for initialization
    void Start()
    {
        m_Eyes = new Eye[1];
        GameObject leftEye = Instantiate(m_EyePrefab, transform.position, transform.rotation) as GameObject;
        leftEye.transform.parent = transform;
        leftEye.transform.Translate( new Vector2(-1, 0));
        m_Eyes[0] = leftEye.GetComponent<Eye>();

        GameObject rightEye = Instantiate(m_EyePrefab, transform.position, transform.rotation) as GameObject;
        rightEye.transform.parent = transform;
        rightEye.transform.Translate(new Vector2(1, 0));
        m_Eyes[1] = rightEye.GetComponent<Eye>();
    }

    // Update is called once per frame
    void Update ()
    {
        float horizontal = Input.GetAxis("LeftHorizontal");
        float vertical = Input.GetAxis("LeftVertical");


    }
}
