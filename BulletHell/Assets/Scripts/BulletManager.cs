using UnityEngine;
using TMPro;

public class BulletManager : MonoBehaviour
{
    public static BulletManager Instance;

    public int activeBullets = 0;
    public TextMeshProUGUI bulletCounterText;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    
    void Update()
    {
        bulletCounterText.text = "Bullets: " + activeBullets;
    }

    public void AddBullet()
    {
        activeBullets++;
    }

    public void RemoveBullet()
    {
        activeBullets--;
    }
}
