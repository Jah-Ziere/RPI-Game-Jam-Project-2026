using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private float moveX;
    private float moveY;

    public int foodEaten = 0;
    public int foodToNextLevel = 5;
    public int level = 1;

    void Start()
{
    rb = GetComponent<Rigidbody2D>();
}

void Update()
{
 moveX = Input.GetAxisRaw("Horizontal");
 moveY = Input.GetAxisRaw("Vertical");
}

void FixedUpdate()
{
    Vector2 moveDirection = new Vector2(moveX, moveY);
    rb.linearVelocity = moveDirection * moveSpeed;
}

void OnTriggerEnter2D(Collider2D other)
{
    if (other.CompareTag("Food"))
    {
        Destroy(other.gameObject);
        foodEaten++;

        if (foodEaten >= foodToNextLevel)
        {
            LevelUp();
        }
    }
}

void LevelUp()
{
    level++;
    foodEaten = 0;
    transform.localScale += new Vector3(0.2f, 0.2f, 0f);
}
    
}
