using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
public class SceneManager : MonoBehaviour
{
    // Play butonuna basýldýðýnda çaðrýlýr
    public void PlayGame()
    {
        // Oyun sahnesini yükler. Örneðin "GameScene" adlý bir sahne.
        UnityEngine.SceneManagement.SceneManager.LoadScene("SampleScene");
    }

    // Quit butonuna basýldýðýnda çaðrýlýr
    public void QuitGame()
    {
        // Uygulamadan çýkar
        Debug.Log("Oyundan çýkýlýyor!"); // Editor'de test ederken bu mesajý görebilirsiniz.
        Application.Quit();
    }

    // Main Menu'ye dönmek için kullanýlabilir (Game Over ekraný için)
    public void GoToMainMenu()
    {
        // Ana menü sahnesine dönmek için sahne adý
        UnityEngine.SceneManagement.SceneManager.LoadScene("Entry");
    }

    // Game Over butonuna basýldýðýnda çaðrýlýr
    public void GameOver()
    {
        // Örneðin, Game Over ekranýna gitmek için
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameOver");
    }
}
