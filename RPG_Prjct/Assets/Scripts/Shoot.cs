using UnityEngine;
using TMPro;

public class Shoot : MonoBehaviour
{
    [SerializeField] private PoolObjectScript poolObjectScript; // Ссылка на пул объектов
    [SerializeField] private TextMeshProUGUI ammoText; // TextMeshPro элемент для отображения патронов
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

        if (Input.GetMouseButtonDown(1) && _currentAmmo > 0) // Правая кнопка мыши
        {
            ShootBullet();
        }

        if (Input.GetKeyDown(KeyCode.R) || _currentAmmo <= 0) // Клавиша R или отсутствие патронов
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
        poolObjectScript.ReloadPool(); // Возврат всех пуль в пул
        UpdateAmmoUI();
        _isReloading = false;
    }

    private void UpdateAmmoUI()
    {
        ammoText.text = $"Ammo: {_currentAmmo}/{_maxAmmo}";
    }
}