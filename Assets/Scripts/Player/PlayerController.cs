using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float knockbackForce = 5f; 
    public float moveSpeed = 5f; // Speed of player movement
    public float rotationSpeed = 720f; // Speed of rotation
    public float jumpForce = 5f; // Jump force
    public LayerMask groundLayer; // Layer mask to detect ground
    public float health = 100f;
    private Rigidbody rb;
    private bool isGrounded;
    public Gamemanager gamemanager;

    void Start()
    {
        // Get the Rigidbody component
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Handle movement
     float moveX = Input.GetAxis("Horizontal"); // A/D or Left/Right arrows
        float moveZ = Input.GetAxis("Vertical"); // W/S or Up/Down arrows

        Vector3 moveDirection = (transform.right * moveX + transform.forward * moveZ).normalized;
        Vector3 velocity = moveDirection * moveSpeed;

        velocity.y = rb.velocity.y; // Keep the current vertical velocity (gravity, jumping)

        rb.velocity = velocity;

        // Rotate the player toward the movement direction
        if (moveDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        // Handle jumping
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("enemy"))
        {
            TakeDamage();
            ApplyKnockback(other);
        }

        if (other.gameObject.CompareTag("ground"))
        {
            isGrounded = true;
        }
        
    }
    private void ApplyKnockback(Collision other)
    {
        if (rb == null) return;

        // Calculate knockback direction
        Vector3 knockbackDirection = (transform.position - other.transform.position).normalized;
        // knockbackDirection.y = 0.1f; // Add a vertical component if needed

        // Apply the knockback force
        rb.AddForce(knockbackDirection * knockbackForce, ForceMode.Force);
    }
    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("ground"))
        {
            isGrounded = false;
        }
        
    }

    

    private void TakeDamage()
    {
        health -= 10f;
        if (health <= 0)
        {
            // lose game
            Destroy(gameObject);
            gamemanager.Youlose();
        }
        
    }
}

