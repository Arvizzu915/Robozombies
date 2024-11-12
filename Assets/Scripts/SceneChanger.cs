using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneChanger : MonoBehaviour
{
    [SerializeField] string sceneToPlay;
    
    public void ChangeScene()
    { 
        SceneManager.LoadScene(sceneToPlay);
        Time.timeScale = 1.0f;

    }
    public void Retry()
    {
        Time.timeScale = 1.0f;
        Cursor.lockState = CursorLockMode.Locked;
        SceneManager.LoadScene("MainGame");
    }
    public void Leaderboard()
    {
        SceneManager.LoadScene("Leaderboard");
    }
    public void CloseGame()
    { Application.Quit();
       

    } 
}
