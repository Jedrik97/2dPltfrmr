using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSaver : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) 
        {
            SaveGame();
            SceneManager.LoadScene("StartMenu");
        }
    }

    public void SaveGame()
    {
        
        PlayerPrefs.SetString("SavedScene", SceneManager.GetActiveScene().name);
        
        Vector3 playerPosition = transform.position;
        PlayerPrefs.SetFloat("PlayerX", playerPosition.x);
        PlayerPrefs.SetFloat("PlayerY", playerPosition.y);
        PlayerPrefs.SetFloat("PlayerZ", playerPosition.z);
    }

    public void LoadGame()
    {
        float x = PlayerPrefs.GetFloat("PlayerX", 0);
        float y = PlayerPrefs.GetFloat("PlayerY", 0);
        float z = PlayerPrefs.GetFloat("PlayerZ", 0);

        transform.position = new Vector3(x, y, z);
    }
}