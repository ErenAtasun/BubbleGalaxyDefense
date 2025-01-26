using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 100; // Maksimum can de�eri
    private int currentHealth;  // Mevcut can de�eri

    public Image healthBarForeground; // Dolum alan� i�in Image
    public Image healthBarBackground; // Arka plan i�in Image (iste�e ba�l�)

    void Start()
    {
        currentHealth = maxHealth; // Oyunun ba��nda can maksimumda
        UpdateHealthBar(); // Sa�l�k bar�n� g�ncelle
    }

    // Hasar alma fonksiyonu
    public void TakeDamage(int damage)
    {
        currentHealth -= damage; // Hasar al�nd���nda mevcut can azal�r
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // Can� s�n�rlar

        UpdateHealthBar(); // Sa�l�k bar�n� g�ncelle
        Debug.Log("Enemy Health: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die(); // Can s�f�ra d��erse �l�m ger�ekle�ir
        }
    }

    // Can yenileme fonksiyonu
    public void Heal(int amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // Maksimum can� a�amaz

        UpdateHealthBar(); // Sa�l�k bar�n� g�ncelle
        Debug.Log("Enemy Healed: " + currentHealth);
    }

    // Sa�l�k bar�n� g�ncelleme fonksiyonu
    private void UpdateHealthBar()
    {
        if (healthBarForeground != null)
        {
            // Sa�l�k oran�n� hesapla (0 ile 1 aras�nda bir de�er)
            float healthPercent = (float)currentHealth / maxHealth;

            // Foreground'un doluluk oran�n� de�i�tir
            healthBarForeground.fillAmount = healthPercent;
        }
    }

    // �l�m fonksiyonu
    void Die()
    {
        Debug.Log("Enemy Died!");
        // Burada karakteri devre d��� b�rakabilir veya �l�m animasyonu oynatabilirsiniz
        gameObject.SetActive(false); // �rne�in, karakteri devre d��� b�rak�r
    }
}
