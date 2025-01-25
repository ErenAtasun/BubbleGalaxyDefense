using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Karakterin hareket h�z�
    public float dashSpeed = 10f; // Dash s�ras�nda h�z
    public float dashDuration = 0.2f; // Dash s�resi
    public float dashCooldown = 1f; // Dash sonras� bekleme s�resi
    public bool canDash = true; // Dash �zelli�ini a��p kapatmak i�in

    private bool isDashing = false;
    private bool isCooldown = false;
    private float dashTime = 0f;
    private float cooldownTime = 0f;

    void Update()
    {
        // Kullan�c�dan hareket giri�lerini alma (WASD veya ok tu�lar�)
        float moveX = Input.GetAxisRaw("Horizontal"); // Sa�-Sol hareketi
        float moveY = Input.GetAxisRaw("Vertical");   // Yukar�-A�a�� hareketi

        // Hareket vekt�r�n� olu�tur
        Vector2 moveInput = new Vector2(moveX, moveY).normalized; // Normalize ederek h�z sabit tutulur

        // Dash kontrol�
        if (canDash && !isCooldown && Input.GetKeyDown(KeyCode.Space) && !isDashing)
        {
            isDashing = true;
            dashTime = dashDuration;
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
}


