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

    public float attackInterval = 1f;
    private float nextAttackTime = 0f;

    public float borderX = 50f;
    public float borderY = 50f;

    public float preyHealAmount = 15f;
    public float predatorHealAmount = 25f;

    public int GetStage()
    {
    return stage;
    }


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
    Vector2 moveDirection = new Vector2(moveX, moveY).normalized;
    rb.linearVelocity = moveDirection * moveSpeed;

    if (moveDirection != Vector2.zero)
    {
        if (moveDirection != Vector2.zero)
{
    float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
    rb.rotation = angle;

    if (angle > 90f || angle < -90f)
    {
        spriteRenderer.flipY = true;
    }
    else
    {
        spriteRenderer.flipY = false;
    }
}
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

void OnTriggerEnter2D(Collider2D other)
{
    if (other.CompareTag("Food"))
    {
        Destroy(other.gameObject);
        AddFood(1);
    }
}

void LevelUp()
{
    level++;
    foodEaten = 0;
    foodToNextLevel = 5 + (level - 1);
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

void OnTriggerStay2D(Collider2D other)
{
    if (Time.time < nextAttackTime)
    {
        return;
    }

    if (other.CompareTag("Prey"))
    {
        Destroy(other.gameObject);
        AddFood(3);
        Heal(preyHealAmount);
        nextAttackTime = Time.time + attackInterval;
    }
    else if (other.CompareTag("Predator"))
{
    Predator predatorScript = other.GetComponent<Predator>();

    if (stage >= predatorScript.requiredPlayerStage)
    {
        predatorScript.TakeDamage(bitePower);
        nextAttackTime = Time.time + attackInterval;

        if (predatorScript.IsDead())
        {
            Heal(predatorHealAmount);
            AddFood(8);
            Destroy(other.gameObject);
        }
    }
}
}

void AddFood(int amount)
{
    foodEaten += amount;

    if (foodEaten >= foodToNextLevel)
    {
        LevelUp();
    }
}

public void Heal(float amount)
{
    currentHealth += amount;

    if (currentHealth > maxHealth)
    {
        currentHealth = maxHealth;
    }

    healthBar.value = currentHealth;
}




    
}
