using UnityEngine;
using System.Collections;

public class PlayerManager : MonoBehaviour
{
    public GameObject m_EyePrefab;
    public GameObject m_LogicalEyePrefab;
	public GameObject beast;

    GameObject m_EyePool;
    GameObject m_LogicalEyes;

    Vector2[] m_Positions = { new Vector2(-3, 2), new Vector2(3, 2), new Vector2(-3, 0.5f), new Vector2(3, 0.5f)};
    string[] m_Colors = { "Blue", "Green", "Purple", "Red", "Yellow" };

    Vector3 RandomEyePosition()
    {
        Vector3 foo = new Vector3(Random.Range(-3, 3), Random.Range(0.5f, 2.5f), 0);
        return foo;
    }

    void Start()
    {
        //Create the eyepool and fill it with eyes
        m_EyePool = new GameObject();
        m_EyePool.transform.SetParent(beast.transform);
        m_EyePool.name = "Eye Pool";

        m_LogicalEyes = new GameObject();
        m_LogicalEyes.transform.SetParent(this.transform);
        m_LogicalEyes.name = "Logical Eye Pool";

        for (int i = 0; i < 4; i++)
        {
            //Vector3 position = m_Positions[i];
            Vector3 position = RandomEyePosition();// new Vector3(Random.Range(-3, 3), Random.Range(0.5f, 2), 0);
            GameObject eye = Instantiate(m_EyePrefab, position, transform.rotation) as GameObject;
            eye.transform.parent = m_EyePool.transform;
            eye.name = "Eye" + i;
        }

        //Create the logical eyes, then will grab eye's from the eye pool as needed
        for (int i = 0; i < 4; i++)
        {
            GameObject logicalEyeObject = Instantiate(m_LogicalEyePrefab, Vector3.zero, transform.rotation) as GameObject;
            LogicalEye logicalEye = logicalEyeObject.GetComponent<LogicalEye>();
            logicalEye.SetPlayer(i / 2, i % 2);
            logicalEye.transform.SetParent(m_LogicalEyes.transform);
            logicalEye.m_PlayerManager = this;
            logicalEye.name = "LogicalEye" + i;
            logicalEye.m_Colour = m_Colors[i / 2];
        }
    }

    public Eye GetEye()
    {
        //Pick a random unused eye
        Transform eye = m_EyePool.transform.GetChild( Random.Range(0, m_EyePool.transform.childCount) );

        //Move it to a random position
        Vector3 position = RandomEyePosition();
        eye.localPosition = position;
        return eye.GetComponent<Eye>();
    }
    public void ReturnEye(Eye eye)
    {
        eye.transform.SetParent(m_EyePool.transform);
    }

    // Update is called once per frame
    void Update()
    {

        //Check that each pair of eyes managed by a single controller have 
        for(int i=0; i<m_LogicalEyes.transform.childCount; i++)
        {
            LogicalEye logicalA = m_LogicalEyes.transform.GetChild(i).GetComponent<LogicalEye>();
            if (logicalA.m_TargetEye)
            {
                Eye eyeA = logicalA.m_TargetEye.GetComponent<Eye>();

                for (int j = i; j < m_LogicalEyes.transform.childCount; j++)
                {
                    LogicalEye logicalB = m_LogicalEyes.transform.GetChild(j).GetComponent<LogicalEye>();
                    if (logicalB.m_TargetEye)
                    {
                        Eye eyeB = logicalB.m_TargetEye.GetComponent<Eye>();

                        if (logicalA.m_PlayerNum == logicalB.m_PlayerNum)
                        {
                            if (eyeA.transform.position.x < eyeB.transform.position.x)
                            {
                                logicalA.SetHand(0);
                                logicalB.SetHand(1);
                            }
                            else if (eyeA.transform.position.x > eyeB.transform.position.x)
                            {
                                logicalA.SetHand(1);
                                logicalB.SetHand(0);
                            }
                        }
                    }
                }
            }
        }
    }
}
