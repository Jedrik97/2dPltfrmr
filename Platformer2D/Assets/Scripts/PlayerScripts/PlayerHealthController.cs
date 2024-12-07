using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthController : MonoBehaviour
{
    [Header("Health Settings")]
    [SerializeField] private RawImage[] healthImages;
    [SerializeField] private Texture2D fullHeartTexture;
    [SerializeField] private Texture2D halfHeartTexture;
    [SerializeField] private Texture2D emptyHeartTexture;

    [Header("Player Parameters")]
    [SerializeField] private PlayerMovement playerMovement; 

    private int maxHearts = 3;
    private int maxHealthPerHeart = 2;
    private int currentHealth;

    private void Start()
    {
        currentHealth = 2;
        UpdateHealthUI();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Max(currentHealth, 0);
        UpdateHealthUI();

        if (currentHealth <= 0)
        {
            Debug.Log("Game Over");
        }

        ResetPlayerSpeed();
    }

    public void Heal(int healAmount)
    {
        currentHealth += healAmount;
        currentHealth = Mathf.Min(currentHealth, maxHearts * maxHealthPerHeart); 
        UpdateHealthUI();
    }

    private void UpdateHealthUI()
    {
        for (int i = 0; i < healthImages.Length; i++)
        {
            int heartHealth = Mathf.Clamp(currentHealth - (i * maxHealthPerHeart), 0, maxHealthPerHeart);

            if (heartHealth == 2)
            {
                healthImages[i].texture = fullHeartTexture;
            }
            else if (heartHealth == 1)
            {
                healthImages[i].texture = halfHeartTexture;
            }
            else
            {
                healthImages[i].texture = emptyHeartTexture;
            }
        }
    }

    private void ResetPlayerSpeed()
    {
        if (playerMovement != null)
        {
            playerMovement.ResetSpeedToDefault();
        }
    }
}
