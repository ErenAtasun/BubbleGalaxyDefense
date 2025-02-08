using UnityEngine;
using UnityEngine.UI;

public class BossHealth : MonoBehaviour
{
    [Header("Boss Sa�l�k Ayarlar�")]
    public int maxHealth = 100;            // Boss'un maksimum can�
    private int currentHealth;             // Boss'un mevcut can�

    [Header("Health Bar UI")]
    [Tooltip("Boss'un sa�l�k bar� doluluk k�sm� (Fill Mode: Fill)")]
    public Image healthBarForeground;      // UI'daki doluluk k�sm�

    // Public property'ler (BossUIManager veya di�er scriptlerin eri�imi i�in)
    public int MaxHealth { get { return maxHealth; } }
    public int CurrentHealth { get { return currentHealth; } }

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthBar();
        Debug.Log("Boss spawn oldu. Can: " + currentHealth);
    }

    void Update()
    {
        // Test ama�l�: "H" tu�una bas�ld���nda boss'a 10 hasar uygula
        if (Input.GetKeyDown(KeyCode.H))
        {
            Debug.Log("H tu�una bas�ld�: Boss'a 10 hasar uygulan�yor.");
            TakeDamage(10);
        }
    }

    /// <summary>
    /// Boss'un hasar almas�n� sa�lar.
    /// </summary>
    /// <param name="damageAmount">Uygulanacak hasar miktar�</param>
    public void TakeDamage(int damageAmount)
    {
        Debug.Log("TakeDamage �a�r�ld�. Hasar miktar�: " + damageAmount);
        currentHealth -= damageAmount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateHealthBar();
        Debug.Log("G�ncel Boss can�: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    /// <summary>
    /// UI'daki sa�l�k bar�n� g�nceller.
    /// </summary>
    private void UpdateHealthBar()
    {
        if (healthBarForeground != null)
        {
            float fillAmount = (float)currentHealth / maxHealth;
            healthBarForeground.fillAmount = fillAmount;
            Debug.Log("Health Bar g�ncellendi: fillAmount = " + fillAmount);
        }
        else
        {
            Debug.LogWarning("healthBarForeground atanmam��! L�tfen Inspector �zerinden referans verin.");
        }
    }

    /// <summary>
    /// Boss �ld���nde �al���r.
    /// </summary>
    private void Die()
    {
        Debug.Log("Boss �ld�!");
        Destroy(gameObject);
    }
}

