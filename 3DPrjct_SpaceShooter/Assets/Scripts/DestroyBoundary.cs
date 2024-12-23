using System;
using UnityEngine;

public class DestroyBoundary : MonoBehaviour
{
    [SerializeField] private PlayerController _playerController;
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Bolt"))
        {
            ReturnBoltToPool(other.gameObject);
        }
        else
        {
            Destroy(other.gameObject); 
        }
    }

  
    private void ReturnBoltToPool(GameObject bolt)
    {
        if (_playerController != null)
        {
            _playerController.ReturnBoltToPool(bolt);
        }
        else
        {
            Destroy(bolt);
        }
    }
}