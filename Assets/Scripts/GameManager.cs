using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using Cinemachine;
public class GameManager : MonoBehaviour
{
    [SerializeField]  GameObject PuseMenu;
    [SerializeField] GameObject WinMenu;
    [SerializeField] GameObject DieMenu;

    // [SerializeField] int ScenesCount;
    [SerializeField] int LastSceneIndex;

    // Start is called before the first frame update
    private GameObject camera;
  
   private bool isPused;

   private void Start() {
       isPused = false;

       
        Time.timeScale = 1;
        PuseMenu.SetActive(false);
        WinMenu.SetActive(false);
        DieMenu.SetActive(false);

       camera = GameObject.Find("CM vcam1");

        camera.GetComponent<CinemachineVirtualCamera>().Follow = GameObject.Find("Player").transform;
   }
  private void Update() {
       if(Input.GetKeyDown(KeyCode.Escape)) {
           if(isPused) {
                    Time.timeScale = 1;
                    isPused = false;
                    PuseMenu.SetActive(false);
           } else {
                    Time.timeScale = 0;
                    isPused = true;
                    PuseMenu.SetActive(true);
           }
       }
   }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            Win();
        }
    }
    
   
    private void Win() {
        Time.timeScale = 0;
        isPused = true;
        WinMenu.SetActive(true);
    }
    public void Die() {
         StartCoroutine(DieCorotine());
    }
    public void Restart()
    {
        StartCoroutine(RestartCorotine());
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
        if(SceneManager.GetActiveScene().buildIndex == LastSceneIndex)
            SceneManager.LoadScene("MainMenu");
        else 
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }
    public void LoadNextScene(string name) {
        SceneManager.LoadScene(name);
    }

     IEnumerator RestartCorotine()
    {
        yield return new WaitForSeconds(1f);
        Debug.Log("Restart");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }
    IEnumerator DieCorotine()
    {
        yield return new WaitForSeconds(1f);
        Time.timeScale = 0;
        isPused = true;
        DieMenu.SetActive(true);

    }
}
