using UnityEngine;

public class Prey : MonoBehaviour
{

    public float wanderSpeed = 1f;
    private Rigidbody2D rb;
    private Vector2 wanderDirection;
    private float wanderTimer;

    public float borderX = 20f;
    public float borderY = 12f;

    private SpriteRenderer spriteRenderer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        PickNewWanderDirection();
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

    void FixedUpdate()
    {
        rb.linearVelocity = wanderDirection * wanderSpeed;

        if (wanderDirection != Vector2.zero)
    {
        float angle = Mathf.Atan2(wanderDirection.y, wanderDirection.x) * Mathf.Rad2Deg;
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

    void PickNewWanderDirection()
    {
        float randomX = Random.Range(-1f, 1f);
        float randomY = Random.Range(-1f, 1f);
        wanderDirection = new Vector2(randomX, randomY).normalized;
        wanderTimer = 2f;
    }
}
