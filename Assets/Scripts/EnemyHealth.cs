using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 100; // Maksimum can de�eri
    private int currentHealth;  // Mevcut can de�eri

    public Slider healthSlider; // Can g�stergesi i�in Slider (UI)

    void Start()
    {
        currentHealth = maxHealth; // Oyunun ba��nda can maksimumda
        if (healthSlider != null)
        {
            healthSlider.maxValue = maxHealth; // Slider'�n maksimum de�erini ayarla
            healthSlider.value = currentHealth; // Ba�lang��ta mevcut can� g�ster
        }
    }

    // Hasar alma fonksiyonu
    public void TakeDamage(int damage)
    {
        currentHealth -= damage; // Hasar al�nd���nda mevcut can azal�r

        if (healthSlider != null)
        {
            healthSlider.value = currentHealth; // Slider g�ncelle
        }

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

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth; // Maksimum can� a�amaz
        }

        if (healthSlider != null)
        {
            healthSlider.value = currentHealth; // Slider g�ncelle
        }

        Debug.Log("enemy Healed: " + currentHealth);
    }

    // �l�m fonksiyonu
    void Die()
    {
        Debug.Log("enemy Died!");
        // Burada karakteri devre d��� b�rakabilir veya �l�m animasyonu oynatabilirsiniz
        gameObject.SetActive(false); // �rne�in, karakteri devre d��� b�rak�r
    }
}
