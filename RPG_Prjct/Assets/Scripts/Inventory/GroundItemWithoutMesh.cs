using UnityEditor;
using UnityEngine;

public class GroundItemWithOutMesh : MonoBehaviour, ISerializationCallbackReceiver
{
    internal ItemsObject item;

public void OnBeforeSerialize()
{
    GetComponentInChildren<SpriteRenderer>().sprite = item.Icon;
    EditorUtility.SetDirty(GetComponentInChildren<SpriteRenderer>());
}

    public void OnAfterDeserialize()
    {
        
    }
}
