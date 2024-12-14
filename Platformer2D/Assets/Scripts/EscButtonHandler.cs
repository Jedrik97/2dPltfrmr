using UnityEngine;
using UnityEngine.SceneManagement;

public class EscButtonHandler : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;

    public void SaveGame()
    {
        PlayerPrefs.SetString("SavedScene", SceneManager.GetActiveScene().name);
        
        PlayerHealthController healthController = FindAnyObjectByType<PlayerHealthController>();
        if (healthController != null)
        {
            PlayerPrefs.SetInt("PlayerHealth", healthController.GetCurrentHealth());
        }
        
        if (gameManager != null)
        {
            PlayerPrefs.SetInt("CollectedGems", gameManager.GetGemCount());
            PlayerPrefs.SetInt("HasKey", gameManager.HasKey() ? 1 : 0);
        }
        
        PlayerMovement playerMovement = FindAnyObjectByType<PlayerMovement>();
        if (playerMovement != null)
        {
            PlayerPrefs.SetFloat("PlayerSpeed", playerMovement.GetCurrentSpeed());
        }

        PlayerPrefs.Save();
        Debug.Log("Game progress saved!");
    }

    public void OnEscButtonClick()
    {
        SaveGame();
        SceneManager.LoadScene("StartMenu");
    }
}