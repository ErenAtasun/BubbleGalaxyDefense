using UnityEngine;

public class Scrap : MonoBehaviour
{
    public float interactionDistance = 0.5f; // Oyuncunun yak�nl�k mesafesi
    private Transform player; // Oyuncunun transform'u

    void Start()
    {
        // Oyuncuyu sahnede "Player" tag'i ile buluyoruz
        player = GameObject.FindGameObjectWithTag("Player").transform;

        if (player == null)
        {
            Debug.LogError("Player bulunamad�! Oyuncu sahnede 'Player' tag'i ile olmal�.");
        }
    }

    void Update()
    {
        // Oyuncu belli bir mesafeye gelmi�se ve "E" tu�una bas�lm��sa
        if (player != null && Vector2.Distance(transform.position, player.position) <= interactionDistance)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Collect();
            }
        }
    }

    public void Collect()
    {
        // Scrap topland���nda ne olaca��n� buraya yaz
        Debug.Log("Scrap collected by player!");
        Destroy(gameObject); // Scrap objesini yok et
    }

    // Etkile�im mesafesini sahnede g�stermek i�in (iste�e ba�l�)
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, interactionDistance);
    }
}
