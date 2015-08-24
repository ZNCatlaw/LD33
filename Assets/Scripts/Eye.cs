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

    GameObject m_Laser;
    float m_LaserIntensity;

    public string m_Horizontal;
    public string m_Vertical;
    public string m_Trigger;

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
        if (m_LookDirection.magnitude > 0.1f)
        {
            Vector2 start = transform.GetChild(0).transform.position;
            Vector2 end = start + m_LookDirection * 25.0f;

            RaycastHit2D rayHit = Physics2D.Raycast(start,
                m_LookDirection,
                100.0f,
                1 << LayerMask.NameToLayer("Col_Good Ships"));

            if (rayHit.collider != null)
            {
                end = rayHit.point;
            }

            Vector2 delta = end - start;
            float distance = delta.magnitude + 1.0f;
            //Debug.Log(distance);

            float angle = Mathf.Atan2(delta.y, delta.x) * Mathf.Rad2Deg;
            Quaternion q = Quaternion.AngleAxis(angle + 90.0f, Vector3.forward);
            m_Laser.transform.rotation = q;// Quaternion.Slerp(m_Eyes[0].m_Laser.transform.rotation, q, Time.deltaTime * 5.0f );
            m_Laser.transform.localScale = new Vector3(0.55f, distance, 1);// 1, distance, 1);
            Vector2 temp = m_Laser.transform.rotation * new Vector2(0, distance * (-0.5f));//.localPosition;
            m_Laser.transform.localPosition = temp;//

            m_LaserIntensity = 1.0f;
        }
    }

	public void SetColor (string colorName) {
		this.GetComponent<SpriteRenderer>().sprite = this.transform.Find ("EyeOpening" + colorName).gameObject.GetComponent<SpriteRenderer>().sprite;
	}
	
	public void Open () {
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
        m_Laser = Instantiate(m_LaserPrefab, transform.position, transform.rotation) as GameObject;
        m_Laser.transform.parent = transform;
        m_Laser.name = "EyeLaser";

		eyeClosed = this.transform.Find ("EyeClosed").gameObject;
		colorLerp = this.GetComponent<ColorLerp> ();
    }

    // Update is called once per frame
    void Update()
    {
		if (this.isClosing && colorLerp.isDone) {
			this.isClosing = false;
			this.isClosed = true;
			eyeClosed.GetComponent<SpriteRenderer> ().enabled = true;
		}

		//Update the laser intensity
        m_LaserIntensity = Mathf.Clamp(m_LaserIntensity - 4.0f * Time.deltaTime, 0, 1);
        Color color = m_Laser.GetComponent<MeshRenderer>().material.GetColor("_Color");
        color.a = m_LaserIntensity;
        m_Laser.GetComponent<Renderer>().material.SetColor("_Color", color);
    }
}
