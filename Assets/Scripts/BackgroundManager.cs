using UnityEngine;
using System.Collections;

public class BackgroundManager : MonoBehaviour {

    public GameObject decoratorPrefab;
    [Range(0.0f, 5.0f)]
    public float spawnRate = 1f;
    [Range(0.0f, 1.0f)]
    public float spawnRateVariance = 0f;
    [Range(1, 4)]
    public int spawnDensity = 2;
    [Range(0.0f, 1.0f)]
    public float spawnDensityVariance = 0f;
    [Range(0.0f, 5.0f)]
    public float scrollSpeed = 0f;

    private float accumulator = 0f;
    private float nextSpawnTime = 0f;
    private int nextSpawnNum = 0;

    private Camera mainCamera;

    // Use this for initialization
    void Start ()
    {
        mainCamera = Camera.main;
        CalculateNextSpawn(); 
	}
	
	// Update is called once per frame
	void Update ()
    {
        accumulator += Time.deltaTime;
        if (accumulator >= nextSpawnTime)
        {
            accumulator = 0f;

            var origin = mainCamera.ViewportToWorldPoint(new Vector3(Random.Range(0f, 1f), 1.05f));
            origin.x = origin.x - (origin.x % 0.05f);
            origin.y = origin.y - (origin.y % 0.05f);
            var decoration = Instantiate(decoratorPrefab, origin, transform.rotation) as GameObject;
            decoration.transform.parent = gameObject.transform;

            var localPosition = decoration.transform.localPosition;
            localPosition.z = 0;
            decoration.transform.localPosition = localPosition;
        }
	}
    void CalculateNextSpawn ()
    {
        nextSpawnTime = 1.0f / (spawnRate + (Random.Range(-1f * spawnRateVariance, 1f * spawnRateVariance)));
        nextSpawnNum = spawnDensity + (int) Mathf.Round(Random.Range(-1f * spawnDensityVariance, 1f * spawnDensityVariance));
    }
}
