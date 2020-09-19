using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] int uselessScenes;
   [SerializeField] int ScenesCount;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            LoadNextScene();

        }
    }
   
    public void Restart()
    {
        StartCoroutine(RestartCorotine());
    }
    IEnumerator RestartCorotine()
    {
        yield return new WaitForSeconds(1f);
        Debug.Log("Restart");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }
    void LoadNextScene()
    {
            if(SceneManager.GetActiveScene().buildIndex + 1 == ScenesCount)
                SceneManager.LoadScene("MainMenu");
            else 
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }

}
