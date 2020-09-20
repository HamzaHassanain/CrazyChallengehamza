using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using Cinemachine;

public class GameMenuManager : MonoBehaviour
{
    [SerializeField] GameObject PuseMenu;
    [SerializeField] int LastSceneIndex;
    // Start is called before the first frame update
  
   private bool isPused;

    public void Restart()
    {
         Debug.Log("Restart");
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);   
    
    }
    public void Resume() {
        Time.timeScale = 1;
        isPused = false;
        PuseMenu.SetActive(false);
    }
    public void Exit() {
        Application.Quit();
    }
    
    public void LoadNextScene()
    {
        if(SceneManager.GetActiveScene().buildIndex  == LastSceneIndex)
            SceneManager.LoadScene("MainMenu");
        else 
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }
    public void LoadNextScene(string name) {
        SceneManager.LoadScene(name);
    }

     

}
