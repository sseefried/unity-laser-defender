using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Level : MonoBehaviour
{
    [SerializeField] float loadDelay = 2f; // seconds

    public void LoadStartMenu()
    {
        SceneManager.LoadScene(0); // only works if it is first scene
    }

    public void LoadGame()
    {
        // String references aren't great since you could change name of level
        SceneManager.LoadScene("Game");
        FindObjectOfType<GameSession>().ResetGame();
    }

    public void LoadGameOver()
    {
        StartCoroutine(WaitAndLoad());
    }
     
    public void QuitGame()
    {
        Application.Quit();
    }

    private IEnumerator WaitAndLoad()
    {
        yield return new WaitForSeconds(loadDelay);
        SceneManager.LoadScene("Game Over");
    }
}
