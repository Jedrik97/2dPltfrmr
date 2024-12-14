using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("Lvl1"); 
    }

    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Game Closed!"); 
    }

    public void LoadGame()
    {
        if (PlayerPrefs.HasKey("SavedScene"))
        {
            string savedScene = PlayerPrefs.GetString("SavedScene");
            SceneManager.LoadScene(savedScene);
        }
    }
}