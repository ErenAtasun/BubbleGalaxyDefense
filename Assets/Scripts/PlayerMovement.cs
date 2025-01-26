using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Karakterin hareket hýzý
    public float dashSpeed = 10f; // Dash sýrasýnda hýz
    public float dashDuration = 0.2f; // Dash süresi
    public float dashCooldown = 1f; // Dash sonrasý bekleme süresi
    public bool canDash = true; // Dash özelliðini açýp kapatmak için
    


    private bool isDashing = false;
    private bool isCooldown = false;
    private float dashTime = 0f;
    private float cooldownTime = 0f;


    private Animator animator; // Animator bileþeni

    void Start()
    {
        // Animator bileþenini al
        animator = GetComponent<Animator>();

        if (animator == null)
        {
            Debug.LogError("Animator component is missing on the player object.");
        }
    }

    void Update()
    {
        // Kullanýcýdan hareket giriþlerini alma (WASD veya ok tuþlarý)
        float moveX = Input.GetAxisRaw("Horizontal"); // Sað-Sol hareketi
        float moveY = Input.GetAxisRaw("Vertical");   // Yukarý-Aþaðý hareketi

        // Hareket vektörünü oluþtur
        Vector2 moveInput = new Vector2(moveX, moveY).normalized; // Normalize ederek hýz sabit tutulur

        if (animator != null)
        {
            bool isMoving = moveInput != Vector2.zero;
            animator.SetBool("isMovingRight", moveX > 0); // Sað hareket
            animator.SetBool("isMovingLeft", moveX < 0);  // Sol hareket
            animator.SetBool("isMovingUp", moveY > 0);    // Yukarý hareket
            animator.SetBool("isMovingDown", moveY < 0);  // Aþaðý hareket
            animator.SetBool("isIdle", !isMoving && !isDashing); // Karakter duruyorsa
            animator.SetBool("isDashing", isDashing); // Dash sýrasýnda
            animator.SetBool("isPressingSpace", Input.GetKey(KeyCode.Space)); // Space tuþuna basýlýp basýlmadýðýný kontrol et
        }


        // Dash kontrolü
        if (canDash && !isCooldown && Input.GetKeyDown(KeyCode.Space) && !isDashing)
        {
            isDashing = true;
            dashTime = dashDuration;
            if (animator != null)
            {
                animator.SetTrigger("Dash"); // Dash animasyonu için trigger
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

        // Cooldown devam ederken normal hareketi engellemeden iþleyiþi saðla
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
}


