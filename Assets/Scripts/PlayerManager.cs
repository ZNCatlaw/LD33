using UnityEngine;
using System.Collections;

public class PlayerManager : MonoBehaviour
{
    public GameObject m_EyePrefab;
    public GameObject m_LogicalEyePrefab;
	public Beast beast;
    public GameObject m_GlyphPrefab;
	public int m_numberOfPlayers;

	public bool playerDead = false;

    GameObject m_EyePool;
    GameObject m_LogicalEyes;

    GameObject[] m_PlayerGlyphs;

    Vector2[] m_Positions = { new Vector2(-3, 2), new Vector2(3, 2), new Vector2(-3, 0.5f), new Vector2(3, 0.5f) };
    string[] m_Colors = { "Blue", "Green", "Purple", "Red", "Yellow" };

    Vector3 RandomEyePosition()
    {
        Vector3 foo = new Vector3(Random.Range(-3, 3), Random.Range(0.5f, 2.5f), 0);
        return foo;
    }

    void Start()
    {
		ProjectileManager projectilManager = GameObject.Find ("Game").GetComponent<ProjectileManager> () as ProjectileManager;

		//Create the eyepool and fill it with eyes
        m_EyePool = new GameObject();
        m_EyePool.transform.SetParent(beast.transform);
        m_EyePool.name = "Eye Pool";

        m_LogicalEyes = new GameObject();
        m_LogicalEyes.transform.SetParent(this.transform);
        m_LogicalEyes.name = "Logical Eye Pool";

        for (int i = 0; i < m_numberOfPlayers*2; i++)
        {
            //Vector3 position = m_Positions[i];
            Vector3 position = RandomEyePosition();// new Vector3(Random.Range(-3, 3), Random.Range(0.5f, 2), 0);
            GameObject eye = Instantiate(m_EyePrefab, position, transform.rotation) as GameObject;
            eye.transform.parent = m_EyePool.transform;
			eye.GetComponent<LinearAttackPattern>().m_projectileManager = projectilManager;
            eye.name = "Eye" + i;
        }

        //Create the logical eyes, then will grab eye's from the eye pool as needed
        for (int i = 0; i < m_numberOfPlayers*2; i++)
        {
            GameObject logicalEyeObject = Instantiate(m_LogicalEyePrefab, Vector3.zero, transform.rotation) as GameObject;
            LogicalEye logicalEye = logicalEyeObject.GetComponent<LogicalEye>();
            logicalEye.SetPlayer(i / 2, i % 2);
            logicalEye.transform.SetParent(m_LogicalEyes.transform);
            logicalEye.m_PlayerManager = this;
            logicalEye.name = "LogicalEye" + i;
            logicalEye.m_Colour = m_Colors[i / 2];
        }

        m_PlayerGlyphs = new GameObject[m_numberOfPlayers];
        for (int i = 0; i < m_numberOfPlayers; i++)
        {
            m_PlayerGlyphs[i] = Instantiate(m_GlyphPrefab, Vector3.zero, transform.rotation) as GameObject;
            m_PlayerGlyphs[i].SetActive(false);
        }
    }

    public Eye GetEye()
    {
        //Pick a random unused eye
        Transform eye = m_EyePool.transform.GetChild(Random.Range(0, m_EyePool.transform.childCount));

        //Move it to a random position
        Vector3 position = RandomEyePosition();
        eye.localPosition = position;
        return eye.GetComponent<Eye>();
    }
    public void ReturnEye(Eye eye)
    {
        eye.transform.SetParent(m_EyePool.transform);
    }

    void FindEyePair(int playerNum, out LogicalEye a, out LogicalEye b)
    {
        a = null;
        b = null;
        for (int i = 0; i < m_LogicalEyes.transform.childCount; i++)
        {
            LogicalEye logicalA = m_LogicalEyes.transform.GetChild(i).GetComponent<LogicalEye>();
            if (logicalA.m_PlayerNum == playerNum && logicalA.m_HandNum == 0)
            {
                a = logicalA;
            }
            if (logicalA.m_PlayerNum == playerNum && logicalA.m_HandNum == 1)
            {
                b = logicalA;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

		if (this.beast.isDead == true) {
			this.playerDead = true;
		}

        //Check that the left/rightness of eye pairs match the world left/rightness
        for (int i = 0; i < m_LogicalEyes.transform.childCount; i++)
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


        //Search for eye pairs
        {
            for (int i = 0; i < m_numberOfPlayers; i++)
            {
                LogicalEye logicalA;
                LogicalEye logicalB;

                FindEyePair(0, out logicalA, out logicalB);
                if (logicalA && logicalA.m_TargetEye && logicalB && logicalB.m_TargetEye)
                {
                    Vector2 startA = logicalA.m_TargetEye.transform.position;
                    Vector2 endA = startA + logicalA.m_TargetEye.m_LookDirection * 25.0f; ;
                    Vector2 startB = logicalB.m_TargetEye.transform.position;
                    Vector2 endB = startB + logicalB.m_TargetEye.m_LookDirection * 25.0f;

                    Vector2 intersection = new Vector2();
                    if (LineIntersection(startA, endA, startB, endB, out intersection))
                    {
                        Debug.Log("Placing Glyph at " + intersection);

                        m_PlayerGlyphs[i].transform.position = intersection;
                        m_PlayerGlyphs[i].SetActive(true);

                    }
                    else
                    {
                        m_PlayerGlyphs[i].SetActive(false);
                    }

                }
            }
        }

    }



    bool LineIntersection(Vector2 ps1, Vector2 pe1, Vector2 ps2, Vector2 pe2, out Vector2 intersection)
    {
        // Get A,B,C of first line - points : ps1 to pe1
        float A1 = pe1.y - ps1.y;
        float B1 = ps1.x - pe1.x;
        float C1 = A1 * ps1.x + B1 * ps1.y;

        // Get A,B,C of second line - points : ps2 to pe2
        float A2 = pe2.y - ps2.y;
        float B2 = ps2.x - pe2.x;
        float C2 = A2 * ps2.x + B2 * ps2.y;

        // Get delta and check if the lines are parallel
        float delta = A1 * B2 - A2 * B1;
        if (delta < 0.001f)
        {
            //Lines are parallel
            intersection = Vector2.zero;
            return false;
        }
        else
        {
            // now return the Vector2 intersection point
            intersection = new Vector2(
                (B2 * C1 - B1 * C2) / delta,
                (A1 * C2 - A2 * C1) / delta);
            return true;
        }
    }
}
