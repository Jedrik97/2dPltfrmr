using UnityEngine;

public class SpeedGemTrigger : MonoBehaviour
{
    [SerializeField] private PlayerMovement playerMovement;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerMovement.IncreaseSpeed(1f); 
            Destroy(gameObject);
        }
    }
}