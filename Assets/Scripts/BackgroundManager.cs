using UnityEngine;
using System.Collections;

public class BackgroundManager : MonoBehaviour {

    public int pixelsPerUnit;
    public GameObject waterDecorationPrefab;
    public int waterDecorationPoolSize = 32;

    [Range(0.0f, 5.0f)]
    public float spawnRate = 1f;
    [Range(0.0f, 1.0f)]
    public float spawnRateVariance = 0f;
    [Range(1, 4)]
    public int spawnDensity = 2;
    [Range(0.0f, 1.0f)]
    public float spawnDensityVariance = 0f;
    [Range(0.0f, 5.0f)]
    public float scrollSpeed = 1f;

    private float accumulator = 0f;
    private float nextSpawnTime = 0f;
    private int nextSpawnNum = 0;

    private GameObject waterDecPoolActive;
    private GameObject waterDecPoolInactive;

    private Camera mainCamera;
    private float screenBottom;

    // Use this for initialization
    void Start ()
    {
        mainCamera = Camera.main;
        screenBottom = mainCamera.ViewportToWorldPoint(new Vector3(0f, 0f)).y;

        waterDecPoolActive = new GameObject("Water Decoration Pool (Active)");
        waterDecPoolActive.transform.parent = gameObject.transform;
        waterDecPoolActive.transform.localPosition = Vector3.zero;
        waterDecPoolActive.transform.localRotation = Quaternion.identity;

        waterDecPoolInactive = new GameObject("Water Decoration Pool (Inactive)");
        waterDecPoolInactive.transform.parent = gameObject.transform;
        waterDecPoolInactive.transform.localPosition = Vector3.zero;
        waterDecPoolInactive.transform.localRotation = Quaternion.identity;
        waterDecPoolInactive.SetActive(false);

        for(var i = 0; i < waterDecorationPoolSize; i++)
        {
            var decoration = Instantiate(waterDecorationPrefab, waterDecPoolInactive.transform.position, waterDecPoolInactive.transform.rotation) as GameObject;
            decoration.transform.parent = waterDecPoolInactive.transform;
        }

        CalculateNextSpawn();
    }
	
	// Update is called once per frame
	void Update ()
    {
        //Accumulate time and spawn new water decorations
        accumulator += Time.deltaTime;
        if (accumulator >= nextSpawnTime && waterDecPoolInactive.transform.childCount > 0)
        {
            accumulator = 0f;

            var newDec = waterDecPoolInactive.transform.GetChild(Random.Range(0, waterDecPoolInactive.transform.childCount)).gameObject;
            var logic = newDec.gameObject.GetComponent<BackgroundDecoration>();
            newDec.transform.parent = waterDecPoolActive.transform;

            var origin = mainCamera.ViewportToWorldPoint(new Vector3(Random.Range(0f, 1f), 1f));
            origin.x -= 0.4f;
            origin.x = Utils.Math.RoundToPixel(origin.x, pixelsPerUnit);
            origin.y = Utils.Math.RoundToPixel(origin.y, pixelsPerUnit);
            origin.z = gameObject.transform.position.z;
            newDec.transform.position = origin;
            logic.desiredPos = origin;
        }

        //Scroll active water decorations, recycle old ones that hit the bottom
        foreach (Transform dec in waterDecPoolActive.transform)
        {
            var logic = dec.gameObject.GetComponent<BackgroundDecoration>();

            if (dec.transform.position.y >= screenBottom)
            {    
                var translate = new Vector3(0.0f, Time.deltaTime * scrollSpeed, 0.0f);
                var newPos = logic.desiredPos - translate;
                logic.desiredPos = newPos;
            }
            else
            {
                dec.transform.parent = waterDecPoolInactive.transform;
            }
        }

	}
    void CalculateNextSpawn ()
    {
        nextSpawnTime = 1.0f / (spawnRate + (Random.Range(-1f * spawnRateVariance, 1f * spawnRateVariance)));
        nextSpawnNum = spawnDensity + (int) Mathf.Round(Random.Range(-1f * spawnDensityVariance, 1f * spawnDensityVariance));
    }
}
