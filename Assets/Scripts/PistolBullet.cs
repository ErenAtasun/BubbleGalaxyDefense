using UnityEngine;

public class PistolBullet : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int damage = 10; // Merminin verdiði hasar

    void OnTriggerEnter2D(Collider2D collision)
    {
        // Eðer çarptýðý obje "Player" ise hasar ver
        if (collision.CompareTag("Enemy")) // Player tag'ini kontrol et
        {
            // Çarptýðý objede PlayerHealth component'i varsa hasar uygula
            EnemyHealth enemyHealth = collision.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage); // Player'a hasar ver
            }

            // Mermi çarptýktan sonra yok olsun
            Destroy(gameObject);
        }
        if (collision.CompareTag("Boss")) // Player tag'ini kontrol et
        {
            // Çarptýðý objede PlayerHealth component'i varsa hasar uygula
            EnemyHealth enemyHealth = collision.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage); // Player'a hasar ver
            }

            // Mermi çarptýktan sonra yok olsun
            Destroy(gameObject);
        }
    }
}
