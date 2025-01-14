using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float knockbackForce = 5f; 
    public float moveSpeed = 5f; 
    public float rotationSpeed = 720f; 
    public float jumpForce = 5f; 
    public LayerMask groundLayer; 
    public float health = 100f;

    public Slider healthBarSlider;
    public Gamemanager gamemanager;
    public Animator animator; 

    private Rigidbody rb;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }

        if (animator == null)
        {
            Debug.LogError("Animator bileşeni bulunamadı! Lütfen bir Animator bileşeni atayın.");
        }

        if (gamemanager == null)
        {
            Debug.LogError("Gamemanager referansı eksik! Lütfen Inspector'da atayın.");
        }

        if (healthBarSlider == null)
        {
            Debug.LogError("Health Bar Slider referansı eksik! Lütfen Inspector'da atayın.");
        }

        UpdateHealthBar();
    }

    void Update()
    {
        HandleMovement();
        HandleRunningAnimation();
        HandleJumping();
    }

    private void HandleMovement()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 moveDirection = (transform.right * moveX + transform.forward * moveZ).normalized;
        Vector3 velocity = moveDirection * moveSpeed;
        velocity.y = rb.velocity.y;

        rb.velocity = velocity;

        if (moveDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

    private void HandleRunningAnimation()
    {
        if (animator == null) return;

        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");
        bool isMoving = Mathf.Abs(moveX) > 0.1f || Mathf.Abs(moveZ) > 0.1f;

        animator.SetBool("isRunning", isMoving);
    }

    private void HandleJumping()
    {
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

        Vector3 knockbackDirection = (transform.position - other.transform.position).normalized;
        rb.AddForce(knockbackDirection * knockbackForce, ForceMode.Impulse);
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
            Destroy(gameObject);
            gamemanager?.Youlose();
        }

        UpdateHealthBar();
    }

    private void UpdateHealthBar()
    {
        if (healthBarSlider != null)
        {
            healthBarSlider.value = health / 1f;
        }
    }
}
