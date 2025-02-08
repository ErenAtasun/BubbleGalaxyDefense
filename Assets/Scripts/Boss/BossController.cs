using UnityEngine;

public class BossController : MonoBehaviour
{
    [Header("Boss Ayarlarý")]
    [Tooltip("Boss'un hareket hýzý")]
    public float moveSpeed = 3f;
    [Tooltip("Player saldýrýlarý arasýndaki bekleme süresi (saniye)")]
    public float attackSpeed = 1f;
    [Tooltip("Uygulanan hasar (player için de, kule için de geçerli)")]
    public int damage = 10;
    [Tooltip("Player'ýn tespit edileceði mesafe")]
    public float detectionRange = 5f;
    [Tooltip("Player'a saldýrý için gereken mesafe")]
    public float attackRange = 1.5f;

    [Header("Kule (Tower) Saldýrý Ayarlarý")]
    [Tooltip("Kuleye saldýrý oraný (saniyede bir kez)")]
    public float towerAttackRate = 1f;
    private float towerAttackCountdown = 0f;
    private bool isAttackingTower = false;

    private Transform tower;          // Kule (Tower) referansý
    private Transform playerTransform; // Player referansý
    private Animator animator;
    private float nextAttackTime = 0f;

    void Start()
    {
        // Animator bileþenini al
        animator = GetComponent<Animator>();

        // Sahnedeki kuleyi (Tower) bul (Kulenin tagý "Tower" olmalýdýr)
        GameObject towerObj = GameObject.FindGameObjectWithTag("Tower");
        if (towerObj != null)
        {
            tower = towerObj.transform;
        }
        else
        {
            Debug.LogError("Tower objesi bulunamadý! Lütfen sahnede 'Tower' taglý bir obje ekleyin.");
        }
    }

    void Update()
    {
        // Eðer boss kule ile temas halindeyse, diðer hedeflerle uðraþmadan kuleye saldýr
        if (isAttackingTower)
        {
            AttackTower();
            return;
        }

        // Henüz player referansý yoksa, bulmaya çalýþ
        if (playerTransform == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
            {
                playerTransform = playerObj.transform;
            }
        }

        // Hedef belirle: Eðer player tespit mesafesindeyse player, deðilse kule (Tower) hedeflenir
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

            // Eðer hedef player ve saldýrý mesafesinde ise, player saldýrýsý yapýlýr
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

                // Hareket animasyonu için yön bilgilerini aktar (Animator Controller'da "MoveX" ve "MoveY" parametreleri olmalý)
                animator.SetFloat("MoveX", direction.x);
                animator.SetFloat("MoveY", direction.y);
            }
        }
    }

    // Player saldýrýsý: 3 farklý saldýrý animasyonu tetiklenir ve player hasar alýr
    void Attack()
    {
        int attackType = Random.Range(1, 4); // 1, 2 veya 3
        animator.SetTrigger("Attack" + attackType);

        // Saldýrý anýnda player hâlâ saldýrý mesafesindeyse hasar uygula
        if (playerTransform != null && Vector2.Distance(transform.position, playerTransform.position) <= attackRange)
        {
            PlayerHealth playerHealth = playerTransform.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }
        }
    }

    // Kuleye saldýrý fonksiyonu (Enemy scriptindeki mantýkla benzer þekilde)
    void AttackTower()
    {
        if (towerAttackCountdown <= 0f)
        {
            // Kulenin canýný azaltacak Tower scriptine eriþim
            Tower towerScript = tower.GetComponent<Tower>();
            if (towerScript != null)
            {
                towerScript.TakeDamage(damage);
            }
            towerAttackCountdown = 1f / towerAttackRate;
        }
        towerAttackCountdown -= Time.deltaTime;
    }

    // Kuleye ulaþýldýðýnda saldýrý moduna geç
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Tower"))
        {
            isAttackingTower = true;
        }
    }

    // Kuleden ayrýldýðýnda saldýrý modundan çýk
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Tower"))
        {
            isAttackingTower = false;
        }
    }
}




