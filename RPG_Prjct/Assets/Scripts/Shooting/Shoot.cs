using UnityEngine;
using TMPro;

public class Shoot : MonoBehaviour
{
    [SerializeField] private PoolObjectScript poolObjectScript; 
    [SerializeField] private TextMeshProUGUI ammoText; 
    private int _currentAmmo;
    private int _maxAmmo = 32;
    private bool _isReloading = false;

    private void Start()
    {
        _currentAmmo = _maxAmmo;
        UpdateAmmoUI();
    }

    private void Update()
    {
        if (_isReloading) return;

        if (Input.GetMouseButtonDown(1) && _currentAmmo > 0) 
        {
            ShootBullet();
        }

        if (Input.GetKeyDown(KeyCode.R) || _currentAmmo <= 0) 
        {
            StartCoroutine(Reload());
        }
    }

    private void ShootBullet()
    {
        if (poolObjectScript.Shoot())
        {
            _currentAmmo--;
            UpdateAmmoUI();
        }
    }

    private System.Collections.IEnumerator Reload()
    {
        _isReloading = true;
        ammoText.text = "Reloading...";
        yield return new WaitForSeconds(1.5f);
        _currentAmmo = _maxAmmo;
        poolObjectScript.ReloadPool();
        UpdateAmmoUI();
        _isReloading = false;
    }

    private void UpdateAmmoUI()
    {
        ammoText.text = $"Ammo: {_currentAmmo}/{_maxAmmo}";
    }
}