using UnityEngine;

public class FoodSpawner : MonoBehaviour
{
    public GameObject foodPrefab;
    public float spawnInterval = 1.5f;
    public float spawnRangeX = 10f;
    public float spawnRangeY = 6f;
    private float timer = 0f;
    private Player player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = FindFirstObjectByType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.GetStage() > 0)
    {
        return;
    }

        timer += Time.deltaTime;

    if (timer >= spawnInterval)
    {
        timer = 0f;
        SpawnFood();
    }

    }

    void SpawnFood()
    {
        float randomX = Random.Range(-spawnRangeX, spawnRangeX);
        float randomY = Random.Range(-spawnRangeY, spawnRangeY);

        Vector3 spawnPosition = transform.position + new Vector3(randomX, randomY, 0f);

        Instantiate(foodPrefab, spawnPosition, Quaternion.identity);

    }   
}
