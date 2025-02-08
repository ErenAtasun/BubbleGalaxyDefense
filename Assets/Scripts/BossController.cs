using UnityEngine;

public class BossController : MonoBehaviour
{
    [Header("Boss Ayarlarý")]
    [Tooltip("Boss'un hareket hýzý")]
    public float moveSpeed = 3f;
    [Tooltip("Saldýrýlar arasýndaki bekleme süresi (saniye)")]
    public float attackSpeed = 1f;
    [Tooltip("Saldýrý hasarý (int)")]
    public int damage = 10;

    [Header("Tespit & Saldýrý Mesafeleri")]
    [Tooltip("Player'ýn tespit edileceði mesafe")]
    public float detectionRange = 5f;
    [Tooltip("Saldýrý için gereken mesafe")]
    public float attackRange = 1.5f;

    private Transform baseTransform;     // "base" taglý objenin Transform'u
    private Transform playerTransform;   // "player" taglý objenin Transform'u
    private Animator animator;
    private float nextAttackTime = 0f;

    void Start()
    {
        // Animator bileþenini al
        animator = GetComponent<Animator>();

        // Sahnedeki "base" taglý objeyi bul
        GameObject baseObj = GameObject.FindGameObjectWithTag("Tower");
        if (baseObj != null)
        {
            baseTransform = baseObj.transform;
        }
        else
        {
            Debug.LogError("Base objesi bulunamadý! Lütfen sahnede 'base' taglý bir obje ekleyin.");
        }
    }

    void Update()
    {
        // Henüz player referansý yoksa, bulmaya çalýþ
        if (playerTransform == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
            {
                playerTransform = playerObj.transform;
            }
        }

        // Hedef olarak default base, ancak player tespit mesafesindeyse player'ý hedefle
        Transform target = baseTransform;
        if (playerTransform != null)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);
            if (distanceToPlayer <= detectionRange)
            {
                target = playerTransform;
            }
        }

        // Eðer hedef belirlendiyse (base veya player) ona doðru hareket et veya saldýr
        if (target != null)
        {
            float distanceToTarget = Vector2.Distance(transform.position, target.position);

            // Eðer hedef player ve saldýrý mesafesindeyse saldýr
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
                // Hedefe doðru hareket et
                Vector2 direction = (target.position - transform.position).normalized;
                transform.position += (Vector3)(direction * moveSpeed * Time.deltaTime);

                // Hareket animasyonlarý için yön bilgisi aktarýlýr (Animator'da "MoveX" ve "MoveY" parametreleri olmalý)
                animator.SetFloat("MoveX", direction.x);
                animator.SetFloat("MoveY", direction.y);
            }
        }
    }

    // Saldýrý metodunda 3 farklý saldýrý tipi rastgele seçilir
    void Attack()
    {
        int attackType = Random.Range(1, 4); // 1, 2 veya 3
        // Seçilen saldýrý tipine ait animasyonu tetikle (Animator Controller'da "Attack1", "Attack2", "Attack3" trigger'larý tanýmlý olmalý)
        animator.SetTrigger("Attack" + attackType);

        // Saldýrý anýnda, player hala saldýrý mesafesindeyse hasarý uygula
        if (playerTransform != null && Vector2.Distance(transform.position, playerTransform.position) <= attackRange)
        {
            PlayerHealth playerHealth = playerTransform.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                // Artýk damage int tipinde olduðu için PlayerHealth scriptindeki TakeDamage(int damage) metoduna uygun þekilde çaðrýlýr.
                playerHealth.TakeDamage(damage);
            }
        }
    }
}



