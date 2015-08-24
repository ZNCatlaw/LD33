using UnityEngine;
using System.Collections;

public class BackgroundManager : MonoBehaviour {

    public int pixelsPerUnit;
    public bool pixelSnap = false;
    public GameObject waterPrefab;
    public int waterPoolSize = 32;
    public GameObject cloudPrefab;
    public int cloudPoolSize = 32;

    [Range(0.0f, 5.0f)]
    public float waterSpawnRate = 1f;
    [Range(0.0f, 1.0f)]
    public float waterSpawnRateVariance = 0f;
    [Range(0.0f, 5.0f)]
    public float cloudSpawnRate = 1f;
    [Range(0.0f, 1.0f)]
    public float cloudSpawnRateVariance = 0f;
    public float cloudYOffset = 8f;

    [Range(0.0f, 5.0f)]
    public float yScrollSpeed = 1f;
    [Range(0.0f, 5.0f)]
    public float xScrollSpeed = 1f;

    [Range(0.0f, 5.0f)]
    public float yParallax = 1f;
    [Range(0.0f, 5.0f)]
    public float xParallax = 1f;

    private float waterAccumulator = 0f;
    private float nextWaterSpawnTime = 0f;

    private float cloudAccumulator = 0f;
    private float nextCloudSpawnTime = 0f;

    private GameObject cloudPoolActive;
    private GameObject cloudPoolInactive;

    private GameObject waterPoolActive;
    private GameObject waterPoolInactive;

    private Camera mainCamera;
    private float screenBottom;
    private float screenLeft;
    private float screenRight;

    // Use this for initialization
    void Start ()
    {
        mainCamera = Camera.main;
        screenBottom = mainCamera.ViewportToWorldPoint(new Vector3(0f, 0f)).y;
        screenLeft = mainCamera.ViewportToWorldPoint(new Vector3(0f, 0f)).x;
        screenRight = mainCamera.ViewportToWorldPoint(new Vector3(1f, 0f)).x;

        waterPoolActive = new GameObject("Water Decoration Pool (Active)");
        waterPoolActive.transform.parent = gameObject.transform;
        waterPoolActive.transform.localPosition = Vector3.zero;
        waterPoolActive.transform.localRotation = Quaternion.identity;

        waterPoolInactive = new GameObject("Water Decoration Pool (Inactive)");
        waterPoolInactive.transform.parent = gameObject.transform;
        waterPoolInactive.transform.localPosition = Vector3.zero;
        waterPoolInactive.transform.localRotation = Quaternion.identity;
        waterPoolInactive.SetActive(false);

        cloudPoolActive = new GameObject("Cloud Decoration Pool (Active)");
        cloudPoolActive.transform.parent = gameObject.transform;
        cloudPoolActive.transform.localPosition = new Vector3(0f, 0f, -1f);
        cloudPoolActive.transform.localRotation = Quaternion.identity;

        cloudPoolInactive = new GameObject("Cloud Decoration Pool (Inactive)");
        cloudPoolInactive.transform.parent = gameObject.transform;
        cloudPoolInactive.transform.localPosition = new Vector3(0f, 0f, -1f);
        cloudPoolInactive.transform.localRotation = Quaternion.identity;
        cloudPoolInactive.SetActive(false);

        for (var i = 0; i < waterPoolSize; i++)
        {
            var decoration = Instantiate(waterPrefab, waterPoolInactive.transform.position, waterPoolInactive.transform.rotation) as GameObject;
            decoration.transform.parent = waterPoolInactive.transform;
        }

        for (var i = 0; i < cloudPoolSize; i++)
        {
            var decoration = Instantiate(cloudPrefab, cloudPoolInactive.transform.position, cloudPoolInactive.transform.rotation) as GameObject;
            decoration.transform.parent = cloudPoolInactive.transform;
        }

        CalculateNextWaterSpawn();
        CalculateNextCloudSpawn();
    }
	
