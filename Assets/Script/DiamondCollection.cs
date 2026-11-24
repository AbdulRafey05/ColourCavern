using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DiamondCollection : MonoBehaviour
{
    public Animator doorAnimator; // Reference to the door's Animator
    public GameObject winPanel; // Reference to the win panel UI
    public Text diamondText; // UI text for displaying the diamond count
    public int diamondCount = 0; // Initial diamond count
    public int requiredDiamondsForDoor = 4; // Diamonds required to open the door
    public int requiredDiamondsForWin = 5; // Diamonds required to win the game
    public float winPanelDelay = 3f; // Delay before showing the win panel

    private AudioManager audioManager;

    private bool doorOpened = false; // Track if the door has already been opened

    private void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        UpdateDiamondText();
        if (winPanel != null)
        {
            winPanel.SetActive(false); // Make sure the win panel is initially hidden
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Assuming diamonds have a tag "Diamond"
        if (other.CompareTag("Diamond"))
        {
            if (audioManager != null)
            {
                audioManager.PlayDiamondCollectSound();
            }
            diamondCount++;
            UpdateDiamondText();

            // Open the door when player collects 4 diamonds
            if (diamondCount == requiredDiamondsForDoor && !doorOpened)
            {
                OpenDoor(); // Trigger the door opening animation
            }

            // Show the win panel when player collects 5 diamonds
            if (diamondCount == requiredDiamondsForWin)
            {
                StartCoroutine(ShowWinPanelAfterDelay(winPanelDelay)); // Show the win panel after a delay
            }

            Destroy(other.gameObject); // Remove the collected diamond
        }
    }

    private void UpdateDiamondText()
    {
        if (diamondText != null)
        {
            diamondText.text = $"Diamonds: {diamondCount}"; // Update the UI to reflect the diamond count
        }
    }

    private void OpenDoor()
    {
        doorOpened = true; // Mark the door as opened
        if (doorAnimator != null)
        {
            doorAnimator.SetTrigger("OpenDoor"); // Trigger the door opening animation
        }
    }

    private IEnumerator ShowWinPanelAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // Wait for the specified delay
        if (winPanel != null)
        {
            winPanel.SetActive(true); // Display the win panel
        }
    }
}
