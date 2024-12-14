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

    private void Start()
    {
        // Загружаем данные из сохранения
        gemCount = PlayerPrefs.GetInt("CollectedGems", 0);
        hasKey = PlayerPrefs.GetInt("HasKey", 0) == 1;

        UpdateGemUI();
        UpdateKeyUI();
    }

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

    public int GetGemCount() => gemCount;

    public void SetGemCount(int count)
    {
        gemCount = count;
        UpdateGemUI();
    }

    public bool HasKey() => hasKey;

    public void SetKey(bool key)
    {
        hasKey = key;
        UpdateKeyUI();
    }
}