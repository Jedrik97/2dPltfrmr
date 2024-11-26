using UnityEngine;

public class FlipController : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public void FlipX(float velocity)
    {
        if (velocity != 0)
        {
            spriteRenderer.flipX = velocity < 0;
        }
    }
}