using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health = 100; // D��man can�
    public float speed = 2f; // D��man hareket h�z�
    public int damage = 10; // Kulenin alaca�� hasar miktar�
    public float attackRate = 1f; // Saniyede bir kez sald�r�
    private float attackCountdown = 0f; // Sald�r� i�in zamanlay�c�

    private bool isAttacking = false; // D��man kuleye ula�t� m�?

    private Transform tower; // Kule referans�

    void Start()
    {
        // Kulenin referans�n� bul ve hedef olarak ayarla
        tower = GameObject.FindGameObjectWithTag("Tower").transform;
    }

    void Update()
    {
        if (!isAttacking)
        {
            MoveTowardsTower(); // E�er kuleye ula�mad�ysa hareket et
        }
        else
        {
            AttackTower(); // E�er ula�t�ysa kuleye sald�r
        }
    }

    // D��man�n kuleye do�ru hareket etmesini sa�lar
    void MoveTowardsTower()
    {
        if (tower == null) return;

        // Kuleye do�ru hareket et
        Vector2 direction = (tower.position - transform.position).normalized;
        transform.Translate(direction * speed * Time.deltaTime, Space.World);
    }

    // D��man�n kuleye sald�rmas�n� sa�lar
    void AttackTower()
    {
        if (attackCountdown <= 0f)
        {
            // Kulenin can�n� azalt
            Tower towerScript = tower.GetComponent<Tower>();
            if (towerScript != null)
            {
                towerScript.TakeDamage(damage); // Kulenin can�n� azalt
            }

            attackCountdown = 1f / attackRate; // Yeni sald�r� i�in zamanlay�c�y� s�f�rla
        }

        attackCountdown -= Time.deltaTime; // Zamanlay�c�y� g�ncelle
    }

    // Kuleye ula�t���nda �al���r
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Tower"))
        {
            isAttacking = true; // Kuleye ula��ld�
        }
    }

    // Kuleden ayr�ld���nda �al���r (opsiyonel, gerekirse)
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Tower"))
        {
            isAttacking = false; // Kuleden uzakla��rsa tekrar hareket edebilir
        }
    }

    // D��man�n hasar almas�n� sa�lar
    public void TakeDamage(int damageAmount)
    {
        health -= damageAmount;

        if (health <= 0)
        {
            Die();
        }
    }

    // D��man �ld���nde �al���r
    void Die()
    {
        Destroy(gameObject); // D��man� sahneden kald�r
    }
}
