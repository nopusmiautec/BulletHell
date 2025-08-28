using UnityEngine;
/// <summary>
/// Represents a bullet projectile in the game.
/// Handles its movement, lifetime, and interaction with the BulletManager.
/// </summary>
public class Bullet : MonoBehaviour
{
    public float speed = 5f;
    private Vector2 direction;

    public void SetDirection(Vector2 dir)
    {
        direction = dir.normalized;
    }
    /// <summary>
    /// Called when the bullet is first created.
    /// Registers the bullet in the BulletManager.
    /// </summary>
    void Start()
    {
        BulletManager.Instance.AddBullet();
    }

    /// <summary>
    /// Called once per frame.
    /// Moves the bullet in its set direction and destroys it if it leaves the camera viewport.
    /// </summary>
    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);

        // Si sale de la pantalla se destruye el proyectil
        Vector2 pos = Camera.main.WorldToViewportPoint(transform.position);
        if (pos.x < 0 || pos.x > 1 || pos.y < 0 || pos.y > 1)
        {
            Destroy(gameObject);
        }
    }
    /// <summary>
    /// Called when the bullet is destroyed.
    /// Unregisters the bullet from the BulletManager.
    /// </summary>
    private void OnDestroy()
    {
        if (BulletManager.Instance != null)
            BulletManager.Instance.RemoveBullet();
    }
}
