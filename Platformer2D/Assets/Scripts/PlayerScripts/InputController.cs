using UnityEngine;
// ReSharper disable All
public class InputController : MonoBehaviour
{
    private float _horizontalInput;
    
    public delegate void HorizontalInputAction();
    public static HorizontalInputAction _horizontalInputAction;
    public delegate void VerticalInputAction();
    public static VerticalInputAction _verticalInputAction;
    void Update()
    {
        _horizontalInput = Input.GetAxis("Horizontal");
        if (_horizontalInput != 0)
        {
            _horizontalInputAction?.Invoke(); 
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            _verticalInputAction?.Invoke();
        }
    }
}