using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    public GameObject m_EyePrefab;
    Eye[] m_Eyes;

    Game m_Game;

    // Use this for initialization
    void Start()
    {
        m_Eyes = new Eye[2];
        GameObject leftEye = Instantiate(m_EyePrefab, transform.position, transform.rotation) as GameObject;
        leftEye.transform.parent = transform;
        leftEye.transform.Translate( new Vector2(-1, 0));
        m_Eyes[0] = leftEye.GetComponent<Eye>();

        GameObject rightEye = Instantiate(m_EyePrefab, transform.position, transform.rotation) as GameObject;
        rightEye.transform.parent = transform;
        rightEye.transform.Translate(new Vector2(1, 0));
        m_Eyes[1] = rightEye.GetComponent<Eye>();

        m_Game = FindObjectOfType<Game>();
    }

    // Update is called once per frame
    void Update ()
    {
        m_Eyes[0].SetInput(Input.GetAxis("LeftHorizontal"), Input.GetAxis("LeftVertical"));
        m_Eyes[1].SetInput(Input.GetAxis("RightHorizontal"), Input.GetAxis("RightVertical"));

        if (Input.GetAxis("LeftFire") > 0.1f)
        {
			ProjectileManager projectileManager = m_Game.GetComponent<ProjectileManager>();
            projectileManager.AddLaser(m_Eyes[0].transform.position, m_Eyes[0].m_LookDirection );
        }
        if (Input.GetAxis("RightFire") > 0.1f)
        {
			ProjectileManager projectileManager = m_Game.GetComponent<ProjectileManager>();
            projectileManager.AddLaser(m_Eyes[1].transform.position, m_Eyes[1].m_LookDirection );
        }
    }
}
