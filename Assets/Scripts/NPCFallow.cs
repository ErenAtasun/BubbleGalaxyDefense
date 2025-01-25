using UnityEngine;

public class NPCFallow : MonoBehaviour
{
    public Transform Player; // Takip edilecek oyuncu
    public float followSpeed = 5f; // Takip h�z�n� ayarla
    public float followDistance = 2f; // Oyuncudan uzakl��� koruma mesafesi

    private Vector3 offset; // Takip�i objenin ba�lang��taki ofseti

    void Start()
    {
        // Oyuncuya olan ba�lang��taki mesafeyi sakla
        offset = transform.position - Player.position;
    }

    void Update()
    {
        // Hedef pozisyon, oyuncunun pozisyonu ve ofset ile hesaplan�r
        Vector3 targetPosition = Player.position + offset;

        // E�er oyuncu ile takip�i aras�nda mesafe fazla ise hareket et
        if (Vector3.Distance(transform.position, Player.position) > followDistance)
        {
            transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);
        }
    }
}
