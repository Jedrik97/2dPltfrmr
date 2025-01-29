using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;

public class DisplayInventory : MonoBehaviour
{
    [SerializeField] private GameObject _inventorySellPref;
    [SerializeField] private InventoryObject _inventorySO;
    public int X_START;
    public int Y_START;
    public int NUMBER_OF_COLUMN;
    public int X_SPACE_ITEM;
    public int Y_SPACE_ITEM;

    private Dictionary<GameObject, InventoryObject.InventorySlot> _itemsDisplayed;
    private Canvas _parentCanvas;
    private MouseItem _mouseItem;

    private void Start()
    {
        _mouseItem = new MouseItem();
        _parentCanvas = transform.parent.GetComponent<Canvas>();
    }

    private void OnEnable()
    {
        CreateSlot();
    }

    private void Update()
    {
        UpdateSlot();
    }

    

    private void OnDisable()
    {
         DestroySlot();
    }

  
    private void CreateSlot()
    {
       _itemsDisplayed = new Dictionary<GameObject, InventoryObject.InventorySlot>();
       for (int i = 0; i < _inventorySO.Container.Items.Length; i++)
       {
           var cellObj = Instantiate(_inventorySellPref, Vector3.zero, transform.rotation, transform);
           cellObj.GetComponent<RectTransform>().localPosition = Vector3.zero;
           cellObj.GetComponent<RectTransform>().anchoredPosition = GetPosition(i);
           
           AddEvent(cellObj, EventTriggerType.PointerEnter, delegate { OnEnter(cellObj); });
           AddEvent(cellObj, EventTriggerType.PointerExit, delegate {OnExit(cellObj); });
           AddEvent(cellObj, EventTriggerType.BeginDrag, delegate {OnBeginDrag(cellObj); });
           AddEvent(cellObj, EventTriggerType.Drag, delegate { OnDrag(cellObj); });
           AddEvent(cellObj, EventTriggerType.EndDrag, delegate { OnDragEnd(cellObj); });
           
           
           SetSlotData(cellObj, _inventorySO.Container.Items[i]);
           
           _itemsDisplayed.Add(cellObj, _inventorySO.Container.Items[i]);
       }
    }

    private void OnDragEnd(GameObject cellObj)
    {
        if (_mouseItem.hoverObj)
        {
            _inventorySO.MoveItem(_itemsDisplayed[cellObj], _itemsDisplayed[_mouseItem.hoverObj]);
        }
        else
        {
            _inventorySO.RemoveItem(_itemsDisplayed[cellObj].item);
        }
        Destroy(_mouseItem.draggedObj);
        _mouseItem.hoverObj = null;
    }

    private void OnDrag(GameObject cellObj)
    {
        if (_mouseItem.draggedObj != null)
        {
           /* RectTransformUtility.ScreenPointToWorldPointInRectangle(_parentCanvas.transform as RectTransform,
                Input.mousePosition,
                Camera.main, 
                out Vector3 position);
           // _mouseItem.draggedObj.transform.position = _parentCanvas.transform.TransformPoint(position);
            _mouseItem.draggedObj.GetComponent<RectTransform>().position = Input.mousePosition;*/
           RectTransform rt = _mouseItem.hoverObj.GetComponent<RectTransform>();
           rt.position = Input.mousePosition;   
        }
    }

    private void OnBeginDrag(GameObject cellObj)
    {
        var mouseObject = new GameObject();
        var rt = mouseObject.AddComponent<RectTransform>();
        rt.sizeDelta = new Vector2(50, 50);
        mouseObject.transform.SetParent(transform.parent);
        if (_itemsDisplayed[cellObj].SlotID >= 0) 
        {
            var img = mouseObject.GetComponent<Image>();
            img.sprite = _inventorySO.database.GetItem[_itemsDisplayed[cellObj].item.Id].Icon; 
            img.raycastTarget = false;
        }
        
        _mouseItem.hoverObj = mouseObject;
        _mouseItem.draggedSlot = _itemsDisplayed[cellObj];

    }

    private void UpdateSlot()
    {
        foreach (KeyValuePair<GameObject, InventoryObject.InventorySlot> _slot in _itemsDisplayed)
        {
            if (_slot.Value.SlotID >= 0)
            {
                _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().sprite = 
                    _inventorySO.database.GetItem[_slot.Value.item.Id].Icon;
                _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 1);
                _slot.Key.transform.GetChild(0).GetComponentInChildren<TextMeshProUGUI>().text = _slot.Value.amount == 1 ? "" : _slot.Value.amount.ToString("n0");
            }
            else
            {
                _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().sprite = null;
                _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 0);
                _slot.Key.transform.GetChild(1).GetComponentInChildren<TextMeshProUGUI>().text = "";
            }
        }
    }

    private void SetSlotData(GameObject cellObj, InventoryObject.InventorySlot slot)
    {
        var imageSprite = cellObj.transform.GetChild(0).GetComponentInChildren<Image>();
        imageSprite.enabled = !slot.IsEmpty;
        imageSprite.sprite = slot.IsEmpty ? null : _inventorySO.GetSpriteByItemId(slot.item.Id);
        cellObj.transform.GetChild(0).GetComponentInChildren<TextMeshProUGUI>().text = slot.amount == 1 ? "" : slot.amount.ToString("n0");
    }

    private void OnExit(GameObject cellobj)
    {
        cellobj.transform.GetComponent<Image>().color = new Color(0.27f, 0.27f, 0.27f, 1f);
        
                     
    }

    private void OnEnter(GameObject cellObj)
    {
        cellObj.transform.GetComponent<Image>().color = new Color(0, 0, 0, 0.5f); 
        _mouseItem.hoverObj = cellObj;
    }

    private void AddEvent(GameObject target, EventTriggerType triggerType, UnityAction<BaseEventData> action)
    {
        EventTrigger trigger =  target.GetComponent<EventTrigger>();
        var eventTrigger = new EventTrigger.Entry();
        eventTrigger.eventID = triggerType;
        eventTrigger.callback.AddListener(action);
        trigger.triggers.Add(eventTrigger);
    }
    private Vector3 GetPosition(int index)
    {
        float x = X_START + X_SPACE_ITEM * (index % NUMBER_OF_COLUMN);
        float y = -(Y_START + Y_SPACE_ITEM * (index / NUMBER_OF_COLUMN));
        return new Vector3(x, y, 0);
    }
}

private void DestroySlot()
{
        
}


public class MouseItem
{
    public GameObject hoverObj;
    public GameObject draggedObj;
    public InventoryObject.InventorySlot draggedSlot;

}

