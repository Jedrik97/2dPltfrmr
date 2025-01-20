using UnityEngine;

public class GroundItem : MonoBehaviour
{
    [SerializeField] private ItemsObject _item;

    public Item CreateItem()
    {
        return _item.CreateItem();
    }
}
