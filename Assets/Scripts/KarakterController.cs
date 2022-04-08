using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BenimKutuphanem;
using UnityEngine.UI;

public class KarakterController : MonoBehaviour
{

    float inputX;
    Animator Anim;
    Vector3 mevcutYon;
    Camera MainCam;
    float maxUzun = 1;
    float RotationSpeed = 10;
    float MaxSpeed;
    Animasyon animasyon = new Animasyon();
    public Image HealtBar;

    public static float Saglik;
    public GameObject GameManager;

    float[] Sol_Yon_Parametreleri = { 0.12f, 0.34f, 0.63f, 0.92f };
    float[] Sag_Yon_Parametreleri = { 0.12f, 0.33f, 0.66f, 0.92f };
    float[] Egilme_Yon_Parametreleri = { 0.2f, 0.35f, 0.40f, 0.45f,1f };

    void Start()
    {
 
        Anim = GetComponent<Animator>();
        MainCam = Camera.main;
        Saglik = 100;
    }

  
    public void SaglikDurumu(float Darbegucu)
    {

        Saglik -= Darbegucu;
        HealtBar.fillAmount = Saglik / 100;

        
        if(Saglik <= 0)
        {
            GameManager.GetComponent<GameManager>().GameOver();
        }
            
    }


    void LateUpdate()
    {
        // animasyon.Karakter_Hareket(Anim, "speed", mevcutYon, MaxSpeed, maxUzun);
        animasyon.Karakter_Hareket(Anim, "speed",  maxUzun, 1, 0.2f);
        animasyon.Karakter_Rotation(MainCam, RotationSpeed,gameObject);
        animasyon.Sol_Hareket(Anim,"Sol_Hareket","Sol_Aktif", animasyon.ParametreOlustur(Sol_Yon_Parametreleri));
        animasyon.Sag_Hareket(Anim, "Sag_Hareket", "Sag_Aktif", animasyon.ParametreOlustur(Sag_Yon_Parametreleri));
        animasyon.Geri_Hareket(Anim, "geriyuru");
        animasyon.Egilme_Hareket(Anim, "Egilme", animasyon.ParametreOlustur(Egilme_Yon_Parametreleri));        
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("OyunSonu"))
        {
            GameManager.GetComponent<GameManager>().YouWin();
            
        }
    }


}
