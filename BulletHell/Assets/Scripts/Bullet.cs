using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 5f;
    private Vector2 direction;

    public void SetDirection(Vector2 dir)
    {
        direction = dir.normalized;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        BulletManager.Instance.AddBullet();
    }

    // Update is called once per frame
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

    private void OnDestroy()
    {
        if (BulletManager.Instance != null)
            BulletManager.Instance.RemoveBullet();
    }
}
