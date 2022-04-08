using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Taramali1 : MonoBehaviour
{

    [Header("AYARLAR")]
    float atesEtmeSikli�i_1;
    public float atesEtmeSikli�i_2;
    public float menzil;
    int ToplamMermiSayisi = 930;
    int SarjorKapasitesi = 30;
    int KalanMermi;
    float DarbeGucu = 25f;
    public TextMeshProUGUI ToplamMermi_Text;
    public TextMeshProUGUI KalanMermi_Text;

    [Header("SESLER")]
    public AudioSource[] Sesler;

    [Header("EFFECTLER")]
    public ParticleSystem [] Efektler;
    /* public ParticleSystem Mermi�zi;
     public ParticleSystem KanEfekti; */

    [Header("GENEL �SLEMLER")]
    public Camera BenimCam;
    public Animator KarekterinAnimatoru;



    void Start()
    {
        KalanMermi = SarjorKapasitesi;
        ToplamMermi_Text.text = ToplamMermiSayisi.ToString();
        KalanMermi_Text.text = KalanMermi.ToString();
    }

    
    void Update()
    {
        if (Input.GetKey(KeyCode.R)) 
        {
            ReloadKontrol();            
        }
        if(KarekterinAnimatoru.GetBool("Reload"))
        {
            ReloadislemiTeknikFonsiyon();
        }


        if (Input.GetKey(KeyCode.Mouse0))
        {
            if (Time.time > atesEtmeSikli�i_1 && KalanMermi!=0)
            {
                AtesEt();
                atesEtmeSikli�i_1 = Time.time + atesEtmeSikli�i_2;
            } 
            if(KalanMermi == 0)
            {              
                Sesler[1].Play();
            }
        }
    }

    void AtesEt()
    {
        KalanMermi--;
        KalanMermi_Text.text = KalanMermi.ToString();
        Efektler[0].Play();
        Sesler[0].Play();
        KarekterinAnimatoru.Play("Egilme_Ates_Etme");

        RaycastHit hit;
        if(Physics.Raycast(BenimCam.transform.position,BenimCam.transform.forward,out hit, menzil))
        {          
            if(hit.transform.gameObject.CompareTag("Enemy"))
            {
                hit.transform.gameObject.GetComponent<Enemy>().SaglikDurumu(DarbeGucu);
                Instantiate(Efektler[2], hit.point, Quaternion.LookRotation(hit.normal));
            }
            else
            {
                Instantiate(Efektler[1], hit.point, Quaternion.LookRotation(hit.normal));
            }
            
        }
    }


    void ReloadKontrol()
    {
        if (KalanMermi < SarjorKapasitesi && ToplamMermiSayisi != 0)
        {
            KarekterinAnimatoru.Play("sarjordegistir");         
            if (!Sesler[2].isPlaying)
            {
                Sesler[2].Play();
            }
        }
    }


    void ReloadislemiTeknikFonsiyon()
    {
        if (KalanMermi == 0) // MERM� YOK
        {
            if (ToplamMermiSayisi <= SarjorKapasitesi)
            {
                ///  3 -- 5 = -2
                KalanMermi = ToplamMermiSayisi;
                ToplamMermiSayisi = 0;
            }
            else
            {
                ToplamMermiSayisi -= SarjorKapasitesi;
                KalanMermi = SarjorKapasitesi;
            }





        }
        else // MERM� VAR
        {
            // kalan mermi   ToplamMermi
            //     3              4 = 7

            if (ToplamMermiSayisi <= SarjorKapasitesi)
            {
                int OlusanDeger = KalanMermi + ToplamMermiSayisi;

                if (OlusanDeger > SarjorKapasitesi)
                {
                    KalanMermi = SarjorKapasitesi; //2
                    ToplamMermiSayisi = OlusanDeger - SarjorKapasitesi;

                }
                else
                {
                    // kalan mermi   ToplamMermi
                    //     3              1 = 4
                    KalanMermi += ToplamMermiSayisi; //4
                    ToplamMermiSayisi = 0;

                }
            }
            else
            {
                int MevcutMermi = SarjorKapasitesi - KalanMermi;
                ToplamMermiSayisi -= MevcutMermi;
                KalanMermi = SarjorKapasitesi;
            }
        }
        ToplamMermi_Text.text = ToplamMermiSayisi.ToString();
        KalanMermi_Text.text = KalanMermi.ToString();

        KarekterinAnimatoru.SetBool("Reload", false);
    }
}


