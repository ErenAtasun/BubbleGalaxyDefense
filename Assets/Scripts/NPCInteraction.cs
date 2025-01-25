using UnityEngine;
using System.Collections;

public class NPCInteraction : MonoBehaviour

{
    public Transform interactableObject; // Belirlenen obje
    public GameObject prefabToSpawn; // Olu�turulacak prefab
    public float interactionRange = 2f; // Eri�im mesafesi
    public float holdTime = 2f; // "E" tu�una bas�l� tutma s�resi

    private bool isPlayerNearby = false;
    private float holdTimer = 0f;

    void Update()
    {
        // Oyuncunun mesafesini kontrol et
        if (Vector3.Distance(transform.position, interactableObject.position) <= interactionRange)
        {
            isPlayerNearby = true;
        }
        else
        {
            isPlayerNearby = false;
            holdTimer = 0f; // Mesafeden ��karsa s�reyi s�f�rla
        }

        // "E" tu�una bas�l� tutuldu�unda
        if (isPlayerNearby && Input.GetKey(KeyCode.E))
        {
            holdTimer += Time.deltaTime;

            if (holdTimer >= holdTime)
            {
                SpawnPrefab();
                holdTimer = 0f; // Timer'� s�f�rla
            }
        }

        // "E" tu�u b�rak�l�rsa s�reyi s�f�rla
        if (Input.GetKeyUp(KeyCode.E))
        {
            holdTimer = 0f;
        }
    }

    void SpawnPrefab()
    {
        Vector3 spawnPosition = interactableObject.position + new Vector3(1f, 0f, 0f); // Objeye yak�n bir pozisyon
        Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity);
        Debug.Log("Prefab olu�turuldu!");

        // Oyuncunun kontrol�n�n etkilenmemesi i�in kontrolleri burada s�f�rl�yoruz
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}




