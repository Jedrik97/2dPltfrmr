using UnityEngine;

public class DestroyByContact : MonoBehaviour
{
    [SerializeField] private GameObject _playerExplosion; 
    [SerializeField] private GameObject _explosion;      

    private void OnTriggerEnter(Collider other)
    {
        
        if (CompareTag("Bolt") && other.CompareTag("Player"))
        {
            return; 
        }
        
        if (CompareTag("Bolt") && other.CompareTag("Enemy"))
        {
            if (_explosion != null)
            {
                Instantiate(_explosion, other.transform.position, other.transform.rotation);
            }
            Destroy(other.gameObject);
            ReturnBoltToPool(gameObject);
        }
    }
    private void ReturnBoltToPool(GameObject bolt)
    {
        PlayerController playerController = FindObjectOfType<PlayerController>(); 
        if (playerController != null)
        {
            playerController.ReturnBoltToPool(bolt); 
        }
        else
        {
            Destroy(bolt);
        }
    }
}