using UnityEngine;
using TMPro; // TextMeshPro bileşeni için

public class PlayerShooting : MonoBehaviour
{
    public GameObject[] bulletPrefabs; // Farklı mermi prefab'ları
    public Transform firePoint; // Merminin çıkış noktası
    public int maxBullets = 10; // Maksimum mermi sayısı
    public float bulletSpeed = 20f; // Merminin hızı

    public int currentBullets; // Mevcut mermi sayısı
    public TextMeshProUGUI bulletCountText; // UI'de mermi sayısını gösterecek TextMeshPro bileşeni

    public Transform cameraHolder; // Kamera için bağımsız bir GameObject

    private int selectedBulletIndex = 0; // Seçili mermi türü
    private SpriteRenderer spriteRenderer;
    private Vector3 originalScale; // Karakterin orijinal ölçeği

    void Start()
    {
        currentBullets = maxBullets; // Başlangıçta maksimum mermi sayısı
        spriteRenderer = GetComponent<SpriteRenderer>(); // Karakterin SpriteRenderer bileşeni
        originalScale = transform.localScale; // Orijinal ölçeği sakla

        UpdateBulletUI(); // UI'yi başlangıçta güncelle
    }

    void Update()
    {
        // Sol tıklama ile ateş et
        if (Input.GetMouseButtonDown(0) && currentBullets > 0)
        {
            Shoot();
        }

        // Mermi türünü değiştirme (örneğin, Q tuşuyla)
        if (Input.GetKeyDown(KeyCode.Q))
        {
            CycleBulletType();
        }

        // Mermileri yenileme (örneğin, R tuşuyla)
        if (Input.GetKeyDown(KeyCode.R))
        {
            Reload();
        }

        // FirePoint'i Mouse'un baktığı yöne çevir
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f; // Z eksenini sıfırla (2D için)
        Vector2 direction = (mousePosition - transform.position).normalized;

        // Karakteri mouse yönüne göre flip yap
        if (mousePosition.x < transform.position.x)
        {
            transform.localScale = new Vector3(-originalScale.x, originalScale.y, originalScale.z); // Karakteri sola çevir
            firePoint.right = -direction; // FirePoint yönünü ters çevir
        }
        else
        {
            transform.localScale = originalScale; // Karakteri sağa çevir
            firePoint.right = direction; // FirePoint yönünü doğru ayarla
        }

        // Kamera pozisyonunu güncelle
        if (cameraHolder != null)
        {
            cameraHolder.position = new Vector3(transform.position.x, transform.position.y, cameraHolder.position.z);
        }
    }

    void Shoot()
    {
        // Seçili mermi prefab'ını oluştur
        GameObject bullet = Instantiate(bulletPrefabs[selectedBulletIndex], firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0f; // Z eksenini sıfırla (2D için)
            Vector2 direction = (mousePosition - firePoint.position).normalized;
            rb.linearVelocity = direction * bulletSpeed; // Mermiyi mouse'un yönüne doğru hareket ettir
        }

        currentBullets--; // Mermi sayısını azalt
        UpdateBulletUI(); // UI'yi her atışta güncelle
    }

    void CycleBulletType()
    {
        selectedBulletIndex = (selectedBulletIndex + 1) % bulletPrefabs.Length; // Mermi türünü sırayla değiştir
    }

    void Reload()
    {
        currentBullets = maxBullets; // Mermiyi tamamen doldur
        Debug.Log("Mermiler yenilendi!"); // Konsola bilgilendirme mesajı yazdır
        UpdateBulletUI(); // UI'yi doldurma sırasında güncelle
    }

    void UpdateBulletUI()
    {
        // Mermi sayısını UI'de güncelle
        bulletCountText.text = $"Bullets: {currentBullets}/{maxBullets}";
    }
}
