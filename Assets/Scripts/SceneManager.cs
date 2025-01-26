using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
public class SceneManager : MonoBehaviour
{
    // Play butonuna bas�ld���nda �a�r�l�r
    public void PlayGame()
    {
        // Oyun sahnesini y�kler. �rne�in "GameScene" adl� bir sahne.
        UnityEngine.SceneManagement.SceneManager.LoadScene("SampleScene");
    }

    // Quit butonuna bas�ld���nda �a�r�l�r
    public void QuitGame()
    {
        // Uygulamadan ��kar
        Debug.Log("Oyundan ��k�l�yor!"); // Editor'de test ederken bu mesaj� g�rebilirsiniz.
        Application.Quit();
    }

    // Main Menu'ye d�nmek i�in kullan�labilir (Game Over ekran� i�in)
    public void GoToMainMenu()
    {
        // Ana men� sahnesine d�nmek i�in sahne ad�
        UnityEngine.SceneManagement.SceneManager.LoadScene("Entry");
    }

    // Game Over butonuna bas�ld���nda �a�r�l�r
    public void GameOver()
    {
        // �rne�in, Game Over ekran�na gitmek i�in
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameOver");
    }
}
