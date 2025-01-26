using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 5f; // Mermi h�z�
    private Transform target;
    public int damage = 10;

    public void SetTarget(Transform _target)
    {
        target = _target;
    }

    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        // Hedefe do�ru hareket et
        Vector2 direction = (Vector2)target.position - (Vector2)transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if (direction.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        transform.Translate(direction.normalized * distanceThisFrame, Space.World);
    }
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
            Debug.Log("D��MANA HASAR VER�LD�");
            // Mermi �arpt�ktan sonra yok olsun
            Destroy(gameObject);
        }
    }
    void HitTarget()
    {
        // Hedefe hasar ver
        Destroy(gameObject);
    }
}
