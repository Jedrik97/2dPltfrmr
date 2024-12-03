using UnityEngine;

public class KeyController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameKeyManager.Instance.CollectKey();
            Destroy(gameObject);
        }
    }
}