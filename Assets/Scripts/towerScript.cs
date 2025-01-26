using UnityEngine;
using UnityEngine.SceneManagement;

public class Tower : MonoBehaviour
{
    public float range = 5f; // Kule menzili
    public float fireRate = 1f; // At�� h�z�
    public GameObject bulletPrefab; // Mermi prefab'i
    public Transform firePoint; // Merminin ��k�� noktas�

    public int health = 100; // Kulenin toplam can�

    private Transform target; // Hedef d��man
    private float fireCountdown = 0f;

    void Update()
    {
        UpdateTarget();

        if (target == null)
            return;

        // Ate� etme i�lemleri
        if (fireCountdown <= 0f)
        {
            Shoot();
            fireCountdown = 1f / fireRate;
        }

        fireCountdown -= Time.deltaTime;
    }

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector2.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;
        }
        else
        {
            target = null;
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage; // Kulenin can�n� azalt
        Debug.Log("Tower Health: " + health);

        if (health <= 0)
        {
            Die(); // Can s�f�rsa kule yok olur
        }
    }

    void Die()
    {
        Debug.Log("Tower destroyed!");
        Destroy(gameObject); // Kulenin yok edilmesi
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameOver");

    }

    void Shoot()
    {
        if (bulletPrefab == null || firePoint == null) return;

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bulletScript = bullet.GetComponent<Bullet>();

        if (bulletScript != null)
        {
            bulletScript.SetTarget(target);
        }
    }

    // �arp��ma Alg�lama
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("NPC")) // E�er �arpan obje NPC ise
        {
            health += 10; // Kulenin can�n� art�r
            Debug.Log("Tower health increased: " + health);
            Destroy(collision.gameObject); // �arpan NPC'yi yok et
        }
    }
}
