using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CoinCollector : MonoBehaviour
{
    public Text scoreText;
    public int score = 0;

    private AudioManager audioManager;

    // Reference to the ChestDemo script
    public ChestDemo chestDemo;

    // Reference to the win panel UI
    public GameObject winPanel;

    private bool chestOpened = false;

    // Make score 18 and score after chest 25 public
    public int chestOpenScore = 18; // Score required to open the chest
    public int winScore = 25;       // Score required to win after chest is opened

    private void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        UpdateScoreText();

        // Ensure the win panel is initially hidden
        if (winPanel != null)
        {
            winPanel.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coin"))
        {
            if (audioManager != null)
            {
                audioManager.PlayDiamondCollectSound();
            }
            score++;
            UpdateScoreText();

            // Check if the score reaches chestOpenScore to trigger the chest animation
            if (score >= chestOpenScore && chestDemo != null && !chestOpened)
            {
                chestDemo.OpenChest(); // Call the method to open the chest
                chestOpened = true; // Mark chest as opened
            }

            // Check if the player has winScore coins after collecting the 7 coins from the chest
            if (chestOpened && score >= winScore)
            {
                StartCoroutine(ShowWinPanelAfterDelay(2f)); // Show the win panel after a 2-second delay
            }

            Destroy(other.gameObject);
        }
    }

    private void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = $"Score: {score}";
        }
    }

    private IEnumerator ShowWinPanelAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); 
        if (winPanel != null)
        {
            winPanel.SetActive(true); 
        }
    }
}

