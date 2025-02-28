using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float dashSpeed = 10f;
    public float dashDuration = 0.2f;
    public float dashCooldown = 1f;
    public bool canDash = true;

    private bool isDashing = false;
    private bool isCooldown = false;
    private float dashTime = 0f;
    private float cooldownTime = 0f;

    private Animator animator;

    private Vector2 moveDirection;

    private Rigidbody2D playerRigidbody;

    private void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        if (animator == null)
        {
            Debug.LogError("Animator component is missing on the player object.");
        }
    }

    void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        Vector2 moveInput = new Vector2(moveX, moveY).normalized;

        moveDirection = moveInput;


        if (animator != null)
        {
            animator.SetFloat("MoveX", moveDirection.x);
            animator.SetFloat("MoveY", moveDirection.y);
            animator.SetBool("isMoving", moveDirection.magnitude > 0);
            animator.SetBool("isDashing", isDashing);
        }

        if (canDash && !isCooldown && Input.GetKeyDown(KeyCode.Space) && !isDashing)
        {
            isDashing = true;
            dashTime = dashDuration;
            if (animator != null)
            {
                animator.SetTrigger("isDashing");
            }
        }
        if (canDash && !isCooldown && Input.GetKeyDown(KeyCode.Space) && !isDashing)
        {
            isDashing = true;
            dashTime = dashDuration;
            if (animator != null)
            {
                animator.SetTrigger("isDashing"); // Dash animasyonu i�in trigger
            }

        }

        if (isDashing)
        {
            transform.Translate(moveInput * dashSpeed * Time.deltaTime);
            dashTime -= Time.deltaTime;

            if (dashTime <= 0)
            {
                isDashing = false;
                isCooldown = true;
                cooldownTime = dashCooldown;
            }
        }

        // Cooldown devam ederken normal hareketi engellemeden i�leyi�i sa�la
        if (!isDashing)
        {
            if (moveInput != Vector2.zero)
            {
                transform.Translate(moveInput * moveSpeed * Time.deltaTime);
            }
        }

        if (isCooldown)
        {
            cooldownTime -= Time.deltaTime;
            if (cooldownTime <= 0)
            {
                isCooldown = false;
            }
        }
    }
        private void FixedUpdate()
    {
        playerRigidbody.MovePosition(playerRigidbody.position + moveDirection * moveSpeed * Time.fixedDeltaTime);
    }
}
