using UnityEngine;

public class NPCFallow : MonoBehaviour
{
    private Transform player; // Oyuncunun Transform bile�eni
    public float speed = 5f; // Takip h�z�
    public float minimumDistanceToPlayer = 1.5f; // Oyuncuyla NPC aras�ndaki minimum mesafe
    public float minimumDistanceToOtherNPCs = 1f; // Di�er NPC'lerle olan minimum mesafe
    public float avoidSpeed = 3f; // Di�er NPC'lerden uzakla�ma h�z�

    void Start()
    {
        // Player objesini sahnede otomatik olarak bul
        GameObject playerObject = GameObject.FindWithTag("Player");

        if (playerObject != null)
        {
            player = playerObject.transform;
        }
        else
        {
            Debug.LogError("Player objesi bulunamad�! Sahnedeki Player objesine 'Player' etiketi ekleyin.");
        }
    }

    void Update()
    {
        // E�er player atanm��sa takip et
        if (player != null)
        {
            // Oyuncu ile NPC aras�ndaki mesafeyi hesapla
            float distanceToPlayer = Vector3.Distance(player.position, transform.position);

            // E�er mesafe minimum mesafeden b�y�kse hareket et
            if (distanceToPlayer > minimumDistanceToPlayer)
            {
                // Di�er NPC'lere olan mesafeyi kontrol et ve hareket et
                if (IsTooCloseToOtherNPCs(out Vector3 avoidDirection))
                {
                    // E�er ba�ka bir NPC'ye �ok yak�nsa uzakla�
                    transform.position += avoidDirection * avoidSpeed * Time.deltaTime;
                }
                else
                {
                    // Oyuncuya do�ru hareket et
                    Vector3 directionToPlayer = (player.position - transform.position).normalized;
                    transform.position += directionToPlayer * speed * Time.deltaTime;
                }
            }
        }
    }

    // Di�er NPC'lere �ok yak�n olup olmad���n� kontrol eder
    bool IsTooCloseToOtherNPCs(out Vector3 avoidDirection)
    {
        avoidDirection = Vector3.zero; // Uzakla�ma y�n�
        GameObject[] allNPCs = GameObject.FindGameObjectsWithTag("NPC");

        foreach (GameObject npc in allNPCs)
        {
            if (npc != this.gameObject) // Kendini kontrol etme
            {
                float distanceToNPC = Vector3.Distance(npc.transform.position, transform.position);

                if (distanceToNPC < minimumDistanceToOtherNPCs)
                {
                    // Uzakla�ma y�n�n� belirle
                    avoidDirection += (transform.position - npc.transform.position).normalized;
                }
            }
        }

        // E�er uzakla�ma y�n� belirlenmi�se, normalize et
        if (avoidDirection != Vector3.zero)
        {
            avoidDirection = avoidDirection.normalized;
            return true;
        }

        return false;
    }
}
