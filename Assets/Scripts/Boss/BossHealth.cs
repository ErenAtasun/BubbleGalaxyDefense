using UnityEngine;
using UnityEngine.UI;

public class BossHealth : MonoBehaviour
{
    [Header("Boss Saðlýk Ayarlarý")]
    public int maxHealth = 100;            // Boss'un maksimum caný
    private int currentHealth;             // Boss'un mevcut caný

    [Header("Health Bar UI")]
    [Tooltip("Boss'un saðlýk barý doluluk kýsmý (Fill Mode: Fill)")]
    public Image healthBarForeground;      // UI'daki doluluk kýsmý

    // Public property'ler (BossUIManager veya diðer scriptlerin eriþimi için)
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
        // Test amaçlý: "H" tuþuna basýldýðýnda boss'a 10 hasar uygula
        if (Input.GetKeyDown(KeyCode.H))
        {
            Debug.Log("H tuþuna basýldý: Boss'a 10 hasar uygulanýyor.");
            TakeDamage(10);
        }
    }

    /// <summary>
    /// Boss'un hasar almasýný saðlar.
    /// </summary>
    /// <param name="damageAmount">Uygulanacak hasar miktarý</param>
    public void TakeDamage(int damageAmount)
    {
        Debug.Log("TakeDamage çaðrýldý. Hasar miktarý: " + damageAmount);
        currentHealth -= damageAmount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateHealthBar();
        Debug.Log("Güncel Boss caný: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    /// <summary>
    /// UI'daki saðlýk barýný günceller.
    /// </summary>
    private void UpdateHealthBar()
    {
        if (healthBarForeground != null)
        {
            float fillAmount = (float)currentHealth / maxHealth;
            healthBarForeground.fillAmount = fillAmount;
            Debug.Log("Health Bar güncellendi: fillAmount = " + fillAmount);
        }
        else
        {
            Debug.LogWarning("healthBarForeground atanmamýþ! Lütfen Inspector üzerinden referans verin.");
        }
    }

    /// <summary>
    /// Boss öldüðünde çalýþýr.
    /// </summary>
    private void Die()
    {
        Debug.Log("Boss öldü!");
        Destroy(gameObject);
    }
}

