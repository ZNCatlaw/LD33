﻿using UnityEngine;
using System.Collections;

public class LogicalEye : MonoBehaviour
{
	private bool m_EyeClosing;
    public Eye m_TargetEye;
    public PlayerManager m_PlayerManager;

    public string m_Colour;
    public string m_Horizontal;
    public string m_Vertical;
    public string m_Trigger;
	public string m_AltTrigger;

    float m_IdleTimeoutRemaining;
    public float m_IdleTimeout;

    public int m_PlayerNum;
    public int m_HandNum;


    string[] m_Hand = { "_Left", "_Right" };

    // Use this for initialization
    void Start ()
    {
        m_IdleTimeoutRemaining = 0;
    }

    public void SetHand( int hand)
    {
        SetPlayer(m_PlayerNum, hand);
    }

    public void SetPlayer(int player, int hand)
    {
        m_PlayerNum = player;
        m_HandNum = hand;

        m_Horizontal = "" + player + m_Hand[hand] + "Horizontal";
        m_Vertical = "" + player + m_Hand[hand] + "Vertical";
        m_Trigger = "" + player + m_Hand[hand] + "FireAxis";
		m_AltTrigger = "" + player + m_Hand[hand] + "FireButton";

    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis(m_Horizontal);
        float vertical = Input.GetAxis(m_Vertical);
        bool trigger = (Input.GetAxis(m_Trigger) > 0.1f || Input.GetButton(m_AltTrigger));

        //Check if the player is idle
        if (vertical != 0 || trigger)
        {
            m_IdleTimeoutRemaining = m_IdleTimeout;
        }
        else
        {
            m_IdleTimeoutRemaining -= Time.deltaTime;
        }

		if(!m_TargetEye && m_IdleTimeoutRemaining > 0.0f)        
		{
			// OPEN The player is not idle, find an eye to use
			m_TargetEye = m_PlayerManager.GetEye();
			m_TargetEye.transform.parent = transform;
			m_TargetEye.SetColor(m_Colour);
			m_TargetEye.Open();
        }
        else if (m_TargetEye && !m_TargetEye.isClosing && m_IdleTimeoutRemaining <= 0)
        {
            // START CLOSING The player is idle, close and release the eye
            m_TargetEye.Close();
		}
        else if (m_TargetEye && m_TargetEye.isClosing && m_IdleTimeoutRemaining > 0.0f) 
		{
			// INTERRUPT CLOSING if the eye is closing and the player provides input, cancel the close
			m_TargetEye.Open();
		}
        else if (m_TargetEye && m_TargetEye.isClosed) 
		{
			// REMOVE EYE if the eye is finished closing, clean it up
			m_PlayerManager.ReturnEye( m_TargetEye );
			m_TargetEye = null;
		} 

        if (m_TargetEye)
        {
            m_TargetEye.SetInput(horizontal, vertical);
            if (trigger)
            {
                m_TargetEye.Fire();
            }
        }
    }
}
