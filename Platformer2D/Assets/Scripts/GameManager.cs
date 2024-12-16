using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private TMP_Text gemText;
    [SerializeField] private TMP_Text keyText;

    private int gemCount = 0;
    private bool hasKey = false;
    
    public System.Action OnGemCollectedAction;
    public System.Action OnKeyCollectedAction;

    public void AddGem()
    {
        gemCount++;
        UpdateGemUI();
        OnGemCollectedAction?.Invoke(); 
    }

    public void CollectKey()
    {
        hasKey = true;
        UpdateKeyUI();
        OnKeyCollectedAction?.Invoke();
    }

    private void UpdateGemUI()
    {
        gemText.text = $"Gems: {gemCount}";
    }

    private void UpdateKeyUI()
    {
        keyText.text = $"Key: {(hasKey ? "Yes" : "No")}";
    }
}