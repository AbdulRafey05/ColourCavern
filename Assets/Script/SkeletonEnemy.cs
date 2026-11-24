using UnityEngine;

public class SkeletonEnemy : MonoBehaviour
{
    public Transform player; // Reference to the player
    public float moveSpeed = 2f; // Speed of the skeleton
    public float detectionRange = 8f; // Range within which the skeleton starts running towards the player
    public float attackRange = 1.5f; // Range within which the skeleton will attack
    public float attackCooldown = 2f; // Time between attacks
    public float damageAmount = 20f; // Amount of damage to inflict on the player
    public float maxHealth = 100f; // Maximum health of the skeleton

    private float currentHealth; // Current health of the skeleton
    private bool canAttack = true; // To control the attack cooldown
    private Animator animator; // Reference to the Animator
    private bool isDead = false; // To track if the enemy is dead

    private void Start()
    {
        // Initialize health
        currentHealth = maxHealth;

        // Get the Animator component
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (isDead) return; // Stop behavior if enemy is dead

        if (player != null)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);

            if (distanceToPlayer <= attackRange)
            {
                // If the player is within the attack range, trigger the kill animation
                animator.SetTrigger("Kill");

                if (canAttack)
                {
                    AttackPlayer();
                }
            }
            else if (distanceToPlayer <= detectionRange)
            {
                // If the player is within detection range but not attack range, run towards the player
                FollowPlayer();
                animator.SetBool("isRunning", true); // Set running animation

                // Stop the kill animation since player is not in attack range
                animator.ResetTrigger("Kill");
            }
            else
            {
                // Player is out of range, stop enemy and go idle
                IdleEnemy();
            }
        }
    }

    private void FollowPlayer()
    {
        // Move towards the player
        Vector3 direction = (player.position - transform.position).normalized;
        transform.position += direction * moveSpeed * Time.deltaTime;
    }

    private void AttackPlayer()
    {
        // Assuming you have a HealthBar script on the player
        HealthBar healthBar = player.GetComponent<HealthBar>();
        if (healthBar != null)
        {
            healthBar.TakeDamage(damageAmount); // Deal damage to the player
            canAttack = false; // Start cooldown
            StartCoroutine(AttackCooldownRoutine());
        }
    }

    private System.Collections.IEnumerator AttackCooldownRoutine()
    {
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true; // Reset attack ability
    }

    // Method to handle enemy taking damage from player attacks
    public void TakeDamage(float damage)
    {
        if (isDead) return; // If already dead, do nothing

        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        isDead = true; // Mark enemy as dead
        animator.SetTrigger("Die"); // Trigger death animation

        // Optionally, disable the enemy after a longer delay
        // StartCoroutine(DisableAfterDeath());
    }

    // Handle idle state when player is out of detection range
    private void IdleEnemy()
    {
        animator.SetBool("isRunning", false); // Stop running animation
        animator.ResetTrigger("Kill"); // Stop kill animation if it was triggered
    }

    // This method detects if the player attacks the enemy
    private void OnTriggerEnter(Collider other)
    {
        // Assuming the player's weapon or attack has a tag "PlayerAttack"
        if (other.CompareTag("PlayerAttack"))
        {
            TakeDamage(50f); // Deal 50 damage to the skeleton when hit by the player's attack
        }
    }
}
