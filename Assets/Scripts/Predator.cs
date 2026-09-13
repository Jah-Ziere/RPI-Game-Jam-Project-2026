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

    public float attackInterval = 1f;
    private float nextAttackTime = 0f;

    public float borderX = 20f;
    public float borderY = 12f;

    public int requiredPlayerStage = 1;

    private SpriteRenderer spriteRenderer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        PickNewWanderDirection();
        currentHealth = maxHealth;
        spriteRenderer = GetComponent<SpriteRenderer>();
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
    Vector2 currentVelocity;

    if (distanceToPlayer <= detectionRange)
    {
        Vector2 directionToPlayer = (player.position - transform.position).normalized;
        currentVelocity = directionToPlayer * chaseSpeed;
    }
    else
    {
        currentVelocity = wanderDirection * wanderSpeed;
    }

    rb.linearVelocity = currentVelocity;

    if (currentVelocity != Vector2.zero)
    {
        float angle = Mathf.Atan2(currentVelocity.y, currentVelocity.x) * Mathf.Rad2Deg;
        rb.rotation = angle;
        spriteRenderer.flipY = (angle > 90f || angle < -90f);
    }

    Vector3 pos = transform.position;

    if (pos.x > borderX)
    {
        pos.x = -borderX;
    }
    else if (pos.x < -borderX)
    {
        pos.x = borderX;
    }

    if (pos.y > borderY)
    {
        pos.y = -borderY;
    }
    else if (pos.y < -borderY)
    {
        pos.y = borderY;
    }

    transform.position = pos;
}

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player") && Time.time >= nextAttackTime)
        {
            Player playerScript = other.GetComponent<Player>();
            playerScript.TakeDamage(damageAmount);
            nextAttackTime = Time.time + attackInterval;
        }
        
    }

    public bool IsDead()
{
    return currentHealth <= 0f;
}
}
