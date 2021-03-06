﻿using UnityEngine;
using System.Collections;

public class BackgroundDecoration : MonoBehaviour {

    public int pixelsPerUnit = 20;
    public bool pixelSnap = false;
    public GameObject[] decoratorPrefabs;

    [HideInInspector]
    public Vector3 desiredPos;

    // Use this for initialization
    void Start ()
    {
        var prefab = decoratorPrefabs[Random.Range(0, decoratorPrefabs.Length)];
        var child = Instantiate(prefab, transform.position, Quaternion.identity) as GameObject;
        child.transform.parent = gameObject.transform;
        desiredPos = transform.position;
    }
	
	// Update is called once per frame
	void Update ()
    {
        var newPosition = transform.position;
        if (pixelSnap)
        {
            newPosition.x = Utils.Math.RoundToPixel(desiredPos.x, pixelsPerUnit);
            newPosition.y = Utils.Math.RoundToPixel(desiredPos.y, pixelsPerUnit);
            newPosition.z = Utils.Math.RoundToPixel(desiredPos.z, pixelsPerUnit);
        }
        else
        {
            newPosition.x = desiredPos.x;
            newPosition.y = desiredPos.y;
            newPosition.z = desiredPos.z;
        }
        transform.position = newPosition;
	}
}
