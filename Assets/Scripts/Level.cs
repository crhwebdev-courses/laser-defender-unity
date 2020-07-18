using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private int _scorePerEnemyKill = 60;
    [SerializeField] private int _gameStartScene = 0;
    [SerializeField] private string _gameScene = "Game";
    [SerializeField] private string _gameOverScene = "Game Over";
    [SerializeField] private float _gameOverDelayOnPlayerDeath = 2f;

    private int _score;

    public void IncrementScore()
    {
        IncrementScore(_scorePerEnemyKill);
    }

    public void IncrementScore(int score)
    {
        _score += score;
        _scoreText.text = _score.ToString();
    }

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
