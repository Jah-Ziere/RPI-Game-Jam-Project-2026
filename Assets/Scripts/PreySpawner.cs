using UnityEngine;

public class PreySpawner : MonoBehaviour
{

    public GameObject[] preyPrefabs;
    public float spawnInterval = 2f;
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
            SpawnPrey();
        }

        void SpawnPrey()
    {
        int stage = player.GetStage();

        if (stage >= preyPrefabs.Length)
        {
            stage = preyPrefabs.Length - 1;
        }
        GameObject prefabToSpawn = preyPrefabs[stage];

        float randomX = Random.Range(-spawnRangeX, spawnRangeX);
        float randomY = Random.Range(-spawnRangeY, spawnRangeY);
        Vector3 spawnPosition = transform.position + new Vector3(randomX, randomY, 0f);

        Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity);
    }
    }
}
