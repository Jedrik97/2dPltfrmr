using UnityEngine;
using TMPro;

public class GameKeyManager : MonoBehaviour
{
    public static GameKeyManager Instance;

    [Header("UI")]
    [SerializeField] private TMP_Text gemText; 
    [SerializeField] private TMP_Text keyText;

    public static event System.Action OnKeyCollected; 

    private int gemCount = 0;
    private bool hasKey = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddGem()
    {
        gemCount++;
        UpdateGemUI();
    }

    public void CollectKey()
    {
        hasKey = true;
        UpdateKeyUI();
        OnKeyCollected?.Invoke(); 
    }

    public bool PlayerHasKey()
    {
        return hasKey;
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