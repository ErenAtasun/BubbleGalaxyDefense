using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100; // Maksimum can deðeri
    private int currentHealth;  // Mevcut can deðeri

    public Image healthBarForeground; // Dolum alaný için Image
    public Image healthBarBackground; // Arka plan için Image (isteðe baðlý)

    void Start()
    {
        currentHealth = maxHealth; // Oyunun baþýnda can maksimumda
        UpdateHealthBar(); // Saðlýk barýný güncelle
    }


    void Update()
    {

        if (Input.GetKeyDown(KeyCode.T))
        {
            TakeDamage(5);
        }
    }
    void LateUpdate()
    {
        if (healthBarForeground != null && healthBarBackground != null)
        {
            // Can barýnýn rotasyonunu sabit tut
            healthBarForeground.transform.rotation = Quaternion.identity;
            healthBarBackground.transform.rotation = Quaternion.identity;
        }
    }
    // Hasar alma fonksiyonu
    public void TakeDamage(int damage)
    {
        currentHealth -= damage; // Hasar alýndýðýnda mevcut can azalýr
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // Caný sýnýrlar

        UpdateHealthBar(); // Saðlýk barýný güncelle
        Debug.Log("Player Health: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die(); // Can sýfýra düþerse ölüm gerçekleþir
        }
    }

    // Can yenileme fonksiyonu
    public void Heal(int amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // Maksimum caný aþamaz

        UpdateHealthBar(); // Saðlýk barýný güncelle
        Debug.Log("Player Healed: " + currentHealth);
    }

    // Saðlýk barýný güncelleme fonksiyonu
    private void UpdateHealthBar()
    {
        if (healthBarForeground != null)
        {
            // Saðlýk oranýný hesapla (0 ile 1 arasýnda bir deðer)
            float healthPercent = (float)currentHealth / maxHealth;

            // Foreground'un doluluk oranýný deðiþtir
            healthBarForeground.fillAmount = healthPercent;
        }
    }

    // Ölüm fonksiyonu
    void Die()
    {
        Debug.Log("Player Died!");
        // Burada karakteri devre dýþý býrakabilir veya ölüm animasyonu oynatabilirsiniz
        gameObject.SetActive(false); // Örneðin, karakteri devre dýþý býrakýr
    }
}
