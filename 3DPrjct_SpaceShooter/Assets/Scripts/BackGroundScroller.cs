using UnityEngine;

public class BackGroundScroller : MonoBehaviour
{
    [SerializeField] private float _scrollSpeed = 5f;
    [SerializeField] private float _backgroundLength = 20f; 
    private Vector3 _startPosition;

    private void Start()
    {
        _startPosition = transform.position;
    }

    private void Update()
    {
        
        transform.position += Vector3.back * _scrollSpeed * Time.deltaTime;
        
        if (transform.position.z <= _startPosition.z - _backgroundLength)
        {
            transform.position = _startPosition;
        }
    }
}