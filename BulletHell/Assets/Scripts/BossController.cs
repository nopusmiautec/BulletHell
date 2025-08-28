using UnityEngine;

/// <summary>
/// Controls the boss attack patterns and manages bullet firing logic.
/// The boss alternates between circular, spiral, and side wave bullet patterns.
/// </summary>
public class BossController : MonoBehaviour
{
    public GameObject Bullet;
    public Transform bulletCenter;
    public Transform bulletLeft;
    public Transform bulletRight;

    public float fireRate;
    private float fireTimer;
    private float patternTimer;
    private int currentPattern = 0; // 0 = circular, 1 = espiral, 2 = oleadas

    private float spiralAngle = 0f;
    private Transform player;
    /// <summary>
    /// Initializes the boss by finding the player and setting the initial fire rate.
    /// </summary>
    void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
            player = playerObj.transform;

        SetFireRateByPattern();
    }
    /// <summary>
    /// Updates timers, switches patterns, and fires bullets when ready.
    /// </summary>
    void Update()
    {
        patternTimer += Time.deltaTime;
        if (patternTimer >= 10f)
        {
            patternTimer = 0f;
            currentPattern = (currentPattern + 1) % 3;
            SetFireRateByPattern();
        }
        fireTimer += Time.deltaTime;
        if (fireTimer >= fireRate)
        {
            fireTimer = 0f;

            switch (currentPattern)
            {
                case 0:
                    FireCircularPattern();
                    break;
                case 1:
                    FireSpiralPattern();
                    break;
                case 2:
                    FireSideWavesPattern();
                    break;
            }
        }
    }
/// <summary>
/// Sets the firing rate based on the current attack pattern.
/// </summary>
    void SetFireRateByPattern()
    {
        if (currentPattern == 0)
            fireRate = 1.5f;

        else if (currentPattern == 2)
            fireRate = 0.5f;
        
        else
            fireRate = 0.2f;
    }
/// <summary>
/// Fires bullets outward in a circular pattern.
/// </summary>
    void FireCircularPattern()
    {
        int bulletCount = 20;
        float angleStep = 360f / bulletCount;

        for (int i = 0; i < bulletCount; i++)
        {
            float angle = i * angleStep * Mathf.Deg2Rad;
            Vector2 dir = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));

            GameObject bullet = Instantiate(Bullet, bulletCenter.position, Quaternion.identity);
            Bullet b = bullet.GetComponent<Bullet>();
            b.SetDirection(dir);
            b.speed = 300f;
        }
    }
/// <summary>
/// Fires bullets in a spiral pattern, incrementing the angle each shot.
/// </summary>
    void FireSpiralPattern()
    {
        spiralAngle += 15f;
        float rad = spiralAngle * Mathf.Deg2Rad;
        Vector2 dir = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad));

        GameObject bullet = Instantiate(Bullet, bulletCenter.position, Quaternion.identity);
        Bullet b = bullet.GetComponent<Bullet>();
        b.SetDirection(dir);
        b.speed = 700f;
    }
/// <summary>
/// Fires two bullets from the left and right points, directed at the player.
/// </summary>
    void FireSideWavesPattern()
    {
        if (player == null) return;

        Vector2 dirLeft = (player.position - bulletLeft.position).normalized;
        Vector2 dirRight = (player.position - bulletRight.position).normalized;

        GameObject bulletL = Instantiate(Bullet, bulletLeft.position, Quaternion.identity);
        Bullet bL = bulletL.GetComponent<Bullet>();
        bL.SetDirection(dirLeft);
        bL.speed = 500f;

        GameObject bulletR = Instantiate(Bullet, bulletRight.position, Quaternion.identity);
        Bullet bR = bulletR.GetComponent<Bullet>();
        bR.SetDirection(dirRight);
        bR.speed = 500f;
    }
}
