using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject[] bulletPrefabs; // Farkl� mermi prefab'lar�
    public Transform firePoint; // Merminin ��k�� noktas�
    public int maxBullets = 10; // Maksimum mermi say�s�
    public float bulletSpeed = 20f; // Merminin h�z�

    private int currentBullets;
    private int selectedBulletIndex = 0; // Se�ili mermi t�r�
    private SpriteRenderer spriteRenderer;
    private Vector3 originalScale; // Karakterin orijinal �l�e�i

    void Start()
    {
        currentBullets = maxBullets; // Ba�lang��ta maksimum mermi say�s�
        spriteRenderer = GetComponent<SpriteRenderer>(); // Karakterin SpriteRenderer bile�eni
        originalScale = transform.localScale; // Orijinal �l�e�i sakla
    }

    void Update()
    {
        // Sol t�klama ile ate� et
        if (Input.GetMouseButtonDown(0) && currentBullets > 0)
        {
            Shoot();
        }

        // Mermi t�r�n� de�i�tirme (�rne�in, Q tu�uyla)
        if (Input.GetKeyDown(KeyCode.Q))
        {
            CycleBulletType();
        }

        // FirePoint'i Mouse'un bakt��� y�ne �evir
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f; // Z eksenini s�f�rla (2D i�in)
        Vector2 direction = (mousePosition - transform.position).normalized;

        // Karakteri mouse y�n�ne g�re flip yap
        if (mousePosition.x < transform.position.x)
        {
            transform.localScale = new Vector3(-originalScale.x, originalScale.y, originalScale.z); // Karakteri sola �evir
            firePoint.right = -direction; // FirePoint y�n�n� ters �evir
        }
        else
        {
            transform.localScale = originalScale; // Karakteri sa�a �evir
            firePoint.right = direction; // FirePoint y�n�n� do�ru ayarla
        }
    }

    void Shoot()
    {
        // Se�ili mermi prefab'�n� olu�tur
        GameObject bullet = Instantiate(bulletPrefabs[selectedBulletIndex], firePoint.position, firePoint.rotation);
        BulletMovement bulletMovement = bullet.GetComponent<BulletMovement>();
        if (bulletMovement != null)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0f; // Z eksenini s�f�rla (2D i�in)
            Vector2 direction = (mousePosition - transform.position).normalized;

            if (transform.localScale.x < 0) // Karakter sola d�n�kse y�n� ters �evir
            {
                direction.x = -direction.x;
            }

            bulletMovement.SetSpeed(direction * bulletSpeed); // Mermiyi mouse'un y�n�ne do�ru hareket ettir
        }

        currentBullets--; // Mermi say�s�n� azalt
    }

    void CycleBulletType()
    {
        selectedBulletIndex = (selectedBulletIndex + 1) % bulletPrefabs.Length; // Mermi t�r�n� s�rayla de�i�tir
    }

    public void Reload(int amount)
    {
        currentBullets = Mathf.Clamp(currentBullets + amount, 0, maxBullets); // Mermiyi yenile
    }
}











