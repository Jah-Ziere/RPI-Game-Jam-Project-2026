using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private float moveX;
    private float moveY;

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
    
}
