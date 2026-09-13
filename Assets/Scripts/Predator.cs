using UnityEngine;

public class Predator : MonoBehaviour
{
    public float wanderSpeed = 1.5f;
    public float chaseSpeed = 3f;
    public float detectionRange = 4f;
    public float damageAmount = 10f;
    private Transform player;
    private Rigidbody2D rb;
    private Vector2 wanderDirection;
    private float wanderTimer;

    public float maxHealth = 20f;
    private float currentHealth;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        PickNewWanderDirection();
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        wanderTimer -= Time.deltaTime;

        if (wanderTimer <= 0f)
        {
            PickNewWanderDirection();
        }
        
    }

    public void TakeDamage(float amount)
    {

    currentHealth -= amount;

        if (currentHealth <= 0f)
        {
            Destroy(gameObject);
        }

    }

    void PickNewWanderDirection()
    {

        float randomX = Random.Range(-1f, 1f);
        float randomY = Random.Range(-1f, 1f);
        wanderDirection = new Vector2(randomX, randomY).normalized;
        wanderTimer = 2f;
    }

    void FixedUpdate()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= detectionRange)
        {
            Vector2 directionToPlayer = (player.position - transform.position).normalized;
            rb.linearVelocity = directionToPlayer * chaseSpeed;
        }
        else
        {
            rb.linearVelocity = wanderDirection * wanderSpeed;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
{
    if (other.CompareTag("Player"))
    {
        Player playerScript = other.GetComponent<Player>();
        playerScript.TakeDamage(damageAmount);
    }
}
}
