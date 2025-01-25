using UnityEngine;

public class AmmoPack : MonoBehaviour
{
    public float interactionDistance = 0.5f; // Oyuncunun yak�nl�k mesafesi
    private Transform player; // Oyuncunun transform'u
    private PlayerShooting playerShooting;

    void Start()
    {
        // Oyuncuyu sahnede "Player" tag'i ile buluyoruz
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");

        if (playerObject == null)
        {
            Debug.LogError("Player bulunamad�! Oyuncu sahnede 'Player' tag'i ile olmal�.");
            return;
        }

        player = playerObject.transform;
        playerShooting = playerObject.GetComponent<PlayerShooting>();

        if (playerShooting == null)
        {
            Debug.LogError("PlayerShooting scripti bulunamad�! Player objesine eklenmi� olmal�.");
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
        if (playerShooting != null)
        {
            Debug.Log("AmmoPack collected by player!");
            playerShooting.currentBullets += 10; // Mermiyi art�r
            Destroy(gameObject); // AmmoPack objesini yok et
        }
    }
}
