using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private int sceneIndex;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void LoadGameWon()
    {
        StartCoroutine(DelayThenLoadGameFinishedScene("GameWon"));
    }

    public void LoadGameOver()
    {
        StartCoroutine(DelayThenLoadGameFinishedScene("GameOver"));
    }

    public void LoadGameScene()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    IEnumerator DelayThenLoadGameFinishedScene(string sceneName)
    {
        //Time.timeScale = 0;
        yield return new WaitForSeconds(2);
        //Time.timeScale = 1;
        SceneManager.LoadScene(sceneName);
    }
}
