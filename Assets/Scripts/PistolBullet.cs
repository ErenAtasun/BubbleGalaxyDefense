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

    public int damage = 10; // Merminin verdi�i hasar

    void OnTriggerEnter2D(Collider2D collision)
    {
        // E�er �arpt��� obje "Player" ise hasar ver
        if (collision.CompareTag("Enemy")) // Player tag'ini kontrol et
        {
            // �arpt��� objede PlayerHealth component'i varsa hasar uygula
            EnemyHealth enemyHealth = collision.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage); // Player'a hasar ver
            }

            // Mermi �arpt�ktan sonra yok olsun
            Destroy(gameObject);
        }
    }
}
