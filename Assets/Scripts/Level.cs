using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    [SerializeField] private int _gameStartScene = 0;
    [SerializeField] private string _gameScene = "Game";
    [SerializeField] private string _gameOverScene = "Game Over";

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
