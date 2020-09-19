using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenu : MonoBehaviour
{


    public void LoadScene(int i) {
        SceneManager.LoadScene(i);
    } 
    public void Exit() {
      Application.Quit();
  }
}