	// Update is called once per frame
	void Update ()
    {
        //Accumulate time and spawn new water decorations
        waterAccumulator += Time.deltaTime;
        cloudAccumulator += Time.deltaTime;

        if (waterAccumulator >= nextWaterSpawnTime && waterPoolInactive.transform.childCount > 0)
        {
            SpawnWater();
            waterAccumulator = 0;
            CalculateNextWaterSpawn();
        }

        if (cloudAccumulator >= nextCloudSpawnTime && cloudPoolInactive.transform.childCount > 0)
        { 
            SpawnCloud();
            cloudAccumulator = 0;
            CalculateNextCloudSpawn();
        }
           
        //Scroll active water decorations (Y only), recycle old ones that hit the bottom
        foreach (Transform dec in waterPoolActive.transform)
        {
            var logic = dec.gameObject.GetComponent<BackgroundDecoration>();

            if (dec.transform.position.y >= screenBottom)
            {    
                var translate = new Vector3(0.0f, Time.deltaTime * yScrollSpeed, 0.0f);
                var newPos = logic.desiredPos - translate;
                logic.desiredPos = newPos;
            }
            else
            {
                dec.transform.parent = waterPoolInactive.transform;
            }
        }

        //Scroll active cloud decorations (X/Y), recycle old ones that hit the bottom or side
        foreach (Transform dec in cloudPoolActive.transform)
        {
            var logic = dec.gameObject.GetComponent<Meteorology>();

            var translate = new Vector3(logic.direction * Time.deltaTime * xScrollSpeed * xParallax, Time.deltaTime * yScrollSpeed * yParallax, 0.0f);
            var newPos = logic.desiredPos - translate;
            logic.desiredPos = newPos;

            if (dec.transform.position.y < screenBottom)
            {
                dec.transform.parent = cloudPoolInactive.transform;
            }
        }

    }

    void CalculateNextWaterSpawn ()
    {
        nextWaterSpawnTime = 1.0f / (waterSpawnRate + (Random.Range(-1f * waterSpawnRateVariance, 1f * cloudSpawnRateVariance)));
    }

    void CalculateNextCloudSpawn()
    {
        nextCloudSpawnTime = 1.0f / (cloudSpawnRate + (Random.Range(-1f * cloudSpawnRateVariance, 1f * cloudSpawnRateVariance)));
    }

    void SpawnWater()
    {
        var newDec = waterPoolInactive.transform.GetChild(Random.Range(0, waterPoolInactive.transform.childCount)).gameObject;
        var logic = newDec.gameObject.GetComponent<BackgroundDecoration>();
        newDec.transform.parent = waterPoolActive.transform;

        var origin = mainCamera.ViewportToWorldPoint(new Vector3(Random.Range(0f, 1f), 1f));
        origin.x -= 0.4f;
        if(pixelSnap)
        {
            origin.x = Utils.Math.RoundToPixel(origin.x, pixelsPerUnit);
            origin.y = Utils.Math.RoundToPixel(origin.y, pixelsPerUnit);
        }
        origin.z = waterPoolActive.transform.position.z;
        newDec.transform.position = origin;
        logic.desiredPos = origin;
    }

    void SpawnCloud()
    {
        var newDec = cloudPoolInactive.transform.GetChild(Random.Range(0, cloudPoolInactive.transform.childCount)).gameObject;
        var logic = newDec.gameObject.GetComponent<Meteorology>();
        newDec.transform.parent = cloudPoolActive.transform;

        var leftRight = Random.Range(0, 2);
        var origin = mainCamera.ViewportToWorldPoint(new Vector3(leftRight, Random.Range(0f, 1f)));

        if (leftRight == 0)
        {
            logic.direction = -1;
            origin.x -= 2;
        }        
        else
        {
            logic.direction = 1;
        }
        origin.y += cloudYOffset;
        if (pixelSnap)
        {
            origin.x = Utils.Math.RoundToPixel(origin.x, pixelsPerUnit);
            origin.y = Utils.Math.RoundToPixel(origin.y, pixelsPerUnit);
        }
        origin.z = cloudPoolActive.transform.position.z;
        newDec.transform.position = origin;
        logic.desiredPos = origin;
    }
}
