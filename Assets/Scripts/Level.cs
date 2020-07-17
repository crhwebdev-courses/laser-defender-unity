using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    [SerializeField] private string _gameStartScene = "Game Start Scene";
    [SerializeField] private string _gameScene = "Game";
    [SerializeField] private string _gameOverScene = "Game Over Scene";

    public void LoadStartMenu()
    {
        SceneManager.LoadScene(_gameStartScene);
    }

    public void LoadGameScene()
    {
        SceneManager.LoadScene(_gameScene);
    }

    public void LoadGameOver()
    {
        SceneManager.LoadScene(_gameOverScene);
    }
            
    public void QuitGame()
    {
        Application.Quit();
    }
}
