using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public GameObject GamaOverPanel;
    public GameObject WinPanel;
    public GameObject Patron;


    void Start()
    {
        
    }

    public  void YouWin()
    {
        Patron.GetComponent<Animator>().Play("Patron_Kaybetme");
        Cursor.lockState = CursorLockMode.None;
        WinPanel.SetActive(true);
        
    }

    public void GameOver()
    {
        Cursor.lockState = CursorLockMode.None;
        GamaOverPanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }

    public void MainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

}
