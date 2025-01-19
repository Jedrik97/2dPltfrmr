using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    [SerializeField] private PoolObjectScript poolObjectScript; 
    private int _currentAmmo;
    private int _maxAmmo = 32;
    private bool _isReloading = false;

    [SerializeField] private float _fireRate = 1f; 
    private float _nextFireTime = 0f;

    private Transform player; 
    private bool _playerInSight = false; 

    [SerializeField] private FieldOfView fieldOfView; 

    private void Start()
    {
        _currentAmmo = _maxAmmo;
    }

    private void Update()
    {
        if (_isReloading) return;

        
        if (fieldOfView._targets.Count > 0)
        {
            player = fieldOfView._targets[0]; 
            _playerInSight = true;
        }
        else
        {
            _playerInSight = false;
        }

        
        if (_playerInSight && _currentAmmo > 0 && Time.time >= _nextFireTime)
        {
            ShootAtPlayer();
        }

        
        if (Input.GetKeyDown(KeyCode.R) || _currentAmmo <= 0)
        {
            StartCoroutine(Reload());
        }
    }

    private void ShootAtPlayer()
    {
        if (poolObjectScript.Shoot())
        {
            _currentAmmo--;
            _nextFireTime = Time.time + _fireRate; 
        }
    }

    private System.Collections.IEnumerator Reload()
    {
        _isReloading = true;
        yield return new WaitForSeconds(1.5f); 
        _currentAmmo = _maxAmmo;
        poolObjectScript.ReloadPool();
        _isReloading = false;
    }
}
