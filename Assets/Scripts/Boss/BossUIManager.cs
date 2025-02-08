using UnityEngine;
using UnityEngine.UI;

public class BossUIManager : MonoBehaviour
{
    [Header("Boss UI Paneli")]
    [Tooltip("Boss�a ait sa�l�k bar� paneli (Canvas i�indeki panel).")]
    public GameObject bossUIPanel;

    [Header("Health Bar")]
    [Tooltip("Sa�l�k bar� doluluk k�sm� (Image bile�eni, Fill Mode olarak ayarlanmal�).")]
    public Image healthBarForeground;

    // Sahnedeki boss�un sa�l�k scripti
    private BossHealth bossHealth;

    void Start()
    {
        // Ba�lang��ta UI panelini kapal� tutun
        if (bossUIPanel != null)
        {
            bossUIPanel.SetActive(false);
        }
    }

    void Update()
    {
        // E�er boss hen�z bulunamad�ysa sahnede "Boss" tag'l� objeyi aray�n
        if (bossHealth == null)
        {
            GameObject bossObj = GameObject.FindGameObjectWithTag("Boss");
            if (bossObj != null)
            {
                bossHealth = bossObj.GetComponent<BossHealth>();
                if (bossHealth != null)
                {
                    // Boss spawn oldu�unda UI panelini aktif hale getir
                    bossUIPanel.SetActive(true);
                }
            }
        }
        else
        {
            // E�er boss mevcutsa sa�l�k bar�n� g�ncelle
            if (healthBarForeground != null)
            {
                float fillAmount = (float)bossHealth.CurrentHealth / bossHealth.MaxHealth;
                healthBarForeground.fillAmount = fillAmount;
            }

            // Boss �ld�yse (ya da Destroy edilmi�se), UI panelini kapat ve boss referans�n� s�f�rla
            if (bossHealth.CurrentHealth <= 0)
            {
                bossUIPanel.SetActive(false);
                bossHealth = null;
            }
        }
    }
}

