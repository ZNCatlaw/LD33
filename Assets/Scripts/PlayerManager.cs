using UnityEngine;
using System.Collections;

public class PlayerManager : MonoBehaviour
{
    public GameObject m_PlayerPrefab;
    Player[] m_Players;

    void Start()
    {
        m_Players = new Player[1];

        GameObject player = Instantiate(m_PlayerPrefab, transform.position, transform.rotation) as GameObject;
        m_Players[0] = player.GetComponent<Player>();
    }

    // Update is called once per frame
    void Update ()
    {
	}
}
