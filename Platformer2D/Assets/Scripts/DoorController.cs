using UnityEngine;

public class DoorController : MonoBehaviour
{
    [Header("Sprites")]
    [SerializeField] private Sprite openDoorSprite;
    [SerializeField] private SpriteRenderer doorSpriteRenderer;

    [SerializeField] private GameManager gameManager;

    private bool isOpen = false;

    private void Start()
    {
        gameManager.OnKeyCollectedAction = OpenDoor;
    }

    private void OpenDoor()
    {
        if (!isOpen)
        {
            isOpen = true;
            doorSpriteRenderer.sprite = openDoorSprite;
            Debug.Log("Door opened!");
        }
    }
}