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

    public float bitePower = 1f;

    public GameObject upgradePanel;

    public float maxHealth = 100f;
    public float currentHealth;
    public UnityEngine.UI.Slider healthBar;

    public GameObject gameOverPanel;

    void Start()
{
    rb = GetComponent<Rigidbody2D>();
    spriteRenderer = GetComponent<SpriteRenderer>();
    spriteRenderer.sprite = fishSprite;
    currentHealth = maxHealth;
    healthBar.value = currentHealth;
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

    upgradePanel.SetActive(true);
    Time.timeScale = 0f;
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

void UpgradeSpeed()
{
    moveSpeed += 1f;
}

public void ChooseSpeedUpgrade()
{
    UpgradeSpeed();
    CloseUpgradePanel();
}

public void ChooseSizeUpgrade()
{
    UpgradeSize();
    CloseUpgradePanel();
}


void UpgradeSize()
{
    transform.localScale += new Vector3(0.15f, 0.15f, 0f);
}

void UpgradeBite()
{
    bitePower += 0.5f;
}

public void ChooseBiteUpgrade()
{
    UpgradeBite();
    CloseUpgradePanel();
}

void CloseUpgradePanel()
{
    upgradePanel.SetActive(false);
    Time.timeScale = 1f;
}

public void TakeDamage(float amount)
{
    currentHealth -= amount;
    healthBar.value = currentHealth;

    if (currentHealth <= 0f)
    {
        currentHealth = 0f;
        Die();
        
    }
}

void Die()
{
    gameOverPanel.SetActive(true);
    Time.timeScale = 0f;
}

public void RespawnButtonPressed()
{
    Time.timeScale = 1f;
    UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
}

    
}
