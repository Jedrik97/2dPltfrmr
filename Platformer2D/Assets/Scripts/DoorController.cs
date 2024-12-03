using UnityEngine;

public class DoorController : MonoBehaviour
{
    [Header("Sprites")]
    [SerializeField] private Sprite closedDoorSprite; 
    [SerializeField] private Sprite openDoorSprite;   
    [SerializeField] private SpriteRenderer doorSpriteRenderer; 

    private bool isOpen = false;

    private void OnEnable()
    {
        
        GameKeyManager.OnKeyCollected += OpenDoor;
    }

    private void OnDisable()
    {
        
        GameKeyManager.OnKeyCollected -= OpenDoor;
    }

    private void OpenDoor()
    {
        if (!isOpen)
        {
            isOpen = true;
            doorSpriteRenderer.sprite = openDoorSprite; 
            Debug.Log("Door opened automatically!");
        }
    }
}