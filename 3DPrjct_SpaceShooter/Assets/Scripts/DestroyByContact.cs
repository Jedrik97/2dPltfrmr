using UnityEngine;

public class DestroyByContact : MonoBehaviour
{
    [SerializeField] private GameObject _playerExplosion;
    [SerializeField] private GameObject _explosion; 

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Boundary"))
        {
            return;
        }
        
        if ((CompareTag("Bolt") && other.CompareTag("Player")) ||
            (CompareTag("EnemyBolt") && other.CompareTag("Enemy")))
        {
            return;
        }

     
        if (_explosion != null && !CompareTag("Bolt"))
        {
            Instantiate(_explosion, transform.position, transform.rotation);
        }

        
        if (other.CompareTag("Player"))
        {
            Instantiate(_playerExplosion, other.transform.position, other.transform.rotation);
        }

        
        Destroy(other.gameObject); 
        Destroy(gameObject); 
    }
}