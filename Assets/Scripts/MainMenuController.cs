using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{

    public GameObject YouSurePanel;

    void Start()
    {
        
    }

   
   public void StartGamee()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        YouSurePanel.SetActive(true);

       // 
    }

   

    public void cikisCevap(string cevap)
    {
        switch (cevap)
        {
            case "Yes":
                Debug.Log("OYUNDAN ÇIKTIN");
                Application.Quit();
                break;

            case "No":
                YouSurePanel.SetActive(false);
                break;
        }
    }
}
