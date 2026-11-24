using UnityEngine;

public class FlyMonster : MonoBehaviour
{
    public Transform player; // Reference to the player
    public float moveSpeed = 3f; // Speed of the fly monster
    public float attackRange = 5f; // Range within which the monster will attack
    public float attackCooldown = 2f; // Time between attacks
    public float damageAmount = 10f; // Amount of damage to inflict on the player
    public float flyRange = 10f; // Maximum range the monster can fly away from its initial position

    private bool canAttack = true; // To control the attack cooldown
    private Vector3 initialPosition; // To store the starting position of the monster

    private void Start()
    {
        // Store the initial position of the monster
        initialPosition = transform.position;
    }

    private void Update()
    {
        if (player != null)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);
            float distanceFromStart = Vector3.Distance(transform.position, initialPosition);

            // Check if the player is within the attack range and the monster is within its fly range
            if (distanceToPlayer <= attackRange && distanceFromStart <= flyRange)
            {
                FollowPlayer();
                if (canAttack)
                {
                    AttackPlayer();
                }
            }
            else if (distanceFromStart > flyRange)
            {
                // If the monster is out of its fly range, return to the initial position
                ReturnToInitialPosition();
            }
        }
    }

    private void FollowPlayer()
    {
        // Move towards the player if the monster is within its fly range
        Vector3 direction = (player.position - transform.position).normalized;
        transform.position += direction * moveSpeed * Time.deltaTime;
    }

    private void ReturnToInitialPosition()
    {
        // Move back to the initial position when out of the fly range
        Vector3 direction = (initialPosition - transform.position).normalized;

        if (Vector3.Distance(transform.position, initialPosition) > 0.1f) // If not at the initial position
        {
            transform.position += direction * moveSpeed * Time.deltaTime;
        }
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
}
