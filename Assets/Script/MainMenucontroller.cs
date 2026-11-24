using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    public Button playButton;
    public Button exitButton;
    public Button instructionsButton;
    public Button playerSelectionButton;
    public Button nextButton; // Reference for the Next button
    public Button level1Button; // Reference for the Level 1 button
    public Button level2Button; // Reference for the Level 2 button
    public Button level3Button; // Reference for the Level 3 button
    public Button levelPlayButton; // Reference for the Level Play button
    public Button levelThreeButton; // Another Level 3 button, as requested

    private void Start()
    {
        // Button listeners
        if (playButton != null)
            playButton.onClick.AddListener(OnPlayButtonClicked);

        if (exitButton != null)
            exitButton.onClick.AddListener(OnExitButtonClicked);

        if (instructionsButton != null)
            instructionsButton.onClick.AddListener(OnInstructionsButtonClicked);

        if (playerSelectionButton != null)
            playerSelectionButton.onClick.AddListener(OnPlayerSelectionButtonClicked);

        if (nextButton != null)
            nextButton.onClick.AddListener(OnNextButtonClicked);

        if (level1Button != null)
            level1Button.onClick.AddListener(OnLevel1ButtonClicked); // Listener for Level 1 button

        if (level2Button != null)
            level2Button.onClick.AddListener(OnLevel2ButtonClicked);

        if (level3Button != null)
            level3Button.onClick.AddListener(OnLevel3ButtonClicked);

        if (levelPlayButton != null)
            levelPlayButton.onClick.AddListener(OnLevelPlayButtonClicked); // Listener for Level Play button

        if (levelThreeButton != null)
            levelThreeButton.onClick.AddListener(OnLevelThreeButtonClicked); // Listener for the second Level 3 button
    }

    private void OnPlayButtonClicked()
    {
        // Load the game scene (replace "GameScene" with your actual scene name)
        SceneManager.LoadScene("button instruction");
    }

    private void OnExitButtonClicked()
    {
        // Quit the application
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // Stop play mode in the editor
#endif
    }

    private void OnInstructionsButtonClicked()
    {
        // Load the instructions scene (replace "InstructionsScene" with your actual scene name)
        SceneManager.LoadScene("InstructionsScene");
    }

    private void OnPlayerSelectionButtonClicked()
    {
        // Load the player selection scene (replace "PlayerSelectionScene" with your actual scene name)
        SceneManager.LoadScene("Level 2 instruction");
    }

    private void OnNextButtonClicked()
    {
        // Load the InstructionScene
        SceneManager.LoadScene("Level one instruction 1");
    }

    private void OnLevel1ButtonClicked()
    {
        // Load the Level 1 scene
        SceneManager.LoadScene("Level 1"); // Replace with the actual name of your Level 1 scene
    }

    private void OnLevel2ButtonClicked()
    {
        // Load the Level 2 scene
        SceneManager.LoadScene("Level 2"); // Replace with the actual name of your Level 2 scene
    }

    private void OnLevel3ButtonClicked()
    {
        // Load the Level 3 scene
        SceneManager.LoadScene("PlayerSElection"); // Replace with the actual name of your Level 3 scene
    }

    private void OnLevelPlayButtonClicked()
    {
        // Load the specific level scene (replace "SpecificLevelScene" with your actual level scene name)
        SceneManager.LoadScene("Level 3 instruction 1"); // Update with your actual level scene name
    }

    private void OnLevelThreeButtonClicked()
    {
        // Load another Level 3 scene, as requested
        SceneManager.LoadScene("LEVEL3"); // Replace with the actual name of your Level 3 scene
    }
}
