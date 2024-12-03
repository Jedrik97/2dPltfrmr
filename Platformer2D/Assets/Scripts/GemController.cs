using UnityEngine;

public class GemController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameKeyManager.Instance.AddGem();
            Destroy(gameObject);
        }
    }
}