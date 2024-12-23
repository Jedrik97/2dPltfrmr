using System;
using UnityEngine;

public class DestroyByContact : MonoBehaviour
{
    [SerializeField] private GameObject _playerExplosion; 
    [SerializeField] private GameObject _explosion;
    
    public System.Action<GameObject> OnReturnToPool;

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
            
            OnReturnToPool?.Invoke(gameObject); 
        }
    }
}