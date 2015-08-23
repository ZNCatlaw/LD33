using UnityEngine;
using System.Collections;

public class PlayerManager : MonoBehaviour
{
    public GameObject m_EyePrefab;
    public GameObject m_LogicalEyePrefab;

    GameObject m_EyePool;
    GameObject m_LogicalEyes;

    Vector2[] m_Positions = { new Vector2(-3, 2), new Vector2(3, 2), new Vector2(-3, 0.5f), new Vector2(3, 0.5f)};
    string[] m_Hand = { "_Left", "_Right" };
    string[] m_Colors = { "Blue", "Green", "Purple", "Red", "Yellow" };

    void Start()
    {
        //Create the eyepool and fill it with eyes
        m_EyePool = new GameObject();
        m_EyePool.transform.SetParent(this.transform);
        m_EyePool.name = "Eye Pool";

        m_LogicalEyes = new GameObject();
        m_LogicalEyes.transform.SetParent(this.transform);
        m_LogicalEyes.name = "Logical Eye Pool";

        for (int i = 0; i < 4; i++)
        {
            Vector3 position = m_Positions[i];
            //Vector3 position = new Vector3(Random.Range(-3, 3), Random.Range(0.5f, 2), 0);
            GameObject eye = Instantiate(m_EyePrefab, position, transform.rotation) as GameObject;
            eye.transform.parent = m_EyePool.transform;
            eye.name = "Eye" + i;
        }

        //Create the logical eyes, then will grab eye's from the eye pool as needed
        for (int i = 0; i < 4; i++)
        {
            GameObject logicalEyeObject = Instantiate(m_LogicalEyePrefab, Vector3.zero, transform.rotation) as GameObject;
            LogicalEye logicalEye = logicalEyeObject.GetComponent<LogicalEye>();
            logicalEye.transform.SetParent(m_LogicalEyes.transform);
            logicalEye.m_PlayerManager = this;
            logicalEye.m_Horizontal = "" + i / 2 + m_Hand[i % 2] + "Horizontal";
            logicalEye.m_Vertical = "" + i / 2 + m_Hand[i % 2] + "Vertical";
            logicalEye.m_Trigger = "" + i / 2 + m_Hand[i % 2] + "Fire";
            logicalEye.name = "LogicalEye" + i;
            logicalEye.m_Colour = m_Colors[i / 2];
        }
    }

    public Eye GetEye()
    {
        Transform eye = m_EyePool.transform.GetChild( Random.Range(0, m_EyePool.transform.childCount) );
        return eye.GetComponent<Eye>();
    }
    public void ReturnEye(Eye eye)
    {
        eye.transform.SetParent(m_EyePool.transform);
    }

    // Update is called once per frame
    void Update ()
    {
	}
}
