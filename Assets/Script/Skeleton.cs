using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyMovementAndAttack : MonoBehaviour
{
    public Transform patrolPoint; // The point where the enemy will patrol to
    public Transform player; // The player transform
    public float detectionRange = 10f; // Distance at which the enemy detects the player
    public float attackRange = 2f; // Distance at which the enemy will attack
    public float moveSpeed = 3f; // Speed of the enemy
    public float attackCooldown = 1.5f; // Time between attacks

    private bool isAttacking = false;
    private float attackTimer = 0f;

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= attackRange)
        {
            // If within attack range, attack the player
            AttackPlayer();
        }
        else if (distanceToPlayer <= detectionRange)
        {
            // If the player is within detection range, chase the player
            ChasePlayer();
        }
        else
        {
            // If the player is out of detection range, patrol to the specified point
            PatrolToPoint();
        }
    }

    void PatrolToPoint()
    {
        // Move towards the patrol point
        Vector3 direction = (patrolPoint.position - transform.position).normalized;
        transform.position += direction * moveSpeed * Time.deltaTime;

        // Rotate to face the patrol point
        if (direction != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), 0.2f);
        }
    }

    void ChasePlayer()
    {
        // Move towards the player
        Vector3 direction = (player.position - transform.position).normalized;
        transform.position += direction * moveSpeed * Time.deltaTime;

        // Rotate to face the player
        if (direction != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), 0.2f);
        }
    }

    void AttackPlayer()
    {
        if (!isAttacking)
        {
            isAttacking = true;
            attackTimer = attackCooldown;
            Debug.Log("Enemy attacks the player!"); // Here, you can add your own attack logic or animation
        }

        // Handle attack cooldown
        if (isAttacking)
        {
            attackTimer -= Time.deltaTime;
            if (attackTimer <= 0f)
            {
                isAttacking = false;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        // Draw detection and attack range spheres in the editor
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
