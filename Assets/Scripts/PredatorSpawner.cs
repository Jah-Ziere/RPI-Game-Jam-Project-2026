using UnityEngine;

public class PredatorSpawner : MonoBehaviour
{
    public GameObject[] predatorPrefabs;
    public float spawnInterval = 3f;
    public float spawnRangeX = 10f;
    public float spawnRangeY = 6f;
    private float timer;
    private Player player;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = FindFirstObjectByType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            timer = 0f;
            SpawnPredator();
        }
    }

    void SpawnPredator()
    {
        int stage = player.GetStage();
        int maxIndex = Mathf.Min(stage, predatorPrefabs.Length - 1);
        int randomIndex = Random.Range(0, maxIndex + 1);

        GameObject prefabToSpawn = predatorPrefabs[randomIndex];

        float randomX = Random.Range(-spawnRangeX, spawnRangeX);
        float randomY = Random.Range(-spawnRangeY, spawnRangeY);
        Vector3 spawnPosition = transform.position + new Vector3(randomX, randomY, 0f);

        Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity);
    }
}
