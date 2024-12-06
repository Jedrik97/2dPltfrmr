using UnityEngine;

public class GemController : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            gameManager.AddGem();
            Destroy(gameObject);
        }
    }
}