using UnityEngine;
using TMPro;

/// <summary>
/// Manages all active bullets in the game.
/// Tracks the number of active bullets and updates the UI counter.
/// </summary>
public class BulletManager : MonoBehaviour
{
    public static BulletManager Instance;

    public int activeBullets = 0;
    public TextMeshProUGUI bulletCounterText;

    /// <summary>
    /// Ensures only one instance of the manager exists (singleton pattern).
    /// Destroys duplicate objects if necessary.
    /// </summary>
    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    /// <summary>
    /// Updates the bullet counter text each frame.
    /// </summary>
    void Update()
    {
        bulletCounterText.text = "Bullets: " + activeBullets;
    }
    /// <summary>
    /// Increments the active bullet count.
    /// Called when a new bullet is created.
    /// </summary>
    public void AddBullet()
    {
        activeBullets++;
    }
    /// <summary>
    /// Decrements the active bullet count.
    /// Called when a bullet is destroyed.
    /// </summary>
    public void RemoveBullet()
    {
        activeBullets--;
    }
}
