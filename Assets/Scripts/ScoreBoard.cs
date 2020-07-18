using UnityEngine;

public class ScoreBoard : MonoBehaviour
{        
    public int Score { get; set; }
    
    private void Awake()
    {
        SetUpSingleton();        
    }

    private void SetUpSingleton()
    {
        if (FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public void Reset()
    {
        Score = 0;
    }
}
