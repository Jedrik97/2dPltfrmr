using UnityEngine;

public class HpGemTrigger : MonoBehaviour
{
    [SerializeField] private PlayerHealthController healthController;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            healthController.Heal(1); 
            Destroy(gameObject);
        }
    }
}