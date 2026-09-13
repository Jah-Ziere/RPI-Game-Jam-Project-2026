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

    public Sprite fishSprite;
    public Sprite sharkSprite;
    public Sprite megSprite;
    private SpriteRenderer spriteRenderer;
    private int stage = 0;  

    void Start()
{
    rb = GetComponent<Rigidbody2D>();

    spriteRenderer = GetComponent<SpriteRenderer>();
    spriteRenderer.sprite = fishSprite;
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
    foodToNextLevel = 5 + (level - 1);
    transform.localScale += new Vector3(0.2f, 0.2f, 0f);
     CheckStage();
}

void CheckStage()
{
    if (level >= 50 && stage < 2)
    {
        stage = 2;
        spriteRenderer.sprite = megSprite;
    }
    else if (level >= 20 && stage < 1)
    {
        stage = 1;
        spriteRenderer.sprite = sharkSprite;
    }
}
    
}
