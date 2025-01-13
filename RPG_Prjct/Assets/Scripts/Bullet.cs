using UnityEngine;
using System.Collections.Generic;

public class Bullet : MonoBehaviour
{
    private PoolObjectScript _pool;
    private List<LayerMask> _targetMasks;
    private float _shootingLength;
    private Vector3 _startPosition;

    [SerializeField] private GameObject collisionEffectPrefab; // Префаб эффекта при столкновении

    public void Initialize(PoolObjectScript pool, List<LayerMask> targetMasks, float shootingLength)
    {
        _pool = pool;
        _targetMasks = targetMasks;
        _shootingLength = shootingLength;
    }

    private void OnEnable()
    {
        _startPosition = transform.position;
    }

    private void Update()
    {
        if (Vector3.Distance(_startPosition, transform.position) >= _shootingLength)
        {
            _pool.ReturnToPool(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Создаем эффект на месте столкновения
        if (collisionEffectPrefab != null)
        {
            GameObject effect = Instantiate(collisionEffectPrefab, collision.contacts[0].point, Quaternion.identity);
            Destroy(effect, 1f); // Уничтожить эффект через 1 секунду
        }

        // Проверяем, является ли объект целью
        foreach (LayerMask mask in _targetMasks)
        {
            if (((1 << collision.gameObject.layer) & mask) != 0)
            {
                Destroy(collision.gameObject);
                break;
            }
        }

        // Возвращаем пулю в пул
        _pool.ReturnToPool(gameObject);
    }

}