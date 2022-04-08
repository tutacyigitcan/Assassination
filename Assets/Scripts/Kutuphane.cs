using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BenimKutuphanem {

    public class Animasyon
    {

        private float MaxSpeedClass;
        private float MaxInputXClass;


        public float YonuDisariCikar()
        {
            return MaxInputXClass;
        }

        /*public void Sol_Hareket(Animator Anim, string AnaParametre, string KontrolParametre,
            float Yurume, float Kosma, float Ýlerisol, float GeriSol)*/

        public void Sol_Hareket(Animator Anim,string AnaParametre,string KontrolParametre,
            List<float> parametreDegerleri)
        {
            if (Input.GetKey(KeyCode.A))
            {
                Anim.SetBool(KontrolParametre, true);

                if (Input.GetKey(KeyCode.LeftShift))
                {
                    Anim.SetFloat(AnaParametre, parametreDegerleri[1]);
                }
                else if (Input.GetKey(KeyCode.W))
                {
                    Anim.SetFloat(AnaParametre, parametreDegerleri[2]);
                }
                else if (Input.GetKey(KeyCode.S))
                {
                    Anim.SetFloat(AnaParametre, parametreDegerleri[3]);
                }
                else
                {
                    Anim.SetFloat(AnaParametre, parametreDegerleri[0]);
                }
            }

            if (Input.GetKeyUp(KeyCode.A))
            {
                Anim.SetFloat(AnaParametre, 0f);
                Anim.SetBool(KontrolParametre, false);
            }
        }


        public void Sag_Hareket(Animator Anim, string AnaParametre, string KontrolParametre,
          List<float> parametreDegerleri)
        {
            if (Input.GetKey(KeyCode.D))
            {
                Anim.SetBool(KontrolParametre, true);

                if (Input.GetKey(KeyCode.LeftShift))
                {
                    Anim.SetFloat(AnaParametre, parametreDegerleri[1]);
                }
                else if (Input.GetKey(KeyCode.W))
                {
                    Anim.SetFloat(AnaParametre, parametreDegerleri[2]);
                }
                else if (Input.GetKey(KeyCode.S))
                {
                    Anim.SetFloat(AnaParametre, parametreDegerleri[3]);
                }
                else
                {
                    Anim.SetFloat(AnaParametre, parametreDegerleri[0]);
                }
            }

            if (Input.GetKeyUp(KeyCode.D))
            {
                Anim.SetFloat(AnaParametre, 0f);
                Anim.SetBool(KontrolParametre, false);
            }
        }

        public void Geri_Hareket(Animator Anim, string AnaParametre)
        {

            if (Input.GetKeyDown(KeyCode.S))
            {
                Anim.SetBool(AnaParametre, true);
            }

            if (Input.GetKeyUp(KeyCode.S))
            {
                Anim.SetBool(AnaParametre, false);
            }
        }

        public void Egilme_Hareket(Animator Anim, string AnaParametre,List<float> parametreDegerleri)
        {
            
            if (Input.GetKey(KeyCode.C))
            {
                if (Input.GetKey(KeyCode.W))
                {
                    Anim.SetFloat(AnaParametre, parametreDegerleri[1]);
                }
                else if (Input.GetKey(KeyCode.S))
                {
                    Anim.SetFloat(AnaParametre, parametreDegerleri[2]);
                }
                else if (Input.GetKey(KeyCode.A))
                {
                    Anim.SetFloat(AnaParametre, parametreDegerleri[3]);
                }
                else if (Input.GetKey(KeyCode.D))
                {
                    Anim.SetFloat(AnaParametre, parametreDegerleri[4]);
                }
                else
                {
                    Anim.SetFloat(AnaParametre, parametreDegerleri[0]);
                }
            }

            if (Input.GetKeyUp(KeyCode.C))
            {
                Anim.SetFloat(AnaParametre, 0f);
            }
        }

        /* public void Karakter_Hareket(Animator Anim,string hizdegeri,Vector3 mevcutYon,float MaxSpeed,float maxUzun)
         {
             Anim.SetFloat(hizdegeri, Vector3.ClampMagnitude(mevcutYon, MaxSpeed).magnitude, maxUzun, Time.deltaTime * 10);
         } */


         public void Karakter_Hareket(Animator Anim,string hizdegeri,float maxUzun, float TamHiz, float YurumeHizi)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                MaxSpeedClass = TamHiz;
            }
            else if (Input.GetKey(KeyCode.W))
            {
                MaxSpeedClass = YurumeHizi;
                MaxInputXClass = 1;
            }
            else
            {
                MaxSpeedClass = 0f;
                MaxInputXClass = 0;
            }

            Anim.SetFloat(hizdegeri, Vector3.ClampMagnitude(new Vector3(MaxInputXClass, 0, 0), MaxSpeedClass).magnitude, maxUzun, Time.deltaTime * 10);
        }

        public void Karakter_Rotation(Camera MainCam, float RotationSpeed,GameObject Karakter)
        {
            Vector3 CamOfSet = MainCam.transform.forward;
            CamOfSet.y = 0;
            Karakter.transform.forward = Vector3.Slerp(Karakter.transform.forward, CamOfSet, Time.deltaTime * RotationSpeed);
        }

        public List<float> ParametreOlustur(float[] deger)
        {
            List<float> Sol_Yon_Parametre = new List<float>();

            foreach (float item in deger)
            {
                Sol_Yon_Parametre.Add(item);
            }

            return Sol_Yon_Parametre;
        }

    }


}


