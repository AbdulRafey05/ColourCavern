using UnityEngine;
using UnityEngine.UI; 
public class GameManager : MonoBehaviour
{
    public float timeLimit = 60f; 
    public GameObject gameOverPanel; 
    public Text timerText; 

    private float timeRemaining;
    private bool isGameOver = false;
  

    private void Start()
    {
       
        timeRemaining = timeLimit;
        UpdateTimerText();

        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false); 
        }
    }

    private void Update()
    {
        if (isGameOver) return;

       
        timeRemaining -= Time.deltaTime;

        if (timeRemaining <= 0)
        {
            timeRemaining = 0;
            EndGame();
        }

        
        UpdateTimerText();
    }

    private void UpdateTimerText()
    {
        if (timerText != null)
        {
            timerText.text = $"Time: {Mathf.Max(timeRemaining, 0):0}";
        }
    }

    private void EndGame()
    {
        isGameOver = true;

        
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }
    }
}
