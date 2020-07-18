using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    [SerializeField] private int _gameStartScene = 0;
    [SerializeField] private string _gameScene = "Game";
    [SerializeField] private string _gameOverScene = "Game Over";
    [SerializeField] private float _gameOverDelayOnPlayerDeath = 2f;

    public void LoadStartMenu()
    {
        SceneManager.LoadScene(_gameStartScene);
    }

    public void LoadGameScene()
    {
        SceneManager.LoadScene(_gameScene);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void LoadGameOver()
    {
        StartCoroutine(DelayedLoadGameOver());
    }

    private IEnumerator DelayedLoadGameOver()
    {
        yield return new WaitForSeconds(_gameOverDelayOnPlayerDeath);
        SceneManager.LoadScene(_gameOverScene);
    }
            
    
}
