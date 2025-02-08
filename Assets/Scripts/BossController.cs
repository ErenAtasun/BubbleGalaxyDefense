using UnityEngine;

public class BossController : MonoBehaviour
{
    [Header("Boss Ayarlar�")]
    [Tooltip("Boss'un hareket h�z�")]
    public float moveSpeed = 3f;
    [Tooltip("Sald�r�lar aras�ndaki bekleme s�resi (saniye)")]
    public float attackSpeed = 1f;
    [Tooltip("Sald�r� hasar� (int)")]
    public int damage = 10;

    [Header("Tespit & Sald�r� Mesafeleri")]
    [Tooltip("Player'�n tespit edilece�i mesafe")]
    public float detectionRange = 5f;
    [Tooltip("Sald�r� i�in gereken mesafe")]
    public float attackRange = 1.5f;

    private Transform baseTransform;     // "base" tagl� objenin Transform'u
    private Transform playerTransform;   // "player" tagl� objenin Transform'u
    private Animator animator;
    private float nextAttackTime = 0f;

    void Start()
    {
        // Animator bile�enini al
        animator = GetComponent<Animator>();

        // Sahnedeki "base" tagl� objeyi bul
        GameObject baseObj = GameObject.FindGameObjectWithTag("Tower");
        if (baseObj != null)
        {
            baseTransform = baseObj.transform;
        }
        else
        {
            Debug.LogError("Base objesi bulunamad�! L�tfen sahnede 'base' tagl� bir obje ekleyin.");
        }
    }

    void Update()
    {
        // Hen�z player referans� yoksa, bulmaya �al��
        if (playerTransform == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
            {
                playerTransform = playerObj.transform;
            }
        }

        // Hedef olarak default base, ancak player tespit mesafesindeyse player'� hedefle
        Transform target = baseTransform;
        if (playerTransform != null)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);
            if (distanceToPlayer <= detectionRange)
            {
                target = playerTransform;
            }
        }

        // E�er hedef belirlendiyse (base veya player) ona do�ru hareket et veya sald�r
        if (target != null)
        {
            float distanceToTarget = Vector2.Distance(transform.position, target.position);

            // E�er hedef player ve sald�r� mesafesindeyse sald�r
            if (target == playerTransform && distanceToTarget <= attackRange)
            {
                if (Time.time >= nextAttackTime)
                {
                    Attack();
                    nextAttackTime = Time.time + attackSpeed;
                }
            }
            else
            {
                // Hedefe do�ru hareket et
                Vector2 direction = (target.position - transform.position).normalized;
                transform.position += (Vector3)(direction * moveSpeed * Time.deltaTime);

                // Hareket animasyonlar� i�in y�n bilgisi aktar�l�r (Animator'da "MoveX" ve "MoveY" parametreleri olmal�)
                animator.SetFloat("MoveX", direction.x);
                animator.SetFloat("MoveY", direction.y);
            }
        }
    }

    // Sald�r� metodunda 3 farkl� sald�r� tipi rastgele se�ilir
    void Attack()
    {
        int attackType = Random.Range(1, 4); // 1, 2 veya 3
        // Se�ilen sald�r� tipine ait animasyonu tetikle (Animator Controller'da "Attack1", "Attack2", "Attack3" trigger'lar� tan�ml� olmal�)
        animator.SetTrigger("Attack" + attackType);

        // Sald�r� an�nda, player hala sald�r� mesafesindeyse hasar� uygula
        if (playerTransform != null && Vector2.Distance(transform.position, playerTransform.position) <= attackRange)
        {
            PlayerHealth playerHealth = playerTransform.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                // Art�k damage int tipinde oldu�u i�in PlayerHealth scriptindeki TakeDamage(int damage) metoduna uygun �ekilde �a�r�l�r.
                playerHealth.TakeDamage(damage);
            }
        }
    }
}



