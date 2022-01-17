using System;
using System.Security.Cryptography.X509Certificates;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;

namespace DefaultNamespace
{
    public class Shooting : MonoBehaviour
    {
      
        public float shootTime;
        [SerializeField] public float shootReloadTime;
        public bool shootReloading = true;
        public float velocity;

        public int bulletsLeft = 6;
        public float reloadDelta;
        public float reloadTime;
        public bool reloading;

        private GameObject ammoUI;
        public AudioSource gunshot;
        public AudioClip gunClip;


        private void Start()
        {
            ammoUI = GameObject.Find("Ammo");
            gunshot = GetComponent<AudioSource>();
        }


        public void Update()
        {
            if (shootTime < shootReloadTime)
            {
                shootReloading = true;
            }

            if (bulletsLeft <= 0)
            {
                reloading = true;
                reloadDelta += Time.deltaTime;
                if (reloadDelta >= reloadTime)
                {
                    bulletsLeft += 12;
                    reloadDelta = 0;
                    ammoUI.GetComponent<Ammo>().ammo = 12;
                }
            }



            if (shootReloading)
            {
                if (bulletsLeft >= 1)
                {
                    reloading = false;
                    // Debug.Log("reloading");
                    shootTime += Time.deltaTime;
                    if (shootTime > shootReloadTime && Input.GetKeyDown(KeyCode.Mouse0))
                    {
                        
                        GameObject pbullet = PlayerPooling.SharedInstance.GetPooledObject();
                        if (pbullet != null)
                        {   
                            gunshot.PlayOneShot(gunClip);

                            Rigidbody rb = pbullet.GetComponent<Rigidbody>();
                            pbullet.transform.position = transform.position;
                            // bullet.transform.rotation = barrel.transform.rotation;

                            pbullet.SetActive(true);
                            // bullet.transform.LookAt(player);
                            rb.AddForce(transform.forward * velocity, ForceMode.Impulse);


                            bulletsLeft -= 1;
                          
                            ammoUI.GetComponent<Ammo>().ammo -= 1;



                        }

                        shootReloading = false;
                        shootTime = 0;

                    }
                }
            }
        }

            
        }
    }

    
    
