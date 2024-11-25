using UnityEngine;

public class FlipController : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public void FlipX(float direction)
    {
        if (direction != 0)
        {
            spriteRenderer.flipX = direction < 0;
        }
    }
}