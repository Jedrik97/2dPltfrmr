using UnityEngine;

public class GroundRayVisualizer : MonoBehaviour
{
    [SerializeField] private float groundCheckRadius = 0.7f;
    [SerializeField] private LayerMask groundMask;

    private void Update()
    {
        DebugGroundRay();
    }
    private void DebugGroundRay()
    {
       Debug.DrawRay(transform.position, Vector2.down * groundCheckRadius, Color.red);
    }
}