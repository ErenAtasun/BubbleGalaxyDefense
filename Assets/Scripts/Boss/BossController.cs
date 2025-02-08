using UnityEngine;

public class BossController : MonoBehaviour
{
    [Header("Boss Ayarlar�")]
    [Tooltip("Boss'un hareket h�z�")]
    public float moveSpeed = 3f;
    [Tooltip("Player sald�r�lar� aras�ndaki bekleme s�resi (saniye)")]
    public float attackSpeed = 1f;
    [Tooltip("Uygulanan hasar (player i�in de, kule i�in de ge�erli)")]
    public int damage = 10;
    [Tooltip("Player'�n tespit edilece�i mesafe")]
    public float detectionRange = 5f;
    [Tooltip("Player'a sald�r� i�in gereken mesafe")]
    public float attackRange = 1.5f;

    [Header("Kule (Tower) Sald�r� Ayarlar�")]
    [Tooltip("Kuleye sald�r� oran� (saniyede bir kez)")]
    public float towerAttackRate = 1f;
    private float towerAttackCountdown = 0f;
    private bool isAttackingTower = false;

    private Transform tower;          // Kule (Tower) referans�
    private Transform playerTransform; // Player referans�
    private Animator animator;
    private float nextAttackTime = 0f;

    void Start()
    {
        // Animator bile�enini al
        animator = GetComponent<Animator>();

        // Sahnedeki kuleyi (Tower) bul (Kulenin tag� "Tower" olmal�d�r)
        GameObject towerObj = GameObject.FindGameObjectWithTag("Tower");
        if (towerObj != null)
        {
            tower = towerObj.transform;
        }
        else
        {
            Debug.LogError("Tower objesi bulunamad�! L�tfen sahnede 'Tower' tagl� bir obje ekleyin.");
        }
    }

    void Update()
    {
        // E�er boss kule ile temas halindeyse, di�er hedeflerle u�ra�madan kuleye sald�r
        if (isAttackingTower)
        {
            AttackTower();
            return;
        }

        // Hen�z player referans� yoksa, bulmaya �al��
        if (playerTransform == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
            {
                playerTransform = playerObj.transform;
            }
        }

        // Hedef belirle: E�er player tespit mesafesindeyse player, de�ilse kule (Tower) hedeflenir
        Transform target = tower;
        if (playerTransform != null)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);
            if (distanceToPlayer <= detectionRange)
            {
                target = playerTransform;
            }
        }

        if (target != null)
        {
            float distanceToTarget = Vector2.Distance(transform.position, target.position);

            // E�er hedef player ve sald�r� mesafesinde ise, player sald�r�s� yap�l�r
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

                // Hareket animasyonu i�in y�n bilgilerini aktar (Animator Controller'da "MoveX" ve "MoveY" parametreleri olmal�)
                animator.SetFloat("MoveX", direction.x);
                animator.SetFloat("MoveY", direction.y);
            }
        }
    }

    // Player sald�r�s�: 3 farkl� sald�r� animasyonu tetiklenir ve player hasar al�r
    void Attack()
    {
        int attackType = Random.Range(1, 4); // 1, 2 veya 3
        animator.SetTrigger("Attack" + attackType);

        // Sald�r� an�nda player h�l� sald�r� mesafesindeyse hasar uygula
        if (playerTransform != null && Vector2.Distance(transform.position, playerTransform.position) <= attackRange)
        {
            PlayerHealth playerHealth = playerTransform.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }
        }
    }

    // Kuleye sald�r� fonksiyonu (Enemy scriptindeki mant�kla benzer �ekilde)
    void AttackTower()
    {
        if (towerAttackCountdown <= 0f)
        {
            // Kulenin can�n� azaltacak Tower scriptine eri�im
            Tower towerScript = tower.GetComponent<Tower>();
            if (towerScript != null)
            {
                towerScript.TakeDamage(damage);
            }
            towerAttackCountdown = 1f / towerAttackRate;
        }
        towerAttackCountdown -= Time.deltaTime;
    }

    // Kuleye ula��ld���nda sald�r� moduna ge�
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Tower"))
        {
            isAttackingTower = true;
        }
    }

    // Kuleden ayr�ld���nda sald�r� modundan ��k
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Tower"))
        {
            isAttackingTower = false;
        }
    }
}




