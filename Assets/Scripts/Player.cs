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
        {
            float horizontal = Input.GetAxis("LeftHorizontal");
            float vertical = Input.GetAxis("LeftVertical");
            vertical = Mathf.Max(vertical, 0);

            Vector3 lookDirection = new Vector2(horizontal, vertical);

            m_Eyes[0].transform.GetChild(0).transform.localPosition =
                new Vector3(lookDirection.x, lookDirection.y, -1);
        }
        {
            float horizontal = Input.GetAxis("RightHorizontal");
            float vertical = Input.GetAxis("RightVertical");
            vertical = Mathf.Max(vertical, 0);

            Vector3 lookDirection = new Vector2(horizontal, vertical);

            m_Eyes[1].transform.GetChild(0).transform.localPosition =
                new Vector3(lookDirection.x, lookDirection.y, -1);
        }

        if (Input.GetButton("Fire1"))
        {
            ProjectileManager projectileManager = m_Game.GetComponent<ProjectileManager>();
            projectileManager.AddLaser(m_Eyes[0].transform.position, new Vector3(0, -1, 0));
        }
    }
}
