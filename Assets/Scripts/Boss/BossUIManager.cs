using UnityEngine;
using UnityEngine.UI;

public class BossUIManager : MonoBehaviour
{
    [Header("Boss UI Paneli")]
    [Tooltip("Boss’a ait saðlýk barý paneli (Canvas içindeki panel).")]
    public GameObject bossUIPanel;

    [Header("Health Bar")]
    [Tooltip("Saðlýk barý doluluk kýsmý (Image bileþeni, Fill Mode olarak ayarlanmalý).")]
    public Image healthBarForeground;

    // Sahnedeki boss’un saðlýk scripti
    private BossHealth bossHealth;

    void Start()
    {
        // Baþlangýçta UI panelini kapalý tutun
        if (bossUIPanel != null)
        {
            bossUIPanel.SetActive(false);
        }
    }

    void Update()
    {
        // Eðer boss henüz bulunamadýysa sahnede "Boss" tag'lý objeyi arayýn
        if (bossHealth == null)
        {
            GameObject bossObj = GameObject.FindGameObjectWithTag("Boss");
            if (bossObj != null)
            {
                bossHealth = bossObj.GetComponent<BossHealth>();
                if (bossHealth != null)
                {
                    // Boss spawn olduðunda UI panelini aktif hale getir
                    bossUIPanel.SetActive(true);
                }
            }
        }
        else
        {
            // Eðer boss mevcutsa saðlýk barýný güncelle
            if (healthBarForeground != null)
            {
                float fillAmount = (float)bossHealth.CurrentHealth / bossHealth.MaxHealth;
                healthBarForeground.fillAmount = fillAmount;
            }

            // Boss öldüyse (ya da Destroy edilmiþse), UI panelini kapat ve boss referansýný sýfýrla
            if (bossHealth.CurrentHealth <= 0)
            {
                bossUIPanel.SetActive(false);
                bossHealth = null;
            }
        }
    }
}

