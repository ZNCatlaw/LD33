using UnityEngine;
using System.Collections;

public class Eye : MonoBehaviour
{
    public Vector2 m_LookDirection;

    //[0..1], [0..1] (I think? or maybe [0..1) ? )
    public void SetInput(float horizontal, float vertical)
    {
        vertical = Mathf.Max(vertical, 0);
        m_LookDirection = new Vector2(horizontal, vertical);
        m_LookDirection.Normalize();

        Vector3 eyeOffset = new Vector3(horizontal * 0.3f, vertical * 0.10f, -1);
        transform.GetChild(0).transform.localPosition = eyeOffset;
    }

        // Use this for initialization
    void Start ()
    {
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
