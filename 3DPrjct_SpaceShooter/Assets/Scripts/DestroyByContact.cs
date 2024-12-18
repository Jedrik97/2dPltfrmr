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

      if (_explosion != null)
      {
         Instantiate(_explosion, transform.position, transform.rotation);
      }

      if (other.tag == "Player")
      {
         Instantiate(_playerExplosion, other.transform.position, other.transform.rotation);
      }
      Destroy(other.gameObject);
      Destroy(gameObject);
   }
}
