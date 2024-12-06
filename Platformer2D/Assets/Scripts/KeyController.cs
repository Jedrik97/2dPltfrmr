using UnityEngine;

public class KeyController : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            gameManager.CollectKey();
            Destroy(gameObject);
        }
    }
}