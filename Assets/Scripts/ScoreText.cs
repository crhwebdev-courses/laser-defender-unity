using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreText : MonoBehaviour
{
    private TextMeshProUGUI _scoreText;
    private ScoreBoard _scoreBoard;
    
    // Start is called before the first frame update
    void Start()
    {
        _scoreText = GetComponent<TextMeshProUGUI>();
        _scoreBoard = FindObjectOfType<ScoreBoard>();
        Debug.Log("score: " + _scoreBoard.Score.ToString());
    }

    // Update is called once per frame
    void Update()
    {
        _scoreText.text = _scoreBoard.Score.ToString();
    }
}
