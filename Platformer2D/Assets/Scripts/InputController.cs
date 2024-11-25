using UnityEngine;
public class InputController : MonoBehaviour
{
    private float _horizontalInput;
    public delegate void HorizontalInputAction(float direction);
    public static HorizontalInputAction _horizontalInputAction;
    void Update()
    {
        _horizontalInput = Input.GetAxis("Horizontal");
        if (_horizontalInput != 0)
        {
            _horizontalInputAction?.Invoke(_horizontalInput); 
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Player.Instance.Jump();
        }
    }
}