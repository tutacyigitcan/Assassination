using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [Header("DÝÐER AYARLAR")]
    NavMeshAgent navmesh;
    Animator animatorum;
    GameObject Hedef;
    public GameObject AnaHedef;


    [Header("GENEL AYARLAR")]
    public float AtesEtmeMenzilDeger = 7;
    public float SuphelenmeMenzilDeger = 10;
    Vector3 baslangicNoktasi;
    //public GameObject Kafa;
    bool Suphelenme = false;
    bool AtesEdiliyorMu = false;
    public GameObject AtesEtmeNoktasi;

    [Header("DEVRÝYE  AYARLARI")]
    public GameObject[] DevriyeNoktalari_1;
    public GameObject[] DevriyeNoktalari_2;
    public GameObject[] DevriyeNoktalari_3;
    GameObject[] AktifOlanNoktaListeleri;

    [Header("SÝLAH AYARLAR")]
    float atesEtmeSikliði_1;
    public float atesEtmeSikliði_2;
    public float menzil;
    public float DarbeGucu;


    [Header("SESLER")]
    public AudioSource[] Sesler;

    [Header("EFFECTLER")]
    public ParticleSystem[] Efektler;

    bool DevriyeVarmi;
    Coroutine DevriyeAt;
    Coroutine DevriyeZaman;
    bool DevriyeKilit;
    public bool DevriyeAtabilirMi;
    float Saglik;

   


    void Start()
    {
        navmesh = GetComponent<NavMeshAgent>();
        animatorum = GetComponent<Animator>();
        baslangicNoktasi = transform.position;
        StartCoroutine(DevriyeZamanKontrol());
        Saglik = 100;
    }

    GameObject[] DevriyeKontrol()
    {
        int deger = Random.Range(1, 3);

        switch(deger)
        {
            case 1:
                AktifOlanNoktaListeleri = DevriyeNoktalari_1;
                break;
            case 2:
                AktifOlanNoktaListeleri = DevriyeNoktalari_2;
                break;
            case 3:
                AktifOlanNoktaListeleri = DevriyeNoktalari_3;
                break;
        }

        return AktifOlanNoktaListeleri;     
    }

 
    IEnumerator DevriyeZamanKontrol()
    {
        while(true && !DevriyeVarmi && DevriyeAtabilirMi)
        {        
                yield return new WaitForSeconds(5f);

                DevriyeKilit = true;
                StopCoroutine(DevriyeZaman);        
        }
    }


    IEnumerator DevriyeTeknikÝslem(GameObject[] GelenObjeler)
    {
        navmesh.isStopped = false;
        DevriyeKilit = false;
        DevriyeVarmi = true;
        animatorum.SetBool("Yuru", true);
        int toplamNokta = GelenObjeler.Length-1;
        int baslangýcdeger = 0;
        navmesh.SetDestination(GelenObjeler[baslangýcdeger].transform.position);

        while(true && DevriyeAtabilirMi)
        {

            if(Vector3.Distance(transform.position, GelenObjeler[baslangýcdeger].transform.position) <= 1f)
            {
                if(toplamNokta> baslangýcdeger)
                {
                    ++baslangýcdeger;
                    navmesh.SetDestination(GelenObjeler[baslangýcdeger].transform.position);
                }
                else
                {
                    navmesh.stoppingDistance = 1;
                    navmesh.SetDestination(baslangicNoktasi);
                   
                }
               
                
            }
            else
            {
                if (toplamNokta > baslangýcdeger)
                {
                    navmesh.SetDestination(GelenObjeler[baslangýcdeger].transform.position);
                }             
            }        
            yield return  null;
        }
    }

    private void LateUpdate()
    {
        if (navmesh.stoppingDistance==1 && navmesh.remainingDistance <= 1)
        {
            animatorum.SetBool("Yuru", false);
            transform.rotation = Quaternion.Euler(0, 180, 0);
            if (DevriyeAtabilirMi)
            {
                DevriyeVarmi = false;
                DevriyeZaman = StartCoroutine(DevriyeZamanKontrol());
                StopCoroutine(DevriyeAt);
            }            
            navmesh.stoppingDistance = 0;
            navmesh.isStopped = true;
        }

        if (DevriyeKilit && DevriyeAtabilirMi)
        {
            DevriyeAt = StartCoroutine(DevriyeTeknikÝslem(DevriyeKontrol()));        
        }

        SuphelenmeMenzil();
        AtesEtmeMenzil();

    }

    void AtesEtmeMenzil()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, AtesEtmeMenzilDeger);
        

        foreach (var objeler in hitColliders)
        {

            if (objeler.gameObject.CompareTag("Player"))
            {

                AtesEt(objeler.gameObject);

                
            }
            else
            {
                if(AtesEdiliyorMu)
                {
                    animatorum.SetBool("AtesEt", false);
                    navmesh.isStopped = false;
                    animatorum.SetBool("Yuru", true);
                    AtesEdiliyorMu = false;
                                                    
                }
                
            }
        }
    }


    void AtesEt(GameObject Hedef)
    {
        AtesEdiliyorMu = true;
        Vector3 aradakiFark = Hedef.gameObject.transform.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(aradakiFark, Vector3.up);
        transform.rotation = rotation;
        animatorum.SetBool("Yuru", false);
        navmesh.isStopped = true;
        animatorum.SetBool("AtesEt", true);

        // ATEÞ ETME TEKNÝK ÝÞLEMLER
        RaycastHit hit;

        if (Physics.Raycast(AtesEtmeNoktasi.transform.position, AtesEtmeNoktasi.transform.forward, out hit, menzil))
        {

            Color color = Color.blue;
            Vector3 degisenPos = new Vector3(Hedef.transform.position.x, Hedef.transform.position.y + 1.5f, Hedef.transform.position.z);
            Debug.DrawLine(AtesEtmeNoktasi.transform.position, degisenPos, color);

            if (Time.time > atesEtmeSikliði_1 )
            {   
                if(hit.transform.gameObject.CompareTag("Player"))
                {
                    hit.transform.gameObject.GetComponent<KarakterController>().SaglikDurumu(DarbeGucu);
                    Instantiate(Efektler[1], hit.point, Quaternion.LookRotation(hit.normal));
                }
                else
                {
                    Instantiate(Efektler[2], hit.point, Quaternion.LookRotation(hit.normal));
                }


                if (!Sesler[0].isPlaying)
                {
                    Sesler[0].Play();
                    Efektler[0].Play();
                }
                atesEtmeSikliði_1 = Time.time + atesEtmeSikliði_2;
            }
            
            
        }

    }

    void SuphelenmeMenzil()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, SuphelenmeMenzilDeger);
        

        foreach (var objeler in hitColliders)
        {

            if (objeler.gameObject.CompareTag("Player"))
            {               
                if(animatorum.GetBool("kosma"))
                {
                    animatorum.SetBool("kosma", false);
                    animatorum.SetBool("Yuru", true);
                }
                else
                {
                    animatorum.SetBool("Yuru", true);
                }
                
                Hedef = objeler.gameObject;
                navmesh.SetDestination(Hedef.transform.position);
                Suphelenme = true;
                if(DevriyeAtabilirMi)
                {
                    StopCoroutine(DevriyeAt);
                }
                
            }
            else
            {

                if(Suphelenme)
                {
                    Hedef = null;
                    
                    if (transform.position != baslangicNoktasi)
                    {
                        navmesh.stoppingDistance = 1;
                        navmesh.SetDestination(baslangicNoktasi);
                        if (navmesh.remainingDistance <= 1)
                        {
                            animatorum.SetBool("Yuru", false);
                            transform.rotation = Quaternion.Euler(0, 180, 0);
                        }

                    }
                }
                Suphelenme = false;
                if (DevriyeAtabilirMi)
                {
                    //DevriyeAt = StartCoroutine(DevriyeTeknikÝslem(DevriyeKontrol()));
                } 
                



            }
        }
    }

    public void SaglikDurumu(float Darbegucu)
    {
        Saglik -= Darbegucu;      

      /*  if(!Suphelenme )
        {
            animatorum.SetBool("kosma",true);
            navmesh.SetDestination(AnaHedef.transform.position);
        }    */ 

        if (Saglik <= 0)
        {
            animatorum.Play("Olme");
            Destroy(gameObject,4f);
        }
           
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, AtesEtmeMenzilDeger);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, SuphelenmeMenzilDeger);
    }


   
}
















/* RaycastHit hit;
        if(Physics.Raycast(Kafa.transform.position,Kafa.transform.forward,out hit,10f))
        {
            if(hit.transform.gameObject.CompareTag("Player"))
            {
                Debug.Log("Çarptý");
            }
        }

        Debug.DrawRay(Kafa.transform.position, Kafa.transform.forward * 10f, Color.blue);
       */