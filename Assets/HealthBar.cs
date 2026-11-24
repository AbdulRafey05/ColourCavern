using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider healthSlider; // Reference to the UI Slider
    public float maxHealth = 100f; // Maximum health
    private float currentHealth; // Current health

    public float damageFromCoin = 10f; // How much damage the red coin does

    private AudioManager audioManager;
    private void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        currentHealth = maxHealth; // Initialize current health to max health
        UpdateHealthBar(); // Update the health bar UI
    }

    // Method to take damage
    public void TakeDamage(float amount)
    {
        if (audioManager != null)
        {
            audioManager.PlayHurtSound();
        }
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // Clamp to prevent negative health
        UpdateHealthBar(); // Update the health bar UI

        if (currentHealth <= 0)
        {
            Die(); // Call die method if health reaches 0
        }
    }

    // Method to heal
    public void Heal(float amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // Clamp to max health
        UpdateHealthBar(); // Update the health bar UI
    }

    // Update the health bar UI
    private void UpdateHealthBar()
    {
        if (healthSlider != null)
        {
            healthSlider.value = currentHealth / maxHealth; // Set the slider value based on current health
        }
    }

    // Detect collision with red coin
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collision detected with: " + other.gameObject.name); // This will print the name of the object the player collides with

        if (other.CompareTag("RedCoin"))
        {
            Debug.Log("Hit by red coin! Health reduced.");
            TakeDamage(damageFromCoin);
            Destroy(other.gameObject);
        }
    }

    // Method to handle player death
    private void Die()
    {
        Debug.Log("Player has died!");

        // Disable player controls (assuming you have a PlayerMovement script)
        PlayerMovement playerController = GetComponent<PlayerMovement>();
        if (playerController != null)
        {
            playerController.enabled = false; // Disable player controls
        }

        // Play death animation (if you have one)
        Animator animator = GetComponent<Animator>();
        if (animator != null)
        {
            animator.SetTrigger("Die"); // Assuming you have a 'Die' trigger in your animator
        }

        // Optionally, you could trigger a game over screen or respawn
        // ShowGameOverScreen(); // Uncomment if you have a method to show game over

        // Disable the health bar or set it inactive
        if (healthSlider != null)
        {
            healthSlider.gameObject.SetActive(false);
        }

        // Optionally, invoke any additional death logic here
    }
}
