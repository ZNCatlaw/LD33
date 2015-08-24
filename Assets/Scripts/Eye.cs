using UnityEngine;
using System.Collections;

public class Eye : MonoBehaviour
{
    public Vector2 m_LookDirection;
    public GameObject m_LaserPrefab;
	public float closeLidDelay;

	public bool isClosing = false;
	public bool isClosed = true;

	public delegate void OnClose ();
	private OnClose onLidDidClose;

	private ColorLerp colorLerp;
	private GameObject eyeClosed;
	private EnragedAttackPattern attackPattern;


    public GameObject m_Laser;
    public float m_LaserIntensity;

	[HideInInspector] public Beast beast;

	[HideInInspector] public string m_Horizontal;
	[HideInInspector] public string m_Vertical;
	[HideInInspector] public string m_Trigger;

    //[0..1], [0..1] (I think? or maybe [0..1) ? )
    public void SetInput(float horizontal, float vertical)
    {
        vertical = Mathf.Max(vertical, 0);

        m_LookDirection = new Vector2(horizontal, vertical);
        m_LookDirection.Normalize();

        Vector3 eyeOffset = transform.GetChild(0).transform.localPosition;
        eyeOffset.x = horizontal * 0.3f;
        eyeOffset.y = vertical * 0.10f;
        transform.GetChild(0).transform.localPosition = eyeOffset;
    }

    public void Fire()
    {
		if (this.beast.isDead) { return; }

        if (m_LookDirection.magnitude > 0.1f)
        {
            Vector2 start = m_Laser.transform.position;// transform.GetChild(0).transform.position;
            Vector2 end = start + (m_LookDirection * 25.0f);

            RaycastHit2D rayHit = Physics2D.Raycast(start,
                m_LookDirection,
                100.0f,
                1 << LayerMask.NameToLayer("Col_Good Ships"));

            if (rayHit.collider != null)
            {
                end = rayHit.point;
            }

            Vector2 delta = end - start;
            float distance = delta.magnitude + 0.5f;

            GameObject beam = m_Laser.transform.GetChild(0).gameObject;
            if (delta.magnitude > 0.1f)
            {
                float angle = Mathf.Atan2(delta.y, delta.x) * Mathf.Rad2Deg;
                Quaternion q = Quaternion.AngleAxis(angle - 90.0f, Vector3.forward);
                m_Laser.transform.rotation = q;// Quaternion.Slerp(m_Eyes[0].m_Laser.transform.rotation, q, Time.deltaTime * 5.0f );
            }
            beam.transform.localScale = new Vector3(1.0f, distance / 0.55f, 1.0f);

            m_LaserIntensity = 1.0f;
        }
    }

	public void SetColor (string colorName)
    {
		this.GetComponent<SpriteRenderer>().sprite = this.transform.Find ("EyeOpening" + colorName).gameObject.GetComponent<SpriteRenderer>().sprite;
        m_Laser.GetComponent<Animator>().Play("EyeLaser" + colorName);
    }

    public void Open ()
    {
		this.isClosed = false;
		this.isClosing = false;
		eyeClosed.GetComponent<SpriteRenderer> ().enabled = false;
		this.colorLerp.ToFullColor ();
	}

	public void Close () {
		this.isClosing = true;
		this.colorLerp.ToGreyscale ();
	}

    // Use this for initialization
    void Start ()
    {
        m_Laser = Instantiate(m_LaserPrefab, Vector3.zero, transform.rotation) as GameObject;
        m_Laser.transform.parent = transform.GetChild(0);
        m_Laser.transform.localPosition = new Vector3(0, 0, -1.0f);
        m_Laser.name = "EyeLaser";

		eyeClosed = this.transform.Find ("EyeClosed").gameObject;
		colorLerp = this.GetComponent<ColorLerp> ();

		attackPattern = this.GetComponent<EnragedAttackPattern> ();
		attackPattern.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
		if (this.isClosing && colorLerp.isDone)
        {
			this.isClosing = false;
			this.isClosed = true;
			eyeClosed.GetComponent<SpriteRenderer>().enabled = true;
		}

		if (this.beast.isEnraged == true) {
			this.attackPattern.enabled = true;
		} else {
			this.attackPattern.enabled = false;
		}

        //Update the laser intensity
        GameObject beam = m_Laser.transform.GetChild(0).gameObject;
        GameObject blast = m_Laser.transform.GetChild(1).gameObject;
        m_LaserIntensity = Mathf.Clamp(m_LaserIntensity - 4.0f * Time.deltaTime, 0, 1);
        Color color = new Color(1.0f, 1.0f, 1.0f, m_LaserIntensity);
        beam.GetComponent<SpriteRenderer>().color = color;
        blast.GetComponent<SpriteRenderer>().color = color;

        //Add a random force to mix things up
        float f = 1000.0f;
        transform.GetComponent<Rigidbody2D>().AddForce( new Vector2(Random.Range( -f, f), Random.Range( -f, f)));
    }
}
