using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CoinCollection2 : MonoBehaviour
{
    public Text scoreText;
    public int score = 0;

    private AudioManager audioManager;
    // Reference to the win panel UI
    public GameObject winPanel;

    // Make the win score public to be adjustable
    public int winScore = 25; // Score required to win the game

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

            // Check if the player has winScore coins to trigger the win panel
            if (score >= winScore)
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
        yield return new WaitForSeconds(delay); // Wait for the specified delay
        if (winPanel != null)
        {
            winPanel.SetActive(true); // Display the win panel after the delay
        }
    }
}
