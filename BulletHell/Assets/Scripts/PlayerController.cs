using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float normalSpeed = 800f;
    public float slowSpeed = 300f;
    public GameObject pBullet;
    public float bulletSpeed = 400f;
    public float fireRate = 0.15f;
    private float fireCooldown = 0f;
    private Rigidbody2D rb;
    private Vector2 moveInput;
    public Transform firePos;
    private float xMAX, xMIN, yMAX, yMIN;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        CalculateScreenBounds();
    }

    void CalculateScreenBounds()
    {
        var camera = Camera.main;
        var hCamera = camera.orthographicSize;
        var wCamera = camera.orthographicSize * camera.aspect;
        var cameraPos = camera.transform.position;

        var spriteSize = GetComponent<SpriteRenderer>().bounds.size.x * 0.5f;

        yMIN = cameraPos.y - hCamera + spriteSize;
        yMAX = cameraPos.y + hCamera - spriteSize;
        xMIN = cameraPos.x - wCamera + spriteSize;
        xMAX = cameraPos.x + wCamera - spriteSize;
    }

    void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        moveInput = new Vector2(moveX, moveY).normalized;

        // --- Logica para disparos ---
        fireCooldown -= Time.deltaTime;
        if (Input.GetKey(KeyCode.Z) && fireCooldown <= 0f) // Z dispara
        {
            Shoot();
            fireCooldown = fireRate;
        }
    }

    void FixedUpdate()
    {
        float currentSpeed = Input.GetKey(KeyCode.LeftShift) ? slowSpeed : normalSpeed;
        Vector2 newVelocity = moveInput * currentSpeed;
        rb.linearVelocity = newVelocity;
        LimitPositionToScreen();
    }

    void LimitPositionToScreen()
    {
        Vector3 clampedPosition = transform.position;
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, xMIN, xMAX);
        clampedPosition.y = Mathf.Clamp(clampedPosition.y, yMIN, yMAX);

        if (transform.position != clampedPosition)
        {
            transform.position = clampedPosition;
            rb.linearVelocity = Vector2.zero; // Detener movimiento al chocar con límite
        } 
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(pBullet, firePos.position, Quaternion.identity);
        Bullet bulletScript = bullet.GetComponent<Bullet>();
        bulletScript.SetDirection(Vector2.up);
        bulletScript.speed = bulletSpeed;
    }
}
